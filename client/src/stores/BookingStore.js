import { makeAutoObservable } from "mobx";
import { CreateBookingRequest } from "../domain/booking";
import Booking from '../domain/booking';
import toast from "react-hot-toast";
import ApiService from "./ApiService.jsx";
import historyStore from "./HistoryStore";

class BookingStore {
  activeBookings = [];
  userBookings = [];
  displayDate = new Date()

  constructor() {
    this.initialize()
    makeAutoObservable(this);
  }

  async initialize() {
    await this.fetchAllActiveBookings()
  }

  // Fetch all active bookings
  async fetchAllActiveBookings() {
    try {
      const response = await ApiService.fetchData('/api/booking/activeBookings', 'Get');

      const bookingsAsJson = await response.json();
      const bookings = this.convertJsonObjectsToBookings(bookingsAsJson)

      this.setActiveBookings(bookings);
    } catch (error) {
      console.error("Error fetching active bookings:", error);
    }
  }


    async createBooking(selectedSeatId) {
      const bookingRequest = new CreateBookingRequest(selectedSeatId, this.displayDate);
        try {
            const response = await fetch('/api/Booking/create', {
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
                const newBookingJson = await response.json();
                const newBookingData = newBookingJson.value
                const newBooking = new Booking(newBookingData.id, newBookingData.userId, newBookingData.userName, newBookingData.seatId, newBookingData.bookingDateTime);
                // Update the store's state with the new booking
                this.activeBookings.push(newBooking);
                historyStore.myActiveBookings.unshift(newBooking)
            }

        } catch (error) {
            console.error('Error:', error);
        }
    }

    //TODO try to reuse the same deletebooking logic like HistoryStore
    async deleteBooking(booking) {
      try {
          const bookingId = booking.bookingId;
          const url = `/api/Booking/${bookingId}`;
          
          const response = await ApiService.fetchData(url, "DELETE");
  
          if (!response.ok) {
              console.error(`Failed to delete booking with ID ${bookingId}`);
              return;
          }
  
          this.removeBookingById(bookingId);
          historyStore.removeBookingById(bookingId);
      } catch (error) {
          console.error(`An error occurred while deleting booking with ID ${booking.bookingId}:`, error);
      }
  }

  removeBookingById(bookingId) {
    this.activeBookings = this.activeBookings.filter(booking => booking.id !== bookingId);
  }

  convertJsonObjectsToBookings(jsonArray) {
    return jsonArray.map(obj => new Booking(obj.id, obj.userId, obj.userName, obj.seatId, obj.bookingDateTime));
  }

  setDisplayDate(date) {
    this.displayDate = date;
  }
    // Update active bookings
    setActiveBookings(bookings) {
        this.activeBookings = bookings;
    }

    // Update user bookings
    setUserBookings(bookings) {
        this.userBookings = bookings;
    }

}

const bookingStore = new BookingStore();
export default bookingStore;
