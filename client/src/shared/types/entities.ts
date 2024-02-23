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

export interface HistoryBooking {
  id: number;
  userId: string;
  userName: string;
  seatIds: number[]; 
  roomIds: number[]; 
  eventName?: string; 
  bookingDateTime: Date;
}


export interface Seat {
  id: number | null;
  roomId: string | null;
  isAvailable: boolean;
  bookings: Booking[];
}

export interface BookingRequest {
  seatId: number;
  bookingDateTime: string;
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
