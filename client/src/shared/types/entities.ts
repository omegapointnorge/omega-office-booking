/**
 * Former models
 */
export interface Booking {
  id: 0;
  userId: string;
  userName: string;
  seatId: 0;
  bookingDateTime: Date;
}

export interface Seat {
  //todo: id er nok number
  id: number | null;
  roomId: string | null;
  isAvailable: boolean;
  bookings: Booking[];
}

export interface Room {
  id: number | null;
  name: string | null;
  seats: Seat[];
}

export interface BookingRequest {
  seatId: number;
  bookingDateTime: Date;
  reCAPTCHAToken: string;
}

export interface EventBookingRequest {
  seatIds: number[];
  bookingDateTime: Date;
  isEvent: boolean;
}


