import { MdDelete } from "react-icons/md";

const BookingItem = ({onClick, bookingDateTime, seatId, showDeleteButton, roomId }) => {
  const date = new Date(bookingDateTime);
  console.log(roomId);
  const dateString = date.toLocaleDateString();
  return (
    <ul className="divide-y divide-gray-100 p-4 rounded-[24px] bg-white w-48">
      <li className="flex flex-col">
          <div className="flex items-center content-center justify-center">
              <svg version="1.1" viewBox="0 0 3725.3 2712" xmlns="http://www.w3.org/2000/svg">
                  <g opacity=".249">
                      {/*large-room*/}
                      <g id="large-room" fill="red">
                          <path
                              d="m2963.3 2158.6-31.186-12.946v-51.276l0.7653-63.265-261.99 0.5102 1.5306-111.73-260.71-0.5102v-101.53h-259.18l2.0408-105.1-262.76 1.5306v-76.531h-165.31l-5.102 194.9 49.49 118.37 1.0204 203.06-52.551 159.18 131.12 27.551 909.69 267.35z"/>
                      </g>
                      <path
                          d="m897.96 1834.7 683.29-5.5963 38.242 3.6077 42.571 14.792 36.077 23.811 23.089 24.532 7.2154 9.38 9.7407 18.038 11.905 29.222 4.69 18.399 1.4431 172.81-51.229 167.4-675.36-128.79-1.0823-138.17-122.3 0.7216z"/>
                      <g id="small-room">
                          {/*small-room*/}
                          <path d="m3406.1 1169.4 214.29-481.12-592.86-230.61-315.82 715.82z"/>
                      </g>
                      <path d="m2200.7 845.46 632.73-4.9004-140.82 321.68-495.92 1.0204z"/>
                      <path d="m2187.9 846.18-1.0823 317.12-476.47 2.7725-1.2347-318.35z"/>
                      <path d="m385.37 1615.7 496.39-2.8284 14.738 430.59-484.5 4z"/>
                      <g id="sales">
                          {/*sales*/}
                          <path d="m112.43 963.79 41.366 644.88 561.8-5.3033-19.092-641.7z"/>
                      </g>
                      <path d="m663.97 283.37 32.652 666.25-584.9 2.8478-19.445-283.9 237.23-0.35355-19.445-384.67z"/>
                      <path d="m1539.4 642.76 651.78 0.53033 4.0659-362.75-660.44 2.2981z"/>
                      <path
                          d="m1695.4 1811.7 2.125-972.95 1138.5-5.25 167.08-378.12-504.17-178.19-294.86 1.4142-1.4142 369.82-673.17 2.8284-6.364-368.4-847.82-2.1213 48.083 1321.6 165.46-0.7071 6.364 217.79z"/>
                  </g>
              </svg>
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
                  <MdDelete
                      onClick={onClick}
                      color="black"
                      className={`h-8 w-8 flex-none text-black hover:text-red-500 transition-colors duration-200 ${!showDeleteButton ? 'opacity-0' : 'opacity-100'}`}
                      src=""
                      alt=""
                      disabled={!showDeleteButton}
                  />
              </div>
          </div>
      </li>
    </ul>

  );
};

export default BookingItem;
