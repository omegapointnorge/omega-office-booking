import React from "react";
import { RoomConfig } from "@/shared/types/entities";
import { ZoomStatus } from "@/shared/types/enums";


interface ClickableRoomProps {
  roomConfig: RoomConfig;
  zoomStatus: ZoomStatus;
  countAvailableSeats: (minSeatId: number, maxSeatId: number) => number;
}


export const ClickableRoom = ({ roomConfig, zoomStatus, countAvailableSeats }: ClickableRoomProps) => {

    const getMonitorPathClassName = (): string => {
        if (isAnySeatInRoomAvailable()) {
            return "seat-available"
        }
        return "seat-booked"
    }

    const getAvailabilityText = (): JSX.Element => {
        if (roomConfig.availabilityTextCoordinates === undefined) {
            return <></>;
        }
        return (
            <>
                <text
                x={roomConfig.availabilityTextCoordinates.x}
                y={roomConfig.availabilityTextCoordinates.y}
                className={zoomStatus === ZoomStatus.ZoomedOut ? "" : "hidden"}
                fill="white"
                stroke="black"
                strokeWidth="3"
                fontSize={roomConfig.fontSize}
                fontWeight="bolder"
                fontFamily="Arial"
                >
                :
                </text>
                <text
                x={roomConfig.availabilityTextCoordinates.x + 70}
                y={roomConfig.availabilityTextCoordinates.y + 5}
                className={zoomStatus === ZoomStatus.ZoomedOut ? "" : "hidden"}
                fill="white"
                stroke="black"
                strokeWidth="3"
                fontSize={roomConfig.fontSize + 10}
                fontWeight="bolder"
                fontFamily="Arial"
                >
                {countAvailableSeats(roomConfig.seatMinId, roomConfig.seatMaxId)}/{roomConfig.seatCount}
                </text>
            </>
        )
    } 

    const isAnySeatInRoomAvailable = (): boolean => {
        const availableSeats = countAvailableSeats(roomConfig.seatMinId, roomConfig.seatMaxId)
        if (availableSeats === 0) {
            return false;
        }
        return true;
    }

    return(
        <>
            <path className={zoomStatus !== roomConfig.zoomedInStatus ? "cursor-pointer" : ""} stroke="black" strokeWidth="3" fill={zoomStatus === roomConfig.zoomedInStatus ? "ivory" : "gray"} d={roomConfig.roomShapePath}/>
            {getAvailabilityText()}
            <g className={zoomStatus === ZoomStatus.ZoomedOut ? "" : "hidden"} stroke="#000" strokeLinecap="round" strokeLinejoin="round" strokeWidth="5">
                <path className={getMonitorPathClassName()} style={{ pointerEvents: 'none' }} d={roomConfig.roomMonitorPath}/>
            </g>
        </>
);
}




