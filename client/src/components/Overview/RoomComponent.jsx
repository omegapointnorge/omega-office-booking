import { SeatComponent } from "./SeatComponent";

const OverviewRoomComponent = ({ roomName, isBigRoom, onClick }) => {
  return (
    <>
      <div className="cursor-pointer " onClick={onClick}>
        <div className="text-center text-slate-500  font-bold pb-4">
          {roomName}
        </div>
        <div>{isBigRoom ? _BigRoomComponent() : _SmallRoomComponent()}</div>
      </div>
    </>
  );
};

const _BigRoomComponent = () => {
  return (
    <div className="w-44 h-36 relative curs">
      <div className="w-44 h-36 left-0 top-0 absolute bg-slate-200 rounded-xl border border-slate-500" />
      <SeatComponent left="left-2" bottom="top-2" rotate="rotate-[89.45deg]" />
      <SeatComponent left="left-10" top="top-2" rotate="rotate-[-89.45deg]" />
      <SeatComponent left="left-2" top="top-12" rotate="rotate-[89.45deg]" />
      <SeatComponent left="left-10" top="top-12" rotate="rotate-[-89.45deg]" />
      <SeatComponent
        left="left-2"
        bottom="bottom-8"
        rotate="rotate-[89.45deg]"
      />
      <SeatComponent
        left="left-10"
        bottom="bottom-8"
        rotate="rotate-[-89.45deg]"
      />
      <SeatComponent
        right="right-2"
        bottom="top-2"
        rotate="rotate-[-89.45deg]"
      />
      <SeatComponent right="right-10" top="top-2" rotate="rotate-[89.45deg]" />
      <SeatComponent
        right="right-2"
        bottom="top-12"
        rotate="rotate-[-89.45deg]"
      />
      <SeatComponent right="right-10" top="top-12" rotate="rotate-[89.45deg]" />
    </div>
  );
};

const _SmallRoomComponent = () => {
  return (
    <div className="w-44 h-36 relative curs">
      <div className="w-44 h-36 left-0 top-0 absolute bg-slate-200 rounded-xl border border-slate-500" />
      <SeatComponent left="left-4" bottom="top-8" rotate="rotate-180" />
      <SeatComponent left="left-4" bottom="bottom-12" />
      <SeatComponent
        left="left-8"
        bottom="bottom-2"
        rotate="rotate-[89.45deg]"
      />
      <SeatComponent
        left="left-16"
        bottom="bottom-2"
        rotate="rotate-[-90.55deg]"
      />
      <SeatComponent
        top="top-10"
        right="right-6"
        bottom="bottom-2"
        rotate="rotate-[-99.76deg]"
      />
    </div>
  );
};

export default OverviewRoomComponent;
