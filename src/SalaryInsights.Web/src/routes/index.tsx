import { lazy } from "react";
import { Navigate } from "react-router-dom";

import Layout from "@/components/layouts/layout.tsx";
import ErrorHandler from "@/components/error-handler.tsx";

const Login = lazy(() => import("../pages/login.tsx"));
const Dashboard = lazy(() => import("../pages/dashboard/dashboard.tsx"));

const Salary = lazy(() => import("../pages/salary/salary.tsx"));
const SalaryDetails = lazy(() => import("../pages/salary/salary-details.tsx"));

const Employment = lazy(() => import("../pages/employment/employment.tsx"));

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
        path: "employment",
        children: [
          {
            path: "",
            element: <Employment />,
          },
        ],
      },
      {
        path: "settings",
        element: <SettingList />,
      },
      {
        path: "/salary",
        children: [
          {
            path: "",
            element: <Salary />,
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
