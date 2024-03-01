import { makeAutoObservable } from "mobx";
import { createBooking } from "@models/booking";
import toast from "react-hot-toast";
import ApiService from "@services/ApiService";
import historyStore from "@stores/HistoryStore";
import { Booking, BookingRequest } from "@/shared/types/entities";
import { createEventBooking } from "../../models/booking";
import { ApiStatus } from "@/shared/types/enums";
import {
  fetchOpeningTimeOfDay,
  getEarliestAllowedBookingDate,
} from "@utils/utils";

class BookingStore {
  activeBookings: Booking[] = [];
  displayDate = new Date();
  bookEventMode = false;
  seatIdSelectedForNewEvent: number[] = [];
  isEventDateChosen: boolean = false;
  apiStatus: ApiStatus = ApiStatus.Idle;
  openingTime: string | undefined;
  unavailableSeatsIds: number[] = [16, 19, 20, 21, 22];


  constructor() {
    makeAutoObservable(this);
  }

  async initialize() {
    await this.fetchAllActiveBookings();
    this.openingTime = await fetchOpeningTimeOfDay();
  }

  async fetchAllActiveBookings() {
    if (this.apiStatus === ApiStatus.Pending) return;
    try {
      this.setApiStatus(ApiStatus.Pending);

      const data = await ApiService.fetchData<Booking[]>(
        "/api/booking/activeBookings",
        "Get"
      ).then((response) => {
        this.setApiStatus(ApiStatus.Success);
        return response;
      });

      const bookings = data.map((booking) => createBooking(booking));
      this.setActiveBookings(bookings);
    } catch (error) {
      console.error("Error fetching active bookings:", error);
      this.setApiStatus(ApiStatus.Error);
    }
  }

  async createBookingRequest(seatId: number, reCAPTCHAToken: string) {
    if (this.apiStatus === ApiStatus.Pending) return;
    try {
      this.setApiStatus(ApiStatus.Pending);

      const url = "/api/Booking/create";
      const bookingDateTimeStandard = this.convertToStandardBookingDateTime(this.displayDate);
      const bookingRequest: BookingRequest = {
        seatId,
        bookingDateTime: bookingDateTimeStandard.toISOString(),
        reCAPTCHAToken,
      };
      const response = await ApiService.fetchData<Booking>(
        url,
        "POST",
        bookingRequest
      ).then((response) => {
        this.setApiStatus(ApiStatus.Success);
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
      }
    } catch (error) {
      this.setApiStatus(ApiStatus.Error);
      console.error("Error:", error);
    }
  }

    async createBookingForEvent(selectedSeatIds: number[], eventName: string ) {
    const bookingRequest = createEventBooking({
      seatIds: selectedSeatIds,
      bookingDateTime: this.displayDate.toISOString(),
      isEvent: true,
      eventName: eventName || "Arrangement",
    });

    try {
      const response = await fetch("/api/Event", {
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
    this.setDisplayDate(this.convertToStandardBookingDateTime(date));
    this.isEventDateChosen = true;
  }

  convertToStandardBookingDateTime(date: Date) {
    const newDate = new Date(date);
    newDate.setHours(12, 0, 0, 0);
    return newDate;
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

  setApiStatus(status: ApiStatus) {
    this.apiStatus = status;
  }

  hasBookingOpened = (displayDate: Date): boolean => {
    if (!this.openingTime) {
      return false;
    }

    let bookingOpeningTime = getEarliestAllowedBookingDate(displayDate);

    const [hour, minutes, seconds] = this.openingTime.split(":").map(Number);

    bookingOpeningTime.setHours(hour, minutes, seconds);

    let currentDateTime = new Date();
    return currentDateTime > bookingOpeningTime;
  };
}

const bookingStore = new BookingStore();
export default bookingStore;
