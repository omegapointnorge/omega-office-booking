import React from "react";
import OfficeRoom from "../../components/OfficeRoom";
import Seat from "../../components/seat";

export default function BigRoom() {
    
    const bigRoomConfig = [
        {
            seatNumber: 1,
            isAvailable: true
        },
        {
            seatNumber: 2,
            isAvailable: false
        },
        {
            seatNumber: 3,
            isAvailable: true
        },
        {
            seatNumber: 4,
            isAvailable: true
        },
        {
            seatNumber: 5,
            isAvailable: true
        },
    ];


    return (
        <div>
            <h1>Hello and welcome to the big office room</h1>
            <OfficeRoom seatData={bigRoomConfig}>

            </OfficeRoom>
        </div>
    )
}