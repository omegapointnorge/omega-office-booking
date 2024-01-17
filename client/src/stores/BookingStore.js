import { makeAutoObservable } from "mobx";
import Booking from '../domain/booking';
import toast from "react-hot-toast";

class BookingStore {
    activeBookings = [];
    userBookings = [];

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
            const response = await fetch('/api/booking/bookings');

            if (!response.ok) {
                throw new Error('Failed to fetch active bookings');
            }

            const data = await response.json();
            this.setActiveBookings(data);
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
                const errorText = await response.text();
                console.error('Error:', errorText);
                toast.error("Error creating booking:" + errorText);
                //throw new Error('Failed to create booking, check if the user has another booking');
            }
            else {
                const newBookingJson = await response.json();
                const newBookingData = newBookingJson.value
                const newBooking = new Booking(newBookingData.id, newBookingData.userId, newBookingData.seatId, newBookingData.bookingDateTime);

                // Update the store's state with the new booking
                this.activeBookings.push(newBooking);
            }

        } catch (error) {
            console.error('Error:', error);
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
}

const bookingStore = new BookingStore();
export default bookingStore;
