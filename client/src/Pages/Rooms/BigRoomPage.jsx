import Heading from "../../components/Heading";
import React, {useState} from "react";
import SeatItem from "../../components/Seat/SeatItem";
import {observer} from "mobx-react-lite";
import {useStores} from "../../stores";
import MyDialog from "../../components/Dialog";

const BigRoomPage = observer(() => {
    const {roomStore} = useStores();
    /*const handleCloseDialog = () => {
        setOpenDialog(false);
    };*/
    
    if(roomStore.seats.length === 0){
        return <div>
            <Heading title="No seats to display"/>
        </div>
    }
    return (
        <>
            
            <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 outline-none focus:outline-none" >
                
                <div className="flex flex-row gap-4 flex-wrap">
                    {roomStore.seats.map((seat) =>
                        (<SeatItem key={seat.id}
                                      name={seat.name}
                                      seatNr={seat.seatId}
                                      isTaken={seat.isTaken}
                                      roomName="Store rommet"
                                   onClick={() => {
                                       roomStore.handleOpenDialog();
                                       roomStore.bookSeat(seat.id, !seat.isTaken);
                                   }}

                        >
                        </SeatItem>))}
                    <MyDialog open={roomStore.openDialog} handleClose={roomStore.handleCloseDialog}  />
                </div>
            </div>
        </>)
})

export default BigRoomPage;