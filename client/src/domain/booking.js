export default class Booking {
  constructor(userId, seatId, bookingDateTime) {
    this.userId = userId;
    this.seatId = seatId;
    this.bookingDateTime = bookingDateTime;
  }
}

export class BookingRequest {
  constructor(seatId) {
    this.seatId = seatId
  }
}
