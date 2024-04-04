import React from "react";
import ReactDOM from "react-dom/client";
import { Provider } from "react-redux";
import { initVChartSemiTheme } from "@visactor/vchart-semi-theme";

import App from "./App.jsx";
import store from "./store/index.js";

initVChartSemiTheme();

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <Provider store={store}>
      <App />
    </Provider>
  </React.StrictMode>
);
