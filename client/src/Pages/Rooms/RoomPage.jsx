import Heading from "../../components/Heading";
import React, { useEffect } from "react";
import SeatItem from "../../components/Seat/SeatItem";
import { observer } from "mobx-react-lite";
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
      <div className="flex justify-center items-center fixed inset-0 outline-none focus:outline-none bg-warmgray25">
        <div className="flex flex-col gap-10">
          <Heading title="Ingen seter å vise!" />
        </div>
      </div>
    );
  }

  const columnCount = 2;
  const rowCount = [3, 2];

  const generateSeatsInBlocks = () => {
    const seatsInBlocks = [];

    roomStore.seats.forEach((seat, seatIndex) => {
      const blockIndex = Math.floor(seatIndex / (rowCount[0] * columnCount));
      const columnIndex = Math.floor(
        (seatIndex % (rowCount[blockIndex] * columnCount)) /
          rowCount[blockIndex]
      );
      const rowIndex = seatIndex % rowCount[blockIndex];

      seatsInBlocks[blockIndex] ??= [];
      seatsInBlocks[blockIndex][columnIndex] ??= [];
      seatsInBlocks[blockIndex][columnIndex][rowIndex] = seat;
    });

    return seatsInBlocks;
  };

  const renderSeatItem = (seat) => (
    <div key={seat.id} className="seat">
      <SeatItem
        name={seat.name}
        seatNr={seat.id}
        isTaken={seat.isTaken}
        onClick={() => {
          roomStore.handleOpenDialog();
            roomStore.bookSeat(new UserBookingRequest(seat.id));
        }}
      />
    </div>
  );

  const renderColumn = (column, columnIndex) => (
    <div key={columnIndex} className="column">
      {column.map(renderSeatItem)}
    </div>
  );

  const renderBlock = (block, blockIndex) => (
    <div key={blockIndex} className="block">
      {block.map(renderColumn)}
    </div>
  );

  const seatsInBlocks = generateSeatsInBlocks();

  return (
    <div className="flex flex-col md:flex-row sm:flex-row flex-grow overflow-y-auto overflow-x-auto px-4 md:px-20 py-4 md:py-20 pt-12 md:pt-24 w-full justify-center items-top">
      {seatsInBlocks.map(renderBlock)}
    </div>
  );
});

class UserBookingRequest {
  constructor(seatId) {
    this.SeatId = seatId;
  }
}

export default RoomPage;
