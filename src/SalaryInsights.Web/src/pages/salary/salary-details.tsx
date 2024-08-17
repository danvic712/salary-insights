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
import { Separator } from "@/components/ui/separator";
import {
  PlusCircle,
  MinusCircle,
  Trash2,
  Calendar,
  Edit,
  Save,
  FileDown,
  TrendingUp,
  TrendingDown,
  DollarSign,
  CreditCard,
  BarChart2,
  PieChart,
} from "lucide-react";
import { format, subMonths } from "date-fns";
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

  const getLastMonthSalary = () => {
    const currentDate = new Date(year, month - 1); // 当前工资的日期
    const lastMonth = subMonths(currentDate, 1); // 上个月的日期

    // 假设salaries是按日期排序的工资数组
    return salaries.find(
      (salary) =>
        salary.year === lastMonth.getFullYear() &&
        salary.month === lastMonth.getMonth() + 1
    );
  };

  // 新增函数计算统计信息
  const calculateStats = () => {
    if (salaries.length === 0) return null;

    const currentSalary = salaries[0];
    const currentTotal = calculateTotal(currentSalary);

    const lastMonthSalary = getLastMonthSalary();
    const lastMonthTotal = lastMonthSalary
      ? calculateTotal(lastMonthSalary)
      : null;

    const monthlyChange = lastMonthTotal
      ? (((currentTotal - lastMonthTotal) / lastMonthTotal) * 100).toFixed(2)
      : null;

    const incomeItems = currentSalary.items.filter(
      (item) => item.type === "increase"
    );
    const expenseItems = currentSalary.items.filter(
      (item) => item.type === "decrease"
    );

    const totalIncome = incomeItems.reduce((sum, item) => sum + item.amount, 0);
    const totalExpense = expenseItems.reduce(
      (sum, item) => sum + item.amount,
      0
    );

    const largestIncomeItem = incomeItems.reduce(
      (max, item) => (item.amount > max.amount ? item : max),
      { amount: 0 }
    );
    const largestExpenseItem = expenseItems.reduce(
      (max, item) => (item.amount > max.amount ? item : max),
      { amount: 0 }
    );

    // 计算收入和支出的百分比
    const incomePercentage = (
      (totalIncome / (totalIncome + totalExpense)) *
      100
    ).toFixed(2);
    const expensePercentage = (
      (totalExpense / (totalIncome + totalExpense)) *
      100
    ).toFixed(2);

    // 按标签分类统计
    const tagStats = currentSalary.items.reduce((acc, item) => {
      item.tags.forEach((tag) => {
        if (!acc[tag]) acc[tag] = 0;
        acc[tag] += item.amount * (item.type === "increase" ? 1 : -1);
      });
      return acc;
    }, {});

    return {
      currentTotal,
      monthlyChange,
      totalIncome,
      totalExpense,
      largestIncomeItem,
      largestExpenseItem,
      incomePercentage,
      expensePercentage,
      tagStats,
    };
  };

  const stats = calculateStats();

  return (
    <div className="">
      <div className="flex flex-col lg:flex-row gap-6">
        {/* 右侧：统计数据 */}
        <div className="w-full lg:w-1/3">
          <div className="lg:sticky lg:top-20">
            <Card>
              <CardHeader>
                <CardTitle className="text-2xl font-bold">统计数据</CardTitle>
              </CardHeader>
              <CardContent>
                {stats && (
                  <>
                    <div className="grid grid-cols-2 gap-4 mb-6">
                      <div className="text-center">
                        <p className="text-sm text-muted-foreground">总收入</p>
                        <p className="text-xl font-semibold text-green-500">
                          ¥{stats.totalIncome.toFixed(2)}
                        </p>
                        <p className="text-xs">{stats.incomePercentage}%</p>
                      </div>
                      <div className="text-center">
                        <p className="text-sm text-muted-foreground">总支出</p>
                        <p className="text-xl font-semibold text-red-500">
                          ¥{stats.totalExpense.toFixed(2)}
                        </p>
                        <p className="text-xs">{stats.expensePercentage}%</p>
                      </div>
                    </div>

                    <Separator className="my-4" />

                    <div className="space-y-4">
                      <div>
                        <p className="text-sm text-muted-foreground">净收入</p>
                        <p className="text-xl font-semibold">
                          ¥{(stats.totalIncome - stats.totalExpense).toFixed(2)}
                        </p>
                      </div>
                      <div>
                        <p className="text-sm text-muted-foreground">
                          最大收入项
                        </p>
                        <p className="text-lg">
                          {stats.largestIncomeItem.description}: ¥
                          {stats.largestIncomeItem.amount.toFixed(2)}
                        </p>
                      </div>
                      <div>
                        <p className="text-sm text-muted-foreground">
                          最大支出项
                        </p>
                        <p className="text-lg">
                          {stats.largestExpenseItem.description}: ¥
                          {stats.largestExpenseItem.amount.toFixed(2)}
                        </p>
                      </div>
                    </div>

                    <Separator className="my-4" />

                    <div>
                      <p className="text-lg font-semibold mb-2">标签统计</p>
                      {Object.entries(stats.tagStats).map(([tag, amount]) => (
                        <div
                          key={tag}
                          className="flex justify-between items-center mb-2"
                        >
                          <Badge variant="outline">{tag}</Badge>
                          <span
                            className={
                              amount >= 0 ? "text-green-500" : "text-red-500"
                            }
                          >
                            ¥{Math.abs(amount).toFixed(2)}
                          </span>
                        </div>
                      ))}
                    </div>
                  </>
                )}
              </CardContent>
            </Card>
          </div>
        </div>

        {/* 左侧：工资详情 */}
        <div className="w-full lg:w-2/3">
          <Card>
            <CardHeader>
              <div className="flex justify-between items-center">
                <div>
                  <CardTitle className="text-2xl font-bold">工资详情</CardTitle>
                  <CardDescription className="text-lg"></CardDescription>
                </div>
                <div className="text-right">
                  <p className="text-3xl font-bold">
                    ¥{stats?.currentTotal.toFixed(2)}
                  </p>
                  {stats?.monthlyChange && (
                    <p
                      className={`text-sm ${
                        parseFloat(stats.monthlyChange) >= 0
                          ? "text-green-500"
                          : "text-red-500"
                      }`}
                    >
                      {parseFloat(stats.monthlyChange) >= 0 ? (
                        <TrendingUp className="inline mr-1" />
                      ) : (
                        <TrendingDown className="inline mr-1" />
                      )}
                      较上月{stats.monthlyChange}%
                    </p>
                  )}
                </div>
              </div>
            </CardHeader>
            <CardContent>
              <div className="flex flex-wrap gap-4 mb-4">
                <Button
                  onClick={() => setIsEditMode(!isEditMode)}
                  variant={isEditMode ? "secondary" : "default"}
                >
                  {isEditMode ? (
                    <Save className="mr-2" />
                  ) : (
                    <Edit className="mr-2" />
                  )}
                  {isEditMode ? "保存" : "编辑"}
                </Button>
                <Button onClick={exportToPDF} variant="outline">
                  <FileDown className="mr-2" />
                  导出PDF
                </Button>
                <Button onClick={exportToExcel} variant="outline">
                  <FileDown className="mr-2" />
                  导出Excel
                </Button>
              </div>

              {salaries.map((salary) => (
                <Card key={salary.id} className="mb-4">
                  <CardHeader>
                    <CardTitle className="flex justify-between items-center">
                      {isEditMode ? (
                        <Select
                          value={salary.company}
                          onValueChange={(value) =>
                            updateSalary(salary.id, "company", value)
                          }
                        >
                          <SelectTrigger className="w-[180px]">
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
                  <CardContent>
                    <div className="space-y-4">
                      {salary.items.map((item) => (
                        <div key={item.id} className="p-4 border rounded-lg">
                          <div className="flex flex-wrap gap-2 items-center mb-2">
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
                                  className="flex-grow"
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
                                  className="w-24"
                                />
                                <Select
                                  value={item.type}
                                  onValueChange={(value) =>
                                    updateSalaryItem(
                                      salary.id,
                                      item.id,
                                      "type",
                                      value
                                    )
                                  }
                                >
                                  <SelectTrigger className="w-24">
                                    <SelectValue placeholder="类型" />
                                  </SelectTrigger>
                                  <SelectContent>
                                    <SelectItem value="increase">
                                      增加
                                    </SelectItem>
                                    <SelectItem value="decrease">
                                      减少
                                    </SelectItem>
                                  </SelectContent>
                                </Select>
                                <Button
                                  variant="ghost"
                                  size="sm"
                                  onClick={() =>
                                    removeSalaryItem(salary.id, item.id)
                                  }
                                >
                                  <MinusCircle className="h-4 w-4" />
                                </Button>
                              </>
                            ) : (
                              <>
                                <span className="flex-grow">
                                  {item.description}
                                </span>
                                <span className="font-medium">
                                  ¥{item.amount.toFixed(2)}
                                </span>
                                <Badge
                                  variant={
                                    item.type === "increase"
                                      ? "success"
                                      : "destructive"
                                  }
                                >
                                  {item.type === "increase" ? "增加" : "减少"}
                                </Badge>
                              </>
                            )}
                          </div>
                          {isEditMode ? (
                            <Select
                              value={item.tags}
                              onValueChange={(value) =>
                                updateSalaryItem(
                                  salary.id,
                                  item.id,
                                  "tags",
                                  value
                                )
                              }
                              multiple
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
                            <div className="flex flex-wrap gap-1 mt-2">
                              {item.tags.map((tag) => (
                                <Badge key={tag} variant="outline">
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
                              <p className="text-sm text-muted-foreground mt-2">
                                {item.note}
                              </p>
                            )
                          )}
                        </div>
                      ))}
                      {isEditMode && (
                        <Button
                          variant="outline"
                          size="sm"
                          onClick={() => addSalaryItem(salary.id)}
                          className="w-full"
                        >
                          <PlusCircle className="h-4 w-4 mr-2" /> 添加项目
                        </Button>
                      )}
                    </div>
                    <Separator className="my-4" />
                    <div className="text-right">
                      <strong className="text-2xl">
                        总计: ¥{calculateTotal(salary).toFixed(2)}
                      </strong>
                    </div>
                  </CardContent>
                </Card>
              ))}

              {isEditMode && (
                <Button onClick={addSalary} className="w-full">
                  <PlusCircle className="h-4 w-4 mr-2" /> 添加新工资条目
                </Button>
              )}
            </CardContent>
          </Card>
        </div>
      </div>
    </div>
  );
};

export default SalaryDetails;
