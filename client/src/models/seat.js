export default class Seat {
  constructor(id = null, roomId = null, isAvailable = true, bookings = []) {
      this.id = id;
      this.roomId = roomId;
      this.isAvailable = isAvailable;
      this.bookings = bookings;
  }
}