import React from "react";
import Loadable from "~/components/Loadable";
import DashboardLayout from "~/layouts/DashboardLayout";
import PrivateRoute from "~/guards/PrivateRoute";


const DashboardPage = Loadable(
  React.lazy(() => import("~/pages/manager/DashboardPage"))
);
const AccountPage = Loadable(
  React.lazy(() => import("~/pages/manager/AccountPage"))
);

const TransactionsPage = Loadable(
    React.lazy(() => import("~/pages/manager/TransationsPage"))
    );

const StatisticByTransactionPage = Loadable(
    React.lazy(() => import("~/pages/manager/StatisticByTransactionPage"))
);

const StatisticByAccountPage = Loadable(
    React.lazy(() => import("~/pages/manager/StatisticByAccountPage"))
);

export const DashboardRoutes = {
  children: [
    {
      path: '/dashboard',
      element: <PrivateRoute component={DashboardLayout} />,
      children: [
        {
          path: '',
          element: <PrivateRoute component={DashboardPage} />,
        },
        {
          path: 'accounts',
          element: <PrivateRoute component={AccountPage} />,
        },
        {
          path: 'transactions',
          element: <PrivateRoute component={TransactionsPage} />,
        },
        {
            path: 'statistics/by-transaction-type',
            element: <StatisticByTransactionPage />,
        },
        {
            path: 'statistics/by-account',
            element: <StatisticByAccountPage />
        }
      ],
    },
  ],
};
