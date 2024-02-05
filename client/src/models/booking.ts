import { Booking, BookingRequest } from "@/shared/types/entities";
// export default class Booking {
//   constructor(
//     id = null,
//     userId = null,
//     userName = null,
//     seatId = null,
//     bookingDateTime = null
//   ) {
//     this.id = id;
//     this.userId = userId;
//     this.userName = userName;
//     this.seatId = seatId;
//     this.bookingDateTime = new Date(bookingDateTime);
//   }
// }
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

// export class CreateBookingRequest {
//   constructor(seatId, bookingDateTime, reCAPTCHAToken) {
//     this.seatId = seatId;
//     this.bookingDateTime = bookingDateTime.toISOString();
//     this.reCAPTCHAToken = reCAPTCHAToken;
//   }
// }

export const createBookingRequest = ({
  seatId,
  bookingDateTime,
  reCAPTCHAToken,
}: BookingRequest) => {
  return {
    seatId,
    bookingDateTime: bookingDateTime.toISOString(),
    reCAPTCHAToken,
  };
};
// export class DeleteBookingRequest {
//   constructor(bookingId) {
//     this.bookingId = bookingId
//   }
// }

export const deleteBookingRequest = (id: { id: number }) => {
  //TODO: check if it exsist and then send deleteReq
  const bookingExist = true;
  if (bookingExist) {
    return id;
  }
};
