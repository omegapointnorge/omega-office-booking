import React from "react";

import { Rooms, ZoomStatus } from "@/shared/types/enums";
import { ZoomOutIcon } from "../../../shared/assets/icons/zoom-out_outline";
import { WorkRoom } from "./WorkRoom";
import { seatsEcon, seatsHr as seatsMarie, seatsLargeRoom, seatsOystein, seatsSales, seatsSmallRoom } from "./SeatPaths";
import { Booking } from "@/shared/types/entities";
import { isSameDate } from "@utils/utils";
import { ClickableRoom } from "@components/OverviewPage/Map/ClickableRoom";
import { smallRoomConfig, largeRoomConfig, salesRoomConfig, economyRoomConfig, oysteinRoomConfig, marieRoomConfig } from "@components/OverviewPage/Map/RoomConfig";
import bookingStore from "@stores/BookingStore";

interface MapProps {
  currentViewBox: string;
  zoomStatus: ZoomStatus;
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
    let isSeatAvailable = !bookingStore.unavailableSeatsIds.includes(seatId);
    if (!isSeatAvailable) {
      continue;
    }

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
}

  return (
    <div className="relative">
      <svg
        version="1.1"
        width="100%"
        height="500"
        viewBox={currentViewBox}
        xmlns="http://www.w3.org/2000/svg"
      >
        <g opacity="1.0" stroke="black" strokeWidth="3" fill="none">
          <path d="m897.96 1834.7 683.29-5.5963 38.242 3.6077 42.571 14.792 36.077 23.811 23.089 24.532 7.2154 9.38 9.7407 18.038 11.905 29.222 4.69 18.399 1.4431 172.81-51.229 167.4-675.36-128.79-1.0823-138.17-122.3 0.7216z" />
          <path d="m2602.1 842.47 232.7-1.4364-142.21 321.76h-96.242z"/>  
          <path d="m385.37 1615.7 496.39-2.8284 14.738 430.59-484.5 4z" />
          <path d="m663.97 283.37 32.652 666.25-584.9 2.8478-19.445-283.9 237.23-0.35355-19.445-384.67z" />
          <path d="m1539.4 642.76 651.78 0.53033 4.0659-362.75-660.44 2.2981z" />
          <path d="m1695.4 1811.7 2.125-972.95 1138.5-5.25 167.08-378.12-504.17-178.19-294.86 1.4142-1.4142 369.82-673.17 2.8284-6.364-368.4-847.82-2.1213 48.083 1321.6 165.46-0.7071 6.364 217.79z" />
          <path d={marieRoomConfig.roomShapePath} />
        </g>

        <g className={zoomStatus === ZoomStatus.ZoomedOut ? `zoomed-out-room origin-[55%_90%]` : 'zoomed-in' } onClick={() => zoomToRoom(Rooms.Large)}>
          <ClickableRoom zoomStatus={zoomStatus} countAvailableSeats={countAvailableSeats} roomConfig={largeRoomConfig}/>
        </g>
        <g className={zoomStatus === ZoomStatus.ZoomedOut ? `zoomed-out-room origin-[85%_20%]` : 'zoomed-in' } onClick={() => zoomToRoom(Rooms.Small)}>
          <ClickableRoom zoomStatus={zoomStatus} countAvailableSeats={countAvailableSeats} roomConfig={smallRoomConfig}/>
        </g>
        <g className={zoomStatus === ZoomStatus.ZoomedOut ? `zoomed-out-room origin-[5%_45%]` : 'zoomed-in' } onClick={() => zoomToRoom(Rooms.Sales)}>
          <ClickableRoom zoomStatus={zoomStatus} countAvailableSeats={countAvailableSeats} roomConfig={salesRoomConfig}/>
        </g>

        <g className={zoomStatus === ZoomStatus.ZoomedOut ? `zoomed-out-room origin-[64%_35%]` : 'zoomed-in' } onClick={() => zoomToRoom(Rooms.Oystein)}>
          <ClickableRoom zoomStatus={zoomStatus} countAvailableSeats={countAvailableSeats} roomConfig={oysteinRoomConfig}/>
        </g>
        {/* <g className={zoomStatus === ZoomStatus.ZoomedOut ? `zoomed-out-room origin-[60%_35%]` : 'zoomed-in' } onClick={() => zoomToRoom(Rooms.Marie)}>
          <ClickableRoom zoomStatus={zoomStatus} countAvailableSeats={countAvailableSeats} roomConfig={hrRoomConfig}/>
        </g> */}
        <g className={zoomStatus === ZoomStatus.ZoomedOut ? `zoomed-out-room origin-[62%_35%]` : 'zoomed-in' } onClick={() => zoomToRoom(Rooms.Econ)}>
          <ClickableRoom zoomStatus={zoomStatus} countAvailableSeats={countAvailableSeats} roomConfig={economyRoomConfig}/>
        </g>

        {/* SALES WORK ROOM */}
        <WorkRoom
          className={zoomStatus === ZoomStatus.Sales ? undefined : "hidden"}
          seats={seatsSales}
          seatClicked={seatClicked}
          getSeatClassName={getSeatClassName}
        />

        {/* SMALL WORK ROOM */}
        <WorkRoom
          className={zoomStatus === ZoomStatus.Small ? undefined : "hidden"}
          seats={seatsSmallRoom}
          seatClicked={seatClicked}
          getSeatClassName={getSeatClassName}
        />
        {/* LARGE WORK ROOM */}
        <WorkRoom
          className={zoomStatus === ZoomStatus.Large ? undefined : "hidden"}
          seats={seatsLargeRoom}
          seatClicked={seatClicked}
          getSeatClassName={getSeatClassName}
        />
        <WorkRoom
          className={zoomStatus === ZoomStatus.Marie ? undefined : "hidden"}
          seats={seatsMarie}
          seatClicked={seatClicked}
          getSeatClassName={getSeatClassName}
        />
        <WorkRoom
          className={zoomStatus === ZoomStatus.EconOystein ? undefined : "hidden"}
          seats={seatsEcon}
          seatClicked={seatClicked}
          getSeatClassName={getSeatClassName}
        />
        <WorkRoom
          className={zoomStatus === ZoomStatus.EconOystein ? undefined : "hidden"}
          seats={seatsOystein}
          seatClicked={seatClicked}
          getSeatClassName={getSeatClassName}
        />
      </svg>

      <button
        className={`absolute top-0 right-0 m-2 p-2 bg-gray-200 text-black rounded hover:bg-gray-300 text-s ${
          zoomStatus !== ZoomStatus.ZoomedOut ? "" : "opacity-50 cursor-not-allowed"
        }`}
        onClick={() => zoomOut()}
      >
        <ZoomOutIcon class="h-6 w-6 inline-block mr-1" />
      </button>
    </div>
  );
};
