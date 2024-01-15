import { makeAutoObservable } from "mobx";
import Booking from "../domain/booking";
import ApiService from "./ApiService.jsx";

const ITEMS_PER_PAGE = 5;

class HistoryStore {
  myActiveBookings = [];
  myPreviousBookings = [];
  myPreviousBookingsCurrentPage = [];
  openDialog = false;
  bookingIdToDelete = null;
  
  pageNumber = 1;
  lastPage;
  isFirstPage = true;
  isLastPage = false;
  isLoading = false;

  constructor() {
    this.initBookings();
    makeAutoObservable(this);
  }

   async initBookings() {
    try {
      this.isLoading = true;
      await this.fetchActiveBookings();
      await this.fetchPreviousBookings();
      this.initPreviousBookings();
      
    } catch (error) {
      console.error(error);
    }
    finally { 
      this.isLoading = false;
  }
}

  async fetchActiveBookings() {
    try {
      const url = "/api/Booking/Bookings/MyActiveBookings";
      const response = await ApiService.fetchData(url, "Get", null);
      const data = await response.json();
      this.myActiveBookings = data.value.map(
      (booking) => new Booking(booking.id, booking.userId, booking.seatId, booking.bookingDateTime)
    );
    } catch (error) {
        console.error("Error active bookings:", error);
    }
  }

  async fetchPreviousBookings() {
    try {
      const url = `/api/Booking/Bookings/MyPreviousBookings`;
      const response = await ApiService.fetchData(url, "Get", null);
      const data = await response.json();
      this.myPreviousBookings = data.value.map(
        (booking) => new Booking(booking.id, booking.userId, booking.seatId, booking.bookingDateTime)
    );

    this.lastPage = Math.ceil(this.myPreviousBookings.length / ITEMS_PER_PAGE);    
    } catch (error) {
        console.error("Error fetching previous bookings:", error);
    }
  }

  async deleteBooking(bookingId) {
    try {
      const url = "/api/Booking/Bookings/" + bookingId;
      await ApiService.fetchData(url, "Delete");
    } catch (error) {
      console.error(error);
    }
  }

  setIsFirstPage(data) {
    this.isFirstPage = data;
  }

   setIsLastPage(data) {
    this.isLastPage = data;
  }

  setLastPage(data) {
    this.lastPage = data;
  }

  navigatePrevious() {
    if (this.pageNumber > 1) {
      this.pageNumber -= 1;
      this.updateNavigation();
    }
  }

  navigateNext() {
    if (this.pageNumber < this.lastPage) {
      this.pageNumber += 1;
      this.updateNavigation();
    }
  }

  updateNavigation() {
    this.setIsFirstPage(this.pageNumber === 1);
    this.setIsLastPage(this.pageNumber === this.lastPage);
    const startIndex = (this.pageNumber - 1) * ITEMS_PER_PAGE;
    const endIndex = this.pageNumber * ITEMS_PER_PAGE;
    this.myPreviousBookingsCurrentPage = this.myPreviousBookings.slice(startIndex, endIndex);
  }

  get isEmpty() {
    return this.myActiveBookings.length === 0 && this.myPreviousBookings.length === 0;
  }

  /* Utils */
  handleOpenDialog(bookingId) {
    this.openDialog = !this.openDialog;
    this.bookingIdToDelete = bookingId;
  };

  handleCloseDialog = () => {
    this.openDialog = !this.openDialog;
  };

  initPreviousBookings() {

    if (this.myPreviousBookings.length > ITEMS_PER_PAGE) {
      this.setIsLastPage(false);
      this.myPreviousBookingsCurrentPage = this.myPreviousBookings.slice(0, ITEMS_PER_PAGE);
    } 
    else {
      this.setIsLastPage(true);
      this.myPreviousBookingsCurrentPage = this.myPreviousBookings;
    }
  }

}

const historyStore = new HistoryStore();
export default historyStore;
