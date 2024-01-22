import React from 'react';
import { observer } from 'mobx-react-lite';
import bookingStore from '../../stores/BookingStore';

const SwitchButton = observer(() => {
    const { displayDate } = bookingStore

    const today = new Date();

    const getNextWorkday = () => {
      const nextDay = new Date();
      nextDay.setDate(nextDay.getDate() + 1);

      while (nextDay.getDay() === 0 || nextDay.getDay() === 6) {
          nextDay.setDate(nextDay.getDate() + 1);
      }
    
      return nextDay;
  }

  const nextWorkday = getNextWorkday()

  const formatDate = (date) => {
    const days = ["Søndag", "Mandag", "Tirsdag", "Onsdag", "Torsdag", "Fredag", "Lørdag"];
    
    const dayOfWeek = days[date.getDay()];
    const dayOfMonth = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0'); // +1 because getMonth() returns 0-11

    return (
      <span>
        <strong>{dayOfWeek}</strong> <span style={{ fontSize: 'smaller' }}>({dayOfMonth}.{month})</span>
      </span>
    );
}


    const handleDateChange = (datePressed) => {

      if (datePressed === 'today') {
        bookingStore.setDisplayDate(today) 
      }
      else {
        bookingStore.setDisplayDate(nextWorkday)
      }
    }    

    const isSameDate = (date1, date2) => {
      if (!(date1 instanceof Date) || !(date2 instanceof Date)) {
        throw new Error('Both arguments must be Date objects.');
    }

    return date1.toDateString() === date2.toDateString();
    } 

  return (
    <div className="flex items-center justify-center py-4">
      <button
        className={`px-4 py-2 text-sm font-medium rounded-l-lg ${
        isSameDate(displayDate, today) ? 'bg-blue-500 text-white' : 'bg-gray-200'
        }`}
        onClick={() => handleDateChange('today')}
      >
      {formatDate(today)}
      </button>
      <button
        className={`px-4 py-2 text-sm font-medium rounded-r-lg ${
        isSameDate(displayDate, nextWorkday) ? 'bg-blue-500 text-white' : 'bg-gray-200'
        }`}
        onClick={() => handleDateChange('nextWorkDay')}
      >
        {formatDate(nextWorkday)}
      </button>
    </div>
  );
});

export default SwitchButton;
