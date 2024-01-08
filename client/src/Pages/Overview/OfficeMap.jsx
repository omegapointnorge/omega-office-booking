import React, {useState} from 'react';
import {ReactComponent as ZoomOutIcon} from '../../assets/icons/zoom-out_outline.svg'

const OfficeMap = ({showSeatInfo, activeBookings, user}) => {

    const [currentViewBox, setCurrentViewBox] = useState("0 0 3725 2712");
    const [zoomedIn, setZoomedIn] = useState(false);

    const userId = user.claims[1].value

  

    const getColorForSeat = (seatId) => {
        const foundBooking = activeBookings.find(booking => booking.seatId === seatId);
    
        if (foundBooking) {
            if (foundBooking.email === userId) {
                return "blue"; // The seat is booked by the current user
            }
            return "red"; // The seat is booked by someone else
        }
    
        return "green"; // The seat is not booked
    };
  
    const zoomToRoom = (roomName) => {
      let newViewBox;
      setZoomedIn(true)
      // Define or calculate the new viewBox for each room
      switch (roomName) {
        case 'large-room':
          newViewBox = "1900 1600 1100 1050"; // Replace with actual values for large-room
          break;
        case 'small-room':
          newViewBox = "2600 400 900 900"; // Replace with actual values for small-room
          break;
        case 'sales':
          newViewBox = "x y width height"; // Replace with actual values for sales
          break;
        // ... other cases ...
        default:
          newViewBox = "0 0 3725 2712"; // Default view
      }
      setCurrentViewBox(newViewBox);
    };

    const zoomOut = () => {
        setCurrentViewBox("0 0 3725 2712")
        setZoomedIn(false)
      }

    const seatClicked = (clickEvent) => {
        showSeatInfo(clickEvent.target.id)
    }


  return (
    <div className='relative'>
      <svg version="1.1" width="745" height="500" viewBox={currentViewBox} xmlns="http://www.w3.org/2000/svg">
        <g opacity="1.0">
          <path d="m897.96 1834.7 683.29-5.5963 38.242 3.6077 42.571 14.792 36.077 23.811 23.089 24.532 7.2154 9.38 9.7407 18.038 11.905 29.222 4.69 18.399 1.4431 172.81-51.229 167.4-675.36-128.79-1.0823-138.17-122.3 0.7216z" stroke="black" strokeWidth="3" fill="none"/>
          <path d="m2200.7 845.46 632.73-4.9004-140.82 321.68-495.92 1.0204z" stroke="black" strokeWidth="3" fill="none"/>
          <path d="m2187.9 846.18-1.0823 317.12-476.47 2.7725-1.2347-318.35z" stroke="black" strokeWidth="3" fill="none"/>
          <path d="m385.37 1615.7 496.39-2.8284 14.738 430.59-484.5 4z" stroke="black" strokeWidth="3" fill="none"/>
          <path d="m663.97 283.37 32.652 666.25-584.9 2.8478-19.445-283.9 237.23-0.35355-19.445-384.67z" stroke="black" strokeWidth="3" fill="none"/>
          <path d="m1539.4 642.76 651.78 0.53033 4.0659-362.75-660.44 2.2981z" stroke="black" strokeWidth="3" fill="none"/>
          <path d="m1695.4 1811.7 2.125-972.95 1138.5-5.25 167.08-378.12-504.17-178.19-294.86 1.4142-1.4142 369.82-673.17 2.8284-6.364-368.4-847.82-2.1213 48.083 1321.6 165.46-0.7071 6.364 217.79z" stroke="black" strokeWidth="3" fill="none"/>
        </g>
        <g className={zoomedIn ? '' : 'hover:scale-125 origin-[55%_90%] duration-100 cursor-pointer'} onClick={() => zoomToRoom("large-room")}>
          <path id="large-room" text="Large room" stroke="black" strokeWidth="3" fill={zoomedIn ? "lightgray" : "gray"} d="m2963.3 2158.6-31.186-12.946v-51.276l0.7653-63.265-261.99 0.5102 1.5306-111.73-260.71-0.5102v-101.53h-259.18l2.0408-105.1-262.76 1.5306v-76.531h-165.31l-5.102 194.9 49.49 118.37 1.0204 203.06-52.551 159.18 131.12 27.551 909.69 267.35z"/>
          <text x="1920" y="1950" className={zoomedIn ? 'hidden' : ""} fill="white" fontSize="65" fontWeight="bolder" fontFamily="Arial">Large Room</text>
          <text x="1920" y="2030" className={zoomedIn ? 'hidden' : ""} fill="white" fontSize="55" fontWeight="bolder" fontFamily="Arial">Available Seats:</text>
        </g>
        <g className={zoomedIn ? '' : 'hover:scale-125 hover:brightness-110 origin-[85%_20%] duration-100 cursor-pointer'} onClick={() => zoomToRoom('small-room')}>
          <path id="small-room" stroke="black" strokeWidth="3" fill={zoomedIn ? "lightgray" : "gray"} d="m3406.1 1169.4 214.29-481.12-592.86-230.61-315.82 715.82z"/>
          <text x="3020" y="730" className={zoomedIn ? 'hidden' : ""} fill="white" fontSize="65" fontWeight="bolder" fontFamily="Arial">Small Room</text>
          <text x="2960" y="810" className={zoomedIn ? 'hidden' : ""} fill="white" fontSize="55" fontWeight="bolder" fontFamily="Arial">Available Seats:</text>

        </g>
        <g className='hover:scale-125 origin-[5%_45%] duration-100 cursor-pointer' opacity="1.0">
          <path id="sales" stroke="black" strokeWidth="3" fill="gray" d="m112.43 963.79 41.366 644.88 561.8-5.3033-19.092-641.7z" onClick={() => zoomToRoom('sales')}/>
          <text x="300" y="1100" className={zoomedIn ? 'hidden' : ""} fill="white" fontSize="65" fontWeight="bolder" fontFamily="Arial">Sales</text>
          <text x="220" y="1180" className={zoomedIn ? 'hidden' : ""} fill="white" fontSize="55" fontWeight="bolder" fontFamily="Arial">Available Seats:</text>
        </g>
        <g className={zoomedIn ? '' : "hidden"} stroke="#000" strokeLinecap="round" strokeLinejoin="round" strokeWidth="9.2308">
          <path id="11" fill={getColorForSeat('11')} className='hover:brightness-110 cursor-pointer' onClick={(clickEvent) => seatClicked(clickEvent)} d="m3368.5 1006.4-2.4516 5.6913a18.462 18.462 23.305 0 1-10.134 9.853l-5.0215 1.9979 50.866 21.911-1.998-5.0216a18.462 18.462 23.305 0 1 0.1983-14.133l2.4516-5.6913m63.126-53.214-23.737 55.105a13.846 13.846 23.305 0 1-18.194 7.2387l-76.299-32.867a13.846 13.846 23.305 0 1-7.2386-18.194l23.737-55.105m101.73 43.823a13.846 13.846 23.305 0 0-7.2386-18.194l-76.299-32.867a13.846 13.846 23.305 0 0-18.194 7.2387m101.73 43.823-16.434 38.149a13.846 13.846 23.305 0 1-18.194 7.2387l-76.299-32.867a13.846 13.846 23.305 0 1-7.2386-18.194l16.434-38.15"/>
          <path id="12" fill={getColorForSeat('12')} className='hover:brightness-110 cursor-pointer' onClick={(clickEvent) => seatClicked(clickEvent)} d="m3489.5 824.32 2.5992-5.6254a18.462 18.462 24.799 0 1 10.388-9.5854l5.0719-1.8662-50.277-23.23 1.8663 5.072a18.462 18.462 24.799 0 1-0.5667 14.123l-2.5992 5.6254m-64.492 51.549 25.166-54.467a13.846 13.846 24.799 0 1 18.377-6.7618l75.416 34.846a13.846 13.846 24.799 0 1 6.7616 18.377l-25.166 54.467m-100.55-46.461a13.846 13.846 24.799 0 0 6.7617 18.377l75.416 34.846a13.846 13.846 24.799 0 0 18.377-6.7617m-100.55-46.461 17.423-37.708a13.846 13.846 24.799 0 1 18.377-6.7618l75.416 34.846a13.846 13.846 24.799 0 1 6.7617 18.377l-17.423 37.708"/>
          <path id="13" fill={getColorForSeat('13')} className='hover:brightness-110 cursor-pointer' onClick={(clickEvent) => seatClicked(clickEvent)} d="m3194.9 1093.5h6.1969a18.462 18.462 0 0 1 13.058 5.4091l3.8215 3.8214v-55.384l-3.8215 3.8216a18.462 18.462 0 0 1-13.058 5.4093h-6.1969m-73.846-36.923h60a13.846 13.846 0 0 1 13.846 13.846v83.077a13.846 13.846 0 0 1-13.846 13.846h-60m0-110.77a13.846 13.846 0 0 0-13.846 13.846v83.077a13.846 13.846 0 0 0 13.846 13.846m0-110.77h41.538a13.846 13.846 0 0 1 13.846 13.846v83.077a13.846 13.846 0 0 1-13.846 13.846h-41.538"/>
          <path id="14" fill={getColorForSeat('14')} className='hover:brightness-110 cursor-pointer' onClick={(clickEvent) => seatClicked(clickEvent)} d="m3124.7 601.4 2.3474-5.7351a18.462 18.462 22.26 0 1 9.9527-10.036l4.9841-2.0891-51.257-20.98 2.0892 4.9844a18.462 18.462 22.26 0 1 0.059 14.134l-2.3474 5.7351m-62.145 54.356 22.728-55.529a13.846 13.846 22.26 0 1 18.059-7.5693l76.886 31.47a13.846 13.846 22.26 0 1 7.5692 18.059l-22.728 55.529m-102.51-41.96a13.846 13.846 22.26 0 0 7.5691 18.059l76.886 31.47a13.846 13.846 22.26 0 0 18.059-7.5693m-102.51-41.96 15.735-38.443a13.846 13.846 22.26 0 1 18.059-7.5693l76.886 31.47a13.846 13.846 22.26 0 1 7.5691 18.059l-15.735 38.443"/>
          <path id="15" fill={getColorForSeat('15')} className='hover:brightness-110 cursor-pointer' onClick={(clickEvent) => seatClicked(clickEvent)} d="m2969.8 1056.5h-6.1969a18.462 18.462 0 0 1-13.058-5.4091l-3.8215-3.8214v55.384l3.8215-3.8216a18.462 18.462 0 0 1 13.058-5.4093h6.1969m73.846 36.923h-60a13.846 13.846 0 0 1-13.846-13.846v-83.077a13.846 13.846 0 0 1 13.846-13.846h60m0 110.77a13.846 13.846 0 0 0 13.846-13.846v-83.077a13.846 13.846 0 0 0-13.846-13.846m0 110.77h-41.538a13.846 13.846 0 0 1-13.846-13.846v-83.077a13.846 13.846 0 0 1 13.846-13.846h41.538"/>
        </g>
        <g className={zoomedIn ? '' : "hidden"} stroke="#000" strokeLinecap="round" strokeLinejoin="round" strokeWidth="9.2308">
          <path id="1" fill={getColorForSeat('1')} className='hover:brightness-110 cursor-pointer' onClick={(clickEvent) => seatClicked(clickEvent)} d="m1984.3 2151.3-5.9477-1.7396a18.462 18.462 16.304 0 1-11.015-8.8576l-2.5951-4.7405-15.548 53.157 4.7406-2.5951a18.462 18.462 16.304 0 1 14.052-1.5258l5.9477 1.7396m60.511 56.169-57.587-16.844a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l57.587 16.844m-31.096 106.31a13.846 13.846 16.304 0 0 17.176-9.4023l23.322-79.736a13.846 13.846 16.304 0 0-9.4024-17.176m-31.096 106.31-39.868-11.661a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l39.868 11.661"/>
          <path id="2" fill={getColorForSeat('2')} className='hover:brightness-110 cursor-pointer' onClick={(clickEvent) => seatClicked(clickEvent)} d="m1945.7 2277.2-5.9477-1.7396a18.462 18.462 16.304 0 1-11.015-8.8576l-2.5951-4.7405-15.548 53.157 4.7406-2.5951a18.462 18.462 16.304 0 1 14.052-1.5258l5.9477 1.7396m60.511 56.169-57.587-16.844a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l57.587 16.844m-31.096 106.31a13.846 13.846 16.304 0 0 17.176-9.4023l23.322-79.736a13.846 13.846 16.304 0 0-9.4024-17.176m-31.096 106.31-39.868-11.661a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l39.868 11.661"/>
          <path id="3" fill={getColorForSeat('3')} className='hover:brightness-110 cursor-pointer' onClick={(clickEvent) => seatClicked(clickEvent)} d="m2164 2244.3 5.9477 1.7396a18.462 18.462 16.304 0 1 11.015 8.8576l2.5951 4.7405 15.548-53.157-4.7406 2.5951a18.462 18.462 16.304 0 1-14.052 1.5258l-5.9477-1.7396m-60.511-56.169 57.587 16.844a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-57.587-16.844m31.096-106.31a13.846 13.846 16.304 0 0-17.176 9.4023l-23.322 79.736a13.846 13.846 16.304 0 0 9.4024 17.176m31.096-106.31 39.868 11.661a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-39.868-11.661"/>
          <path id="4" fill={getColorForSeat('4')} className='hover:brightness-110 cursor-pointer' onClick={(clickEvent) => seatClicked(clickEvent)} d="m2125.4 2368.2 5.9477 1.7396a18.462 18.462 16.304 0 1 11.015 8.8576l2.5951 4.7405 15.548-53.157-4.7406 2.5951a18.462 18.462 16.304 0 1-14.052 1.5258l-5.9477-1.7396m-60.511-56.169 57.587 16.844a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-57.587-16.844m31.096-106.31a13.846 13.846 16.304 0 0-17.176 9.4023l-23.322 79.736a13.846 13.846 16.304 0 0 9.4024 17.176m31.096-106.31 39.868 11.661a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-39.868-11.661"/>
          <path id="5" fill={getColorForSeat('5')} className='hover:brightness-110 cursor-pointer' onClick={(clickEvent) => seatClicked(clickEvent)} d="m2513.2 2179.3-5.9477-1.7396a18.462 18.462 16.304 0 1-11.015-8.8576l-2.5951-4.7405-15.548 53.157 4.7406-2.5951a18.462 18.462 16.304 0 1 14.052-1.5258l5.9477 1.7396m60.511 56.169-57.587-16.844a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l57.587 16.844m-31.096 106.31a13.846 13.846 16.304 0 0 17.176-9.4023l23.322-79.736a13.846 13.846 16.304 0 0-9.4024-17.176m-31.096 106.31-39.868-11.661a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l39.868 11.661"/>
          <path id="6" fill={getColorForSeat('6')} className='hover:brightness-110 cursor-pointer' onClick={(clickEvent) => seatClicked(clickEvent)} d="m2476.6 2302.1-5.9477-1.7396a18.462 18.462 16.304 0 1-11.015-8.8576l-2.5951-4.7405-15.548 53.157 4.7406-2.5951a18.462 18.462 16.304 0 1 14.052-1.5258l5.9477 1.7396m60.511 56.169-57.587-16.844a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l57.587 16.844m-31.096 106.31a13.846 13.846 16.304 0 0 17.176-9.4023l23.322-79.736a13.846 13.846 16.304 0 0-9.4024-17.176m-31.096 106.31-39.868-11.661a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l39.868 11.661"/>
          <path id="7" fill={getColorForSeat('7')} className='hover:brightness-110 cursor-pointer' onClick={(clickEvent) => seatClicked(clickEvent)} d="m2440.7 2425.7-5.9477-1.7396a18.462 18.462 16.304 0 1-11.015-8.8576l-2.5951-4.7405-15.548 53.157 4.7406-2.5951a18.462 18.462 16.304 0 1 14.052-1.5258l5.9477 1.7396m60.511 56.169-57.587-16.844a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l57.587 16.844m-31.096 106.31a13.846 13.846 16.304 0 0 17.176-9.4023l23.322-79.736a13.846 13.846 16.304 0 0-9.4024-17.176m-31.096 106.31-39.868-11.661a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l39.868 11.661"/>
          <path id="8" fill={getColorForSeat('8')} className='hover:brightness-110 cursor-pointer' onClick={(clickEvent) => seatClicked(clickEvent)}d="m2690.3 2270.6 5.9477 1.7396a18.462 18.462 16.304 0 1 11.015 8.8576l2.5951 4.7405 15.548-53.157-4.7406 2.5951a18.462 18.462 16.304 0 1-14.052 1.5258l-5.9477-1.7396m-60.511-56.169 57.587 16.844a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-57.587-16.844m31.096-106.31a13.846 13.846 16.304 0 0-17.176 9.4023l-23.322 79.736a13.846 13.846 16.304 0 0 9.4024 17.176m31.096-106.31 39.868 11.661a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-39.868-11.661"/>
          <path id="9" fill={getColorForSeat('9')} className='hover:brightness-110 cursor-pointer' onClick={(clickEvent) => seatClicked(clickEvent)} d="m2651.5 2392 5.9477 1.7396a18.462 18.462 16.304 0 1 11.015 8.8576l2.5951 4.7405 15.548-53.157-4.7406 2.5951a18.462 18.462 16.304 0 1-14.052 1.5258l-5.9477-1.7396m-60.511-56.169 57.587 16.844a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-57.587-16.844m31.096-106.31a13.846 13.846 16.304 0 0-17.176 9.4023l-23.322 79.736a13.846 13.846 16.304 0 0 9.4024 17.176m31.096-106.31 39.868 11.661a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-39.868-11.661"/>
          <path id="10" fill={getColorForSeat('10')} className='hover:brightness-110 cursor-pointer' onClick={(clickEvent) => seatClicked(clickEvent)} d="m2614.9 2514.4 5.9477 1.7396a18.462 18.462 16.304 0 1 11.015 8.8576l2.5951 4.7405 15.548-53.157-4.7406 2.5951a18.462 18.462 16.304 0 1-14.052 1.5258l-5.9477-1.7396m-60.511-56.169 57.587 16.844a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-57.587-16.844m31.096-106.31a13.846 13.846 16.304 0 0-17.176 9.4023l-23.322 79.736a13.846 13.846 16.304 0 0 9.4024 17.176m31.096-106.31 39.868 11.661a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-39.868-11.661"/>
        </g>
      </svg>


      <button className={`absolute top-0 right-0 m-2 p-2 bg-gray-200 text-black rounded hover:bg-gray-300 text-s ${zoomedIn ? '' : 'opacity-50 cursor-not-allowed'}`} onClick={() => zoomOut()}>
        <ZoomOutIcon className="h-6 w-6 inline-block mr-1" /> Zoom Out
      </button>
    </div>
  );
};

export default OfficeMap;