import { makeAutoObservable } from "mobx";
import toast from "react-hot-toast";

class RoomStore {
  seats = [];

  openDialog = false;

  constructor() {
    this.initialize();

    makeAutoObservable(this);
  }

  async initialize() {
    try {
      const url = "/api/Seat/seats";

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

      console.log(response);
      const data = await response.json();

      this.setSeats(data);
    } catch (error) {
      console.error(error);
    }
  }

  setSeats(data) {
    this.seats = data.value;
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

export default RoomStore;
