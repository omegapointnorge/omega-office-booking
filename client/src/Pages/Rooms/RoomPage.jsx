import Heading from "../../components/Heading";
import React, { useEffect } from "react";
import SeatItem from "../../components/Seat/SeatItem";
import { observer } from "mobx-react-lite";
import { useLocation } from "react-router-dom";
import roomStore from "../../stores/RoomStore";
import Loading from "../../components/Loading";
import PrimaryModal from "../../components/Modals/PrimaryModal";
import BookingModal from "../../components/Modals/BookingModal";

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
        <div className="flex flex-row gap-4 items-center flex-wrap">
          {roomStore.seats.map((seat) => (
            <div className="sm:px-8 md:pl-24">
              <SeatItem
                key={seat.id}
                name={seat.name}
                seatNr={seat.seatId}
                isTaken={seat.isTaken}
                roomName="Store rommet"
                onClick={() => {
                  roomStore.setSelectedSeat(seat);
                  roomStore.handleOpenDialog();
                }}
              ></SeatItem>
            </div>
          ))}
          <BookingModal
            title="Book Sete"
            content="Er du sikker på at du vil booke denne plassen? Bookingen din vil være final, og du har frem til klokken 22:00 å avbooke plassen din. Hvis du ikke gjør det, så vil du straffes hardt med en plass på wall of shame muahahah"
            open={roomStore.openDialog}
            isTaken={roomStore.selectedSeat.isTaken}
            positiveAction={() => {
              roomStore.bookSeat();
              roomStore.handleCloseDialog();
            }}
            negativeAction={roomStore.handleCloseDialog}
          />
        </div>
      </div>
    </>
  );
});

export default RoomPage;
