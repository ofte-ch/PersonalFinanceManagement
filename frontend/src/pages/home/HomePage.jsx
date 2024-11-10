import Carousel from "~/components/Carousel";
const HomePage = () => {
  return (
    <div className="flex justify-between">
      <h1 className="w-1/5 text-[5vh] font-semibold pt-20 pr-10 font-bad-script text-center ">
          Welcome to Personal Finance Management!
          Manage your finance today !
      </h1>
      <div className="w-4/5">
        <Carousel />
      </div>
    </div>
  );
};
export default HomePage;
