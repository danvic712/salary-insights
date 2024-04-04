import { Suspense } from "react";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Routes from "./routes";

const router = createBrowserRouter(Routes);

function App() {
  return (
    <Suspense fallback={<div className="container">Loading...</div>}>
      <RouterProvider router={router} />
    </Suspense>
  );
}

export default App;
