import { createSeat } from "@models/seat";
import { Seat, Room } from "@/shared/types/entities";

// export default class Room {
//   constructor(id = null, name = null, seats = []) {
//       this.id = id;
//       this.name = name;
//       this.seats = seats.map(seat => new Seat(seat.id, seat.roomId, seat.isAvailable, seat.bookings));
//   }
// }

export const createRoom = (
  id: number | null = null,
  name: string | null = null,
  seats: Seat[] = []
): Room => {
  return {
    id,
    name,
    //TODO: tror dette er unødvendig
    seats: seats.map(({ id, roomId, isAvailable, bookings }) =>
      createSeat({ id, roomId, isAvailable, bookings })
    ),
  };
};
