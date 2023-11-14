import { makeAutoObservable } from "mobx";
import toast from "react-hot-toast";
class RoomStore {
  rooms = [];

  seats = [];

  openDialog = false;
  constructor() {
    makeAutoObservable(this);
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

  setSeats(chosenSeats) {
    this.seats = chosenSeats;
  }

  /* DIALOG */
  handleOpenDialog = () => {
    this.openDialog = !this.openDialog;
  };

  handleCloseDialog = () => {
    this.openDialog = !this.openDialog;
  };
}

export default RoomStore;