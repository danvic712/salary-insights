import React from "react";
import ReactDOM from "react-dom/client";
import { Provider } from "react-redux";
import { initVChartSemiTheme } from "@visactor/vchart-semi-theme";

import App from "./App.jsx";
import store from "./store/index.js";

import zh_CN from "@douyinfe/semi-ui/lib/es/locale/source/zh_CN";
import en_US from "@douyinfe/semi-ui/lib/es/locale/source/en_US";
import { LocaleProvider } from "@douyinfe/semi-ui";

initVChartSemiTheme();

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <Provider store={store}>
      <LocaleProvider locale={en_US}>
        <App />
      </LocaleProvider>
    </Provider>
  </React.StrictMode>
);
