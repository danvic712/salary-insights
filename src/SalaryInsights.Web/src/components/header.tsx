import { CircleUser, Menu, Package2 } from "lucide-react";
import { Button } from "./ui/button";
import { Sheet, SheetContent, SheetTrigger } from "./ui/sheet";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "./ui/dropdown-menu";
import { NavLink, useLocation } from "react-router-dom";
import { ModeToggle } from "./mode-toggle";

export default function Header() {
  const location = useLocation();

  const isDashboardLink = () => {
    return location.pathname === "/" || location.pathname === "/dashboard";
  };

  return (
    <header className="sticky top-0 flex h-16 items-center gap-4 border-b bg-background px-4 md:px-8">
      <nav className="hidden flex-col gap-6 text-lg font-medium md:flex md:flex-row md:items-center md:gap-5 md:text-sm lg:gap-6">
        <NavLink
          to="/"
          className="flex items-center gap-2 text-lg font-semibold md:text-base"
        >
          <Package2 className="h-6 w-6" />
          <span className="sr-only">Salary Insights</span>
        </NavLink>

        <NavLink
          to="/dashboard"
          className={
            isDashboardLink()
              ? "text-foreground transition-colors hover:text-foreground"
              : "text-muted-foreground transition-colors hover:text-foreground"
          }
        >
          Dashboard
        </NavLink>
        <NavLink
          to="/salaries"
          className={({ isActive }) =>
            isActive
              ? "text-foreground transition-colors hover:text-foreground"
              : "text-muted-foreground transition-colors hover:text-foreground"
          }
        >
          Salaries
        </NavLink>
        <NavLink
          to="companies"
          className={({ isActive }) =>
            isActive
              ? "text-foreground transition-colors hover:text-foreground"
              : "text-muted-foreground transition-colors hover:text-foreground"
          }
        >
          Companies
        </NavLink>
        <NavLink
          to="settings"
          className={({ isActive }) =>
            isActive
              ? "text-foreground transition-colors hover:text-foreground"
              : "text-muted-foreground transition-colors hover:text-foreground"
          }
        >
          Settings
        </NavLink>
      </nav>
      <Sheet>
        <SheetTrigger asChild>
          <Button variant="outline" size="icon" className="shrink-0 md:hidden">
            <Menu className="h-5 w-5" />
            <span className="sr-only">Toggle navigation menu</span>
          </Button>
        </SheetTrigger>
        <SheetContent side="left">
          <nav className="grid gap-6 text-lg font-medium">
            <NavLink
              to="/"
              className="flex items-center gap-2 text-lg font-semibold"
            >
              <Package2 className="h-6 w-6" />
              <span className="sr-only">Salary Insights</span>
            </NavLink>
            <NavLink
              to="/dashboard"
              className={({ isActive }) =>
                isActive
                  ? "text-foreground transition-colors hover:text-foreground"
                  : "text-muted-foreground transition-colors hover:text-foreground"
              }
            >
              Dashboard
            </NavLink>
            <NavLink
              to="/salaries"
              className={({ isActive }) =>
                isActive
                  ? "text-foreground transition-colors hover:text-foreground"
                  : "text-muted-foreground transition-colors hover:text-foreground"
              }
            >
              Salaries
            </NavLink>
            <NavLink
              to="/companies"
              className={({ isActive }) =>
                isActive
                  ? "text-foreground transition-colors hover:text-foreground"
                  : "text-muted-foreground transition-colors hover:text-foreground"
              }
            >
              Companies
            </NavLink>
            <NavLink
              to="/settings"
              className={({ isActive }) =>
                isActive
                  ? "text-foreground transition-colors hover:text-foreground"
                  : "text-muted-foreground transition-colors hover:text-foreground"
              }
            >
              Settings
            </NavLink>
          </nav>
        </SheetContent>
      </Sheet>
      <div className="flex justify-end w-full items-center gap-4 md:ml-auto md:gap-2 lg:gap-4">
        <ModeToggle />
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <Button variant="secondary" size="icon" className="rounded-full">
              <CircleUser className="h-5 w-5" />
              <span className="sr-only">Toggle user menu</span>
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent align="end">
            <DropdownMenuLabel>My Account</DropdownMenuLabel>
            <DropdownMenuSeparator />
            <DropdownMenuItem>Settings</DropdownMenuItem>
            <DropdownMenuItem>Support</DropdownMenuItem>
            <DropdownMenuSeparator />
            <DropdownMenuItem>Logout</DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      </div>
    </header>
  );
}
