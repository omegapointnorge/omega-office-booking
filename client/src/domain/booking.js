export default class Booking {
  constructor(id = null, userId = null, userName = null, seatId = null, bookingDateTime = null) {
    this.id = id;
    this.userId = userId;
    this.userName = userName;
    this.seatId = seatId;
    this.bookingDateTime = new Date(bookingDateTime);
  }
}

export class CreateBookingRequest {
  constructor(seatId, bookingDateTime, reCAPTCHAToken) {
    this.seatId = seatId;
    this.bookingDateTime = bookingDateTime.toISOString();
    this.reCAPTCHAToken = reCAPTCHAToken;
  }
}

export class DeleteBookingRequest {
  constructor(bookingId) {
    this.bookingId = bookingId
  }
}
