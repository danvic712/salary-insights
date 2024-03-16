import { Suspense } from "react";

import RenderRouters from "./routes/index";

function App() {
  return (
    <Suspense fallback={<div className="container">Loading...</div>}>
      <RenderRouters />
    </Suspense>
  );
}

export default App;
