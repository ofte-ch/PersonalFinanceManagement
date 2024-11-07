import { Button, Space,Switch } from "antd";
import {
     MenuFoldOutlined,
     MenuUnfoldOutlined,
   } from "@ant-design/icons";
import { Header } from "antd/es/layout/layout";

const HeaderLayout = ({ collapsed, setCollapsed,theme,setTheme }) => {
  const changeTheme = (value) =>{
    setTheme(value ? 'dark' : 'light');
  }
  return (
    <Header className={`sticky top-0 z-50 p-5 flex items-center justify-between ${theme === 'dark' ? 'bg-dark border-dark' : 'bg-white' }`}>
      <Space>
          <Button
               type="text"
               icon={collapsed ? <MenuUnfoldOutlined className={theme === 'dark' ? 'text-white' : 'text-dark'} /> : <MenuFoldOutlined className={theme === 'dark' ? 'text-white' : 'text-dark '} />}
               onClick = {() => setCollapsed(!collapsed)}
          />
          <Switch
              checked={theme === 'dark'}
              onChange={changeTheme}
              checkedChildren="Dark"
              unCheckedChildren="Light"
          />
      </Space>
      <Space></Space>
    </Header>
  );
};
export default HeaderLayout;