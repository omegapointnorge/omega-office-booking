export default class Booking {
  constructor(id = null, userId = null, userName = null, seatId = null, bookingDateTime = null) {
    this.id = id;
    this.userId = userId;
    this.userName = userName;
    this.seatId = seatId;
    this.bookingDateTime = new Date(bookingDateTime);
  }
}

export class MyBookingsResponse extends Booking {
  constructor(id, userId, userName, seatId, bookingDateTime, roomId) {
    super(id, userId, userName, seatId, bookingDateTime);
    this.roomId = roomId;
  }
}

export  class Room {
    constructor(id = null, name = null, seats = []) {
        this.id = id;
        this.name = name;
        this.seats = seats.map(seat => new Seat(seat.id, seat.roomId, seat.isAvailable, seat.bookings));
    }
}

export class Seat {
    constructor(id = null, roomId = null, isAvailable = true, bookings = []) {
        this.id = id;
        this.roomId = roomId;
        this.isAvailable = isAvailable;
        this.bookings = bookings;
    }
}

export class CreateBookingRequest {
  constructor(seatId, bookingDateTime) {
    this.seatId = seatId
    this.bookingDateTime = bookingDateTime.toISOString();
  }
}

export class DeleteBookingRequest {
  constructor(bookingId) {
    this.bookingId = bookingId
  }
}
