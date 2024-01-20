import { lazy } from 'react';

// project imports
import MainLayout from '../layout/MainLayout/index';
import Loadable from '../components/Loadable';

// dashboard routing
const DashboardDefault = Loadable(lazy(() => import('../pages/dashboard')));


const PortfolioSummary = Loadable(lazy(() => import('../pages/portfolio/Summary.jsx')));

// ==============================|| MAIN ROUTING ||============================== //

const MainRoutes = {
    path: '/',
    element: <MainLayout />,
    children: [
        {
            path: '/',
            element: <DashboardDefault />
        },
        {
            path: 'dashboard',
            children: [
                {
                    path: '',
                    element: <DashboardDefault />
                }
            ]
        },
        {
            path: 'salary',
            children: [
                {
                    path: '',
                    element: <PortfolioSummary />
                }
            ]
        }
    ]
};

export default MainRoutes;
