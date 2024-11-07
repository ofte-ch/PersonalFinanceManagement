import React from "react";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import saving from "../assets/carousel/savings.png";
import transaction from "../assets/carousel/transaction.png";
import investment from "../assets/carousel/investment.png";

const Carousel = () => {
  const settings = {
    arrows: false,
    infinite: true,
    speed: 500,
    slidesToShow: 1,
    slidesToScroll: 1,
    autoplay: true,
    autoplaySpeed: 3000,
  };

  const images = [
     saving,
     transaction,
     investment
  ];

  return (
      <Slider {...settings}>
        {images.map((image, index) => (
          <div key={index} className="flex justify-center">
            <img src={image} alt={`Slide ${index + 1}`} className="w-[60vw] h-[75vh] object-cover"/>
          </div>
        ))}
      </Slider>
  );
};

export default Carousel;
