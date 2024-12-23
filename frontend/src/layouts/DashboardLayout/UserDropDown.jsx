import {
  LogoutOutlined,
  SettingOutlined,
  UserOutlined,
} from "@ant-design/icons";
import { Avatar, Divider, Dropdown, Flex, Space, theme } from "antd";
import Cookies from "js-cookie";
import React from "react";
import { useNavigate } from "react-router-dom";
import { useAuthStore } from "~/stores/auth/authStore";
const { useToken } = theme;

const UserDropdown = () => {
  const navigate = useNavigate();
  const { user, clearUser } = useAuthStore();

  const handleLogout = () => {
    Cookies.remove('access_token');
    clearUser();
    navigate("/auth/login");
  };

  const moveProfile = () => {
    navigate("/profile");
  };

  // const moveSetting = () => {
  //   navigate("/settings");
  // };

  const { token } = useToken();
  const contentStyle = {
    backgroundColor: token.colorBgElevated,
    borderRadius: token.borderRadiusLG,
    boxShadow: token.boxShadowSecondary,
  };
  const menuStyle = {
    boxShadow: "none",
  };

  const items = [
    {
      key: "1",
      label: "Thông tin",
      icon: <UserOutlined />,
      onClick: moveProfile,
    },
    // {
    //   key: "2",
    //   label: "Settings",
    //   icon: <SettingOutlined />,
    //   onClick: moveSetting,
    // },
    {
      key: "2",
      label: "Đăng xuất",
      icon: <LogoutOutlined />,
      onClick: handleLogout,
      danger: true,
    },
  ];
  return (
    <Dropdown
      menu={{
        items,
      }}
      dropdownRender={(menu) => (
        <div style={contentStyle}>
          <Space
            style={{
              padding: 8,
            }}
          >
            <Flex vertical justify="start">
              <p className="text-primary font-medium text-sm">
                {user?.firstname} {user?.lastname}
              </p>
              <p className="text-[red]] text-xs">{user?.email}</p>
            </Flex>
          </Space>
          <Divider
            style={{
              margin: 0,
            }}
          />
          {React.cloneElement(menu, {
            style: menuStyle,
          })}
        </div>
      )}
      trigger={["click"]}
      placement="bottomRight"
      arrow
    >
      <Avatar
        // src={"https://robohash.org/2001:4860:7:412::1.png"}
        src={"https://avatars.githubusercontent.com/trandangnam"}
        className="border-2 border-primary cursor-pointer"
        size={40}
      >
        P
      </Avatar>
    </Dropdown>
  );
};

export default UserDropdown;
