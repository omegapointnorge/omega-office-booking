import React from 'react';
import { observer } from 'mobx-react-lite';
import bookingStore from '../../stores/BookingStore';

const SwitchButton = observer(() => {
    const { displayDate } = bookingStore

    const today = new Date();
    const tomorrow = new Date(today.getFullYear(), today.getMonth(), today.getDate() + 1);


    const buttonClicked = (dateChangeFactor) => {
        const newDate = new Date(today);
        newDate.setDate(newDate.getDate() + dateChangeFactor);
        bookingStore.setDisplayDate(newDate);
    }
    

    const isSameDate = (date1, date2) => {
        if (!(date1 instanceof Date) || !(date2 instanceof Date)) {
          throw new Error('Both arguments must be Date objects.');
      }
  
      return  date1.getDate() === date2.getDate() &&
              date1.getFullYear() === date2.getFullYear() &&
              date1.getMonth() === date2.getMonth();
      }

  return (
    <div className="flex items-center justify-center py-4">
      <button
        className={`px-4 py-2 text-sm font-medium rounded-l-lg ${
        isSameDate(displayDate, today) ? 'bg-blue-500 text-white' : 'bg-gray-200'
        }`}
        onClick={() => buttonClicked(0)}
      >
        I dag
      </button>
      <button
        className={`px-4 py-2 text-sm font-medium rounded-r-lg ${
        isSameDate(displayDate, tomorrow) ? 'bg-blue-500 text-white' : 'bg-gray-200'
        }`}
        onClick={() => buttonClicked(1)}
      >
        I morgen
      </button>
    </div>
  );
});

export default SwitchButton;
