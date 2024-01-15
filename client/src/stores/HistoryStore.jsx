import { makeAutoObservable } from "mobx";
import Booking from "../domain/booking";
import toast from "react-hot-toast";
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

  constructor() {
    this.fetchActiveBookings();
    this.initPreviousBookings();
    makeAutoObservable(this);
  }

  async fetchActiveBookings() {
    try {
      const url = "/api/Booking/Bookings/MyActiveBookings";
      const response = await ApiService.fetchData(url, "Get", null);
      const data = await response.json();
      this.setActiveBookings(data);
    } catch (error) {
        console.error("Error active bookings:", error);
    }
  }

  async fetchPreviousBookings() {
    try {
      const url = `/api/Booking/Bookings/MyPreviousBookings`;
      const response = await ApiService.fetchData(url, "Get", null);
      const data = await response.json();
      this.setPreviousBookings(data);
    } catch (error) {
        console.error("Error fetching previous bookings:", error);
    }
  }

  async initPreviousBookings() {
    await this.fetchPreviousBookings();
    this.setLastPage(Math.ceil(this.myPreviousBookings.length / ITEMS_PER_PAGE))

    if (this.myPreviousBookings.length > ITEMS_PER_PAGE) {
      this.setIsLastPage(false);
      this.myPreviousBookingsCurrentPage = this.myPreviousBookings.slice(0, ITEMS_PER_PAGE);
    } 
    else {
      this.setIsLastPage(true);
      this.myPreviousBookingsCurrentPage = this.myPreviousBookings;
    }
}

  async deleteBookingCall(bookingId) {
    try {
      const url = "/api/Booking/Bookings/" + bookingId;
      await ApiService.fetchData(url, "Delete");
    } catch (error) {
      console.error(error);
    }
  }

  setActiveBookings(data) {
    this.myActiveBookings = data.value.map(
      (booking) => new Booking(booking.id, booking.userId, booking.seatId, booking.bookingDateTime)
    );
  }

  setPreviousBookings(data) {
    this.myPreviousBookings = data.value.map(
        (booking) => new Booking(booking.id, booking.userId, booking.seatId, booking.bookingDateTime)
    );
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

  deleteBooking(bookingId) {
    const bookingToDelete = this.myActiveBookings.find((booking) => booking.id === bookingId);
    if (bookingToDelete) {
      let newBookingList = this.myActiveBookings.filter(item => item !== bookingToDelete);
      // newBookingList = newBookingList.slice().reverse();
      this.myActiveBookings = newBookingList;
      this.deleteBookingCall(bookingId)
      toast.success("Booking deleted for Booking Nr: " + bookingId);
      this.handleCloseDialog();
    } else {
      console.log(`bookingId:  ${bookingId} not found.`);
      toast.error("bookingId not found");
    }
  }
  /* DIALOG */
  handleOpenDialog(bookingId) {
    this.openDialog = !this.openDialog;
    this.bookingIdToDelete = bookingId;
  };

  handleCloseDialog = () => {
    this.openDialog = !this.openDialog;
  };

}

const historyStore = new HistoryStore();
export default historyStore;
