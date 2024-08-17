import { DataTable } from "@/components/data-table/data-table";
import { Employment, columns } from "./columns";
import { CardDescription, CardTitle } from "@/components/ui/card";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Input } from "@/components/ui/input";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { Button } from "@/components/ui/button";
import { cn } from "@/lib/utils";
import { CalendarIcon } from "lucide-react";
import { Calendar } from "@/components/ui/calendar";
import React from "react";
import { format } from "date-fns";

function getData(): Employment[] {
  // Fetch data from your API here.
  return [
    {
      id: "1",
      companyName: "Company A",
      position: "Software Engineer",
      startDate: "2020-01-01",
      endDate: "2021-01-01",
      employmentType: "fullTime",
    },
    {
      id: "2",
      companyName: "Company A",
      position: "Senior Engineer",
      startDate: "2021-02-01",
      endDate: "2022-03-01",
      employmentType: "fullTime",
    },
    {
      id: "3",
      companyName: "Company B",
      position: "Team Lead",
      startDate: "2019-05-01",
      endDate: "2020-12-01",
      employmentType: "fullTime",
    },
  ];
}

const SearchForm = () => {
  const [startDate, setStartDate] = React.useState<Date>();
  const [endDate, setEndDate] = React.useState<Date>();

  return (
    <div className="mb-4 flex flex-col space-y-4">
      <div className="flex flex-col space-y-4 md:flex-row md:space-y-0 md:space-x-4">
        <div className="flex-1">
          <Input
            type="text"
            placeholder="Search by company name..."
            className="p-2 border rounded"
          />
        </div>
        <div className="flex-1">
          <Popover>
            <PopoverTrigger asChild>
              <Button
                variant={"outline"}
                className={cn(
                  "justify-start text-left font-normal w-full",
                  !startDate && "text-muted-foreground"
                )}
              >
                <CalendarIcon className="mr-2 h-4 w-4" />
                {startDate ? (
                  format(startDate, "PPP")
                ) : (
                  <span>Pick a start date</span>
                )}
              </Button>
            </PopoverTrigger>
            <PopoverContent className="w-auto p-0" align="start">
              <Calendar
                mode="single"
                selected={startDate}
                onSelect={setStartDate}
                initialFocus
              />
            </PopoverContent>
          </Popover>
        </div>
        <div className="flex-1">
          <Popover>
            <PopoverTrigger asChild>
              <Button
                variant={"outline"}
                className={cn(
                  "justify-start text-left font-normal w-full",
                  !endDate && "text-muted-foreground"
                )}
              >
                <CalendarIcon className="mr-2 h-4 w-4" />
                {endDate ? (
                  format(endDate, "PPP")
                ) : (
                  <span>Pick a end date</span>
                )}
              </Button>
            </PopoverTrigger>
            <PopoverContent className="w-auto p-0" align="start">
              <Calendar
                mode="single"
                selected={endDate}
                onSelect={setEndDate}
                initialFocus
              />
            </PopoverContent>
          </Popover>
        </div>
        <div className="flex-1">
          <Select>
            <SelectTrigger className="flex-1">
              <SelectValue placeholder="Employment Type" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="fullTime">全职</SelectItem>
              <SelectItem value="partTime">兼职</SelectItem>
            </SelectContent>
          </Select>
        </div>
      </div>
    </div>
  );
};

export default function EmploymentList() {
  const data = getData();

  return (
    <DataTable
      columns={columns}
      data={data}
      cardHeader={
        <>
          <CardTitle>Employments</CardTitle>
          <CardDescription>Manage your work experience</CardDescription>
        </>
      }
      searchForm={<SearchForm />}
    />
  );
}
