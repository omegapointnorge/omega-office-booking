import React, {useState, useEffect, useCallback, useRef } from 'react';
import { useLocation } from "react-router-dom";
import { useAuthContext } from '@auth/useAuthContext';
import {ReactComponent as ZoomOutIcon} from '@assets/icons/zoom-out_outline.svg'
import { observer } from 'mobx-react-lite';
import bookingStore from '@stores/BookingStore';
import './OverviewMap.css'


const OverviewMap = observer(({showSeatInfo}) => {

    const { user } = useAuthContext() ?? {};
    const { activeBookings, displayDate } = bookingStore
    const location = useLocation();

    const zoomedOutViewBoxParameters = "0 0 3725 2712"
    const zoomedToLargeRoomViewBoxParameters = "1900 1600 1100 1050"
    const zoomedToSmallRoomViewBoxParameters = "2600 400 900 900"
    const zoomedToSalesBoxParameters = "0 900 800 800"

    const [currentViewBox, setCurrentViewBox] = useState(zoomedOutViewBoxParameters);
    const [zoomStatus, setZoomStatus] = useState("ZoomedOut")
    const currentViewBoxRef = useRef(currentViewBox); // useRef to store currentViewBox

    const userId = user?.claims?.find(claim => claim.key === 'http://schemas.microsoft.com/identity/claims/objectidentifier')?.value;

    const getSeatClassName = (seatId) => {
      const bookingForSeat = activeBookings.find(booking => booking.seatId === seatId && isSameDate(displayDate, booking.bookingDateTime));
    
      if (bookingForSeat) {
        return bookingForSeat.userId === userId ? "seat-booked-by-user" : "seat-booked";
      }
      
      const isAnySeatBookedByUser = activeBookings.some(booking => booking.userId === userId && isSameDate(displayDate, booking.bookingDateTime));
      if (isAnySeatBookedByUser) {
        return "seat-unavailable"
      }

      if (!hasBookingOpened()) {
        return "seat-available-later"
      }

      return "seat-available";
    }

    const hasBookingOpened = () => {
      let bookingOpeningTime = new Date(displayDate);
      bookingOpeningTime.setDate(bookingOpeningTime.getDate() - 1);
      bookingOpeningTime.setHours(16, 0, 0, 0);

      let currentDateTime = new Date();
      return currentDateTime > bookingOpeningTime;
    }

    const isSameDate = (date1, date2) => {
      if (!(date1 instanceof Date) || !(date2 instanceof Date)) {
        throw new Error('Both arguments must be Date objects.');
    }

    return date1.toDateString() === date2.toDateString();
    } 

    const seatClicked = (clickEvent) => {
      showSeatInfo(clickEvent.target.id)
  }

  const countAvailableSeats = (minSeatId, maxSeatId) => {
    let availableSeats = 0;
  
    for (let seatId = minSeatId; seatId <= maxSeatId; seatId++) {
      let isSeatAvailable = true;
  
      for (const booking of activeBookings) {
        if (booking.seatId === seatId && isSameDate(booking.bookingDateTime, displayDate)) {
          isSeatAvailable = false;
          break;
        }
      }
  
      if (isSeatAvailable) {
        availableSeats++;
      }
    }
  
    return availableSeats;
  }


    //--------------- Zoom functionality ------------------

  
    const zoomToRoom = (roomName) => {
      let newViewBox;
      // Define or calculate the new viewBox for each room
      switch (roomName) {
        case 'large-room':
          newViewBox = zoomedToLargeRoomViewBoxParameters; 
          break;
        case 'small-room':
          newViewBox = zoomedToSmallRoomViewBoxParameters; 
          break;
        case 'sales':
          newViewBox = zoomedToSalesBoxParameters;
          break;
        default:
          newViewBox = zoomedOutViewBoxParameters; // Default view
      }

      setZoomStatus("Zooming")
      animateViewBox(newViewBox, 500)

      // setCurrentViewBox(newViewBox);
    };

    const updateZoomStatus = useCallback((newViewBox) => {
      if (newViewBox === zoomedOutViewBoxParameters) {
          setZoomStatus("ZoomedOut");
      } else {
          setZoomStatus("ZoomedIn");
      }
  }, [zoomedOutViewBoxParameters]);


  const animateViewBox = useCallback((newViewBox, duration) => {
    const currentViewBoxValues = currentViewBoxRef.current.split(' ').map(Number);
    const newViewBoxValues = newViewBox.split(' ').map(Number);

    let startTime;

    const step = (timestamp) => {
      if (!startTime) startTime = timestamp;
      const progress = (timestamp - startTime) / duration;

      if (progress < 1) {
        const intermediateViewBox = currentViewBoxValues.map((start, index) => {
          const end = newViewBoxValues[index];
          return start + (end - start) * progress; // Linear interpolation
        }).join(' ');

        setCurrentViewBox(intermediateViewBox);
        window.requestAnimationFrame(step);
      } else {
        setCurrentViewBox(newViewBox); // Ensure final value is set
        updateZoomStatus(newViewBox)
      }
    };

    window.requestAnimationFrame(step);
  }, [updateZoomStatus]); // Remove currentViewBox from dependencies

  // Update currentViewBoxRef whenever currentViewBox state changes
  useEffect(() => {
    currentViewBoxRef.current = currentViewBox;
  }, [currentViewBox]);

    const zoomOut = useCallback(() => {
      console.log("zoomOut")
      setZoomStatus("Zooming");
      animateViewBox(zoomedOutViewBoxParameters, 500);
  }, [animateViewBox, zoomedOutViewBoxParameters]);

    useEffect(() => {
      if (location.pathname === "/overview") {
        zoomOut();
      }
    }, [location, zoomOut]);

//--------------- End of zoom functionality -----------------

  return (
    <div className='relative'>
      <svg version="1.1" width="745" height="500" viewBox={currentViewBox} xmlns="http://www.w3.org/2000/svg">
        <g opacity="1.0" stroke="black" strokeWidth="3" fill="none">
          <path d="m897.96 1834.7 683.29-5.5963 38.242 3.6077 42.571 14.792 36.077 23.811 23.089 24.532 7.2154 9.38 9.7407 18.038 11.905 29.222 4.69 18.399 1.4431 172.81-51.229 167.4-675.36-128.79-1.0823-138.17-122.3 0.7216z"/>
          <path d="m2200.7 845.46 632.73-4.9004-140.82 321.68-495.92 1.0204z"/>
          <path d="m2187.9 846.18-1.0823 317.12-476.47 2.7725-1.2347-318.35z"/>
          <path d="m385.37 1615.7 496.39-2.8284 14.738 430.59-484.5 4z"/>
          <path d="m663.97 283.37 32.652 666.25-584.9 2.8478-19.445-283.9 237.23-0.35355-19.445-384.67z"/>
          <path d="m1539.4 642.76 651.78 0.53033 4.0659-362.75-660.44 2.2981z" />
          <path d="m1695.4 1811.7 2.125-972.95 1138.5-5.25 167.08-378.12-504.17-178.19-294.86 1.4142-1.4142 369.82-673.17 2.8284-6.364-368.4-847.82-2.1213 48.083 1321.6 165.46-0.7071 6.364 217.79z" />
          <path d="m112.43 963.79 41.366 644.88 561.8-5.3033-19.092-641.7z"/>
        </g>
        <g className={zoomStatus === "ZoomedOut" ? 'zoomed-out-room origin-[55%_90%]' : 'zoomed-in-room'} onClick={() => zoomToRoom("large-room")}>
          <path id="large-room" text="Large room" stroke="black" strokeWidth="3" fill={zoomStatus === "ZoomedIn" ? "ivory" : "gray"} d="m2963.3 2158.6-31.186-12.946v-51.276l0.7653-63.265-261.99 0.5102 1.5306-111.73-260.71-0.5102v-101.53h-259.18l2.0408-105.1-262.76 1.5306v-76.531h-165.31l-5.102 194.9 49.49 118.37 1.0204 203.06-52.551 159.18 131.12 27.551 909.69 267.35z"/>
          <text x="2110" y="2040" className={zoomStatus === "ZoomedOut" ? '' : "hidden"} fill="white" stroke="black" strokeWidth="3" fontSize="105" fontWeight="bolder" fontFamily="Arial">:</text>
          <text x="2180" y="2045" className={zoomStatus === "ZoomedOut" ? '' : "hidden"} fill="white" stroke="black" strokeWidth="3" fontSize="115" fontWeight="bolder" fontFamily="Arial">{countAvailableSeats(1,10)}/10</text>

          <text x="2110" y="2240" className={zoomStatus === "ZoomedOut" ? '' : "hidden"} fill="white" stroke="black" strokeWidth="3" fontSize="105" fontWeight="bolder" fontFamily="Arial">:</text>
          <text x="2180" y="2245" className={zoomStatus === "ZoomedOut" ? '' : "hidden"} fill="white" stroke="black" strokeWidth="3" fontSize="115" fontWeight="bolder" fontFamily="Arial">{10-countAvailableSeats(1,10)}/10</text>

          <g className={zoomStatus === "ZoomedOut" ? '' : 'hidden'} stroke="#000" strokeLinecap="round" strokeLinejoin="round" strokeWidth="5">
            <path className='seat-available' d="m1982 2069.1v8.8107a26.249 26.249 0 0 1-7.6907 18.566l-5.4332 5.4334h78.745l-5.4335-5.4334a26.249 26.249 0 0 1-7.691-18.566v-8.8107m52.497-104.99v85.308a19.686 19.686 0 0 1-19.686 19.686h-118.12a19.686 19.686 0 0 1-19.686-19.686v-85.308m157.49 0a19.686 19.686 0 0 0-19.686-19.686h-118.12a19.686 19.686 0 0 0-19.686 19.686m157.49 0v59.059a19.686 19.686 0 0 1-19.686 19.686h-118.12a19.686 19.686 0 0 1-19.686-19.686v-59.059"/>
            <path className='seat-booked' d="m1982 2269.2v8.8108a26.249 26.249 0 0 1-7.6907 18.566l-5.4332 5.4334h78.745l-5.4335-5.4334a26.249 26.249 0 0 1-7.691-18.566v-8.8108m52.497-104.99v85.308a19.686 19.686 0 0 1-19.686 19.686h-118.12a19.686 19.686 0 0 1-19.686-19.686v-85.308m157.49 0a19.686 19.686 0 0 0-19.686-19.686h-118.12a19.686 19.686 0 0 0-19.686 19.686m157.49 0v59.059a19.686 19.686 0 0 1-19.686 19.686h-118.12a19.686 19.686 0 0 1-19.686-19.686v-59.059"/>
          </g>
        </g>

        <g className={zoomStatus === "ZoomedOut" ? 'zoomed-out-room origin-[85%_20%]' : 'zoomed-in-room'} onClick={() => zoomToRoom('small-room')}>
          <path id="small-room" stroke="black" strokeWidth="3" fill={zoomStatus === "ZoomedIn" ? "ivory" : "gray"} d="m3406.1 1169.4 214.29-481.12-592.86-230.61-315.82 715.82z"/>
          <text x="3160" y="750" className={zoomStatus === "ZoomedOut" ? '' : "hidden"} fill="white" stroke="black" strokeWidth="3" fontSize="105" fontWeight="bolder" fontFamily="Arial">:</text>
          <text x="3230" y="755" className={zoomStatus === "ZoomedOut" ? '' : "hidden"} fill="white" stroke="black" strokeWidth="3" fontSize="115" fontWeight="bolder" fontFamily="Arial">{countAvailableSeats(11,15)}/5</text>

          <text x="3160" y="950" className={zoomStatus === "ZoomedOut" ? '' : "hidden"} fill="white" stroke="black" strokeWidth="3" fontSize="105" fontWeight="bolder" fontFamily="Arial">:</text>
          <text x="3230" y="955" className={zoomStatus === "ZoomedOut" ? '' : "hidden"} fill="white" stroke="black" strokeWidth="3" fontSize="115" fontWeight="bolder" fontFamily="Arial">{5-countAvailableSeats(11,15)}/5</text>
          <g className={zoomStatus === "ZoomedOut" ? '' : 'hidden'} stroke="#000" strokeLinecap="round" strokeLinejoin="round" strokeWidth="5">
            <path className='seat-available' d="m3037.7 769.2v8.8108a26.249 26.249 0 0 1-7.6907 18.566l-5.4332 5.4334h78.745l-5.4335-5.4334a26.249 26.249 0 0 1-7.691-18.566v-8.8108m52.497-104.99v85.308a19.686 19.686 0 0 1-19.686 19.686h-118.12a19.686 19.686 0 0 1-19.686-19.686v-85.308m157.49 0a19.686 19.686 0 0 0-19.686-19.686h-118.12a19.686 19.686 0 0 0-19.686 19.686m157.49 0v59.059a19.686 19.686 0 0 1-19.686 19.686h-118.12a19.686 19.686 0 0 1-19.686-19.686v-59.059"/>
            <path className='seat-booked' d="m3037.7 969.1v8.8108a26.249 26.249 0 0 1-7.6907 18.566l-5.4332 5.4334h78.745l-5.4335-5.4334a26.249 26.249 0 0 1-7.691-18.566v-8.8108m52.497-104.99v85.308a19.686 19.686 0 0 1-19.686 19.686h-118.12a19.686 19.686 0 0 1-19.686-19.686v-85.308m157.49 0a19.686 19.686 0 0 0-19.686-19.686h-118.12a19.686 19.686 0 0 0-19.686 19.686m157.49 0v59.059a19.686 19.686 0 0 1-19.686 19.686h-118.12a19.686 19.686 0 0 1-19.686-19.686v-59.059"/>
          </g>
        </g>
        {/* <g className={zoomStatus === "ZoomedOut" ? 'zoomed-out-room origin-[5%_45%]' : 'zoomed-in-room'} opacity="1.0">
          <path id="sales" stroke="black" strokeWidth="3" fill="gray" d="m112.43 963.79 41.366 644.88 561.8-5.3033-19.092-641.7z" onClick={() => zoomToRoom('sales')}/>
          <text x="300" y="1100" className={zoomStatus === "ZoomedOut" ? '' : "hidden"} fill="white" fontSize="65" fontWeight="bolder" fontFamily="Arial">Sales</text>
          <text x="220" y="1180" className={zoomStatus === "ZoomedOut" ? '' : "hidden"} fill="white" fontSize="55" fontWeight="bolder" fontFamily="Arial">Available Seats:</text>
        </g> */}
        <g className={zoomStatus === "ZoomedIn" ? '' : "hidden"} stroke="#000" strokeLinecap="round" strokeLinejoin="round" strokeWidth="5">
          <path id="11" className={getSeatClassName(11)} onClick={(clickEvent) => seatClicked(clickEvent)} d="m3368.5 1006.4-2.4516 5.6913a18.462 18.462 23.305 0 1-10.134 9.853l-5.0215 1.9979 50.866 21.911-1.998-5.0216a18.462 18.462 23.305 0 1 0.1983-14.133l2.4516-5.6913m63.126-53.214-23.737 55.105a13.846 13.846 23.305 0 1-18.194 7.2387l-76.299-32.867a13.846 13.846 23.305 0 1-7.2386-18.194l23.737-55.105m101.73 43.823a13.846 13.846 23.305 0 0-7.2386-18.194l-76.299-32.867a13.846 13.846 23.305 0 0-18.194 7.2387m101.73 43.823-16.434 38.149a13.846 13.846 23.305 0 1-18.194 7.2387l-76.299-32.867a13.846 13.846 23.305 0 1-7.2386-18.194l16.434-38.15"/>
          <path id="12" className={getSeatClassName(12)} onClick={(clickEvent) => seatClicked(clickEvent)} d="m3489.5 824.32 2.5992-5.6254a18.462 18.462 24.799 0 1 10.388-9.5854l5.0719-1.8662-50.277-23.23 1.8663 5.072a18.462 18.462 24.799 0 1-0.5667 14.123l-2.5992 5.6254m-64.492 51.549 25.166-54.467a13.846 13.846 24.799 0 1 18.377-6.7618l75.416 34.846a13.846 13.846 24.799 0 1 6.7616 18.377l-25.166 54.467m-100.55-46.461a13.846 13.846 24.799 0 0 6.7617 18.377l75.416 34.846a13.846 13.846 24.799 0 0 18.377-6.7617m-100.55-46.461 17.423-37.708a13.846 13.846 24.799 0 1 18.377-6.7618l75.416 34.846a13.846 13.846 24.799 0 1 6.7617 18.377l-17.423 37.708"/>
          <path id="13" className={getSeatClassName(13)} onClick={(clickEvent) => seatClicked(clickEvent)} d="m3194.9 1093.5h6.1969a18.462 18.462 0 0 1 13.058 5.4091l3.8215 3.8214v-55.384l-3.8215 3.8216a18.462 18.462 0 0 1-13.058 5.4093h-6.1969m-73.846-36.923h60a13.846 13.846 0 0 1 13.846 13.846v83.077a13.846 13.846 0 0 1-13.846 13.846h-60m0-110.77a13.846 13.846 0 0 0-13.846 13.846v83.077a13.846 13.846 0 0 0 13.846 13.846m0-110.77h41.538a13.846 13.846 0 0 1 13.846 13.846v83.077a13.846 13.846 0 0 1-13.846 13.846h-41.538"/>
          <path id="14" className={getSeatClassName(14)} onClick={(clickEvent) => seatClicked(clickEvent)} d="m3124.7 601.4 2.3474-5.7351a18.462 18.462 22.26 0 1 9.9527-10.036l4.9841-2.0891-51.257-20.98 2.0892 4.9844a18.462 18.462 22.26 0 1 0.059 14.134l-2.3474 5.7351m-62.145 54.356 22.728-55.529a13.846 13.846 22.26 0 1 18.059-7.5693l76.886 31.47a13.846 13.846 22.26 0 1 7.5692 18.059l-22.728 55.529m-102.51-41.96a13.846 13.846 22.26 0 0 7.5691 18.059l76.886 31.47a13.846 13.846 22.26 0 0 18.059-7.5693m-102.51-41.96 15.735-38.443a13.846 13.846 22.26 0 1 18.059-7.5693l76.886 31.47a13.846 13.846 22.26 0 1 7.5691 18.059l-15.735 38.443"/>
          <path id="15" className={getSeatClassName(15)} onClick={(clickEvent) => seatClicked(clickEvent)} d="m2969.8 1056.5h-6.1969a18.462 18.462 0 0 1-13.058-5.4091l-3.8215-3.8214v55.384l3.8215-3.8216a18.462 18.462 0 0 1 13.058-5.4093h6.1969m73.846 36.923h-60a13.846 13.846 0 0 1-13.846-13.846v-83.077a13.846 13.846 0 0 1 13.846-13.846h60m0 110.77a13.846 13.846 0 0 0 13.846-13.846v-83.077a13.846 13.846 0 0 0-13.846-13.846m0 110.77h-41.538a13.846 13.846 0 0 1-13.846-13.846v-83.077a13.846 13.846 0 0 1 13.846-13.846h41.538"/>
        </g>
        <g className={zoomStatus === "ZoomedIn" ? '' : "hidden"} stroke="#000" strokeLinecap="round" strokeLinejoin="round" strokeWidth="5">
          <path id="1" className={getSeatClassName(1)} onClick={(clickEvent) => seatClicked(clickEvent)} d="m1984.3 2151.3-5.9477-1.7396a18.462 18.462 16.304 0 1-11.015-8.8576l-2.5951-4.7405-15.548 53.157 4.7406-2.5951a18.462 18.462 16.304 0 1 14.052-1.5258l5.9477 1.7396m60.511 56.169-57.587-16.844a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l57.587 16.844m-31.096 106.31a13.846 13.846 16.304 0 0 17.176-9.4023l23.322-79.736a13.846 13.846 16.304 0 0-9.4024-17.176m-31.096 106.31-39.868-11.661a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l39.868 11.661"/>
          <path id="2" className={getSeatClassName(2)} onClick={(clickEvent) => seatClicked(clickEvent)} d="m1945.7 2277.2-5.9477-1.7396a18.462 18.462 16.304 0 1-11.015-8.8576l-2.5951-4.7405-15.548 53.157 4.7406-2.5951a18.462 18.462 16.304 0 1 14.052-1.5258l5.9477 1.7396m60.511 56.169-57.587-16.844a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l57.587 16.844m-31.096 106.31a13.846 13.846 16.304 0 0 17.176-9.4023l23.322-79.736a13.846 13.846 16.304 0 0-9.4024-17.176m-31.096 106.31-39.868-11.661a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l39.868 11.661"/>
          <path id="3" className={getSeatClassName(3)} onClick={(clickEvent) => seatClicked(clickEvent)} d="m2164 2244.3 5.9477 1.7396a18.462 18.462 16.304 0 1 11.015 8.8576l2.5951 4.7405 15.548-53.157-4.7406 2.5951a18.462 18.462 16.304 0 1-14.052 1.5258l-5.9477-1.7396m-60.511-56.169 57.587 16.844a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-57.587-16.844m31.096-106.31a13.846 13.846 16.304 0 0-17.176 9.4023l-23.322 79.736a13.846 13.846 16.304 0 0 9.4024 17.176m31.096-106.31 39.868 11.661a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-39.868-11.661"/>
          <path id="4" className={getSeatClassName(4)} onClick={(clickEvent) => seatClicked(clickEvent)} d="m2125.4 2368.2 5.9477 1.7396a18.462 18.462 16.304 0 1 11.015 8.8576l2.5951 4.7405 15.548-53.157-4.7406 2.5951a18.462 18.462 16.304 0 1-14.052 1.5258l-5.9477-1.7396m-60.511-56.169 57.587 16.844a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-57.587-16.844m31.096-106.31a13.846 13.846 16.304 0 0-17.176 9.4023l-23.322 79.736a13.846 13.846 16.304 0 0 9.4024 17.176m31.096-106.31 39.868 11.661a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-39.868-11.661"/>
          <path id="5" className={getSeatClassName(5)} onClick={(clickEvent) => seatClicked(clickEvent)} d="m2513.2 2179.3-5.9477-1.7396a18.462 18.462 16.304 0 1-11.015-8.8576l-2.5951-4.7405-15.548 53.157 4.7406-2.5951a18.462 18.462 16.304 0 1 14.052-1.5258l5.9477 1.7396m60.511 56.169-57.587-16.844a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l57.587 16.844m-31.096 106.31a13.846 13.846 16.304 0 0 17.176-9.4023l23.322-79.736a13.846 13.846 16.304 0 0-9.4024-17.176m-31.096 106.31-39.868-11.661a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l39.868 11.661"/>
          <path id="6" className={getSeatClassName(6)} onClick={(clickEvent) => seatClicked(clickEvent)} d="m2476.6 2302.1-5.9477-1.7396a18.462 18.462 16.304 0 1-11.015-8.8576l-2.5951-4.7405-15.548 53.157 4.7406-2.5951a18.462 18.462 16.304 0 1 14.052-1.5258l5.9477 1.7396m60.511 56.169-57.587-16.844a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l57.587 16.844m-31.096 106.31a13.846 13.846 16.304 0 0 17.176-9.4023l23.322-79.736a13.846 13.846 16.304 0 0-9.4024-17.176m-31.096 106.31-39.868-11.661a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l39.868 11.661"/>
          <path id="7" className={getSeatClassName(7)} onClick={(clickEvent) => seatClicked(clickEvent)} d="m2440.7 2425.7-5.9477-1.7396a18.462 18.462 16.304 0 1-11.015-8.8576l-2.5951-4.7405-15.548 53.157 4.7406-2.5951a18.462 18.462 16.304 0 1 14.052-1.5258l5.9477 1.7396m60.511 56.169-57.587-16.844a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l57.587 16.844m-31.096 106.31a13.846 13.846 16.304 0 0 17.176-9.4023l23.322-79.736a13.846 13.846 16.304 0 0-9.4024-17.176m-31.096 106.31-39.868-11.661a13.846 13.846 16.304 0 1-9.4023-17.176l23.322-79.736a13.846 13.846 16.304 0 1 17.176-9.4023l39.868 11.661"/>
          <path id="8" className={getSeatClassName(8)} onClick={(clickEvent) => seatClicked(clickEvent)}d="m2690.3 2270.6 5.9477 1.7396a18.462 18.462 16.304 0 1 11.015 8.8576l2.5951 4.7405 15.548-53.157-4.7406 2.5951a18.462 18.462 16.304 0 1-14.052 1.5258l-5.9477-1.7396m-60.511-56.169 57.587 16.844a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-57.587-16.844m31.096-106.31a13.846 13.846 16.304 0 0-17.176 9.4023l-23.322 79.736a13.846 13.846 16.304 0 0 9.4024 17.176m31.096-106.31 39.868 11.661a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-39.868-11.661"/>
          <path id="9" className={getSeatClassName(9)} onClick={(clickEvent) => seatClicked(clickEvent)} d="m2651.5 2392 5.9477 1.7396a18.462 18.462 16.304 0 1 11.015 8.8576l2.5951 4.7405 15.548-53.157-4.7406 2.5951a18.462 18.462 16.304 0 1-14.052 1.5258l-5.9477-1.7396m-60.511-56.169 57.587 16.844a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-57.587-16.844m31.096-106.31a13.846 13.846 16.304 0 0-17.176 9.4023l-23.322 79.736a13.846 13.846 16.304 0 0 9.4024 17.176m31.096-106.31 39.868 11.661a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-39.868-11.661"/>
          <path id="10" className={getSeatClassName(10)} onClick={(clickEvent) => seatClicked(clickEvent)} d="m2614.9 2514.4 5.9477 1.7396a18.462 18.462 16.304 0 1 11.015 8.8576l2.5951 4.7405 15.548-53.157-4.7406 2.5951a18.462 18.462 16.304 0 1-14.052 1.5258l-5.9477-1.7396m-60.511-56.169 57.587 16.844a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-57.587-16.844m31.096-106.31a13.846 13.846 16.304 0 0-17.176 9.4023l-23.322 79.736a13.846 13.846 16.304 0 0 9.4024 17.176m31.096-106.31 39.868 11.661a13.846 13.846 16.304 0 1 9.4023 17.176l-23.322 79.736a13.846 13.846 16.304 0 1-17.176 9.4023l-39.868-11.661"/>
        </g>

      </svg>


      <button className={`absolute top-0 right-0 m-2 p-2 bg-gray-200 text-black rounded hover:bg-gray-300 text-s ${zoomStatus === "ZoomedIn" ? '' : 'opacity-50 cursor-not-allowed'}`} onClick={() => zoomOut()}>
        <ZoomOutIcon className="h-6 w-6 inline-block mr-1" />
      </button>
    </div>
  );
});

export default OverviewMap;