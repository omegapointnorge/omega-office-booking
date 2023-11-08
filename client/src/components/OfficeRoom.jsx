import React from "react";
import Seat from "./seat";

function OfficeRoom(props) {
    const {seatsData} = props;
    return (
        <div>
            <div className="text-center text-lg text-blue-800 font-extrabold">
                <p>Test2</p>
            </div>
            <div className="flex-wrap">
                <p>Seats</p>
                {seatsData.map
                (
                    (seat, index) => 
                      (<Seat key={index} seatNumber={seat.seatNumber} isAvailable={seat.isAvailable} />)
                )
                }
            </div>
        </div>
    );
};

/*
const OfficeRoom = () => {
    return (
        <div className="text-center text-lg text-blue-800 font-extrabold">
            <p>Hello world, I am the template for OfficeRoom</p>
            <h2>Office Room</h2>
            <div className="flex flex-wrap">
                {seatsData.map((seat, index) => (
                    <Seat key={index} seatNumber={seat.seatNumber} isAvailable={seat.isAvailable} />
                ))}
            </div>

        </div>
    );
};
*/
export default OfficeRoom;