import Heading from "../../components/Heading";
import React, { useEffect } from "react";
import SeatItem from "../../components/Seat/SeatItem";
import { observer } from "mobx-react-lite";
import MyDialog from "../../components/Dialog";
import { useLocation } from "react-router-dom";
import roomStore from "../../stores/RoomStore";
import Loading from "../../components/Loading";

const RoomPage = observer(() => {
  const location = useLocation();

  useEffect(() => {
    roomStore.initializeRooms(location.state.id);
  }, [location]);

  if (roomStore.isLoading) {
    return (
      <>
        <Loading />
      </>
    );
  }

  if (roomStore.seats.length === 0) {
    return (
      <>
        <div
          className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 outline-none focus:outline-none"
        >
          <div className="flex flex-col gap-10">
            <Heading title="Ingen seter å vise!" />
          </div>
        </div>
      </>
    );
  }

  return (
    <>
      <div
        className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 outline-none focus:outline-none"
      >
        <div className="flex flex-row gap-4 flex-wrap">
          {roomStore.seats.map((seat) => (
            <div className="sm:px-8 ">
              <SeatItem
                key={seat.id}
                name={seat.name}
                seatNr={seat.seatId}
                isTaken={seat.isTaken}
                roomName="Store rommet"
                onClick={() => {
                  roomStore.handleOpenDialog();
                  roomStore.bookSeat(seat.id, !seat.isTaken);
                }}
              ></SeatItem>
            </div>
          ))}
          <MyDialog
            open={roomStore.openDialog}
            handleClose={roomStore.handleCloseDialog}
          />
        </div>
      </div>
    </>
  );
});

export default RoomPage;
