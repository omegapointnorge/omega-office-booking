import React from "react"
import Seat from "./seat";


const OfficeRoom = () => {
    return (
        <div className="text-center text-lg text-blue-800 font-extrabold">
                <p>Hello world, I am the template for OfficeRoom</p>
                <h2>Office Room</h2>
                <div className="flex flex-wrap">
                    {seatsData.map((seat, index) =>( 
                    <Seat key={index} seatNumber={seat.seatNumber} isAvailable={seat.isAvailable} />
                    ))}
                </div>

        </div>
    );
};

export default OfficeRoom;