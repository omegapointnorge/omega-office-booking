import React from "react";
import { Seat } from "./Seat";
import { SeatPath } from "@/shared/types/entities";

interface WorkRoomProps {
  className?: string;
  seats: SeatPath[];
  seatClicked: (e: React.MouseEvent<SVGPathElement>) => void;
  getSeatClassName: (seatId: number) => string;
}

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
