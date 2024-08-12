import { lazy } from "react";
import { Navigate } from "react-router-dom";

import Layout from "@/components/layouts/layout.tsx";
import ErrorHandler from "@/components/error-handler.tsx";

const Login = lazy(() => import("../pages/login.tsx"));
const Dashboard = lazy(() => import("../pages/dashboard/dashboard.tsx"));

const SalaryOverview = lazy(
  () => import("../pages/salaries/salary-overview.tsx")
);
const SalaryDetails = lazy(
  () => import("../pages/salaries/salary-details.tsx")
);

const CompanyList = lazy(() => import("../pages/companies/company-list.tsx"));
const SettingList = lazy(() => import("../pages/settings/setting-list.tsx"));

const Routes = [
  {
    path: "/login",
    element: <Login />,
    errorElement: <ErrorHandler />,
  },
  {
    path: "/",
    element: <Layout />,
    children: [
      {
        index: true,
        element: <Dashboard />,
      },
      {
        path: "dashboard",
        element: <Dashboard />,
      },
      {
        path: "companies",
        element: <CompanyList />,
      },
      {
        path: "settings",
        element: <SettingList />,
      },
      {
        path: "/salary",
        children: [
          {
            index: true,
            element: <Navigate to="/salary/overview" replace />,
          },
          {
            path: "overview",
            element: <SalaryOverview />,
          },
          {
            path: ":month/details",
            element: <SalaryDetails />,
          },
        ],
      },
    ],
    errorElement: <ErrorHandler />,
  },
];

export default Routes;
