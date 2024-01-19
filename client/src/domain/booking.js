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
  constructor(id, userId, seatId, bookingDateTime, roomId) {
    super(id, userId, seatId, bookingDateTime);
    this.roomId = roomId;
  }
}

export class CreateBookingRequest {
  constructor(seatId, bookingDateTime) {
    this.seatId = seatId
    this.bookingDateTime = bookingDateTime
  }
}

export class DeleteBookingRequest {
  constructor(bookingId) {
    this.bookingId = bookingId
  }
}
