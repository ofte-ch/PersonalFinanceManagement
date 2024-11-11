import React from "react";
import Loadable from "~/components/Loadable";
import TransactionsLayout from "~/layouts/TransactionsLayout";

const TransactionsPage = Loadable(
  React.lazy(() => import("~/pages/manager/TransationsPage"))
);

export const TransactionsRoutes = {
  children: [
    {
      path: "/transactions",
      element: <TransactionsLayout />,
      children: [
        {
          path: "",
          element: <TransactionsPage />,
        },
      ],
    },
  ],
};
