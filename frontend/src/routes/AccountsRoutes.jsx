import React from "react";
import Loadable from "~/components/Loadable";
import AccountsLayout from "~/layouts/AccountsLayout";

const AccountsPage = Loadable(
  React.lazy(() => import("~/pages/manager/TransationsPage"))
);

export const TransactionsRoutes = {
  children: [
    {
      path: "/transactions",
      element: <AccountsLayout />,
      children: [
        {
          path: "",
          element: <AccountsPage />,
        },
      ],
    },
  ],
};
