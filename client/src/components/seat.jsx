import React from "react";

const Seat = ({ seatNumber, isAvailable }) => {
    const seatClasses = `w-12 h-12 m-2 rounded-full border border-gray-300 flex items-center justify-center ${
        isAvailable ? 'bg-green-200' : 'bg-red-200'
      }`;

      return (
        <div className="{seatClasses}">
                {seatNumber}
        </div>
      );
};

export default Seat;