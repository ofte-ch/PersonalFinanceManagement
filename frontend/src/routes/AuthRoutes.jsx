import AuthLayout from "~/layouts/AuthLayout";
import Loadable from "~/components/Loadable";
import React from "react";
import AuthRoute from "~/guards/AuthRoute";

const LoginPage = Loadable(React.lazy(() => import("~/pages/auth/LoginRegis")));

export const AuthRoutes = {
  children: [
    {
      path: "/auth",
      element: <AuthRoute component={AuthLayout} />,
      children: [
        {
          path: "login",
          element: <AuthRoute component={LoginPage} />
        },
      ]
    }
  ]
};