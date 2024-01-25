import Seat from "./seat";

export default class Room {
  constructor(id = null, name = null, seats = []) {
      this.id = id;
      this.name = name;
      this.seats = seats.map(seat => new Seat(seat.id, seat.roomId, seat.isAvailable, seat.bookings));
  }
}