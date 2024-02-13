import { makeAutoObservable } from "mobx";
import { createBooking } from "@models/booking";
import toast from "react-hot-toast";
import ApiService from "@services/ApiService";
import historyStore from "@stores/HistoryStore";
import { Booking, BookingRequest } from "@/shared/types/entities";
import { createEventBookingRequest } from "../../models/booking";

enum ApiStatus {
  Idle = "IDLE",
  Pending = "PENDING",
  Error = "ERROR",
  Success = "SUCCESS",
}
class BookingStore {
  activeBookings: Booking[] = [];
  userBookings = [];
  displayDate = new Date();
  bookEventMode = false;
  seatIdSelectedForNewEvent: number[] = [];
  isEventDateChosen: boolean = false;
  apiStatus: ApiStatus = ApiStatus.Idle;

  constructor() {
    makeAutoObservable(this);
  }

  async initialize() {
    await this.fetchAllActiveBookings();
  }

  async fetchAllActiveBookings() {
    if (this.apiStatus === ApiStatus.Pending) return;
    try {
      this.apiStatus = ApiStatus.Pending;

      const data = await ApiService.fetchData<Booking[]>(
        "/api/booking/activeBookings",
        "Get"
      ).then((response) => {
        this.apiStatus = ApiStatus.Success;
        return response;
      });

      const bookings = data.map((booking) => createBooking(booking));
      this.setActiveBookings(bookings);
    } catch (error) {
      console.error("Error fetching active bookings:", error);
      this.apiStatus = ApiStatus.Error;
    }
  }

  async createBookingRequest(seatId: number, reCAPTCHAToken: string) {
    if (this.apiStatus === ApiStatus.Pending) return;
    try {
      this.apiStatus = ApiStatus.Pending;

      const url = "/api/Booking/create";
      const bookingRequest: BookingRequest = {
        seatId,
        bookingDateTime: this.displayDate.toISOString(),
        reCAPTCHAToken,
      };
      const response = await ApiService.fetchData<Booking>(
        url,
        "POST",
        bookingRequest
      ).then((response) => {
        this.apiStatus = ApiStatus.Success;
        return response;
      });

      if (!response) {
        const errorText = `Failed to create booking`;
        toast.error("Error creating booking:" + errorText);
        //refresh the page in case someone has booked the seat recently
        await this.fetchAllActiveBookings();
      } else {
        const newBooking: Booking = {
          ...response,
          bookingDateTime: new Date(response.bookingDateTime),
        };

        // Update the store's state with the new booking
        this.activeBookings.push(newBooking);
        historyStore.myActiveBookings.unshift(newBooking);
      }
    } catch (error) {
      this.apiStatus = ApiStatus.Error;
      console.error("Error:", error);
    }
  }

  async createBookingForEvent(selectedSeatIds: number[]) {
    const bookingRequest = createEventBookingRequest({
      seatIds: selectedSeatIds,
      bookingDateTime: this.displayDate,
      isEvent: true,
    });

    try {
      const response = await fetch("/api/Booking/CreateEventBookingsForSeats", {
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
      }
      await this.fetchAllActiveBookings();
      this.resetCurrentEvent();
      historyStore.fetchMyBookings();
    } catch (error) {
      console.error("Error:", error);
    }
  }

  async deleteBooking(bookingId: number) {
    historyStore.deleteBooking(bookingId).then(() => {
      this.removeBookingById(bookingId);
      historyStore.removeBookingById(bookingId);
    });
  }
  removeBookingById(bookingId: number) {
    this.activeBookings = this.activeBookings.filter(
      (booking) => booking.id !== bookingId
    );
  }

  toggleSeatSelectionForNewEvent(seatId: number): void {
    const index = this.seatIdSelectedForNewEvent.indexOf(seatId);
    if (index !== -1) {
      this.seatIdSelectedForNewEvent.splice(index, 1);
    } else {
      this.seatIdSelectedForNewEvent.push(seatId);
    }
  }

  handleEventDate(date: Date) {
    const newDate = new Date(date);
    newDate.setHours(newDate.getHours() + 12);
    this.setDisplayDate(newDate);
    this.isEventDateChosen = true;
  }

  setDisplayDate(date: Date) {
    this.displayDate = date;
  }
  // Update active bookings
  setActiveBookings(bookings: Booking[]) {
    this.activeBookings = bookings;
  }

  toggleEventMode() {
    this.bookEventMode = !this.bookEventMode;

    if (!this.bookEventMode) {
      this.resetCurrentEvent();
    }
  }

  resetCurrentEvent() {
    this.seatIdSelectedForNewEvent = [];
    this.bookEventMode = false;
    this.isEventDateChosen = false;
  }
}

const bookingStore = new BookingStore();
export default bookingStore;
