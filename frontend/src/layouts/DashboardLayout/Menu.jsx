import { ConfigProvider, Menu } from "antd";
import { useCallback,useMemo } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import MenuConfig from "~/constants/menuConfig";

const MenuCustom = ({ onClose,theme = "light", ...props }) => {
  const location = useLocation();
  const navigate = useNavigate();
  const menuItems = MenuConfig();

  const findItemByPath = useCallback((items, path) => {
    for (const item of items) {
      if (item.path && item.path.includes(path)) {
        return item;
      }
      if (item.children) {
        const child = findItemByPath(item.children, path);
        if (child) return child;
      }
    }
  }, []);

  const selectedKeys = useMemo(() => {
    const item = findItemByPath(menuItems, location.pathname);
    return item ? [item.key] : [];
  }, [location.pathname, findItemByPath]);

  const handleMenuClick = useCallback(
    (e) => {
      navigate(e.item.props.path);
    },
    [onClose, navigate]
  );

  return (
    <ConfigProvider
      theme={{
        token: {
          controlHeight: 50,
        },
      }}
    >
      <Menu
        theme={theme}
        selectedKeys={selectedKeys}
        items={menuItems}
        rootClassName="!border-none"
        onClick={handleMenuClick}
        {...props}

      >
        {menuItems.map((item) => (
          <Menu.Item key={item.key} icon={item.icon}>
            {item.title}
          </Menu.Item>
        ))}
      </Menu>
    </ConfigProvider>
  );
};

export default MenuCustom;
