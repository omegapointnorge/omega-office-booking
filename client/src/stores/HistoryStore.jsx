import { makeAutoObservable } from "mobx";
import { Booking } from "../domain/booking";
import toast from "react-hot-toast";
import ApiService from "./ApiService.jsx";
import {parse} from "date-fns";

class HistoryStore {
  myBookings = [];
  myUpcomingBookings = [];
  myEarlierBookings = [];
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
        (booking) => new Booking(booking.id, booking.seatId, booking.dateTime)
    );
    let pastBookings = []
    let futureBookings = []
    this.myBookings.forEach((booking) => {
      console.log("booking datetime", booking.dateTime)
      const givenDate = parse(booking.dateTime, 'dd/MM/yyyy HH:mm:ss', new Date());
      const currentDate = new Date()
      console.log("test")
      console.log(currentDate)
      if (givenDate <  currentDate){
        pastBookings.push(booking)
      }
      else futureBookings.unshift(booking)
    })
    this.myEarlierBookings = pastBookings
    this.myUpcomingBookings = futureBookings
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