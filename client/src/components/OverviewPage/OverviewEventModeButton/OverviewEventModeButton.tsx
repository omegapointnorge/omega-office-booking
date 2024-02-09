import React from 'react';
import { observer } from 'mobx-react-lite';
import bookingStore from '@stores/BookingStore';

const OverviewEventModeButton = () => {

  const handleBook = async () => {
    await bookingStore.createBookingForEvent(bookingStore.seatIdSelectedForNewEvent);
  };

  const formatDate = (date : Date) => {
    return new Date(date).toLocaleDateString('no-NO', {
      day: 'numeric',
      month: 'long',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  };

   let text = bookingStore.isEventDateChosen
    ? `Oppretter arrangement for ${formatDate(bookingStore.displayDate)}`
    : `Viser seter for ${formatDate(bookingStore.displayDate)}`;

    const seatCount = bookingStore.seatIdSelectedForNewEvent.length;
    const pluralSuffix = seatCount === 1 ? '' : 'er'; 

  return (
    <div className="flex flex-col items-center">
      <span>{text}</span>
      <button
        className="px-4 py-2 mt-2 text-sm font-medium rounded-lg bg-blue-500 text-white hover:bg-blue-600"
        onClick={() => bookingStore.toggleEventMode()} 
      >
        {bookingStore.bookEventMode ? "Avbryt oppretting av nytt arrangement" : "Opprett arrangement"}
      </button>
      {seatCount !== 0 && (
        <button
          className="px-4 py-2 mt-2 text-sm font-medium rounded-lg bg-blue-500 text-white hover:bg-blue-600"
          onClick={handleBook} 
        >
          Reserver {seatCount} plass{pluralSuffix}
        </button>
      )}
    </div>
  );
};

export default observer(OverviewEventModeButton);
