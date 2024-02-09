import React from "react";
import { seatsSmallRoom } from "./SeatPaths";
import { Seat } from "./Seat";

interface WorkRoomProps {
  className?: string;
  seatClicked: (e: React.MouseEvent<SVGPathElement>) => void;
  getSeatClassName: (seatId: number) => string;
}

export const SmallWorkRoom = (props: WorkRoomProps) => {
  return (
    <g
      className={props.className}
      stroke="#000"
      strokeLinecap="round"
      strokeLinejoin="round"
      strokeWidth="5"
    >
      {seatsSmallRoom.map(({ id, d }) => (
        <Seat key={id} id={id} d={d} {...props} />
      ))}
    </g>
  );
};
