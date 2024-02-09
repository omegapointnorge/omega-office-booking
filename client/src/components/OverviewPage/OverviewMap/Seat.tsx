import { SeatInRoom } from "@/shared/types/entities";
import React, { useState } from "react";

export const Seat = ({ id, d, seatClicked, getSeatClassName }: SeatInRoom) => {
  const [isSeatClicked, setIsSeatClicked] = useState(false);

  const handleSeatClicked = (
    e: React.MouseEvent<SVGPathElement, MouseEvent>
  ) => {
    setIsSeatClicked(!isSeatClicked);
    seatClicked(e);
  };
  return (
    <path
      key={id}
      id={id}
      className={getSeatClassName(Number(id))}
      onClick={handleSeatClicked}
      d={d}
    />
  );
};
