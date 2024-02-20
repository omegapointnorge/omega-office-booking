import React from "react";
import { observer } from "mobx-react-lite";
import bookingStore from "@stores/BookingStore";

export const EventModeButton = observer(() => {
  const handleBook = async () => {
    await bookingStore.createBookingForEvent(
      bookingStore.seatIdSelectedForNewEvent
    );
  };

  const formatDate = (date: Date) => {
    return new Date(date).toLocaleDateString("no-NO", {
      day: "numeric",
      month: "long",
      year: "numeric",
      hour: "2-digit",
      minute: "2-digit",
    });
  };

  let text = bookingStore.isEventDateChosen
    ? `Oppretter arrangement for ${formatDate(bookingStore.displayDate)}`
    : `Viser seter for ${formatDate(bookingStore.displayDate)}`;

  const seatCount = bookingStore.seatIdSelectedForNewEvent.length;
  const pluralSuffix = seatCount === 1 ? "" : "er";

  return (
    <div className="flex flex-col items-center">
      <span>{text}</span>
      <div className="flex flex-row">
        {bookingStore.bookEventMode ? (
          <button
            className="px-4 py-2 mx-1 mt-1 bg-blue-500 text-white text-sm font-medium rounded-md shaddow-sm hover:bg-blue-600"
            onClick={() => bookingStore.toggleEventMode()}
          >
            Avbryt oppretting av nytt arrangement
          </button>
        ) : (
          <button
            className="px-4 py-2 mt-1 text-sm font-medium rounded-md bg-blue-500 text-white hover:bg-blue-600"
            onClick={() => bookingStore.toggleEventMode()}
          >
            Opprett arrangement
          </button>
        )}
        {seatCount !== 0 && (
          <button
            className="basis-1/2 px-2 py-1 mx-1 mt-1 bg-green-600 text-white text-sm font-medium rounded-md shadow-sm hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-green-500 focus:ring-offset-2"
            onClick={handleBook}
          >
            Reserver {seatCount} plass{pluralSuffix}
          </button>
        )}
      </div>
    </div>
  );
});
