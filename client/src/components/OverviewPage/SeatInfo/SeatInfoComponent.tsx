import { CircularProgress } from "@mui/material";
import { Booking } from "@/shared/types/entities";
import React from "react";

interface Props {
  userGuid: string;
  isEventAdmin: boolean;
  selectedSeatId: number;
  loading: boolean;
  displayDate: Date;
  handleBooking: () => void;
  onClose: () => void;
  handleDelete: () => void;
  selectedBooking?: Booking;
}
export const SeatInfoComponent = ({
  loading,
  displayDate,
  onClose,
  selectedBooking,
  userGuid,
  isEventAdmin,
  handleDelete,
  selectedSeatId,
  handleBooking,
}: Props) => {
  const getButtonGroup = () => {
    const isBooked = !!selectedBooking?.userId;
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
          {(isYourBooking || (isBooked && isEventAdmin)) && (
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
};
