import { MdDelete } from "react-icons/md";

const BookingItem = ({ onClick, roomName, bookingDateTime, seatId, showDeleteButton }) => {
  return (
      <ul className="divide-y divide-gray-100 p-4 rounded-[24px] bg-white">
        <li className="flex justify-between gap-x-6 py-5">
          <div className="flex min-w-0 gap-x-4">
            <div className="min-w-0 flex-auto">
              <p className="text-sm font-semibold leading-6 text-gray-900">
                {bookingDateTime}
              </p>
              <p className="mt-1 truncate text-xs leading-5 text-gray-500">
                {roomName}Romnavn, sete {seatId}
              </p>
            </div>
                <MdDelete
                    onClick={onClick}
                    className={`h-10 w-10 flex-none text-black hover:text-red-500 transition-colors duration-200 ${!showDeleteButton ? 'opacity-0' : 'opacity-100'}`}
                    src=""
                    alt=""
                    disabled={!showDeleteButton}
                />
          </div>
        </li>
      </ul>
  );
};

export default BookingItem;