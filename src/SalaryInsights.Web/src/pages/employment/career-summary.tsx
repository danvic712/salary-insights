import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import {
  BriefcaseIcon,
  CalendarIcon,
  BuildingIcon,
  ClockIcon,
  BarChartIcon,
} from "lucide-react";

const CareerSummary = ({ summary, onOpenSheet }) => (
  <Card>
    <CardHeader>
      <CardTitle>Career Summary</CardTitle>
    </CardHeader>
    <CardContent className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-4">
      <SummaryPanel
        title="Current Company"
        value={summary.currentCompany || "N/A"}
        icon={<BriefcaseIcon className="w-5 h-5 text-primary" />}
        onClick={() => onOpenSheet(1)}
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

interface SummaryPanelProps {
  title: string;
  value: string | number;
  icon: React.ReactNode;
  onClick?: () => void;
}

const SummaryPanel: React.FC<SummaryPanelProps> = ({
  title,
  value,
  icon,
  onClick = undefined,
}) => (
  <div className="flex items-center">
    <div className="flex-shrink-0 mr-3 text-primary">{icon}</div>
    <div>
      <p className="text-sm font-medium text-gray-600">{title}</p>
      <p
        className={`text-base font-semibold text-gray-900 ${
          onClick ? "cursor-pointer hover:text-primary" : ""
        }`}
        onClick={onClick}
      >
        {value}
      </p>
    </div>
  </div>
);

export default CareerSummary;
