import { Col, Row } from "antd";
import { Outlet } from "react-router-dom";
import { ArrowLeftOutlined } from "@ant-design/icons";
import { useNavigate } from "react-router-dom";

const AuthLayout = () => {
  const navigate = useNavigate();

  const handleNavigate = () => {
    navigate("/"); // Thay bằng route bạn muốn chuyển hướng
  };
  return (
    <Row className="h-screen overflow-hidden justify-center">
      <div className="absolute inset-0 z-[-1] blur-[70px] overflow-hidden">
        <div className="absolute top-0 right-0 w-[300px] h-[300px] bg-purple-200 rounded-full opacity-100" />
        <div className="absolute bottom-[180px] w-[250px] h-[250px] bg-slate-400 rounded-full ml-20 opacity-100" />
        <div className="absolute bottom-0 left-[-50px] w-[200px] h-[200px] bg-blue-300 rounded-full opacity-100" />
        <div className="absolute bottom-0 right-[-50px] w-[200px] h-[200px] bg-orange-100 rounded-full opacity-100" />
      </div>
      <Col xs={22} md={12} xl={8} className="flex items-center justify-center">
      <div className="group fixed top-2 left-2 flex items-center">
        <a
          onClick={handleNavigate}
          className="flex items-center gap-2 px-4 py-2 min-h-9 bg-slate-300 text-white rounded hover:bg-slate-500 cursor-pointer"
        >
          <ArrowLeftOutlined />
        </a>
        
        <span className="flex left-full ml-2 px-4 py-2 text-sm bg-transparent text-slate-400 opacity-0 group-hover:opacity-100 transition-opacity">
          Back to home page
        </span>
      </div>
        <Outlet />
      </Col>
    </Row>
  );
};

export default AuthLayout;
