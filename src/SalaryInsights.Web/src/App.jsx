import { Suspense } from "react";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { Spin } from "@douyinfe/semi-ui";

import Routes from "./routes";

const router = createBrowserRouter(Routes);

function App() {
  return (
    <Suspense
      fallback={
        <Spin
          tip="loading"
          size="large"
          style={{
            margin: "10px 0px",
          }}
        />
      }
    >
      <RouterProvider router={router} />
    </Suspense>
  );
}

export default App;
