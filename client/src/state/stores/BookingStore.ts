import { makeAutoObservable } from "mobx";
import { createBookingRequest } from "@models/booking";
import toast from "react-hot-toast";
import ApiService from "@services/ApiService";
import historyStore from "@stores/HistoryStore";
import { Booking } from "@/shared/types/entities";

class BookingStore {
  activeBookings: Booking[] = [];
  userBookings = [];
  displayDate = new Date();

  constructor() {
    this.initialize();
    makeAutoObservable(this);
  }

  async initialize() {
    await this.fetchAllActiveBookings();
  }

  // Fetch all active bookings
  async fetchAllActiveBookings() {
    try {
      const response = await ApiService.fetchData<Booking[]>(
        "/api/booking/activeBookings",
        "Get"
      );

      const bookingsAsJson = (await response.json()) as Booking[];
      //TODO: test
      console.log(" bookingsAsJson ", bookingsAsJson);
      const bookings = this.convertJsonObjectsToBookings(bookingsAsJson);

      this.setActiveBookings(bookings);
    } catch (error) {
      console.error("Error fetching active bookings:", error);
    }
  }

  async createBooking(seatId: number, reCAPTCHAToken: string) {
    const bookingRequest = createBookingRequest({
      seatId,
      bookingDateTime: this.displayDate,
      reCAPTCHAToken,
    });
    try {
      const response = await fetch("/api/Booking/create", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "Access-Control-Allow-Origin": "*",
        },
        body: JSON.stringify(bookingRequest),
      });

      if (!response.ok) {
        const errorText = await response.text();
        console.error("Error:", errorText);
        toast.error("Error creating booking:" + errorText);
        //refresh the page in case someone has booked the seat recently
        await this.fetchAllActiveBookings();
      } else {
        const newBookingJson = (await response.json()) as Booking;
        console.log("test - newBookingJson", newBookingJson);
        const newBooking: Booking = newBookingJson;
        // const newBookingData = newBookingJson.value;
        // const newBooking = new Booking(
        //   newBookingData.id,
        //   newBookingData.userId,
        //   newBookingData.userName,
        //   newBookingData.seatId,
        //   newBookingData.bookingDateTime
        // );

        // Update the store's state with the new booking
        this.activeBookings.push(newBooking);
        historyStore.myActiveBookings.unshift(newBooking);
      }
    } catch (error) {
      console.error("Error:", error);
    }
  }

  //TODO: try to reuse the same deletebooking logic like HistoryStore
  async deleteBooking(bookingId: number) {
    try {
      const url = `/api/Booking/${bookingId}`;

      const response = await ApiService.fetchData(url, "DELETE");

      if (!response.ok) {
        console.error(`Failed to delete booking with ID ${bookingId}`);
        return;
      }

      this.removeBookingById(bookingId);
      historyStore.removeBookingById(bookingId);
    } catch (error) {
      console.error(
        `An error occurred while deleting booking with ID ${bookingId}:`,
        error
      );
    }
  }

  removeBookingById(bookingId: number) {
    this.activeBookings = this.activeBookings.filter(
      (booking) => booking.id !== bookingId
    );
  }

  convertJsonObjectsToBookings(jsonArray: Booking[]) {
    return jsonArray;
    // return jsonArray.map(
    //   (obj) =>
    //     new Booking(
    //       obj.id,
    //       obj.userId,
    //       obj.userName,
    //       obj.seatId,
    //       obj.bookingDateTime
    //     )
    // );
  }

  setDisplayDate(date: Date) {
    this.displayDate = date;
  }
  // Update active bookings
  setActiveBookings(bookings: Booking[]) {
    this.activeBookings = bookings;
  }

  // Update user bookings
  //TODO: brukes denne?
  // setUserBookings(bookings: Booking[]) {
  //   this.userBookings = bookings;
  // }
}

const bookingStore = new BookingStore();
export default bookingStore;
