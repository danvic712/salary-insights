import { lazy } from 'react';
import { useRoutes } from 'react-router-dom';

import MainLayout from '../layouts';

const Login = lazy(() => import('../pages/login'));
const Analysis = lazy(() => import('../pages/dashboard/analysis'));

const authRoutes = {
    path: '/login',
    element: <Login />,
};

const mainRoutes = {
    path: '/',
    element: <MainLayout />,
    children: [
        {
            path: '',
            element: <Analysis />,
        },
        {
            path: 'analysis',
            element: <Analysis />,
        },
    ],
};

export default function RouteRenders() {
    return useRoutes([authRoutes, mainRoutes]);
}
