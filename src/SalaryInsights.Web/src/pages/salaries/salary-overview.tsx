import { Button } from "@/components/ui/button";
import { Calendar } from "@/components/ui/calendar";
import { Card, CardContent } from "@/components/ui/card";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { cn } from "@/lib/utils";
import { format, startOfYear } from "date-fns";
import { CalendarClockIcon, CalendarIcon } from "lucide-react";
import React from "react";
import { useState } from "react";
import { DateRange } from "react-day-picker";
import SalaryCard from "./salary-card";

export default function SalaryOverview() {
  const [currentPage, setCurrentPage] = useState(1);
  const [itemsPerPage] = useState(12);
  const [date, setDate] = React.useState<DateRange | undefined>({
    from: startOfYear(new Date()),
    to: new Date(),
  });

  const totalItems = 100;
  const totalPages = Math.ceil(totalItems / itemsPerPage);
  const indexOfLastItem = currentPage * itemsPerPage;
  const indexOfFirstItem = indexOfLastItem - itemsPerPage;
  const currentItems = Array.from(
    { length: itemsPerPage },
    (_, i) => i + indexOfFirstItem
  )
    .filter((i) => i < totalItems)
    .map((i) => ({
      month: `June ${i + 1}, 2023`,
      actualIncome: `${(i + 1) * 1000}.00`,
    }));
  const handlePageChange = (page) => {
    setCurrentPage(page);
  };
  return (
    <div className="">
      <div className="flex items-center justify-between">
        <h1 className="text-3xl font-bold">Overview</h1>
        <div className="flex items-center gap-4">
          <Popover>
            <PopoverTrigger asChild>
              <Button
                id="date"
                variant={"outline"}
                className={cn(
                  "justify-start text-left font-normal",
                  !date && "text-muted-foreground"
                )}
              >
                <CalendarIcon className="mr-2 h-4 w-4" />
                {date?.from ? (
                  date.to ? (
                    <>
                      {format(date.from, "yyyy/MM/dd")} -{" "}
                      {format(date.to, "yyyy/MM/dd")}
                    </>
                  ) : (
                    format(date.from, "yyyy/MM/dd")
                  )
                ) : (
                  <span>Pick a date</span>
                )}
              </Button>
            </PopoverTrigger>
            <PopoverContent className="w-auto p-0" align="end">
              <Calendar
                initialFocus
                mode="range"
                defaultMonth={date?.from}
                selected={date}
                onSelect={setDate}
                numberOfMonths={2}
              />
            </PopoverContent>
          </Popover>
          <Button variant="outline" className="w-full">
            添加
          </Button>
        </div>
      </div>
      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 xs:grid-cols-12 gap-6 py-4">
        {currentItems.map((item, index) => (
          <SalaryCard
            month={item.month}
            amount={item.actualIncome}
            transactions={index + 1}
            growth={5.2}
            onDelete={() => console.log(111)}
          />
        ))}
      </div>
      <div className="flex justify-end items-center gap-2">
        {Array.from({ length: totalPages }, (_, i) => i + 1).map((page) => (
          <Button
            key={page}
            variant={page === currentPage ? "solid" : "outline"}
            onClick={() => handlePageChange(page)}
            className={`px-4 py-2 rounded-md ${
              page === currentPage ? "bg-primary text-primary-foreground" : ""
            }`}
          >
            {page}
          </Button>
        ))}
      </div>
    </div>
  );
}
