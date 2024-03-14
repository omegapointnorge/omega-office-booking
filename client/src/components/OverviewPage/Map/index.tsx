import React, { useState, useEffect, useCallback, useRef } from "react";

import { useLocation } from "react-router-dom";
import { useAuthContext } from "@auth/useAuthContext";
import { observer } from "mobx-react-lite";
import bookingStore from "@stores/BookingStore";
import { MapComponent } from "./MapComponent";
import { isSameDate } from "@utils/utils";
import { Rooms, ZoomStatus } from "@/shared/types/enums";

import "./OverviewMap.css";
import { ToggleSwitch } from "../ToggleButton/Toggle";

interface OverviewMapProps {
  showSeatInfo: (seatId: string) => void;
}

const OverviewMap = observer(({ showSeatInfo }: OverviewMapProps) => {
  const { user } = useAuthContext() ?? {};

  const isEventAdmin = user.claims.role === "EventAdmin";
  const userId = user.claims.objectidentifier;

  const {
    activeBookings,
    displayDate,
    bookEventMode,
    seatIdSelectedForNewEvent,
  } = bookingStore;

  const location = useLocation();

  const zoomedOutViewBoxParameters = "0 0 3725 2712";
  const zoomedToLargeRoomViewBoxParameters = "1750 1400 1200 1500";
  const zoomedToSmallRoomViewBoxParameters = "2700 400 900 900";
  const zoomedToSalesViewBoxParameters = "0 900 800 800";
  const zoomedToMarieViewBoxParameters = "1600 650 800 700"
  const zoomedToEconOysteinViewBoxParameters = "2050 650 800 700"

  const [currentViewBox, setCurrentViewBox] = useState(
    zoomedOutViewBoxParameters
  );
  const [zoomStatus, setZoomStatus] = useState(ZoomStatus.ZoomedOut);
  const [selectAllSeats, setSelectAllSeats] = useState(false);

  const handleAllSeatsSelected = () => {
    
    switch (zoomStatus) {
      case ZoomStatus.Large:
        upddateSeatSelectionForEvent(1, selectAllSeats);
        break;
      case ZoomStatus.Small:
        upddateSeatSelectionForEvent(2, selectAllSeats);
        break;
      case ZoomStatus.Sales:
        upddateSeatSelectionForEvent(3, selectAllSeats);
        break;
      case ZoomStatus.Marie:
        upddateSeatSelectionForEvent(1, selectAllSeats);
        break;
      case ZoomStatus.EconOystein: 
        upddateSeatSelectionForEvent(5, selectAllSeats);
        upddateSeatSelectionForEvent(6, selectAllSeats);
        break;
      default:
        break;// Default view
    }
    setSelectAllSeats(!selectAllSeats);
  }

  const upddateSeatSelectionForEvent = (roomId: number, isSelect: boolean): void => {
    console.log("Room number: ", roomId);
    const seatsIdsInRoom = bookingStore.allSeats.filter(seat => {
      // console.log("RoomId: ", seat.roomId, roomId);
      // const variable = seat.roomId === roomId;
      // console.log("Result: ", variable);
      // console.log("TypeOf: ", typeof(seat.roomId), typeof(roomId));
      
      return seat.roomId === roomId
    }).map(seat => seat.id as number);
    console.log("SeatsInRoom: ", seatsIdsInRoom);

    if(isSelect){
      seatsIdsInRoom.forEach(seatId => bookingStore.addSeatToEventSelection(seatId));
    }
    else{
      bookingStore.removeSeatsFromEventSelection(seatsIdsInRoom);
    }
    
    bookingStore.seatIdSelectedForNewEvent.forEach(id => console.log("ID: ", id));
  }


  const currentViewBoxRef = useRef(currentViewBox); // useRef to store currentViewBox

  const getSeatClassName = (seatId: number): string => {
    const bookingForSeat = activeBookings.find(
      (booking) =>
        booking.seatId === seatId &&
        isSameDate(displayDate, booking.bookingDateTime)
    );

    if (bookEventMode) {
      if (seatIdSelectedForNewEvent.includes(seatId)) {
        return "seat-selected-for-event";
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

    if (isAnySeatBookedByUser && !isEventAdmin) {
      return "seat-unavailable";
    }

    if (!bookingStore.hasBookingOpened(displayDate) && !isEventAdmin) {
      return "seat-available-later";
    }

    return "seat-available";
  };

  const seatClicked = (e: React.MouseEvent<SVGPathElement>): void => {
    const seatId = e.currentTarget.id;

    if (bookEventMode) {
      const seatIsBooked = bookingStore.activeBookings.some((booking) => {
        return (
          isSameDate(booking.bookingDateTime, bookingStore.displayDate) &&
          booking.seatId === Number(seatId)
        );
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

  //--------------- Zoom functionality ------------------
  //TODO: ta ut zoom
  const zoomToRoom = (roomName: Rooms): void => {
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
        newViewBox = zoomedToSalesViewBoxParameters;
        break;
      case Rooms.Marie:
        newViewBox = zoomedToMarieViewBoxParameters;
        break;
      case Rooms.Econ: 
        newViewBox = zoomedToEconOysteinViewBoxParameters;
        break;
      case Rooms.Oystein: 
        newViewBox = zoomedToEconOysteinViewBoxParameters;
        break;
      default:
        newViewBox = zoomedOutViewBoxParameters; // Default view
    }

    if (newViewBox === currentViewBox) {
      return;
    }

    setZoomStatus(ZoomStatus.Transition);
    animateViewBox(newViewBox, 500);

    // setCurrentViewBox(newViewBox);
  };

  const updateZoomStatus = useCallback(
    (newViewBox: string) => {
      switch (newViewBox) {
        case (zoomedToLargeRoomViewBoxParameters):
          setZoomStatus(ZoomStatus.Large)
          break;
        case (zoomedToSmallRoomViewBoxParameters):
          setZoomStatus(ZoomStatus.Small)
          break;
        case (zoomedToSalesViewBoxParameters):
          setZoomStatus(ZoomStatus.Sales)
          break;
        case (zoomedToMarieViewBoxParameters):
          setZoomStatus(ZoomStatus.Marie)
          break;
        case (zoomedToEconOysteinViewBoxParameters):
          setZoomStatus(ZoomStatus.EconOystein)
          break;
        default:
          setZoomStatus(ZoomStatus.ZoomedOut)
          break;
      }
    },
    []
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
    setZoomStatus(ZoomStatus.Transition);
    animateViewBox(zoomedOutViewBoxParameters, 500);
  }, [animateViewBox, zoomedOutViewBoxParameters]);

  useEffect(() => {
    if (location.pathname === "/overview") {
      zoomOut();
    }
  }, [location, zoomOut]);

  //--------------- End of zoom functionality -----------------

  return (
    <div>
      <MapComponent
        zoomStatus={zoomStatus}
        currentViewBox={currentViewBox}
        zoomToRoom={zoomToRoom}
        activeBookings={activeBookings}
        getSeatClassName={getSeatClassName}
        displayDate={displayDate}
        seatClicked={seatClicked}
        zoomOut={zoomOut}
      />
      {bookingStore.bookEventMode && (zoomStatus === ZoomStatus.Large || zoomStatus === ZoomStatus.Small) && 
        <ToggleSwitch isToggled={selectAllSeats} handleToggleChange={handleAllSeatsSelected}/>
      }
    </div>

  );
});

export default OverviewMap;
