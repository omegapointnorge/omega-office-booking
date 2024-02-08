import React from 'react';
import bookingStore from '@stores/BookingStore';

const OverviewEventModeButton = () => {

  const eventDate = bookingStore.eventDate ? new Date(bookingStore.eventDate) : null;
  const eventCreationDateText = eventDate ? eventDate.toLocaleDateString('no-NO', { day: 'numeric', month: 'long', year: 'numeric' }) : "";

  return (
    <div className="flex flex-col items-center">
      {eventCreationDateText &&
        <span>{`Oppretter arrangement for ${eventCreationDateText}`}</span>
      }
      <button
        className="px-4 py-2 text-sm font-medium rounded-lg bg-blue-500 text-white hover:bg-blue-600"
        onClick={() => bookingStore.toggleEventMode()} 
      >
        {bookingStore.bookEventMode ? "Avbryt oppretting av nytt arrangement" : "Opprett arrangement"}
      </button>
    </div>
  );
}

export default OverviewEventModeButton;
