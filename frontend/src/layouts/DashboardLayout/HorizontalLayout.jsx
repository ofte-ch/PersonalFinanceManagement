import { Layout } from "antd";
import { Content } from "antd/es/layout/layout";
import { useState } from "react";
import { Outlet } from "react-router-dom";
import CustomSider from "./Sider";
import HeaderLayout from "./Header";
import FooterLayout from "./Footer";

const HorizontalLayout = () =>{
     const [collapsed, setCollapsed] = useState(false);
     const [theme, setTheme] = useState('light'); // true for light mode, false for dark mode

     return (
          <Layout hasSider>
            <CustomSider collapsed={collapsed} setCollapsed={setCollapsed} theme={theme}/>
            <Layout>
              <HeaderLayout collapsed={collapsed} setCollapsed={setCollapsed} theme={theme} setTheme={setTheme}/>
              <Content className="px-6 py-6 content">
                <Outlet />
              </Content>
              <FooterLayout />
            </Layout>
          </Layout>
        );
}
export default HorizontalLayout;