import Heading from "../../components/Heading";
import {seats} from "../../data/seats";
import {MenuItem} from "@mui/material";
import React from "react";
import BookingItem from "../../components/Bookings/BookingItem";
import SeatItem from "../../components/Seat/SeatItem";
const BigRoomPage = () => {
    
    if(seats.length === 0){
        return <div>
            <Heading title="No seats to display"/>
        </div>
    }
    return (
        <>
            
            <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 outline-none focus:outline-none" >
                
                <div className="flex flex-row gap-4 flex-wrap">
                    {seats.map((seat) =>
                        (<SeatItem key={seat.id}
                                      name={seat.name}
                                      seatNr={seat.seatId}
                                      isTaken={seat.isTaken}
                                      roomName="Store rommet"

                        >
                        </SeatItem>))}
                </div>
            </div>
        </>)
}

export default BigRoomPage;