import React from "react";

import { Avatar, AvatarFallback, AvatarImage } from "@/components/ui/avatar";

import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";

import {
  ChartConfig,
  ChartContainer,
  ChartTooltip,
  ChartTooltipContent,
} from "@/components/ui/chart";

import { CartesianGrid, Line, LineChart, XAxis } from "recharts";
import { Activity, CreditCard, DollarSign, Users } from "lucide-react";

import YearlyRevenue from "./yearly-revenue";
import TopMonthlyRevenue from "./top-monthly-revenue";
import MetricCard from "./metric-card";

export default function Dashboard() {
  const chartData = [
    { date: "2024-01", desktop: 222, mobile: 150 },
    { date: "2024-02", desktop: 97, mobile: 180 },
    { date: "2024-03", desktop: 167, mobile: 120 },
    { date: "2024-04", desktop: 242, mobile: 260 },
    { date: "2024-05", desktop: 373, mobile: 290 },
    { date: "2024-06", desktop: 301, mobile: 340 },
    { date: "2024-07", desktop: 245, mobile: 180 },
    { date: "2024-08", desktop: 409, mobile: 320 },
    { date: "2024-09", desktop: 59, mobile: 110 },
    { date: "2024-10", desktop: 261, mobile: 190 },
    { date: "2024-11", desktop: 92, mobile: 150 },
    { date: "2024-12", desktop: 492, mobile: 420 },
  ];

  const chartConfig = {
    views: {
      label: "Amount",
    },
    desktop: {
      label: "Desktop",
      color: "hsl(var(--chart-2))",
    },
    mobile: {
      label: "Mobile",
      color: "hsl(var(--chart-1))",
    },
  } satisfies ChartConfig;

  const [activeChart, setActiveChart] =
    React.useState<keyof typeof chartConfig>("desktop");

  const total = React.useMemo(
    () => ({
      desktop: chartData.reduce((acc, curr) => acc + curr.desktop, 0),
      mobile: chartData.reduce((acc, curr) => acc + curr.mobile, 0),
    }),
    []
  );

  const revenueData = [
    { year: "2018", revenue: 50000 },
    { year: "2019", revenue: 65000 },
    { year: "2020", revenue: 75000 },
    { year: "2021", revenue: 90000 },
    { year: "2022", revenue: 110000 },
    { year: "2023", revenue: 120000 },
    { year: "2024", revenue: 200000 },
  ];

  const topMonthlyRevenueData = [
    { month: "2024-02", revenue: 186 },
    { month: "2023-12", revenue: 305 },
    { month: "2024-06", revenue: 237 },
    { month: "2022-03", revenue: 731 },
    { month: "2021-08", revenue: 209 },
  ];

  return (
    <>
      <div className="flex justify-between items-center">
        <h1 className="text-3xl font-bold">Dashboard</h1>
      </div>
      <div className="grid gap-4 md:grid-cols-2 md:gap-8 lg:grid-cols-4">
        <MetricCard
          title="Total Revenue"
          icon={DollarSign}
          value="$45,231.89"
          description="+20.1% from last month"
        />
        <MetricCard
          title="Subscriptions"
          icon={Users}
          value="2,350"
          description="+180.1% from last month"
        />
        <MetricCard
          title="Sales"
          icon={CreditCard}
          value="+12,234"
          description="+19% from last month"
        />
        <MetricCard
          title="Active Now"
          icon={Activity}
          value="573"
          description="+201 since last hour"
        />
      </div>
      <div className="grid gap-4 md:gap-8 lg:grid-cols-2 xl:grid-cols-3">
        <Card className="xl:col-span-2" x-chunk="dashboard-01-chunk-4">
          <CardHeader className="flex flex-col items-stretch space-y-0 border-b p-0 sm:flex-row">
            <div className="flex flex-1 flex-col gap-1 p-6">
              <CardTitle>
                Recent Salary
                <CardDescription className="py-2">
                  January - June 2024
                </CardDescription>
              </CardTitle>
            </div>
            <div className="flex">
              {["desktop", "mobile"].map((key) => {
                const chart = key as keyof typeof chartConfig;
                return (
                  <button
                    key={chart}
                    data-active={activeChart === chart}
                    className="flex flex-1 flex-col justify-center gap-1 border-t px-6 py-4 text-left even:border-l data-[active=true]:bg-muted/50 sm:border-l sm:border-t-0 sm:px-8 sm:py-6"
                    onClick={() => setActiveChart(chart)}
                  >
                    <span className="text-xs text-muted-foreground">
                      {chartConfig[chart].label}
                    </span>
                    <span className="text-lg font-bold leading-none sm:text-3xl">
                      {total[key as keyof typeof total].toLocaleString()}
                    </span>
                  </button>
                );
              })}
            </div>
          </CardHeader>
          <CardContent>
            <ChartContainer
              config={chartConfig}
              className="aspect-auto h-[250px] w-full"
            >
              <LineChart
                accessibilityLayer
                data={chartData}
                margin={{
                  left: 12,
                  right: 12,
                }}
              >
                <CartesianGrid vertical={false} />
                <XAxis
                  dataKey="date"
                  tickLine={false}
                  axisLine={false}
                  tickMargin={8}
                  minTickGap={32}
                  tickFormatter={(value) => {
                    const date = new Date(value);
                    return date.toLocaleDateString("en-US", {
                      month: "short",
                    });
                  }}
                />
                <ChartTooltip
                  content={
                    <ChartTooltipContent
                      className="w-[150px]"
                      nameKey="views"
                      labelFormatter={(value) => {
                        return new Date(value).toLocaleDateString("en-US", {
                          month: "short",
                        });
                      }}
                    />
                  }
                />
                <Line
                  dataKey={activeChart}
                  type="monotone"
                  stroke={`var(--color-${activeChart})`}
                  strokeWidth={2}
                  dot={false}
                />
              </LineChart>
            </ChartContainer>
          </CardContent>
        </Card>
        <Card x-chunk="dashboard-01-chunk-5">
          <CardHeader>
            <CardTitle>Recent Sales</CardTitle>
          </CardHeader>
          <CardContent className="grid gap-8">
            <div className="flex items-center gap-4">
              <Avatar className="hidden h-9 w-9 sm:flex">
                <AvatarImage src="/avatars/01.png" alt="Avatar" />
                <AvatarFallback>OM</AvatarFallback>
              </Avatar>
              <div className="grid gap-1">
                <p className="text-sm font-medium leading-none">
                  Olivia Martin
                </p>
                <p className="text-sm text-muted-foreground">
                  olivia.martin@email.com
                </p>
              </div>
              <div className="ml-auto font-medium">+$1,999.00</div>
            </div>
            <div className="flex items-center gap-4">
              <Avatar className="hidden h-9 w-9 sm:flex">
                <AvatarImage src="/avatars/02.png" alt="Avatar" />
                <AvatarFallback>JL</AvatarFallback>
              </Avatar>
              <div className="grid gap-1">
                <p className="text-sm font-medium leading-none">Jackson Lee</p>
                <p className="text-sm text-muted-foreground">
                  jackson.lee@email.com
                </p>
              </div>
              <div className="ml-auto font-medium">+$39.00</div>
            </div>
            <div className="flex items-center gap-4">
              <Avatar className="hidden h-9 w-9 sm:flex">
                <AvatarImage src="/avatars/03.png" alt="Avatar" />
                <AvatarFallback>IN</AvatarFallback>
              </Avatar>
              <div className="grid gap-1">
                <p className="text-sm font-medium leading-none">
                  Isabella Nguyen
                </p>
                <p className="text-sm text-muted-foreground">
                  isabella.nguyen@email.com
                </p>
              </div>
              <div className="ml-auto font-medium">+$299.00</div>
            </div>
            <div className="flex items-center gap-4">
              <Avatar className="hidden h-9 w-9 sm:flex">
                <AvatarImage src="/avatars/04.png" alt="Avatar" />
                <AvatarFallback>WK</AvatarFallback>
              </Avatar>
              <div className="grid gap-1">
                <p className="text-sm font-medium leading-none">William Kim</p>
                <p className="text-sm text-muted-foreground">will@email.com</p>
              </div>
              <div className="ml-auto font-medium">+$99.00</div>
            </div>
            <div className="flex items-center gap-4">
              <Avatar className="hidden h-9 w-9 sm:flex">
                <AvatarImage src="/avatars/05.png" alt="Avatar" />
                <AvatarFallback>SD</AvatarFallback>
              </Avatar>
              <div className="grid gap-1">
                <p className="text-sm font-medium leading-none">Sofia Davis</p>
                <p className="text-sm text-muted-foreground">
                  sofia.davis@email.com
                </p>
              </div>
              <div className="ml-auto font-medium">+$39.00</div>
            </div>
          </CardContent>
        </Card>
      </div>
      <div className="grid gap-4 md:grid-cols-2 md:gap-8 lg:grid-cols-2">
        <YearlyRevenue data={revenueData} />
        <TopMonthlyRevenue data={topMonthlyRevenueData} />
      </div>
    </>
  );
}
