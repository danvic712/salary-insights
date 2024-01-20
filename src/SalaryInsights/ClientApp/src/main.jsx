import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'

import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'react-redux';

import { store } from './store/index';

// style + assets
import './assets/scss/style.scss';
import config from './AppSettings';

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <Provider store={store}>
      <BrowserRouter basename={config.basename}>
        <App />
      </BrowserRouter>
    </Provider>
  </React.StrictMode>
)