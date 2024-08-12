import { useEffect } from "react";
import { CircleUser, Menu, Package2 } from "lucide-react";
import { Button } from "../ui/button";
import { Sheet, SheetContent, SheetTrigger } from "../ui/sheet";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuRadioGroup,
  DropdownMenuRadioItem,
  DropdownMenuSeparator,
  DropdownMenuSub,
  DropdownMenuSubContent,
  DropdownMenuSubTrigger,
  DropdownMenuTrigger,
} from "../ui/dropdown-menu";
import { NavLink, useLocation } from "react-router-dom";
import { ModeToggle } from "../mode-toggle";
import { useTranslation } from "react-i18next";
import { loadNamespaces } from "@/lib/i18nLoader";

export default function Header() {
  const { t, i18n } = useTranslation("common");

  useEffect(() => {
    loadNamespaces("common");
  }, []);

  const changeLanguage = (lng) => {
    i18n.changeLanguage(lng);
  };

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
    <header className="sticky top-0 flex h-16 items-center gap-4 border-b bg-background px-4 md:px-8">
      <nav className="hidden flex-col gap-6 text-lg font-medium md:flex md:flex-row md:items-center md:gap-5 md:text-sm lg:gap-6">
        <NavLink
          to="/"
          className="flex items-center gap-2 text-lg font-semibold md:text-base"
        >
          <Package2 className="h-6 w-6" />
          <span className="sr-only">Salary Insights</span>
        </NavLink>

        <NavLink to="/dashboard" className={linkClass("/")}>
          Dashboard
        </NavLink>
        <NavLink to="/salary/overview" className={linkClass("/salary")}>
          Salaries
        </NavLink>
        <NavLink to="/companies" className={linkClass("/companies")}>
          Companies
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
            <NavLink to="/dashboard" className={linkClass("/")}>
              Dashboard
            </NavLink>
            <NavLink to="/salary/overview" className={linkClass("/salary")}>
              Salaries
            </NavLink>
            <NavLink to="/companies" className={linkClass("/companies")}>
              Companies
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
            <DropdownMenuLabel>{t("myAccount")}</DropdownMenuLabel>
            <DropdownMenuSeparator />
            <DropdownMenuItem>{t("settings")}</DropdownMenuItem>
            <DropdownMenuSub>
              <DropdownMenuSubTrigger>{t("language")}</DropdownMenuSubTrigger>
              <DropdownMenuSubContent>
                <DropdownMenuRadioGroup
                  value={i18n.language}
                  onValueChange={changeLanguage}
                >
                  <DropdownMenuRadioItem value="en-US">
                    {t("languages.english")}
                  </DropdownMenuRadioItem>
                  <DropdownMenuRadioItem value="zh-CN">
                    {t("languages.chinese")}
                  </DropdownMenuRadioItem>
                </DropdownMenuRadioGroup>
              </DropdownMenuSubContent>
            </DropdownMenuSub>
            <DropdownMenuSeparator />
            <DropdownMenuItem>{t("logout")}</DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      </div>
    </header>
  );
}
