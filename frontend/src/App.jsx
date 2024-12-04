import { ConfigProvider, App as AntApp } from "antd";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ThemeRoutes } from "./routes";
import { useAuthStore } from "./stores/auth/authStore";
import { Children, useEffect } from "react";
import { useMe } from "./api/auth/me";

const queryClient = new QueryClient();

function App() {
  const router = createBrowserRouter(ThemeRoutes);

  return (
    <>
      <ConfigProvider
        theme={{
          token: {
            fontFamily: "Inter",
            borderRadius: 4,
            controlHeight: 40,
          },
          components: {
            Table: {
              defaultProps: {
                size: "middle",
                bordered: true,
                scroll: { x: "max-content" },
              },
            },
          },
          hashed: false,
        }}
      >
        <QueryClientProvider client={queryClient}>
          <AuthProvider>
            <AntApp>
              <RouterProvider router={router} />
            </AntApp>
          </AuthProvider>
        </QueryClientProvider>
      </ConfigProvider>
    </>
  );
}

const AuthProvider = ({ children }) => {
  const { data, isSuccess, isFetching } = useMe();
  const { setIsAuthenticated, setUser } = useAuthStore((state) => state);

  useEffect(() => {
    if (isSuccess && data) {
      setIsAuthenticated(true);
      setUser(data.user);
    } else {
      setIsAuthenticated(false);
      setUser(null);
    }
  }, [isSuccess, data, setIsAuthenticated, setUser]);

  if (isFetching) {
    return null;
  }

  return children;
};

export default App;
