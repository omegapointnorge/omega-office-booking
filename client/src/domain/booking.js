export default class Booking {
  constructor(id = null, userId = null, seatId = null, bookingDateTime = null) {
    this.id = id;
    this.userId = userId;
    this.seatId = seatId;
    this.bookingDateTime = new Date(bookingDateTime);
  }
}

export class CreateBookingRequest {
  constructor(seatId) {
    this.seatId = seatId
  }
}

export class DeleteBookingRequest {
  constructor(bookingId) {
    this.bookingId = bookingId
  }
}
