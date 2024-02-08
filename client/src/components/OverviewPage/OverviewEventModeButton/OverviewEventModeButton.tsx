import React from 'react';
import bookingStore from '@stores/BookingStore';

const OverviewEventModeButton = () => {
  let text;

  if (bookingStore.isEventDateChosen) {
    text = `Oppretter arrangement for ${new Date(bookingStore.displayDate).toLocaleDateString('no-NO', { day: 'numeric', month: 'long', year: 'numeric', hour: '2-digit', minute: '2-digit' })}`;
  } else {
    text = `Viser seter for ${new Date(bookingStore.displayDate).toLocaleDateString('no-NO', { day: 'numeric', month: 'long', year: 'numeric', hour: '2-digit', minute: '2-digit' })}`;
  }

  return (
    <div className="flex flex-col items-center">
      <span>{text}</span>
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
