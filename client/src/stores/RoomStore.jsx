import { makeAutoObservable } from "mobx";
import toast from "react-hot-toast";
import { Seat } from "../domain/seat";
import HistoryStore from "./HistoryStore.jsx";
import { Booking } from "../domain/booking";
import ApiService from "./ApiService.jsx";
class RoomStore {
  seats = [];

  isLoading = false;

  openDialog = false;

  constructor() {
    makeAutoObservable(this);
  }

  async initializeRooms(roomId) {
    try {
      this.isLoading = true;
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
      this.isLoading = false;
    }
  }

  async createBookings(req, seatToUpdate) {
    try {
      this.isLoading = true;
      const url = `/client/User/UpsertUserBooking`;

      const response = await ApiService.fetchData(url, "POST", req);
      const responseData = await response.json();
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }
      else if (responseData.error != null) {
        toast.error("Error: " + responseData.error);
      } else {
        // Extracting the booking entity
        const bookingId = responseData.userResponse.bookings[0].id;
        const seatId = responseData.userResponse.bookings[0].seatId;
        const dateTime = responseData.userResponse.bookings[0].dateTime;
        HistoryStore.myBookings.unshift(new Booking(bookingId, seatId, dateTime));
        seatToUpdate.isTaken = true;
        toast.success("Booked Seat OK with Seat " + seatId);
      }

    } catch (error) {
      console.error(error);
    } finally {
      this.isLoading = false;
    }
  }
  setSeats(data) {
    this.seats = data.value.map(
      (seat) => new Seat(seat.id, seat.roomId, seat.bookings)
    );
  }

  bookSeat(req) {
    const seatToUpdate = this.seats.find((seat) => seat.id === req.SeatId);

    if (seatToUpdate) {
      this.createBookings(req, seatToUpdate);
      // toast.success("Booked seat");
    } else {
      console.log(`Seat with ID ${req.SeatId} not found.`);
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
