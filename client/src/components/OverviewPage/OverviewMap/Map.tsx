import React from "react";

import { Rooms } from "@/shared/types/enums";
import { ZoomOutIcon } from "../../../shared/assets/icons/zoom-out_outline";
import { WorkRoom } from "./SmallWorkRoom";
import { seatsLargeRoom, seatsSmallRoom } from "./SeatPaths";
import { Booking } from "@/shared/types/entities";
import { isSameDate } from "@/shared/utils";

interface MapProps {
  currentViewBox: string;
  zoomStatus: string;
  zoomToRoom: (value: Rooms) => void;
  getSeatClassName: (value: number) => string;
  displayDate: Date;
  seatClicked: (e: React.MouseEvent<SVGPathElement>) => void;
  activeBookings: Booking[];
  zoomOut: () => void;
}

export const MapComponent = ({
  currentViewBox,
  zoomStatus,
  zoomToRoom,
  getSeatClassName,
  displayDate,
  activeBookings,
  seatClicked,
  zoomOut,
}: MapProps) => {
  const countAvailableSeats = (
    minSeatId: number,
    maxSeatId: number
  ): number => {
    let availableSeats = 0;

    for (let seatId = minSeatId; seatId <= maxSeatId; seatId++) {
      let isSeatAvailable = true;

      for (const booking of activeBookings) {
        if (
          booking.seatId === seatId &&
          isSameDate(booking.bookingDateTime, displayDate)
        ) {
          isSeatAvailable = false;
          break;
        }
      }

      if (isSeatAvailable) {
        availableSeats++;
      }
    }

    return availableSeats;
  };

  return (
    <div className="relative">
      <svg
        version="1.1"
        width="745"
        height="500"
        viewBox={currentViewBox}
        xmlns="http://www.w3.org/2000/svg"
      >
        <g opacity="1.0" stroke="black" strokeWidth="3" fill="none">
          <path d="m897.96 1834.7 683.29-5.5963 38.242 3.6077 42.571 14.792 36.077 23.811 23.089 24.532 7.2154 9.38 9.7407 18.038 11.905 29.222 4.69 18.399 1.4431 172.81-51.229 167.4-675.36-128.79-1.0823-138.17-122.3 0.7216z" />
          <path d="m2200.7 845.46 632.73-4.9004-140.82 321.68-495.92 1.0204z" />
          <path d="m2187.9 846.18-1.0823 317.12-476.47 2.7725-1.2347-318.35z" />
          <path d="m385.37 1615.7 496.39-2.8284 14.738 430.59-484.5 4z" />
          <path d="m663.97 283.37 32.652 666.25-584.9 2.8478-19.445-283.9 237.23-0.35355-19.445-384.67z" />
          <path d="m1539.4 642.76 651.78 0.53033 4.0659-362.75-660.44 2.2981z" />
          <path d="m1695.4 1811.7 2.125-972.95 1138.5-5.25 167.08-378.12-504.17-178.19-294.86 1.4142-1.4142 369.82-673.17 2.8284-6.364-368.4-847.82-2.1213 48.083 1321.6 165.46-0.7071 6.364 217.79z" />
          <path d="m112.43 963.79 41.366 644.88 561.8-5.3033-19.092-641.7z" />
        </g>
        <g
          className={
            zoomStatus === "ZoomedOut"
              ? "zoomed-out-room origin-[55%_90%]"
              : "zoomed-in-room"
          }
          onClick={() => zoomToRoom(Rooms.Large)}
        >
          <path
            id="large-room"
            stroke="black"
            strokeWidth="3"
            fill={zoomStatus === "ZoomedIn" ? "ivory" : "gray"}
            d="m2963.3 2158.6-31.186-12.946v-51.276l0.7653-63.265-261.99 0.5102 1.5306-111.73-260.71-0.5102v-101.53h-259.18l2.0408-105.1-262.76 1.5306v-76.531h-165.31l-5.102 194.9 49.49 118.37 1.0204 203.06-52.551 159.18 131.12 27.551 909.69 267.35z"
          />
          <text
            x="2110"
            y="2040"
            className={zoomStatus === "ZoomedOut" ? "" : "hidden"}
            fill="white"
            stroke="black"
            strokeWidth="3"
            fontSize="105"
            fontWeight="bolder"
            fontFamily="Arial"
          >
            :
          </text>
          <text
            x="2180"
            y="2045"
            className={zoomStatus === "ZoomedOut" ? "" : "hidden"}
            fill="white"
            stroke="black"
            strokeWidth="3"
            fontSize="115"
            fontWeight="bolder"
            fontFamily="Arial"
          >
            {countAvailableSeats(1, 10)}/10
          </text>

          <text
            x="2110"
            y="2240"
            className={zoomStatus === "ZoomedOut" ? "" : "hidden"}
            fill="white"
            stroke="black"
            strokeWidth="3"
            fontSize="105"
            fontWeight="bolder"
            fontFamily="Arial"
          >
            :
          </text>
          <text
            x="2180"
            y="2245"
            className={zoomStatus === "ZoomedOut" ? "" : "hidden"}
            fill="white"
            stroke="black"
            strokeWidth="3"
            fontSize="115"
            fontWeight="bolder"
            fontFamily="Arial"
          >
            {10 - countAvailableSeats(1, 10)}/10
          </text>

          <g
            className={zoomStatus === "ZoomedOut" ? "" : "hidden"}
            stroke="#000"
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth="5"
          >
            <path
              className="seat-available"
              d="m1982 2069.1v8.8107a26.249 26.249 0 0 1-7.6907 18.566l-5.4332 5.4334h78.745l-5.4335-5.4334a26.249 26.249 0 0 1-7.691-18.566v-8.8107m52.497-104.99v85.308a19.686 19.686 0 0 1-19.686 19.686h-118.12a19.686 19.686 0 0 1-19.686-19.686v-85.308m157.49 0a19.686 19.686 0 0 0-19.686-19.686h-118.12a19.686 19.686 0 0 0-19.686 19.686m157.49 0v59.059a19.686 19.686 0 0 1-19.686 19.686h-118.12a19.686 19.686 0 0 1-19.686-19.686v-59.059"
            />
            <path
              className="seat-booked"
              d="m1982 2269.2v8.8108a26.249 26.249 0 0 1-7.6907 18.566l-5.4332 5.4334h78.745l-5.4335-5.4334a26.249 26.249 0 0 1-7.691-18.566v-8.8108m52.497-104.99v85.308a19.686 19.686 0 0 1-19.686 19.686h-118.12a19.686 19.686 0 0 1-19.686-19.686v-85.308m157.49 0a19.686 19.686 0 0 0-19.686-19.686h-118.12a19.686 19.686 0 0 0-19.686 19.686m157.49 0v59.059a19.686 19.686 0 0 1-19.686 19.686h-118.12a19.686 19.686 0 0 1-19.686-19.686v-59.059"
            />
          </g>
        </g>

        <g
          className={
            zoomStatus === "ZoomedOut"
              ? "zoomed-out-room origin-[85%_20%]"
              : "zoomed-in-room"
          }
          onClick={() => zoomToRoom(Rooms.Small)}
        >
          <path
            id="small-room"
            stroke="black"
            strokeWidth="3"
            fill={zoomStatus === "ZoomedIn" ? "ivory" : "gray"}
            d="m3406.1 1169.4 214.29-481.12-592.86-230.61-315.82 715.82z"
          />
          <text
            x="3160"
            y="750"
            className={zoomStatus === "ZoomedOut" ? "" : "hidden"}
            fill="white"
            stroke="black"
            strokeWidth="3"
            fontSize="105"
            fontWeight="bolder"
            fontFamily="Arial"
          >
            :
          </text>
          <text
            x="3230"
            y="755"
            className={zoomStatus === "ZoomedOut" ? "" : "hidden"}
            fill="white"
            stroke="black"
            strokeWidth="3"
            fontSize="115"
            fontWeight="bolder"
            fontFamily="Arial"
          >
            {countAvailableSeats(11, 15)}/5
          </text>

          <text
            x="3160"
            y="950"
            className={zoomStatus === "ZoomedOut" ? "" : "hidden"}
            fill="white"
            stroke="black"
            strokeWidth="3"
            fontSize="105"
            fontWeight="bolder"
            fontFamily="Arial"
          >
            :
          </text>
          <text
            x="3230"
            y="955"
            className={zoomStatus === "ZoomedOut" ? "" : "hidden"}
            fill="white"
            stroke="black"
            strokeWidth="3"
            fontSize="115"
            fontWeight="bolder"
            fontFamily="Arial"
          >
            {5 - countAvailableSeats(11, 15)}/5
          </text>
          <g
            className={zoomStatus === "ZoomedOut" ? "" : "hidden"}
            stroke="#000"
            strokeLinecap="round"
            strokeLinejoin="round"
            strokeWidth="5"
          >
            <path
              className="seat-available"
              d="m3037.7 769.2v8.8108a26.249 26.249 0 0 1-7.6907 18.566l-5.4332 5.4334h78.745l-5.4335-5.4334a26.249 26.249 0 0 1-7.691-18.566v-8.8108m52.497-104.99v85.308a19.686 19.686 0 0 1-19.686 19.686h-118.12a19.686 19.686 0 0 1-19.686-19.686v-85.308m157.49 0a19.686 19.686 0 0 0-19.686-19.686h-118.12a19.686 19.686 0 0 0-19.686 19.686m157.49 0v59.059a19.686 19.686 0 0 1-19.686 19.686h-118.12a19.686 19.686 0 0 1-19.686-19.686v-59.059"
            />
            <path
              className="seat-booked"
              d="m3037.7 969.1v8.8108a26.249 26.249 0 0 1-7.6907 18.566l-5.4332 5.4334h78.745l-5.4335-5.4334a26.249 26.249 0 0 1-7.691-18.566v-8.8108m52.497-104.99v85.308a19.686 19.686 0 0 1-19.686 19.686h-118.12a19.686 19.686 0 0 1-19.686-19.686v-85.308m157.49 0a19.686 19.686 0 0 0-19.686-19.686h-118.12a19.686 19.686 0 0 0-19.686 19.686m157.49 0v59.059a19.686 19.686 0 0 1-19.686 19.686h-118.12a19.686 19.686 0 0 1-19.686-19.686v-59.059"
            />
          </g>
        </g>

        {/* Draft for sales */}
        {/* <g
          className={
            zoomStatus === "ZoomedOut"
              ? "zoomed-out-room origin-[5%_45%]"
              : "zoomed-in-room"
          }
          opacity="1.0"
        >
          <path
            id="sales"
            stroke="black"
            strokeWidth="3"
            fill="gray"
            d="m112.43 963.79 41.366 644.88 561.8-5.3033-19.092-641.7z"
            onClick={() => zoomToRoom(Rooms.Sales)}
          />
          <text
            x="300"
            y="1100"
            className={zoomStatus === "ZoomedOut" ? "" : "hidden"}
            fill="white"
            fontSize="65"
            fontWeight="bolder"
            fontFamily="Arial"
          >
            Sales
          </text>
          <text
            x="220"
            y="1180"
            className={zoomStatus === "ZoomedOut" ? "" : "hidden"}
            fill="white"
            fontSize="55"
            fontWeight="bolder"
            fontFamily="Arial"
          >
            Available Seats:
          </text>
        </g> */}

        {/* SMALL WORK ROOM */}
        <WorkRoom
          className={zoomStatus === "ZoomedIn" ? undefined : "hidden"}
          seats={seatsSmallRoom}
          seatClicked={seatClicked}
          getSeatClassName={getSeatClassName}
        />
        {/* LARGE WORK ROOM */}
        <WorkRoom
          className={zoomStatus === "ZoomedIn" ? undefined : "hidden"}
          seats={seatsLargeRoom}
          seatClicked={seatClicked}
          getSeatClassName={getSeatClassName}
        />
      </svg>

      <button
        className={`absolute top-0 right-0 m-2 p-2 bg-gray-200 text-black rounded hover:bg-gray-300 text-s ${
          zoomStatus === "ZoomedIn" ? "" : "opacity-50 cursor-not-allowed"
        }`}
        onClick={() => zoomOut()}
      >
        <ZoomOutIcon class="h-6 w-6 inline-block mr-1" />
      </button>
    </div>
  );
};
