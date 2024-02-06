import React from "react";

import Heading from "@common-components/Heading";
import { observer } from "mobx-react-lite";
import BookingItem from "./HistoryBookingItem/HistoryBookingItem";
import historyStore from "@stores/HistoryStore";
import PrimaryDialog from "@common-components/Dialog";
import { IoIosArrowBack, IoIosArrowForward } from "react-icons/io";
import Loading from "@common-components/Loading";

const ActiveBookings = observer(() => (
  <div className="flex flex-row gap-5">
    <button
      onClick={() => historyStore.navigatePrevious()}
      className="opacity-0"
      disabled={true}
    >
      <IoIosArrowBack />
    </button>
    {historyStore.myActiveBookings.map((booking) => (
      <BookingItem
        key={booking.id}
        seatId={booking.seatId}
        bookingDateTime={booking.bookingDateTime}
        showDeleteButton={true}
        roomId={historyStore.getRoomIdBySeatId(booking.seatId)}
        onClick={() => {
          historyStore.handleOpenDialog(booking.id);
        }}
      ></BookingItem>
    ))}
    <button
      onClick={() => historyStore.navigateNext()}
      className="opacity-0"
      disabled={true}
    >
      <IoIosArrowForward />
    </button>

    <PrimaryDialog
      title="Slett reservasjon?"
      open={historyStore.openDialog}
      handleClose={historyStore.handleCloseDialog}
      onClick={() => {
        historyStore.bookingIdToDelete &&
          historyStore.deleteBooking(historyStore.bookingIdToDelete);
        historyStore.handleCloseDialog();
      }}
    />
  </div>
));

const PreviousBookings = observer(() => (
  <div className="flex flex-row gap-5">
    <button
      onClick={() => historyStore.navigatePrevious()}
      className={`${historyStore.isFirstPage ? "opacity-0" : "opacity-100"}`}
      disabled={historyStore.isFirstPage}
    >
      <IoIosArrowBack />
    </button>
    {historyStore.myPreviousBookingsCurrentPage.map((booking) => (
      <BookingItem
        key={booking.id}
        seatId={booking.seatId}
        bookingDateTime={booking.bookingDateTime}
        showDeleteButton={false}
        roomId={historyStore.getRoomIdBySeatId(booking.seatId)}
      ></BookingItem>
    ))}
    <button
      onClick={() => historyStore.navigateNext()}
      className={`${historyStore.isLastPage ? "opacity-0" : "opacity-100"}`}
      disabled={historyStore.isLastPage}
    >
      <IoIosArrowForward />
    </button>
  </div>
));

const HistoryPage = observer(() => {
  if (historyStore.isLoading) {
    return <Loading />;
  }

  if (historyStore.isEmpty) {
    return (
      <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto inset-0 outline-none focus:outline-none">
        <Heading title="Du har ingen reservasjoner" />
      </div>
    );
  }

  return (
    <div className="justify-center items-center flex flex-col inset-0">
      <Heading title="Dine reservasjoner" />
      <div className="container mt-3">
        <div className="flex flex-col gap-4 mb-10">
          <p className="text-left text-xl font-semibold heading mb-3 pl-11">
            AKTIVE RESERVASJONER
          </p>
          <ActiveBookings />
        </div>
        <div className="flex flex-col gap-4">
          <p className="text-left text-xl font-semibold heading mb-3 pl-11">
            TIDLIGERE RESERVASJONER
          </p>
          <PreviousBookings />
        </div>
      </div>
    </div>
  );
});

export default HistoryPage;
