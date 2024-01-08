import { MdDelete } from "react-icons/md";

const BookingItem = ({ onClick, roomName, dateTime, seatId, showDeleteButton }) => {
  return (
      <ul className="divide-y divide-gray-100 p-4 rounded-[24px] bg-white">
        <li className="flex justify-between gap-x-6 py-5">
          <div className="flex min-w-0 gap-x-4">
            <div className="min-w-0 flex-auto">
              <p className="text-sm font-semibold leading-6 text-gray-900">
                {dateTime}
              </p>
              <p className="mt-1 truncate text-xs leading-5 text-gray-500">
                {roomName}, sete {seatId}
              </p>
            </div>
            {showDeleteButton && (
                <MdDelete
                    onClick={onClick}
                    color="black"
                    className="h-10 w-10 flex-none hover:bg-red-100 transition-colors duration-300"
                    src=""
                    alt=""
                />
            )}
          </div>
        </li>
      </ul>
  );
};

export default BookingItem;
