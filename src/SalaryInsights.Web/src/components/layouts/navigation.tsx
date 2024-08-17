import { NavLink, useLocation } from "react-router-dom";

import { Wallet } from "lucide-react";

const Navigation = () => {
  const location = useLocation();

  const isActive = (path: string) => {
    if (path === "/") {
      return location.pathname === "/" || location.pathname === "/dashboard";
    }

    return location.pathname.startsWith(path);
  };

  const linkClass = (path: string) =>
    isActive(path)
      ? "text-foreground transition-colors hover:text-foreground"
      : "text-muted-foreground transition-colors hover:text-foreground";

  return (
    <>
      <NavLink
        to="/"
        className="flex items-center gap-2 text-lg font-semibold md:text-base"
      >
        <Wallet className="h-6 w-6" />
        <span className="sr-only">Salary Insights</span>
      </NavLink>
      <NavLink to="/dashboard" className={linkClass("/")}>
        Dashboard
      </NavLink>
      <NavLink to="/salary" className={linkClass("/salary")}>
        Salary
      </NavLink>
      <NavLink to="/employment" className={linkClass("/employment")}>
        Employment
      </NavLink>
    </>
  );
};

export default Navigation;
