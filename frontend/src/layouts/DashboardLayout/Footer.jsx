import { Footer } from "antd/es/layout/layout";

const FooterLayout = () => {
  return (
    <Footer
      className="text-center font-bad-script"
    >
      Copyright © {new Date().getFullYear()} Phát triển bởi <span className="font-bold">Nhóm 1</span>
    </Footer>
  );
};

export default FooterLayout;
