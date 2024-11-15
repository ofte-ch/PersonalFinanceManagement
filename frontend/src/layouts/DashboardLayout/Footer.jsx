import { Footer } from "antd/es/layout/layout";

const FooterLayout = () => {
  return (
    <Footer
      className="text-center font-bad-script"
    >
      Copyright © {new Date().getFullYear()} Created by <span className="font-bold">Raiden Shogun</span>
    </Footer>
  );
};

export default FooterLayout;
