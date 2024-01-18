import { makeAutoObservable } from "mobx";
import Booking from '../domain/booking';
import toast from "react-hot-toast";
import ApiService from "./ApiService.jsx";

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
            const response = await ApiService.fetchData('/api/booking/bookings', 'Get');

            const data = await response.json();
            this.setActiveBookings(data);
        } catch (error) {
            console.error("Error fetching active bookings:", error);
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
               //refresh the page in case someone has booked the seat recently
                await this.fetchAllActiveBookings();

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

    //TODO try to reuse the same deletebooking logic like HistoryStore
    async deleteBooking(booking) {
        try {
            const bookingId = booking.bookingId;
            const url = "/api/Booking/" + bookingId;
            await ApiService.fetchData(url, "Delete");
            this.removeBookingById(bookingId);
        } catch (error) {
            console.error(error);
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
