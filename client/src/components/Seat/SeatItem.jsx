
type SeatItemProps = {
    onClick: () => void;
    date: string;
    roomName: string;
    seatNr: string;
    name: string;
    isTaken: boolean;
}
const SeatItem = ({onClick, isTaken = false,  date, roomName, seatNr, name}) : SeatItemProps => {
    return (
        <ul  className={`divide-y divide-gray-100 p-4 rounded-[24px] 
            ${isTaken ? "bg-red" : "bg-cyan-100"} cursor-pointer`} 
             onClick={onClick}>
            <li className="flex justify-between gap-x-6 py-5">
                <div className="flex min-w-0 gap-x-4">
                    <div className="min-w-0 flex-auto">
                        <p className="text-sm font-semibold leading-6 text-gray-900">{name}</p>
                        <p className="mt-1 truncate text-xs leading-5 text-gray-500">{roomName}</p>
                        <p className="mt-1 truncate text-xs leading-5 text-gray-500">Taken: {isTaken ? 'Taken' : 'Free'}</p>
                    </div>
                </div>
                <div className="hidden shrink-0 sm:flex sm:flex-col sm:items-end">
                    <p className="text-sm leading-6 text-gray-900">{date}</p>
                    <p className="mt-1 text-xs leading-5 text-gray-500">{seatNr}</p>
                </div>
            </li>

        </ul>
    )
}

export default SeatItem;