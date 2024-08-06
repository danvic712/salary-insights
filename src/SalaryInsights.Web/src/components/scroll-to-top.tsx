import React, { useState, useEffect } from "react";
import { ArrowUp } from "lucide-react";

const ScrollToTop = () => {
  const [isVisible, setIsVisible] = useState(false);
  const [isRotating, setIsRotating] = useState(false);

  useEffect(() => {
    const toggleVisibility = () => {
      if (window.pageYOffset > 300) {
        setIsVisible(true);
      } else {
        setIsVisible(false);
      }
    };

    window.addEventListener("scroll", toggleVisibility);

    return () => window.removeEventListener("scroll", toggleVisibility);
  }, []);

  const scrollToTop = () => {
    setIsRotating(true);
    window.scrollTo({
      top: 0,
      behavior: "smooth",
    });
    setTimeout(() => setIsRotating(false), 1000); // 动画持续1秒
  };

  return (
    <>
      {isVisible && (
        <button
          onClick={scrollToTop}
          className={`fixed bottom-8 right-8 w-12 h-12 rounded-full shadow-lg transition-all duration-300 ease-in-out
                     bg-blue-500 hover:bg-blue-600
                     text-white border-none outline-none focus:ring-2 focus:ring-blue-400 focus:ring-opacity-50
                     flex items-center justify-center
                     ${isRotating ? "animate-spin" : ""}`}
        >
          <ArrowUp className="w-8 h-8" />
        </button>
      )}
      <style>
        {`
          @keyframes spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
          }
          .animate-spin {
            animation: spin 1s linear;
          }
        `}
      </style>
    </>
  );
};

export default ScrollToTop;
