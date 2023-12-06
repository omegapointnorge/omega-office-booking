import { makeAutoObservable } from "mobx";
import toast from "react-hot-toast";
import { Seat } from "../domain/seat";
class RoomStore {
  seats = [];

  selectedSeatId = 0;

  isLoading = false;

  openDialog = false;

  constructor() {
    makeAutoObservable(this);
  }

  async initializeRooms(roomId) {
    try {
      this.setLoading();
      const url = `/api/Room/${roomId}/Seats`;

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
    } finally {
      this.setLoading();
    }
  }

  setSeats(data) {
    this.seats = data.value.map(
      (seat) => new Seat(seat.id, seat.roomId, seat.bookings)
    );
  }

  setSelectedSeat(seatId) {
    this.selectedSeatId = seatId;
  }

  setLoading() {
    this.isLoading = !this.isLoading;
  }

  bookSeat() {
    const seatToUpdate = this.seats.find(
      (seat) => seat.id === this.selectedSeatId
    );

    if (seatToUpdate) {
      seatToUpdate.isTaken = !seatToUpdate.isTaken;
      toast.success("Booked seat");
    } else {
      console.log(`Seat with ID ${this.selectedSeatId} not found.`);
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
