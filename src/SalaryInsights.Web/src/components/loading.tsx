import React from "react";

const Loading: React.FC = () => {
  return (
    <div className="flex flex-1 flex-col items-center justify-center h-screen">
      <div className="animate-spin rounded-full h-16 w-16 border-t-4 border-b-4 border-blue-500 mb-4"></div>
      <p className="text-lg">Loading...</p>
    </div>
  );
};

export default Loading;
