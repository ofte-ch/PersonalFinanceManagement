import { Navigate } from "react-router-dom";
import { useAuthStore } from "~/stores/auth/authStore";

const AuthRoute = ({ component: Component }) => {
  const { isAuthenticated } = useAuthStore((state) => state);
  if (isAuthenticated) {
    return <Navigate to="/dashboard" replace={true} />;
  }
  return <Component />;
};

export default AuthRoute;