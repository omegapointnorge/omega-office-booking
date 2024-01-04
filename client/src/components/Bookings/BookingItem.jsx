import { MdDelete } from "react-icons/md";

const BookingItem = ({ onClick, roomName, dateTime, seatId}) => {
  return (
    <ul className="divide-y divide-gray-100 p-4 rounded-[24px] bg-cyan-50">
      <li className="flex justify-between gap-x-6 py-5">
        <div className="flex min-w-0 gap-x-4">
          <div className="min-w-0 flex-auto">
            <p className="text-sm font-semibold leading-6 text-gray-900">
              {dateTime}
            </p>
            <p className="mt-1 truncate text-xs leading-5 text-gray-500">
              Sete: {seatId}, {roomName}
            </p>
          </div>
        </div>
        <MdDelete
          onClick={onClick}
          color="red"
          className="h-12 w-12 flex-none bg-gray-50 hover:bg-red-100 transition-colors duration-300"
          src=""
          alt=""
        />
      </li>
    </ul>

  );
};

export default BookingItem;
