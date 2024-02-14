import React, { useState, useEffect } from "react";
import { observer } from "mobx-react-lite";
import bookingStore from "../../../state/stores/BookingStore";
import { useAuthContext } from "../../../core/auth/useAuthContext";
import { Booking } from "@/shared/types/entities";
import { isSameDate } from "@/shared/utils";
import { CircularProgress } from "@mui/material";
import toast from "react-hot-toast";

interface OverviewSeatInfoProps {
  onClose: () => void;
  selectedSeatId: number;
}

const RECAPTCHA_SITE_KEY = "6Lc1tV8pAAAAABKV5g3LrYZNzUx1KGQkYHR-hSzo";

const OverviewSeatInfo = observer(
  ({ onClose, selectedSeatId }: OverviewSeatInfoProps) => {
    const { user } = useAuthContext() ?? {};
    const isEventAdmin = user.claims.role === "EventAdmin";

    const userGuid = user.claims.objectidentifier;

    const { activeBookings, displayDate } = bookingStore;
    const [selectedBooking, setSelectedBooking] = useState<Booking>();
    const [loading, setLoading] = useState(false);

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

    useEffect(() => {
      // Dynamically load reCAPTCHA script
      const script = document.createElement("script");
      script.src = `https://www.google.com/recaptcha/enterprise.js?render=${RECAPTCHA_SITE_KEY}`;
      script.async = true;
      document.head.appendChild(script);

      // Clean up function to remove the script when the component is unmounted
      return () => {
        document.head.removeChild(script);
      };
    }, []);

    const executeRecaptcha = async (): Promise<string> => {
      // Execute reCAPTCHA and return the token
      return new Promise((resolve) => {
        (window as any).grecaptcha.enterprise.ready(async () => {
          const token = await (window as any).grecaptcha.enterprise.execute(
            RECAPTCHA_SITE_KEY,
            { action: "BOOKING" }
          );
          resolve(token);
        });
      });
    };

    const handleBooking = async () => {
      setLoading(true);
      const reCAPTCHAtoken = await executeRecaptcha();
      await bookingStore
        .createBookingRequest(selectedSeatId, reCAPTCHAtoken)
        .then(() => setLoading(false));
      onClose();
    };

    const handleDelete = async () => {
      setLoading(true);
      if (claimKey !== selectedBooking?.userId) {
        toast.success("Husk å meld fra til: " + selectedBooking?.userName);
      }
      if (selectedBooking?.id) {
        await bookingStore.deleteBooking(selectedBooking.id).then(() => {
          setLoading(false);
          onClose();
        });
      } else {
        console.error("Unable to delete on id");
      }
    };

    const getButtonGroup = () => {

      const isBooked = !!selectedBooking?.userId
      const isYourBooking = userGuid === selectedBooking?.userId;

      if (isBooked && !isYourBooking && !isEventAdmin) {
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
            {(isYourBooking ||
              (isBooked && isEventAdmin)) && (
              <button
                onClick={handleDelete}
                className="px-5 py-2 bg-red-600 text-white text-sm font-medium rounded-md shadow-sm hover:bg-red-700 focus:outline-none focus:ring-2 focus:ring-red-500 focus:ring-offset-2"
              >
                Slett reservasjon
              </button>
            )}
            {!isBooked && (
              <button
                onClick={handleBooking}
                className="px-5 py-2 bg-green-600 text-white text-sm font-medium rounded-md shadow-sm hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-green-500 focus:ring-offset-2"
              >
                Reserver sete
              </button>
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

            <div className="flex flex-row space-y-3">
              <div className="basis-2/3 text-left">
                <p className="text-sm text-gray-600">
                  Sete ID:{" "}
                  <span className="text-gray-700 font-medium">
                    {selectedSeatId}
                  </span>
                </p>
                <p className="text-sm text-gray-600">
                  Reservert av:{" "}
                  <span className="text-gray-700 font-medium">
                    {selectedBooking?.userName || "Ikke reservert"}
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
              {loading && (
                <div className="basis-1/3">
                  <CircularProgress
                    color="primary"
                    size={30}
                    style={{ margin: "1rem" }}
                  />
                </div>
              )}
            </div>

            {getButtonGroup()}
          </div>
        </div>
      </div>
    );
  }
);

export default OverviewSeatInfo;
