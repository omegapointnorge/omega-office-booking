import React from "react";

import { MdDelete } from "react-icons/md";
import { BookingSvg } from "../BookingItem/BookingSvg";

interface BookingItemProps {
  bookingDateTime: Date;
  seatIds: number[];
  roomIds: number[];
  eventName: string | undefined;
  onDelete?: () => void;
}

export const BookingItem = ({
  onDelete,
  bookingDateTime,
  seatIds,
  roomIds,
  eventName,
}: BookingItemProps) => {
  const date = new Date(bookingDateTime);
  const dateString = date.toLocaleDateString();

  return (
    <ul className="divide-y divide-gray-100 p-4 rounded-[24px] bg-white w-48">
      <li className="flex flex-col">
        {
          <div>
            <div className={`text-center ${!eventName ? "pt-6" : ""}`}>{eventName}</div>
            <div className="flex items-center justify-center">
              <BookingSvg highlightedIds={roomIds} />
            </div>
          </div>
        }
        <div className="flex flex-row justify-between">
          <div>
            <div className="text-sm leading-6 text-gray-900">{dateString}</div>
            <div className="truncate text-xs leading-5 text-gray-900">
              {seatIds.length > 1 ? (
                <p>Antall seter: {seatIds.length}</p>
              ) : (
                <p>Sete: {seatIds[0]}</p>
              )}
            </div>
          </div>
          {onDelete && (
            <button data-testid="delete-btn">
              <MdDelete
                onClick={onDelete}
                className={`h-8 w-8 flex-none text-black hover:text-red-500 transition cursor-pointer`}
              />
            </button>
          )}
        </div>
      </li>
    </ul>
  );
};
