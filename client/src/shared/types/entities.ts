/**
 * Former models
 */
export interface Booking {
  id: number;
  userId: string;
  userName: string;
  seatId: number;
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

export interface SeatPath {
  id: string;
  d: string;
}
export interface SeatInRoom extends SeatPath {
  seatClicked: (e: React.MouseEvent<SVGPathElement>) => void;
  getSeatClassName: (seatId: number) => string;
  class?: string;
}
