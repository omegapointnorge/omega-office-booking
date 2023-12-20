import { makeAutoObservable } from "mobx";
import { Booking } from "../domain/booking";

class HistoryStore {
  myBookings = [];

  constructor() {
    this.initialize();
    makeAutoObservable(this);
  }


  async initialize() {
    try {
      const url = "/api/Booking/bookings";

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

      // this.setBookings(data);
    } catch (error) {
      console.error(error);
    }
  }
  setBookings(data) {Booking
    this.myBookings = data.value.map(
      (booking) => new Booking(booking.id, booking.seatId, booking.dateTime)
    );
  }
  getRouteName(route) {
    return route.replace(/\s+/g, "-").toLowerCase();
  }
}

const historyStore = new HistoryStore();
export default historyStore;
