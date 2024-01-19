import { makeAutoObservable } from "mobx";
import Booking from '../domain/booking';

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
      const response = await fetch('/api/booking/activeBookings');
      
      if (!response.ok) { 
        throw new Error('Failed to fetch active bookings');
      }
      
      const bookingsAsJson = await response.json();
      const bookings = this.convertJsonObjectsToBookings(bookingsAsJson)
      this.setActiveBookings(bookings);
    } catch (error) {
      console.error("Error fetching active bookings:", error);
    }
  }

  // Fetch all bookings for a specific user
  async fetchUserBookings(userId) {
    try {
      const response = await fetch(`/api/Booking/Bookings/MyActiveBookings`);

      if (!response.ok) {
        throw new Error('Failed to fetch user bookings');
      }

      const data = await response.json();
      this.setUserBookings(data);
    } catch (error) {
      console.error("Error fetching user bookings:", error);
    }
  }

  async createBooking(bookingRequest) {
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
        throw new Error('Failed to create booking');
      }
      const newBookingJson = await response.json();
      const newBookingData = newBookingJson.value
      const newBooking = new Booking(newBookingData.id, newBookingData.userId, newBookingData.userName, newBookingData.seatId, newBookingData.bookingDateTime);

      // Update the store's state with the new booking
      this.activeBookings.push(newBooking);

    } catch (error) {
      console.error("Error creating booking:", error);
    }
  }

  async deleteBooking(deleteBookingRequest) {
    try {
      const url = `/api/Booking/${deleteBookingRequest.bookingId}`;
      const response = await fetch(url, {
        method: 'DELETE',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if (!response.ok) {
        throw new Error(`Failed to delete booking: ${response.status}`);
      }

      this.removeBookingById(deleteBookingRequest.bookingId)

    } catch (error) {
      console.error("Error deleting booking:", error);
    }
  }


  // Update active bookings
  setActiveBookings(bookings) {
    this.activeBookings = bookings;
  }

  // Update user bookings
  setUserBookings(bookings) {
    this.userBookings = bookings;
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
}

const bookingStore = new BookingStore();
export default bookingStore;
