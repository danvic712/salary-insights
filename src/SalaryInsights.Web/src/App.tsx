import { Suspense } from "react";

import { ThemeProvider } from "./components/theme-provider";
import { RouterProvider, createBrowserRouter } from "react-router-dom";

import Loading from "./components/loading";

import Routes from "./routes";
import "./i18n";

const router = createBrowserRouter(Routes);

function App() {
  return (
    <ThemeProvider defaultTheme="light" storageKey="salary-insights-ui-theme">
      <Suspense fallback={<Loading />}>
        <RouterProvider router={router} />
      </Suspense>
    </ThemeProvider>
  );
}

export default App;
