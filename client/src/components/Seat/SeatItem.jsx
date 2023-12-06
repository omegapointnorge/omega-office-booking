const SeatItem = ({
  onClick,
  isTaken = false,
  date,
  roomName,
  seatNr,
  name,
  left,
  top,
}) => {
  return (
    <div
      className="origin-top-left rotate-[-90.62deg] w-14 h-10 relative cursor-pointer"
      onClick={onClick}
    >
      <div
        className={`w-14 h-8 left-0 top-0 absolute origin-top-left rotate-[-90.62deg] ${
          !isTaken ? "bg-slate-300" : "bg-orange-400"
        } rounded-lg`}
      />
      <div
        className={`w-6 h-5 left-[21.82px] top-[-16.24px] absolute origin-top-left rotate-[-90.62deg] ${
          !isTaken ? "bg-slate-300" : "bg-orange-400"
        } rounded-lg`}
      />
    </div>
  );
};

export default SeatItem;
