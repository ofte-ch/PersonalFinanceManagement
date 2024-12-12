import Carousel from "~/components/Carousel";
const HomePage = () => {
  return (
    <div className="flex justify-between">
      <h1 className="w-1/5 text-[5vh] font-semibold pt-20 pr-10 font-roboto text-center ">
        Chào mừng đến với Web Quản Lý Tài Chính cá nhân
        Trợ thủ đắc lực hỗ trỡ quản lý chi tiêu của bạn.
      </h1>
      <div className="w-4/5">
        <Carousel />
      </div>
    </div>
  );
};
export default HomePage;
