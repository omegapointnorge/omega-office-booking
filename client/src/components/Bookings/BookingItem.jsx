import {MdDelete} from "react-icons/md";


type BookingItemProps = {
    onClick: () => void;
    date: string;
    roomName: string;
    seatNr: string;
    name: string;
}
const BookingItem = ({onClick, date, roomName, seatNr, name}) => {
    return (
        <ul role="list" className="divide-y divide-gray-100 p-4 rounded-[24px] bg-cyan-50">
            <li className="flex justify-between gap-x-6 py-5">
                <div className="flex min-w-0 gap-x-4">
                        <div className="min-w-0 flex-auto">
                            <p className="text-sm font-semibold leading-6 text-gray-900">{name}</p>
                            <p className="mt-1 truncate text-xs leading-5 text-gray-500">{roomName}</p>
                        </div>
                </div>
                <div className="hidden shrink-0 sm:flex sm:flex-col sm:items-end">
                    <p className="text-sm leading-6 text-gray-900">{date}</p>
                    <p className="mt-1 text-xs leading-5 text-gray-500">{seatNr}</p>
                </div>
                <MdDelete onClick={onClick} color="red" className="h-12 w-12 flex-none rounded-full bg-gray-50" src="" alt="" />
            </li>
            
        </ul>
        )
}

export default BookingItem;