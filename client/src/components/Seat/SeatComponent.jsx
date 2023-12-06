export const SeatComponent = ({ bottom, right, left, top, rotate }) => {
  return (
    <div
      className={`w-8 h-7  ${rotate} ${bottom} ${left ?? null} ${
        right ?? null
      } ${top} absolute`}
    >
      <div
        className={`w-8 h-5 left-0 top-0 absolute bg-slate-300 rounded-lg`}
      />
      <div className="w-3.5 h-3 left-[9.47px] top-[14.72px] absolute bg-slate-300 rounded-lg" />
    </div>
  );
};
