import { makeAutoObservable } from "mobx";
import Booking from "../domain/booking"

class BookingStore {
  activeBookings = [];
  userBookings = [];

  constructor() {
    makeAutoObservable(this);
  }

  // Fetch all active bookings
  async fetchAllActiveBookings() {
    try {
      const response = await fetch('/api/bookings');
      if (!response.ok) throw new Error('Failed to fetch active bookings');
      const data = await response.json();
      this.setActiveBookings(data);
    } catch (error) {
      console.error("Error fetching active bookings:", error);
    }
  }

  // Fetch all bookings for a specific user
  async fetchUserBookings(userId) {
    try {
      const response = await fetch(`/api/Booking/Bookings/MyBookings`);
      if (!response.ok) throw new Error('Failed to fetch user bookings');
      const data = await response.json();
      this.setUserBookings(data);
    } catch (error) {
      console.error("Error fetching user bookings:", error);
    }
  }

  async createBooking(bookingDetails) {
    try {
      const response = await fetch('/api/bookings/create', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(bookingDetails),
      });

      if (!response.ok) {
        throw new Error('Failed to create booking');
      }

      const newBookingData = await response.json();
      const newBooking = new Booking(newBookingData.id, newBookingData.userId, newBookingData.date);

      // Update the store's state with the new booking
      this.activeBookings.push(newBooking);

      // Optionally, if the new booking is for the current user, update userBookings as well
      // if (newBooking.userId === currentUser.id) {
      //   this.userBookings.push(newBooking);
      // }
    } catch (error) {
      console.error("Error creating booking:", error);
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
}

const bookingStore = new BookingStore();
export default bookingStore;
