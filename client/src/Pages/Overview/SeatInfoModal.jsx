import React, { useState, useEffect } from "react";
import { observer } from "mobx-react-lite";
import ReCAPTCHA from "react-google-recaptcha";
import bookingStore from "../../stores/BookingStore";
import Booking, { DeleteBookingRequest } from "../../domain/booking";
import { useAuthContext } from "../../api/useAuthContext";

const SeatInfoModal = observer(({ onClose, selectedSeatId }) => {
  const { user } = useAuthContext() ?? {};
  const [recaptchaValue, setRecaptchaValue] = useState(null);

  const userId = user?.claims?.find(
    (claim) =>
      claim.key ===
      "http://schemas.microsoft.com/identity/claims/objectidentifier"
  )?.value;

  const { activeBookings, displayDate } = bookingStore;
  const [selectedBooking, setSelectedBooking] = useState(new Booking());

  useEffect(() => {
    const foundBooking = activeBookings.find(
      (booking) =>
        booking.seatId === selectedSeatId &&
        isSameDate(displayDate, booking.bookingDateTime)
    );
    if (foundBooking) {
      setSelectedBooking(foundBooking);
    }
  }, [selectedSeatId, activeBookings, displayDate]);

  const isSameDate = (date1, date2) => {
    if (!(date1 instanceof Date) || !(date2 instanceof Date)) {
      throw new Error("Both arguments must be Date objects.");
    }

    return date1.toDateString() === date2.toDateString();
  };

  const handleBook = async () => {
    // Check if reCAPTCHA is validated
    if (!recaptchaValue) {
      // You can handle the case where reCAPTCHA is not validated
      console.error("reCAPTCHA validation failed");
      return;
    }

    await bookingStore.createBooking(selectedSeatId);
    onClose();
  };

  const handleDelete = async () => {
    const deleteBookingRequest = new DeleteBookingRequest(selectedBooking.id);
    await bookingStore.deleteBooking(deleteBookingRequest);
    onClose();
  };

  const getButtonGroup = () => {
    if (selectedBooking.id && userId !== selectedBooking.userId) {
      return (
        <button
          onClick={onClose}
          className="mt-5 px-5 py-2 bg-blue-600 text-white text-sm font-medium rounded-md w-full shadow-sm hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
        >
          Lukk
        </button>
      );
    } else {
      return (
        <div className="flex justify-between">
          <button
            onClick={onClose}
            className="px-5 py-2 bg-blue-600 text-white text-sm font-medium rounded-md shadow-sm hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
          >
            Lukk
          </button>
          {userId === selectedBooking.userId && (
            <button
              onClick={handleDelete}
              className="px-5 py-2 bg-red-600 text-white text-sm font-medium rounded-md shadow-sm hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-red-500 focus:ring-offset-2"
            >
              Slett reservasjon
            </button>
          )}
          {selectedBooking.id === null && (
            <div>
              <ReCAPTCHA
                sitekey="6LdoUF0pAAAAAFeY4G_SGfKlRx53ITsl45p8n3Cp" // Replace with your reCAPTCHA site key
                onChange={(value) => setRecaptchaValue(value)}
              />
              <button
                onClick={handleBook}
                className="px-5 py-2 bg-green-600 text-white text-sm font-medium rounded-md shadow-sm hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-green-500 focus:ring-offset-2"
              >
                Reserver sete
              </button>
            </div>
          )}
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
              Sete ID:{" "}
              <span className="text-gray-700 font-medium">
                {selectedSeatId}
              </span>
            </p>
            <p className="text-sm text-gray-600">
              Reservert av:{" "}
              <span className="text-gray-700 font-medium">
                {selectedBooking.userName || "Ikke reservert"}
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

export default SeatInfoModal;
