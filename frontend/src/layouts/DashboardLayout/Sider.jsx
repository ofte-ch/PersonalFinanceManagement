import MenuCustom from "./Menu";
import { Layout, Space, Typography } from "antd";
import logo from "../../assets/logo.png";
import { useNavigate } from "react-router-dom";

const { Sider } = Layout;

function CustomSider({ collapsed, setCollapsed, theme }) {
  const navigate = useNavigate();
  return (
    <Sider
      theme={theme}
      trigger={null}
      className="overflow-auto h-screen !sticky top-0 left-0 bottom-0 px-1"
      collapsed={collapsed}
      width="200"
    >
      <>
        <Space
          className="flex flex-col items-center justify-center p-1 m-2 cursor-pointer"
          onClick={() => {
            navigate("/");
          }}
        >
          <img src={logo} className="w-14 h-14" />
        </Space>
        <MenuCustom
          mode="inline"
          theme={theme}
          onClose={() => setCollapsed(false)}
        />
      </>
    </Sider>
  );
}

export default CustomSider;