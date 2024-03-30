import { lazy } from "react";
import { createBrowserRouter, useRoutes } from "react-router-dom";

import MainLayout from "../layouts";

const Login = lazy(() => import("../pages/login"));

const Analysis = lazy(() => import("../pages/dashboard/analysis"));

const Payroll = lazy(() => import("../pages/payroll"));

const Configuration = lazy(() => import("../pages/settings/configurations"));

const Exception = lazy(() => import("../pages/exception"));

const authRoutes = {
  path: "/login",
  element: <Login />,
};

const mainRoutes = {
  path: "/",
  element: <MainLayout />,
  children: [
    {
      path: "",
      element: <Analysis />,
    },
    {
      path: "payroll",
      element: <Payroll />,
    },
    {
      path: "analysis",
      element: <Analysis />,
    },
    {
      path: "configurations",
      element: <Configuration />,
    },
  ],
};

const exceptionRoutes = {
  path: "*",
  element: <MainLayout />,
  children: [
    {
      path: "",
      element: <Exception />,
    },
  ],
};

export default function RenderRouters() {
  return useRoutes([authRoutes, mainRoutes, exceptionRoutes]);
}
