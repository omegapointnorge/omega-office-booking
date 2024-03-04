import React, { useEffect } from "react";

import { Heading } from "@common-components/Heading";
import { observer } from "mobx-react-lite";
import { BookingItem } from "./BookingItem";
import historyStore from "@stores/HistoryStore";
import { PrimaryDialog } from "@common-components/PrimaryDialog";
import { IoIosArrowBack, IoIosArrowForward } from "react-icons/io";
import { Loading } from "@common-components/Loading";
import { ApiStatus } from "@/shared/types/enums";

const ActiveBookings = observer(() => {
  const handleDelete = () => {
    const bookingToDelete = historyStore.myActiveBookings.find(
      (historyBooking) =>
        historyBooking.id === historyStore.historyBookingIdToDelete
    );
    const isEvent = !!bookingToDelete?.eventName;

    if (isEvent) {
      historyStore.deleteEvent(historyStore.historyBookingIdToDelete);
    } else {
      historyStore.deleteBooking(historyStore.historyBookingIdToDelete);
    }

    historyStore.handleCloseDialog();
  };

  return (
    <div className="flex flex-row gap-5">
      <button
        onClick={() => historyStore.navigatePrevious()}
        disabled={true}
      >
        <IoIosArrowBack className="invisible" />
      </button>
      {historyStore.myActiveBookings.map((booking) => (
        <BookingItem
          key={booking.id}
          seatIds={booking.seatIds}
          bookingDateTime={booking.bookingDateTime}
          roomIds={booking.roomIds}
          eventName={booking.eventName}
          onDelete={() => {
            historyStore.handleOpenDialog(booking.id);
          }}
        />
      ))}
      <button
        onClick={() => historyStore.navigateNext()}
        className="opacity-0"
      >
      <IoIosArrowForward className="invisible"/>
      </button>
      {/* //TODO: Ta i bruk samme dialog! */}
      <PrimaryDialog
        title="Slett reservasjon?"
        open={historyStore.openDialog}
        handleClose={historyStore.handleCloseDialog}
        onClick={handleDelete}
      />
    </div>
  );
});

const PreviousBookings = observer(() => (
  <div className="flex flex-row gap-5">
    <button
      onClick={() => historyStore.navigatePrevious()}
      className={`${historyStore.isFirstPage ? "invisible" : ""}`}
      disabled={historyStore.isFirstPage}
    >
      <IoIosArrowBack />
    </button>
    {historyStore.myPreviousBookingsCurrentPage.map((booking) => (
      <BookingItem
        key={booking.id}
        seatIds={booking.seatIds}
        bookingDateTime={booking.bookingDateTime}
        roomIds={booking.roomIds}
        eventName={booking.eventName}
              aria-label="Forrige"

      />
    ))}
    <button
      onClick={() => historyStore.navigateNext()}
      className={`${historyStore.isLastPage ? "invisible" : ""}`}
      disabled={historyStore.isLastPage}
      aria-label="Neste"
    >
      <IoIosArrowForward />
    </button>
  </div>
));

export const HistoryPage = observer(() => {
  useEffect(() => {
    historyStore.initialize();
  }, []);

  if (historyStore.apiStatus === ApiStatus.Pending) {
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
