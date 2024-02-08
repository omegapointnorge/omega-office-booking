import { makeAutoObservable } from "mobx";
import { createBooking, createBookingRequest } from "@models/booking";
import toast from "react-hot-toast";
import ApiService from "@services/ApiService";
import historyStore from "@stores/HistoryStore";
import { Booking } from "@/shared/types/entities";
import { createEventBookingRequest } from "../../models/booking";

class BookingStore {
  activeBookings: Booking[] = [];
  userBookings = [];
  displayDate = new Date();
  bookEventMode = false
  seatIdSelectedForNewEvent : number[] = [];
  isEventDateChosen : boolean = false;

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
      const data = await ApiService.fetchData<Booking[]>(
        "/api/booking/activeBookings",
        "Get"
      );

      const bookings = data.map((booking) => createBooking(booking));
      this.setActiveBookings(bookings);
    } catch (error) {
      console.error("Error fetching active bookings:", error);
    }
  }

  async createBooking(seatId: number, reCAPTCHAToken: string) {
    try {
      const url = "/api/Booking/create";
      const bookingRequest = createBookingRequest({
        seatId,
        bookingDateTime: this.displayDate,
        reCAPTCHAToken,
      });

      const response = await ApiService.fetchData<Booking>(
        url,
        "POST",
        bookingRequest
      );

      if (!response) {
        const errorText = `Failed to create booking`;
        console.error("Error:", errorText);
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
      console.error("Error:", error);
    }
  }

  async createBookingForEvent(selectedSeatIds : number[]) {
      const bookingRequest = createEventBookingRequest({ seatIds: selectedSeatIds, bookingDateTime: this.displayDate, isEvent : true });

        try {
            const response = await fetch('/api/Booking/CreateEventBookingsForSeats', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    "Access-Control-Allow-Origin": "*",
                },
                body: JSON.stringify(bookingRequest),
            });

            if (!response.ok) {
                const errorText = await response.text();
                console.error('Error:', errorText);
                toast.error("Error creating booking:" + errorText);
               //refresh the page in case someone has booked the seat recently
                await this.fetchAllActiveBookings();

            }
            else {
                await this.fetchAllActiveBookings();
                this.resetCurrentEvent()
                // historyStore refresh history
            }

        } catch (error) {
            console.error('Error:', error);
        }
    }


  //TODO: try to reuse the same deletebooking logic like HistoryStore
  async deleteBooking(bookingId: number) {
    try {
      const url = `/api/Booking/${bookingId}`;

      const response = await ApiService.fetchData<undefined>(url, "DELETE");

      if (!response) {
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

  toggleSeatSelectionForNewEvent(seatId: number): void {
    const index = this.seatIdSelectedForNewEvent.indexOf(seatId);
    if (index !== -1) {
        this.seatIdSelectedForNewEvent.splice(index, 1);
    } else {
        this.seatIdSelectedForNewEvent.push(seatId);
    }
}

  handleEventDate(date : Date){
    const newDate = new Date(date);
    newDate.setHours(newDate.getHours() + 12); 
    this.setDisplayDate(newDate)
    this.isEventDateChosen = true
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
    
    if(!this.bookEventMode){
      this.resetCurrentEvent()
    }
  }

  resetCurrentEvent(){
    this.seatIdSelectedForNewEvent = []
    this.bookEventMode = false
    this.isEventDateChosen  = false;
  }

  // Update user bookings
  //TODO: brukes denne?
  // setUserBookings(bookings: Booking[]) {
  //   this.userBookings = bookings;
  // }
}

const bookingStore = new BookingStore();
export default bookingStore;
