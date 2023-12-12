import Icon from "@mui/material/Icon";

const SeatItem = ({ onClick, isTaken = false, seatNr }) => {
  return (
    <div className="flex flex-col gap-4 w-20 outline text-marine rounded-lg content-end bg-warmgray">
      <div
        className={`divide-y divide-gray-100 p-4 cursor-pointer w-full`}
        onClick={onClick}
      >
        <div className="flex min-w-0 gap-x-4 flex-auto">
          <div className="mt-1 truncate text-xs leading-5 content-evenly font-semibold">
            <h1
              className={`mt-1 text-sm leading-5 ${
                isTaken ? "text-red-700" : ""
              }`}
            >
              Seat {seatNr}
            </h1>
            <Icon fontSize="large">
              <span
                className={`material-symbols-outlined ${
                  isTaken ? "text-red-700" : ""
                }`}
              >
                {isTaken ? "desktop_access_disabled" : "desktop_windows"}
              </span>
            </Icon>
            <h2 className={`${isTaken ? "text-red-700" : ""}`}>
              {isTaken ? "Booked" : "Available"}
            </h2>
          </div>
        </div>
      </div>
    </div>
  );
};

export default SeatItem;
