import { Button, Layout, Space, Typography } from "antd";
import { Content, Footer } from "antd/es/layout/layout";
import { Link, Outlet, useNavigate } from "react-router-dom";
import {
  LoginOutlined,
  GithubOutlined,
  FacebookOutlined,
} from "@ant-design/icons";
import logo from "../../assets/logo.png";

const { Header } = Layout;

const HomeLayout = () => {
  const navigate = useNavigate();
  return (
    <>
      <Layout className="min-h-screen bg-gradient-to-b from-gray-400 to-slate-300">
        <Header className="flex justify-between items-center bg-gradient-to-r from-slate-600 to-blue-200 shadow-md px-4 md:px-8">
          <div className="flex items-center">
            <img
              src={logo}
              alt="logo"
              className="w-10 h-10 md:w-12 md:h-12 transition-transform duration-300 cursor-pointer"
              onClick={() => navigate("/")}
            />
            <span className="text-xs md:text-xl font-medium ml-2 text-yellow-400">
              Personal Finance Management
            </span>
          </div>
          <div>
            <Space>
              <Button
                type="primary"
                icon={<LoginOutlined />}
                className="bg-white text-zinc-950 hover:bg-blue-500 transition-colors duration-300"
              >
                Login
              </Button>
            </Space>
          </div>
        </Header>
        <Content className="p-4 md:p-8 lg:p-16"><Outlet /></Content>
        <Footer className="text-center bg-slate-400 py-4 md:py-8">
          <div className="mb-4">
            <Space size="large">
              <Link to="https://github.com/trandangnam">
                <GithubOutlined className="text-xl md:text-2xl hover:text-blue-500 transition-colors duration-300 cursor-pointer" />
              </Link>
              <Link to="https://www.facebook.com/profile.php?id=100027954192211&mibextid=ZbWKwL">
                <FacebookOutlined className="text-xl md:text-2xl hover:text-blue-500 transition-colors duration-300 cursor-pointer" />
              </Link>
            </Space>
          </div>
          <Typography.Text className="text-gray-600 text-sm md:text-base">
            Ant Design ©{new Date().getFullYear()} Created by{" "}
            <span className="font-bold">Raiden Shogun</span>
          </Typography.Text>
        </Footer>
      </Layout>
    </>
  );
};

export default HomeLayout;
