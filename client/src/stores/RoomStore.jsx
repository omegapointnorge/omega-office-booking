import { makeAutoObservable } from "mobx";
import toast from "react-hot-toast";
import { Seat } from "../domain/seat";
class RoomStore {
  seats = [];

  openDialog = false;

  constructor() {
    makeAutoObservable(this);
  }

  async initializeRooms(roomId) {
    try {
      const url = `/api/Seat/${roomId}/seats`;

      const response = await fetch(url, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      });

      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }

      const data = await response.json();

      this.setSeats(data);
    } catch (error) {
      console.error(error);
    }
  }

  setSeats(data) {
    this.seats = data.value.map(
      (seat) => new Seat(seat.id, seat.roomId, seat.bookings)
    );
  }

  bookSeat(id, isTaken) {
    const seatToUpdate = this.seats.find((seat) => seat.id === id);

    if (seatToUpdate) {
      seatToUpdate.isTaken = isTaken;
      toast.success("Booked seat");
    } else {
      console.log(`Seat with ID ${id} not found.`);
      toast.error("Seat not found");
    }
  }

  /* DIALOG */
  handleOpenDialog = () => {
    this.openDialog = !this.openDialog;
  };

  handleCloseDialog = () => {
    this.openDialog = !this.openDialog;
  };
}

const roomStore = new RoomStore();
export default roomStore;
