import React, { useCallback, useState } from "react";
import { SeatPath } from "@/shared/types/entities";
import { SeatInRoom } from "@/shared/types/entities";
import bookingStore from "@stores/BookingStore";
import { isSameDate } from "@utils/utils";
import { useAuthContext } from "@auth/useAuthContext";

interface WorkRoomProps {
  className?: string;
  seats: SeatPath[];
  seatClicked: (e: React.MouseEvent<SVGPathElement>) => void;
}
const Seat = ({ id, d, seatClicked }: SeatInRoom) => {
  const [isSeatClicked, setIsSeatClicked] = useState(false);

  const { user } = useAuthContext() ?? {};

  const isEventAdmin = user.claims.role === "EventAdmin";
  const userId = user.claims.objectidentifier;


  console.log("selected seats", bookingStore.seatIdSelectedForNewEvent);
  

  const handleSeatClicked = (
    e: React.MouseEvent<SVGPathElement, MouseEvent>
  ) => {
    setIsSeatClicked(!isSeatClicked);
    seatClicked(e);
  };

  const getSeatClassName = useCallback((seatId: number): string => {
    console.log("INSIDE CALLBACK", bookingStore.seatIdSelectedForNewEvent);
    // setIsSeatClicked(true);
    
    const bookingForSeat = bookingStore.activeBookings.find(
      (booking) =>
        booking.seatId === seatId &&
        isSameDate(bookingStore.displayDate, booking.bookingDateTime)
    );

    console.log(bookingStore.bookEventMode);
    
    if (bookingStore.bookEventMode) {
      console.log(bookingStore.seatIdSelectedForNewEvent);
      console.log(typeof seatId);
      bookingStore.seatIdSelectedForNewEvent.forEach(e => {console.log(typeof e)});
      
      
      if (bookingStore.isSeatSelectedForEvent(seatId)) {
        console.log("here");
        
        return "seat-selected-for-event";
      }
    }

    if (bookingForSeat) {
      return bookingForSeat.userId === userId
        ? "seat-booked-by-user"
        : "seat-booked";
    }

    const isAnySeatBookedByUser = bookingStore.activeBookings.some(
      (booking) =>
        booking.userId === userId &&
        isSameDate(bookingStore.displayDate, booking.bookingDateTime)
    );

    if (isAnySeatBookedByUser && !isEventAdmin) {
      return "seat-unavailable";
    }

    if (!bookingStore.hasBookingOpened(bookingStore.displayDate) && !isEventAdmin) {
      return "seat-available-later";
    }
    
    return "seat-available";
  }, [isEventAdmin, userId, bookingStore])

  const seatclassname = getSeatClassName(Number(id))
  console.log(seatclassname);
  

  return (
    <path
      key={id}
      id={id}
      className={seatclassname}
      onClick={handleSeatClicked}
      d={d}
    />
  );
};

export const WorkRoom = ({ seats, ...props }: WorkRoomProps) => (
  <g
    className={props.className}
    stroke="#000"
    strokeLinecap="round"
    strokeLinejoin="round"
    strokeWidth="5"
  >
    {seats.map(({ id, d }) => (
      <Seat key={id} id={id} d={d} {...props} />
    ))}
  </g>
);
