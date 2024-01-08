import { makeAutoObservable } from "mobx";
import { Booking } from "../domain/booking";
import toast from "react-hot-toast";
import ApiService from "./ApiService.jsx";

class HistoryStore {
  myUpcomingBookings = [];
  myPreviousBookings = [];
  openDialog = false;
  bookingIdToDelete = null;
  itemCount = 6;
  pageNumber;

  constructor() {
    this.fetchUpcomingBookings();
    this.fetchPreviousBookings(1, this.itemCount);
    makeAutoObservable(this);
  }

  async fetchUpcomingBookings() {
    try {
      const url = "/api/Booking/Bookings/MyUpcomingBookings";
      const response = await ApiService.fetchData(url, "Get", null);
      const data = await response.json();
      this.setUpcomingBookings(data);
    } catch (error) {
      console.error(error);
    }
  }

  async fetchPreviousBookings(pageNumber, itemCount) {
    try {
      const url = `/api/Booking/Bookings/MyPreviousBookings?pageNumber=${pageNumber}&itemCount=${itemCount}`;
      const response = await ApiService.fetchData(url, "Get", null);
      const data = await response.json();
      this.setPreviousBookings(data);
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

  setUpcomingBookings(data) {
    this.myUpcomingBookings = data.value.map(
        (booking) => new Booking(booking.id, booking.seatId, booking.dateTime)
    );
  }

  setPreviousBookings(data) {
    this.myPreviousBookings = data.value.map(
        (booking) => new Booking(booking.id, booking.seatId, booking.dateTime)
    );
  }

  deleteBooking(bookingId) {
    const bookingToDelete = this.myUpcomingBookings.find((booking) => booking.id === bookingId);
    if (bookingToDelete) {
      let newBookingList = this.myUpcomingBookings.filter(item => item !== bookingToDelete);
      // newBookingList = newBookingList.slice().reverse();
      this.myUpcomingBookings = newBookingList;
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