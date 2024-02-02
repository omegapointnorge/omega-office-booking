/**
 * Models
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

interface Room {
  id: number | null;
  seats: Seat[];
  name?: string | null;
}

export interface BookingRequest {
  seatId: number;
  bookingDateTime: Date;
  reCAPTCHAToken: string;
}
