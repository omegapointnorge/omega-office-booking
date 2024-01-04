import { makeAutoObservable } from "mobx";
import { Booking } from "../domain/booking";
import toast from "react-hot-toast";
import ApiService from "./ApiService.jsx";

class HistoryStore {
  myBookings = [];
  openDialog = false;
  bookingIdToDelete = null;

  constructor() {
    this.initialize();
    makeAutoObservable(this);
  }


  async initialize() {
    try {
      const url = "/api/Booking/Bookings/MyBookings";
      const response = await ApiService.fetchData(url, "Get", null);
      const data = await response.json();
      this.setBookings(data);
    } catch (error) {
      console.error(error);
    }
  }

  async deleteBookingCall(bookingId) {
    try {
      const url = "/api/Booking/Bookings/" + bookingId;
      await ApiService.fetchData(url, "Delete");
    } catch (error) {
      console.error(error);
    }
  }




  setBookings(data) {
    this.myBookings = data.value.map(
      (booking) => new Booking(booking.roomName, booking.bookingDto.id, booking.bookingDto.seatId, booking.bookingDto.dateTime)
    );
  }
  deleteBooking(bookingId) {
    const bookingToDelete = this.myBookings.find((booking) => booking.id === bookingId);
    if (bookingToDelete) {
      let newBookingList = this.myBookings.filter(item => item !== bookingToDelete);
      // newBookingList = newBookingList.slice().reverse();
      this.myBookings = newBookingList;
      this.deleteBookingCall(bookingId)
      toast.success("Booking deleted for Booking Nr: " + bookingId);
      this.handleCloseDialog();
    } else {
      console.log(`bookingId:  ${bookingId} not found.`);
      toast.error("bookingId not found");
    }
  }
  /* DIALOG */
  handleOpenDialog(bookingId) {
    this.openDialog = !this.openDialog;
    this.bookingIdToDelete = bookingId;
  };

  handleCloseDialog = () => {
    this.openDialog = !this.openDialog;
  };

}

const historyStore = new HistoryStore();
export default historyStore;
