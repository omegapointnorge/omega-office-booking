import ApiService from "../Services/ApiService.jsx";

class BookingService{
    
    async fetchMyBookings() {
        try {
            const url = "/api/Booking/Bookings/MyBookings";
            const response = await ApiService.fetchData(url, "Get", null);
            const data = await response.json();
            
            return data;

        } catch (error) {
            console.error("Error fetching bookings:", error);
        }
    }

}

const bookingService = new BookingService();
export default bookingService;