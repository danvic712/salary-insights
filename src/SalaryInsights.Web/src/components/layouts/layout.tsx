import { Outlet } from "react-router-dom";

import Header from "./header";
import ScrollToTop from "../scroll-to-top";

interface LayoutProps {
  outlet?: React.ReactNode;
}

export default function Layout(props: LayoutProps) {
  return (
    <div className="flex min-h-screen w-full flex-col">
      <Header />

      <main className="flex flex-1 flex-col gap-4 p-4 md:gap-8 md:p-8">
        {props?.outlet ? props.outlet : <Outlet />}

        <ScrollToTop />
      </main>
    </div>
  );
}
