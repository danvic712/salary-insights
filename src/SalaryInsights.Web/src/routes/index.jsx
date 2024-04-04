import { lazy } from "react";

import MainLayout from "../layouts";

const Login = lazy(() => import("../pages/login"));

const Dashboard = lazy(() => import("../pages/dashboard"));

const Payroll = lazy(() => import("../pages/payroll"));

const Settings = lazy(() => import("../pages/settings"));
const User = lazy(() => import("../pages/users"));

const Exception = lazy(() => import("../pages/exceptions"));

const Routes = [
  {
    path: "/login",
    element: <Login />,
  },
  {
    path: "/",
    element: <MainLayout />,
    children: [
      {
        path: "",
        element: <Dashboard />,
      },
      {
        path: "payroll",
        element: <Payroll />,
      },
      {
        path: "dashboard",
        element: <Dashboard />,
      },
      {
        path: "settings",
        element: <Settings />,
      },
      {
        path: "users",
        element: <User />,
      },
    ],
    errorElement: <MainLayout outlet={<Exception />} />,
  },
];

export default Routes;
