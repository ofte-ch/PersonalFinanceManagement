import Loadable from "~/components/Loadable";
import React from "react";
import PrivateRoute from "~/guards/PrivateRoute";
import ProfileLayout from "~/layouts/Profile";

const ProfilePage = Loadable(React.lazy(() => import("~/pages/profile/ProfilePage")));

export const ProfileRoutes = {
  children: [
    {
      path: "/profile",
      element: <PrivateRoute component={ProfileLayout} />,
      children: [
        {
          path: "",
          element: <PrivateRoute component={ProfilePage} />
        },
      ]
    }
  ]
};