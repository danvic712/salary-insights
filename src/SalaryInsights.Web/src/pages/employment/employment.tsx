import { useState, useEffect } from "react";
import CareerSummary from "./career-summary";
import EmploymentList from "./employment-list";
import EmploymentDetails from "./employment-details";

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

  const [isSheetOpen, setIsSheetOpen] = useState(false);
  const [companyId, setCompanyId] = useState(null);

  const handleOpenSheet = (id) => {
    setCompanyId(id);
    setIsSheetOpen(true);
  };

  const handleSave = (updatedCompany) => {
    console.log("Updated company data:", updatedCompany);
    // 在实际应用中，这里会发送数据到服务器
    setIsSheetOpen(false);
  };

  return (
    <>
      <CareerSummary summary={summary} onOpenSheet={handleOpenSheet} />

      <EmploymentList />

      {companyId && (
        <EmploymentDetails
          companyId={companyId}
          isOpen={isSheetOpen}
          onSave={handleSave}
          onClose={() => setIsSheetOpen(false)}
        />
      )}
    </>
  );
};

export default Employment;
