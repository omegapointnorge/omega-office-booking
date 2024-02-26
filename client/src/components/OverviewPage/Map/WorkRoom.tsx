import React, { useState } from "react";
import { SeatPath } from "@/shared/types/entities";
import { SeatInRoom } from "@/shared/types/entities";

interface WorkRoomProps {
  className?: string;
  seats: SeatPath[];
  seatClicked: (e: React.MouseEvent<SVGPathElement>) => void;
  getSeatClassName: (seatId: number) => string;
}
const Seat = ({ id, d, seatClicked, getSeatClassName }: SeatInRoom) => {
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

export const WorkRoom = ({ seats, ...props }: WorkRoomProps) => (
  <g
    className={props.className}
    stroke="#000"
    strokeLinecap="round"
    strokeLinejoin="round"
    strokeWidth="5"
  >
    {seats.map(({ id, d }) => (
      <Seat key={id} id={id} d={d} {...props} />
    ))}
  </g>
);
