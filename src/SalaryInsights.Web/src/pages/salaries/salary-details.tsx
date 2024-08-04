import React, { useState, useEffect, useCallback } from "react";
import { useParams } from "react-router-dom";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
  CardDescription,
} from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Textarea } from "@/components/ui/textarea";
import { Badge } from "@/components/ui/badge";
import {
  PlusCircle,
  MinusCircle,
  Trash2,
  DollarSign,
  TrendingUp,
  TrendingDown,
  Calendar,
  Edit,
  Save,
  FileDown,
} from "lucide-react";
import { format } from "date-fns";
import { jsPDF } from "jspdf";
import "jspdf-autotable";
import * as XLSX from "xlsx";
import debounce from "lodash/debounce";

const SalaryDetails = () => {
  const { id } = useParams(); // id format: "YYYY-MM"
  const [year, month] = [2024, 12];
  const [salaries, setSalaries] = useState([]);
  const [companies, setCompanies] = useState(["公司A", "公司B", "公司C"]);
  const [totalIncome, setTotalIncome] = useState(0);
  const [previousMonthIncome, setPreviousMonthIncome] = useState(0);
  const [isEditMode, setIsEditMode] = useState(false);
  const [tags, setTags] = useState(["工资", "奖金", "税收", "社保", "公积金"]);

  useEffect(() => {
    // 从API获取该月的工资数据
    // 这里使用模拟数据
    setSalaries([
      {
        id: 1,
        company: "公司A",
        items: [
          {
            id: 1,
            description: "基本工资",
            amount: 10000,
            type: "increase",
            tags: ["工资"],
            note: "",
          },
          {
            id: 2,
            description: "奖金",
            amount: 2000,
            type: "increase",
            tags: ["奖金"],
            note: "",
          },
          {
            id: 3,
            description: "税收",
            amount: 1000,
            type: "decrease",
            tags: ["税收"],
            note: "",
          },
        ],
      },
    ]);
    setPreviousMonthIncome(11000); // 模拟上月收入
  }, [id]);

  useEffect(() => {
    const newTotalIncome = salaries.reduce(
      (total, salary) => total + calculateTotal(salary),
      0
    );
    setTotalIncome(newTotalIncome);
  }, [salaries]);

  const debouncedSave = useCallback(
    debounce((newSalaries) => {
      // 这里应该调用API来保存数据
      console.log("Saving data:", newSalaries);
    }, 1000),
    []
  );

  const updateSalaries = (newSalaries) => {
    setSalaries(newSalaries);
    debouncedSave(newSalaries);
  };

  const addSalary = () => {
    const newSalaries = [
      ...salaries,
      { id: Date.now(), company: "", items: [] },
    ];
    updateSalaries(newSalaries);
  };

  const removeSalary = (salaryId) => {
    const newSalaries = salaries.filter((salary) => salary.id !== salaryId);
    updateSalaries(newSalaries);
  };

  const updateSalary = (salaryId, field, value) => {
    const newSalaries = salaries.map((salary) =>
      salary.id === salaryId ? { ...salary, [field]: value } : salary
    );
    updateSalaries(newSalaries);
  };

  const addSalaryItem = (salaryId) => {
    const newSalaries = salaries.map((salary) =>
      salary.id === salaryId
        ? {
            ...salary,
            items: [
              ...salary.items,
              {
                id: Date.now(),
                description: "",
                amount: 0,
                type: "increase",
                tags: [],
                note: "",
              },
            ],
          }
        : salary
    );
    updateSalaries(newSalaries);
  };

  const updateSalaryItem = (salaryId, itemId, field, value) => {
    const newSalaries = salaries.map((salary) =>
      salary.id === salaryId
        ? {
            ...salary,
            items: salary.items.map((item) =>
              item.id === itemId ? { ...item, [field]: value } : item
            ),
          }
        : salary
    );
    updateSalaries(newSalaries);
  };

  const removeSalaryItem = (salaryId, itemId) => {
    const newSalaries = salaries.map((salary) =>
      salary.id === salaryId
        ? {
            ...salary,
            items: salary.items.filter((item) => item.id !== itemId),
          }
        : salary
    );
    updateSalaries(newSalaries);
  };

  const calculateTotal = (salary) => {
    return salary.items.reduce((total, item) => {
      return item.type === "increase"
        ? total + item.amount
        : total - item.amount;
    }, 0);
  };

  const calculateGrowth = () => {
    const growth =
      ((totalIncome - previousMonthIncome) / previousMonthIncome) * 100;
    return growth.toFixed(2);
  };

  const exportToPDF = () => {
    const doc = new jsPDF();
    doc.text(`${year}年${month}月工资详情`, 14, 15);

    salaries.forEach((salary, index) => {
      doc.text(`${salary.company}`, 14, 25 + index * 40);

      const tableData = salary.items.map((item) => [
        item.description,
        item.amount.toFixed(2),
        item.type === "increase" ? "增加" : "减少",
        item.tags.join(", "),
        item.note,
      ]);

      doc.autoTable({
        startY: 30 + index * 40,
        head: [["描述", "金额", "类型", "标签", "备注"]],
        body: tableData,
      });
    });

    doc.save(`salary_details_${year}_${month}.pdf`);
  };

  const exportToExcel = () => {
    const workbook = XLSX.utils.book_new();

    salaries.forEach((salary) => {
      const ws_data = [
        ["描述", "金额", "类型", "标签", "备注"],
        ...salary.items.map((item) => [
          item.description,
          item.amount,
          item.type === "increase" ? "增加" : "减少",
          item.tags.join(", "),
          item.note,
        ]),
      ];
      const ws = XLSX.utils.aoa_to_sheet(ws_data);
      XLSX.utils.book_append_sheet(workbook, ws, salary.company);
    });

    XLSX.writeFile(workbook, `salary_details_${year}_${month}.xlsx`);
  };

  return (
    <div className="">
      <Card className="mb-8 shadow-lg">
        <CardHeader className="">
          <div className="flex flex-col md:flex-row justify-between items-center">
            <div>
              <CardTitle className="text-3xl font-bold mb-2">
                {year}年{month}月工资详情
              </CardTitle>
              <CardDescription className="text-lg text-gray-200">
                <Calendar className="inline mr-2" />
                {format(new Date(year, month - 1), "yyyy年MM月")}
              </CardDescription>
            </div>
            <div className="text-right mt-4 md:mt-0">
              <p className="text-3xl font-bold">¥{totalIncome.toFixed(2)}</p>
              <p
                className={`text-sm ${
                  calculateGrowth() >= 0 ? "text-green-300" : "text-red-300"
                }`}
              >
                {calculateGrowth() >= 0 ? (
                  <TrendingUp className="inline mr-1" />
                ) : (
                  <TrendingDown className="inline mr-1" />
                )}
                较上月{calculateGrowth()}%
              </p>
            </div>
          </div>
        </CardHeader>
        <CardContent className="mt-6">
          <div className="flex flex-col sm:flex-row justify-between items-center space-y-4 sm:space-y-0">
            <Button
              onClick={() => setIsEditMode(!isEditMode)}
              className="w-full sm:w-auto"
            >
              {isEditMode ? (
                <Save className="mr-2" />
              ) : (
                <Edit className="mr-2" />
              )}
              {isEditMode ? "保存" : "编辑"}
            </Button>
            <div className="flex flex-col sm:flex-row space-y-2 sm:space-y-0 sm:space-x-2 w-full sm:w-auto">
              <Button onClick={exportToPDF} className="w-full sm:w-auto">
                <FileDown className="mr-2" />
                导出PDF
              </Button>
              <Button onClick={exportToExcel} className="w-full sm:w-auto">
                <FileDown className="mr-2" />
                导出Excel
              </Button>
            </div>
          </div>
        </CardContent>
      </Card>

      <div className="grid gap-6 md:grid-cols-2">
        {salaries.map((salary) => (
          <Card key={salary.id} className="shadow-md">
            <CardHeader className="bg-gray-100">
              <CardTitle className="flex justify-between items-center">
                {isEditMode ? (
                  <Select
                    value={salary.company}
                    onValueChange={(value) =>
                      updateSalary(salary.id, "company", value)
                    }
                  >
                    <SelectTrigger className="w-full md:w-[180px]">
                      <SelectValue placeholder="选择公司" />
                    </SelectTrigger>
                    <SelectContent>
                      {companies.map((company) => (
                        <SelectItem key={company} value={company}>
                          {company}
                        </SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                ) : (
                  <span className="text-xl font-semibold">
                    {salary.company}
                  </span>
                )}
                {isEditMode && (
                  <Button
                    variant="destructive"
                    size="sm"
                    onClick={() => removeSalary(salary.id)}
                  >
                    <Trash2 className="h-4 w-4" />
                  </Button>
                )}
              </CardTitle>
            </CardHeader>
            <CardContent className="mt-4">
              {salary.items.map((item) => (
                <div
                  key={item.id}
                  className="mb-6 p-4 border rounded-lg shadow-sm"
                >
                  <div className="flex flex-col md:flex-row items-center space-y-2 md:space-y-0 md:space-x-2 mb-2">
                    {isEditMode ? (
                      <>
                        <Input
                          placeholder="描述"
                          value={item.description}
                          onChange={(e) =>
                            updateSalaryItem(
                              salary.id,
                              item.id,
                              "description",
                              e.target.value
                            )
                          }
                          className="w-full md:w-1/3"
                        />
                        <Input
                          type="number"
                          placeholder="金额"
                          value={item.amount}
                          onChange={(e) =>
                            updateSalaryItem(
                              salary.id,
                              item.id,
                              "amount",
                              parseFloat(e.target.value)
                            )
                          }
                          className="w-full md:w-1/4"
                        />
                        <Select
                          value={item.type}
                          onValueChange={(value) =>
                            updateSalaryItem(salary.id, item.id, "type", value)
                          }
                          className="w-full md:w-1/4"
                        >
                          <SelectTrigger>
                            <SelectValue placeholder="类型" />
                          </SelectTrigger>
                          <SelectContent>
                            <SelectItem value="increase">增加</SelectItem>
                            <SelectItem value="decrease">减少</SelectItem>
                          </SelectContent>
                        </Select>
                        <Button
                          variant="ghost"
                          size="sm"
                          onClick={() => removeSalaryItem(salary.id, item.id)}
                        >
                          <MinusCircle className="h-4 w-4" />
                        </Button>
                      </>
                    ) : (
                      <>
                        <span className="w-full md:w-1/3">
                          {item.description}
                        </span>
                        <span className="w-full md:w-1/4 text-right">
                          ¥{item.amount.toFixed(2)}
                        </span>
                        <span
                          className={`w-full md:w-1/4 text-center ${
                            item.type === "increase"
                              ? "text-green-500"
                              : "text-red-500"
                          }`}
                        >
                          {item.type === "increase" ? "增加" : "减少"}
                        </span>
                      </>
                    )}
                  </div>
                  {isEditMode ? (
                    <Select
                      value={item.tags}
                      onValueChange={(value) =>
                        updateSalaryItem(salary.id, item.id, "tags", value)
                      }
                      multiple
                      className="mt-2"
                    >
                      <SelectTrigger>
                        <SelectValue placeholder="选择标签" />
                      </SelectTrigger>
                      <SelectContent>
                        {tags.map((tag) => (
                          <SelectItem key={tag} value={tag}>
                            {tag}
                          </SelectItem>
                        ))}
                      </SelectContent>
                    </Select>
                  ) : (
                    <div className="mt-2">
                      {item.tags.map((tag) => (
                        <Badge
                          key={tag}
                          variant="secondary"
                          className="mr-1 mb-1"
                        >
                          {tag}
                        </Badge>
                      ))}
                    </div>
                  )}
                  {isEditMode ? (
                    <Textarea
                      placeholder="备注"
                      value={item.note}
                      onChange={(e) =>
                        updateSalaryItem(
                          salary.id,
                          item.id,
                          "note",
                          e.target.value
                        )
                      }
                      className="mt-2"
                    />
                  ) : (
                    item.note && (
                      <p className="text-gray-600 mt-2">{item.note}</p>
                    )
                  )}
                </div>
              ))}
              {isEditMode && (
                <Button
                  variant="outline"
                  size="sm"
                  onClick={() => addSalaryItem(salary.id)}
                  className="mt-2 w-full"
                >
                  <PlusCircle className="h-4 w-4 mr-2" /> 添加项目
                </Button>
              )}
              <div className="mt-6 text-right">
                <strong className="text-2xl">
                  总计: ¥{calculateTotal(salary).toFixed(2)}
                </strong>
              </div>
            </CardContent>
          </Card>
        ))}
      </div>

      {isEditMode && (
        <Button onClick={addSalary} className="mt-8 w-full">
          <PlusCircle className="h-4 w-4 mr-2" /> 添加新工资条目
        </Button>
      )}
    </div>
  );
};

export default SalaryDetails;
