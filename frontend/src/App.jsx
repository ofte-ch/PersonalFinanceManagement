import { ConfigProvider, App as AntApp } from "antd";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ThemeRoutes } from "./routes";
import { useAuthStore } from "./stores/auth/authStore";
import { Children, useEffect } from "react";
import AuthProvider from "./AuthProvider";

const queryClient = new QueryClient();

function App() {
  const router = createBrowserRouter(ThemeRoutes, {
    future: {
      v7_fetcherPersist: true,
      v7_normalizeFormMethod: true,
      v7_partialHydration: true,
      v7_relativeSplatPath: true,
      v7_skipActionErrorRevalidation: true,
      v7_startTransition: true,
    },
  });

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

export default App;
