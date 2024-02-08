import React, { useState, useEffect, useCallback, useRef } from "react";

import { useLocation } from "react-router-dom";
import { useAuthContext } from "@auth/useAuthContext";
import { observer } from "mobx-react-lite";
import bookingStore from "@stores/BookingStore";
import { MapComponent } from "./Map";
import { isSameDate } from "@/shared/utils";
import { Rooms } from "@/shared/types/enums";

import "./OverviewMap.css";

interface OverviewMapProps {
  showSeatInfo: (seatId: string) => void;
}

const OverviewMap = observer(({ showSeatInfo }: OverviewMapProps) => {
  const { user } = useAuthContext() ?? {};
  const isEventAdmin = user.claims.role === "EventAdmin"

  const { activeBookings, displayDate, bookEventMode, seatIdSelectedForNewEvent} = bookingStore;
  const location = useLocation();

  const zoomedOutViewBoxParameters = "0 0 3725 2712";
  const zoomedToLargeRoomViewBoxParameters = "1900 1600 1100 1050";
  const zoomedToSmallRoomViewBoxParameters = "2600 400 900 900";
  const zoomedToSalesBoxParameters = "0 900 800 800";

  const [currentViewBox, setCurrentViewBox] = useState(
    zoomedOutViewBoxParameters
  );
  const [zoomStatus, setZoomStatus] = useState("ZoomedOut");
  const currentViewBoxRef = useRef(currentViewBox); // useRef to store currentViewBox

  const userId = user.claims.objectidentifier


  const [needsUpdate, setNeedsUpdate] = useState(false);
  

  const getSeatClassName = (seatId: number) => {
    const bookingForSeat = activeBookings.find(
      (booking) =>
        booking.seatId === seatId &&
        isSameDate(displayDate, booking.bookingDateTime)
    );

    if(bookEventMode){
      if(seatIdSelectedForNewEvent.includes(seatId)){
        setNeedsUpdate(true)
        return "seat-selected-for-event"
      }
    }

    if (bookingForSeat) {
      return bookingForSeat.userId === userId
        ? "seat-booked-by-user"
        : "seat-booked";
    }

    const isAnySeatBookedByUser = activeBookings.some(
      (booking) =>
        booking.userId === userId &&
        isSameDate(displayDate, booking.bookingDateTime)
    );
    if (isAnySeatBookedByUser) {
      return "seat-unavailable";
    }

    if (!hasBookingOpened() && !isEventAdmin) {
      return "seat-available-later";
    }

    return "seat-available";
  };

  const hasBookingOpened = () => {
    let bookingOpeningTime = getEarliestAllowedBookingTime(displayDate);

    let currentDateTime = new Date();
    return currentDateTime > bookingOpeningTime;
  };

  const getEarliestAllowedBookingTime = (date: Date) => {
    let earliestAllowedTime = new Date(date);

    // If it's Monday, set the time to the Friday before at 16:00
    if (earliestAllowedTime.getDay() === 1) {
      // Monday has index 1 in JavaScript (0 is Sunday)
      earliestAllowedTime.setDate(earliestAllowedTime.getDate() - 3); // 3 days back to Friday
    } else {
      // Otherwise, set the time to the day before at 16:00
      earliestAllowedTime.setDate(earliestAllowedTime.getDate() - 1);
    }
    earliestAllowedTime.setHours(16, 0, 0, 0);
    return earliestAllowedTime;
  };

  const seatClicked = (e: React.MouseEvent<SVGPathElement>) => {
  const seatId = e.currentTarget.id;
  
   if (bookEventMode) {
  const seatIsBooked = bookingStore.activeBookings.some(booking => {
    return isSameDate(booking.bookingDateTime, bookingStore.displayDate) && booking.seatId === Number(seatId);
  });

  if (seatIsBooked) {
    showSeatInfo(seatId); 
    return;
  }

  bookingStore.toggleSeatSelectionForNewEvent(Number(seatId));
  return;
}

  showSeatInfo(seatId);
};

  const countAvailableSeats = (minSeatId: number, maxSeatId: number) => {
    let availableSeats = 0;

    for (let seatId = minSeatId; seatId <= maxSeatId; seatId++) {
      let isSeatAvailable = true;

      for (const booking of activeBookings) {
        if (
          booking.seatId === seatId &&
          isSameDate(booking.bookingDateTime, displayDate)
        ) {
          isSeatAvailable = false;
          break;
        }
      }

      if (isSeatAvailable) {
        availableSeats++;
      }
    }

    return availableSeats;
  };

  //--------------- Zoom functionality ------------------
  //TODO: ta ut zoom
  const zoomToRoom = (roomName: Rooms) => {
    let newViewBox;
    // Define or calculate the new viewBox for each room
    switch (roomName) {
      case Rooms.Large:
        newViewBox = zoomedToLargeRoomViewBoxParameters;
        break;
      case Rooms.Small:
        newViewBox = zoomedToSmallRoomViewBoxParameters;
        break;
      case Rooms.Sales:
        newViewBox = zoomedToSalesBoxParameters;
        break;
      default:
        newViewBox = zoomedOutViewBoxParameters; // Default view
    }

    setZoomStatus("Zooming");
    animateViewBox(newViewBox, 500);

    // setCurrentViewBox(newViewBox);
  };

  const updateZoomStatus = useCallback(
    (newViewBox: string) => {
      if (newViewBox === zoomedOutViewBoxParameters) {
        setZoomStatus("ZoomedOut");
      } else {
        setZoomStatus("ZoomedIn");
      }
    },
    [zoomedOutViewBoxParameters]
  );

  const animateViewBox = useCallback(
    (newViewBox: string, duration: number) => {
      const currentViewBoxValues = currentViewBoxRef.current
        .split(" ")
        .map(Number);
      const newViewBoxValues = newViewBox.split(" ").map(Number);

      let startTime: number;

      const step = (timestamp: number) => {
        if (!startTime) startTime = timestamp;
        const progress = (timestamp - startTime) / duration;

        if (progress < 1) {
          const intermediateViewBox = currentViewBoxValues
            .map((start, index) => {
              const end = newViewBoxValues[index];
              return start + (end - start) * progress; // Linear interpolation
            })
            .join(" ");

          setCurrentViewBox(intermediateViewBox);
          window.requestAnimationFrame(step);
        } else {
          setCurrentViewBox(newViewBox); // Ensure final value is set
          updateZoomStatus(newViewBox);
        }
      };

      window.requestAnimationFrame(step);
    },
    [updateZoomStatus]
  ); // Remove currentViewBox from dependencies

  // Update currentViewBoxRef whenever currentViewBox state changes
  useEffect(() => {
    currentViewBoxRef.current = currentViewBox;
  }, [currentViewBox]);

  const zoomOut = useCallback(() => {
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
    <>
      <MapComponent
        zoomStatus={zoomStatus}
        currentViewBox={currentViewBox}
        zoomToRoom={zoomToRoom}
        getSeatClassName={getSeatClassName}
        countAvailableSeats={countAvailableSeats}
        seatClicked={seatClicked}
        zoomOut={zoomOut}
        needsUpdate={needsUpdate}
      />
    </>
  );
});

export default OverviewMap;
