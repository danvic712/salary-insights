import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import {
  BriefcaseIcon,
  CalendarIcon,
  BuildingIcon,
  ClockIcon,
  BarChartIcon,
} from "lucide-react";

const CareerSummary = ({ summary }) => (
  <Card className="mb-6">
    <CardHeader>
      <CardTitle>Career Summary</CardTitle>
    </CardHeader>
    <CardContent className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-4">
      <SummaryPanel
        title="Current Company"
        value={summary.currentCompany || "N/A"}
        icon={<BriefcaseIcon className="w-5 h-5 text-primary" />}
      />
      <SummaryPanel
        title="Start Date"
        value={`2024-01-01`}
        icon={<CalendarIcon className="w-5 h-5 text-primary" />}
      />
      <SummaryPanel
        title="Total Companies"
        value={summary.totalCompanies}
        icon={<BuildingIcon className="w-5 h-5 text-primary" />}
      />
      <SummaryPanel
        title="Years of Experience"
        value={`${summary.totalYearsExperience} years`}
        icon={<ClockIcon className="w-5 h-5 text-primary" />}
      />
      <SummaryPanel
        title="Longest Tenure"
        value={`${summary.longestTenure} years`}
        icon={<CalendarIcon className="w-5 h-5 text-primary" />}
      />
      <SummaryPanel
        title="Average Tenure"
        value={`${summary.averageTenure} years`}
        icon={<BarChartIcon className="w-5 h-5 text-primary" />}
      />
    </CardContent>
  </Card>
);

const SummaryPanel = ({ title, value, icon }) => (
  <div className="flex items-center">
    <div className="flex-shrink-0 mr-3 text-primary">{icon}</div>
    <div>
      <p className="text-sm font-medium text-gray-600">{title}</p>
      <p className="text-base font-semibold text-gray-900">{value}</p>
    </div>
  </div>
);

export default CareerSummary;
