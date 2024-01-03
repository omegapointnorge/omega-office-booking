import React, {useState, useEffect} from 'react';

const SeatInfoModal = ({ onClose, selectedSeatId, user, activeBookings }) => {

    const today = new Date().toISOString().split('T')[0];
    const userId = user.claims[1].value

    const [bookingDate, setBookingDate] = useState(today);
    const [bookedBy, setBookedBy] = useState('');

    useEffect(() => {
        const foundBooking = activeBookings.find(booking => booking.seatId === selectedSeatId);
        if (foundBooking) {
            setBookedBy(foundBooking.email);
        } else {
            setBookedBy('');
        }
    }, [selectedSeatId, activeBookings]);

    const onDateChange = (dateValue) => {
        console.log(dateValue)
        setBookingDate(dateValue)
    }

    const handleBook = () => {
        // Logic for booking the seat
        console.log("Booking seat:", selectedSeatId);
    };

    const handleDelete = () => {
        // Logic for booking the seat
        console.log("Booking seat:", selectedSeatId);
    };

    const getButtonGroup = () => {
        if (bookedBy && userId !== bookedBy) {
            return (
                <button onClick={onClose} className="mt-5 px-5 py-2 bg-blue-600 text-white text-sm font-medium rounded-md w-full shadow-sm hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2">
                Close
              </button>
            )
        }
        else {
            return (
                <div className="flex justify-between">
                    <button onClick={onClose} className="px-5 py-2 bg-blue-600 text-white text-sm font-medium rounded-md shadow-sm hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2">
                        Close
                    </button>
                    {(userId === bookedBy) &&
                    <button onClick={handleDelete} className="px-5 py-2 bg-red-600 text-white text-sm font-medium rounded-md shadow-sm hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-red-500 focus:ring-offset-2">
                        Slett reservasjon
                    </button>
                    }
                    {(bookedBy === "") &&
                        <button onClick={handleBook} className="px-5 py-2 bg-green-600 text-white text-sm font-medium rounded-md shadow-sm hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-green-500 focus:ring-offset-2">
                        Reserver sete
                    </button>
                    }
                </div>  
            )
        }


    }
  
    return (
      <div className="fixed inset-0 z-50 flex items-center justify-center bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full">
        <div className="relative mx-auto p-6 border w-full max-w-md rounded-lg bg-white shadow-xl">
          <div className="text-center">
            <h3 className="text-xl font-semibold text-gray-900 mb-4">Seat Information</h3>
            <div className="text-left space-y-3">
              <p className="text-sm text-gray-600">Seat ID: <span className="text-gray-700 font-medium">{selectedSeatId}</span></p>
              <p className="text-sm text-gray-600">Booked by: <span className="text-gray-700 font-medium">{bookedBy || 'Not booked'}</span></p>
              <div className="flex items-center text-sm text-gray-600 space-x-2">
                <label htmlFor="booking-date" className="font-medium">Booking Date:</label>
                <input 
                    id="booking-date"
                    type="date" 
                    value={bookingDate || ''}
                    onChange={e => onDateChange(e.target.value)}
                    className="flex-1 px-3 py-2 border border-gray-300 rounded-md text-gray-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                    disabled={!user.admin}
                />
                </div>
            </div>
            {getButtonGroup()}
          </div>
        </div>
      </div>
    );
  };
  
  export default SeatInfoModal;
  