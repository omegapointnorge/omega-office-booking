import { MdDelete } from "react-icons/md";
import BookingSvg from "./BookingSvg";

const BookingItem = ({onClick, bookingDateTime, seatId, showDeleteButton, roomId }) => {
  const date = new Date(bookingDateTime);
  const dateString = date.toLocaleDateString();

  return (
    <ul className="divide-y divide-gray-100 p-4 rounded-[24px] bg-white w-48">
      <li className="flex flex-col">
          <div className="flex items-center content-center justify-center">
              <BookingSvg highlightedId={roomId}/>
          </div>
          <div className="flex flex-row justify-between">
              <div>
                  <p className="text-sm leading-6 text-gray-900">
                      {dateString}
                  </p>
                  <p className="truncate text-xs leading-5 text-gray-900">
                      Sete {seatId}
                  </p>
              </div>
              <div className="flex items-center">
                <button>
                  <MdDelete
                      onClick={onClick}
                      color="black"
                      className={`h-8 w-8 flex-none text-black hover:text-red-500 transition-colors duration-200 ${!showDeleteButton ? 'opacity-0' : 'opacity-100'}`}
                      src=""
                      alt=""
                      disabled={!showDeleteButton}
                  />
                  </button>
              </div>
          </div>
      </li>
    </ul>

  );
};

export default BookingItem;
