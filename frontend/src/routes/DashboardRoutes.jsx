import React from "react";
import Loadable from "~/components/Loadable";
import DashboardLayout from "~/layouts/DashboardLayout";


const DashboardPage = Loadable(
  React.lazy(() => import("~/pages/manager/DashboardPage"))
);
const AccountPage = Loadable(
  React.lazy(() => import("~/pages/manager/AccountPage"))
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
      element: <DashboardLayout />,
      children: [
        {
          path: '',
          element: <DashboardPage />,
        },
        {
          path: 'accounts',
          element: <AccountPage />,
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
