import React from "react";
import Loadable from "~/components/Loadable";
import DashboardLayout from "~/layouts/DashboardLayout";

const DashboardPage = Loadable(
  React.lazy(() => import("~/pages/manager/DashboardPage"))
);

export const DashboardRoutes = {
  children: [
    {
      path: "/dashboard",
      element: <DashboardLayout />,
      children: [
        {
          path: "",
          element: <DashboardPage />,
        },
      ],
    },
  ],
};
