import React from "react";

import { MdDelete } from "react-icons/md";
import HistoryBookingSvg from "@components/HistoryPage/HistoryBookingItem/HistoryBookingSvg";

interface BookingItemProps {
  bookingDateTime: Date;
  seatId: number;
  showDeleteButton: boolean;
  roomId: number | null;
  onClick?: () => void;
}
const HistoryBookingItem = ({
  onClick,
  bookingDateTime,
  seatId,
  showDeleteButton,
  roomId,
}: BookingItemProps) => {
  const date = new Date(bookingDateTime);
  const dateString = date.toLocaleDateString();

  return (
    <ul className="divide-y divide-gray-100 p-4 rounded-[24px] bg-white w-48">
      <li className="flex flex-col">
        {roomId && (
          <div className="flex items-center content-center justify-center">
            <HistoryBookingSvg highlightedId={roomId} />
          </div>
        )}
        <div className="flex flex-row justify-between">
          <div>
            <p className="text-sm leading-6 text-gray-900">{dateString}</p>
            <p className="truncate text-xs leading-5 text-gray-900">
              Sete {seatId}
            </p>
          </div>
          <div className="flex items-center">
            {" "}
            <button disabled={!showDeleteButton}>
              <MdDelete
                onClick={onClick}
                className={`h-8 w-8 flex-none text-black hover:text-red-500 transition cursor-pointer ${
                  !showDeleteButton ? "opacity-0" : "opacity-100"
                }`}
              />
            </button>
          </div>
        </div>
      </li>
    </ul>
  );
};

export default HistoryBookingItem;
