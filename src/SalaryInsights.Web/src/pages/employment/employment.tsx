import { useState, useEffect } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
} from "@/components/ui/dialog";
import { Label } from "@/components/ui/label";
import CareerSummary from "./career-summary";
import EmploymentList from "./employment-list";

const Employment = () => {
  const [companies, setCompanies] = useState([]);
  const [summary, setSummary] = useState({
    totalCompanies: 0,
    totalYearsExperience: 0,
    longestTenure: 0,
    currentCompany: "",
  });
  const [editingCompany, setEditingCompany] = useState(null);
  const [isDialogOpen, setIsDialogOpen] = useState(false);

  useEffect(() => {
    // 模拟从 API 获取数据
    const fetchData = async () => {
      const mockData = [
        {
          id: 1,
          name: "Tech Solutions Inc.",
          startDate: "2020-01-15",
          endDate: "2022-06-30",
          position: "软件工程师",
        },
        {
          id: 2,
          name: "Digital Marketing Agency",
          startDate: "2022-07-01",
          endDate: null,
          position: "高级开发者",
        },
        {
          id: 3,
          name: "Software Innovations Ltd.",
          startDate: "2018-03-10",
          endDate: "2019-12-31",
          position: "初级开发者",
        },
      ];
      setCompanies(mockData);
    };

    fetchData();
  }, []);

  useEffect(() => {
    const calculateSummary = () => {
      const now = new Date();
      let totalExperience = 0;
      let longestTenure = 0;
      let currentCompany = "";

      companies.forEach((company) => {
        const startDate = new Date(company.startDate);
        const endDate = company.endDate ? new Date(company.endDate) : now;
        const tenure = (endDate - startDate) / (1000 * 60 * 60 * 24 * 365.25);

        totalExperience += tenure;
        if (tenure > longestTenure) longestTenure = tenure;
        if (!company.endDate) currentCompany = company.name;
      });

      setSummary({
        totalCompanies: companies.length,
        totalYearsExperience: totalExperience.toFixed(1),
        longestTenure: longestTenure.toFixed(1),
        currentCompany: currentCompany,
        averageTenure: 5,
      });
    };

    calculateSummary();
  }, [companies]);

  const handleEdit = (company) => {
    setEditingCompany(company);
    setIsDialogOpen(true);
  };

  const handleSave = () => {
    setCompanies(
      companies.map((c) => (c.id === editingCompany.id ? editingCompany : c))
    );
    setIsDialogOpen(false);
    setEditingCompany(null);
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setEditingCompany({ ...editingCompany, [name]: value });
  };

  return (
    <div>
      <CareerSummary summary={summary} />

      <EmploymentList />

      <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
        <DialogContent className="sm:max-w-lg">
          <DialogHeader>
            <DialogTitle>
              {editingCompany?.id ? "编辑公司信息" : "添加新公司"}
            </DialogTitle>
          </DialogHeader>
          <div className="mt-4 space-y-4">
            <div>
              <Label htmlFor="name">公司名称</Label>
              <Input
                id="name"
                name="name"
                value={editingCompany?.name || ""}
                onChange={handleInputChange}
              />
            </div>
            <div>
              <Label htmlFor="position">职位</Label>
              <Input
                id="position"
                name="position"
                value={editingCompany?.position || ""}
                onChange={handleInputChange}
              />
            </div>
            <div>
              <Label htmlFor="startDate">入职时间</Label>
              <Input
                id="startDate"
                name="startDate"
                type="date"
                value={editingCompany?.startDate || ""}
                onChange={handleInputChange}
              />
            </div>
            <div>
              <Label htmlFor="endDate">离职时间</Label>
              <Input
                id="endDate"
                name="endDate"
                type="date"
                value={editingCompany?.endDate || ""}
                onChange={handleInputChange}
              />
            </div>
          </div>
          <div className="mt-6 flex justify-end space-x-3">
            <Button onClick={() => setIsDialogOpen(false)} variant="outline">
              取消
            </Button>
            <Button onClick={handleSave}>保存</Button>
          </div>
        </DialogContent>
      </Dialog>
    </div>
  );
};

export default Employment;
