import React from 'react';
import bookingStore from '@stores/BookingStore';
import { Calendar, DateObject } from 'react-multi-date-picker';

const OverviewEventModeButton = () => {
  const today = new Date();
  const { displayDate } = bookingStore;

  const dateObjectToDate = (dateObject: DateObject) => {
    return new Date(dateObject.year, dateObject.month.number - 1, dateObject.day);
  };

  return (
    <div className="flex flex-col items-center">
      <button
        className="px-4 py-2 text-sm font-medium rounded-lg bg-blue-500 text-white hover:bg-blue-600"
        onClick={() => bookingStore.toggleEventMode()} 
      >
        {bookingStore.bookEventMode ? "Cancel new event booking" : "Book event"}
      </button>
      {bookingStore.bookEventMode && (
        <Calendar
          value={displayDate}
          onChange={(newDate: DateObject) => bookingStore.setDisplayDate(dateObjectToDate(newDate))}
          minDate={today}
          multiple={false}
        />
      )}
    </div>
  );
}

export default OverviewEventModeButton;
