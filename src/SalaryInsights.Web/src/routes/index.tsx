import { lazy } from "react";

import Layout from "@/components/layout.tsx";

const Login = lazy(() => import("../pages/login.tsx"));
const Dashboard = lazy(() => import("../pages/dashboard/dashboard.tsx"));
const SalaryList = lazy(() => import("../pages/salaries/salary-list.tsx"));
const CompanyList = lazy(() => import("../pages/companies/company-list.tsx"));
const SettingList = lazy(() => import("../pages/settings/setting-list.tsx"));

const Routes = [
  {
    path: "/login",
    element: <Login />,
  },
  {
    path: "/",
    element: <Layout />,
    children: [
      {
        path: "",
        element: <Dashboard />,
      },
      {
        path: "dashboard",
        element: <Dashboard />,
      },
      {
        path: "salaries",
        element: <SalaryList />,
      },
      {
        path: "companies",
        element: <CompanyList />,
      },
      {
        path: "settings",
        element: <SettingList />,
      },
    ],
  },
];

export default Routes;
