import { makeAutoObservable } from "mobx";
import { Booking } from "../domain/booking";
import toast from "react-hot-toast";

class HistoryStore {
  myBookings = [];

  constructor() {
    this.initialize();
    makeAutoObservable(this);
  }


  async initialize() {
    try {
      const url = "/api/Booking/Bookings/MyBookings";
      const response = await this.fetchData(url, "Get");
      const data = await response.json();
      this.setBookings(data);
    } catch (error) {
      console.error(error);
    }
  }

  async deleteBookingCall(bookingId) {
    try {
      const url = "/api/Booking/Bookings/" + bookingId;
      await this.fetchData(url, "Delete");
    } catch (error) {
      console.error(error);
    }
  }



  async fetchData(url, method) {
    const response = await fetch(url, {
      method: method,
      headers: {
        "Content-Type": "application/json",
        "Access-Control-Allow-Origin": "*",
      },
    });

    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }

    return response;
  }
  setBookings(data) {
    this.myBookings = data.value.map(
      (booking) => new Booking(booking.id, booking.seatId, booking.dateTime)
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
    } else {
      console.log(`bookingId:  ${bookingId} not found.`);
      toast.error("bookingId not found");
    }
  }

}

const historyStore = new HistoryStore();
export default historyStore;
