import { Booking } from "@/shared/types/entities";
import { EventBookingRequest } from "../shared/types/entities";

export const createBooking = ({
  id,
  userId,
  userName,
  seatId,
  bookingDateTime,
}: Booking) => {
  return {
    id,
    userId,
    userName,
    seatId,
    bookingDateTime: new Date(bookingDateTime),
  };
};

export const createEventBooking = ({
  seatIds,
  bookingDateTime,
  eventName,
}: EventBookingRequest) => {
  const isEvent = true;
  return {
    SeatList: seatIds,
    bookingDateTime: bookingDateTime,
    isEvent: isEvent,
    eventName : eventName
  };
};
