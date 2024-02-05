import React, { useState, useEffect } from "react";
import { observer } from "mobx-react-lite";
import bookingStore from "../../../state/stores/BookingStore";
import { useAuthContext } from "../../../core/auth/useAuthContext";


const { displayDate } = bookingStore;
const OverviewEventInfo = observer(({ onClose, selectedSeatIds }) => {
  const { user } = useAuthContext() ?? {};
  const userRole = user?.claims?.find(claim => claim.key === 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role')?.value;
  const isEventAdmin = userRole === "EventAdmin";


  const handleBook = async () => {
    await bookingStore.createBookingForEvent(selectedSeatIds);
    onClose();
  };

  const getButtonGroup = () => {
    if (isEventAdmin) {

      return (
        <div className="flex justify-between">
          <button
            onClick={onClose}
            className="px-5 py-2 bg-blue-600 text-white text-sm font-medium rounded-md shadow-sm hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
          >
            Lukk
          </button>


              <button
                onClick={handleBook}
                style={{  display: selectedSeatIds.length != 0 ? 'block' : 'none' }}
                className="px-5 py-2 bg-green-600 text-white text-sm font-medium rounded-md shadow-sm hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-green-500 focus:ring-offset-2"
              >
                Reserver sete
              </button>
       
        </div>
      );
    }
  };

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full">
      <div className="relative mx-auto p-6 border w-full max-w-md rounded-lg bg-white shadow-xl">
        <div className="text-center">
          <h3 className="text-xl font-semibold text-gray-900 mb-4">
            Seteinformasjon
          </h3>
          <div className="text-left space-y-3">
            <p className="text-sm text-gray-600">
              Skal reservere antall seter:{" "}
              <span className="text-gray-700 font-medium">
                {selectedSeatIds.length}
              </span>
            </p>
            <p className="text-sm text-gray-600">
              Reservert av:{" "}
              <span className="text-gray-700 font-medium">
                {userRole || "Ikke reservert"}
              </span>
            </p>
            <p className="text-sm text-gray-600">
              Dato:{" "}
              <span className="text-gray-700 font-medium">
                {displayDate.toLocaleDateString()}
              </span>
            </p>
            <br />
          </div>
          {getButtonGroup()}
        </div>
      </div>
    </div>
  );
});

export default OverviewEventInfo;
