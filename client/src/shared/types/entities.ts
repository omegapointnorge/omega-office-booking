import { ZoomStatus } from "@/shared/types/enums";

/**
 * Former models
 */
export interface Booking {
  id: number;
  userId: string;
  userName: string;
  seatId: number;
  bookingDateTime: Date;
  eventName : string | null
}

export interface HistoryBooking  {
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
  roomId: number | null;
}

export interface BookingRequest {
  seatId: number;
  bookingDateTime: string;
  reCAPTCHAToken: string;
}

export interface EventBookingRequest {
  seatIds: number[];
  bookingDateTime: string;
  isEvent: boolean;
  eventName : string;
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

export interface Coordinates {
  x: number;
  y: number;
}

export interface RoomConfig {
  roomShapePath: string;
  roomMonitorPath: string;
  availabilityTextCoordinates?: Coordinates;
  seatMinId: number;
  seatMaxId: number;
  seatCount: number;
  fontSize: number;
  zoomedInStatus: ZoomStatus;
}
