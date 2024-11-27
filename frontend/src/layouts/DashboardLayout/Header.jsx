import { Button, Space,Switch } from "antd";
import {
     MenuFoldOutlined,
    MenuUnfoldOutlined,
    MoonOutlined,
    SunOutlined
   } from "@ant-design/icons";
import { Header } from "antd/es/layout/layout";

const HeaderLayout = ({ collapsed, setCollapsed,theme,setTheme }) => {
  const changeTheme = (value) =>{
    if(localStorage.getItem("theme") == "light")
      localStorage.setItem("theme", "dark")
    else
      localStorage.setItem("theme", "light")
    setTheme(value ? 'dark' : 'light');
  }
  return (
    <Header 
      theme={theme}
      className="sticky top-0 z-50 p-5 flex items-center justify-between\">

      <Space>
          <Button
              id="menu-btn"
               type="text"
               icon={collapsed ? <MenuUnfoldOutlined className={theme === 'dark' ? 'text-white' : 'text-dark'} /> : <MenuFoldOutlined className={theme === 'dark' ? 'text-white' : 'text-dark '} />}
               onClick = {() => setCollapsed(!collapsed)}
          />
              <Switch
                  checked={theme === 'dark'}
                  onChange={changeTheme}
                  checkedChildren={<MoonOutlined />}
                  unCheckedChildren={<SunOutlined />}
          />
      </Space>
    </Header>
  );
};
export default HeaderLayout;
