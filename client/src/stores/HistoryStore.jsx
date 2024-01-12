import { makeAutoObservable } from "mobx";
import Booking from "../domain/booking";
import toast from "react-hot-toast";
import ApiService from "./ApiService.jsx";

class HistoryStore {
  myActiveBookings = [];
  myPreviousBookings = [];
  myPreviousBookingsCurrentPage = [];
  openDialog = false;
  bookingIdToDelete = null;
  itemCount = 5;
  pageNumber = 0;
  lastPage;
  isFirstPage;
  isLastPage;

  constructor() {
    this.fetchActiveBookings();
    this.fetchPreviousBookings();
    this.setIsFirstPage(true);
    this.setIsLastPage(false);
    makeAutoObservable(this);
  }


  async fetchActiveBookings() {
    try {
      const url = "/api/Booking/Bookings/MyActiveBookings";
      const response = await ApiService.fetchData(url, "Get", null);
      const data = await response.json();
      this.setActiveBookings(data);
    } catch (error) {
      console.error(error);
    }
  }

  async fetchPreviousBookings() {
    try {
      const url = `/api/Booking/Bookings/MyPreviousBookings`;
      const response = await ApiService.fetchData(url, "Get", null);
      const data = await response.json();
      this.setPreviousBookings(data);
      this.setLastPage(Math.ceil(this.myPreviousBookings.length / this.itemCount))
      this.initPreviousBookingsPageCurrentPage();

    } catch (error) {
      console.error(error);
    }
  }

  initPreviousBookingsPageCurrentPage() {
  try {
    if (this.myPreviousBookings.length > 5) {
      this.setIsLastPage(false);
      this.myPreviousBookingsCurrentPage = this.myPreviousBookings.slice(0, 5);
    } else {
      this.setIsLastPage(true);
      this.myPreviousBookingsCurrentPage = this.myPreviousBookings;
    }
  } catch (error) {
    console.error(error);
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
    this.setIsLastPage(false);
    this.setIsFirstPage(this.pageNumber === 1);
    const startIndex = (this.pageNumber - 1) * this.itemCount;
    const endIndex = this.pageNumber * this.itemCount;
    this.myPreviousBookingsCurrentPage = this.myPreviousBookings.slice(startIndex, endIndex);
  }
}

navigateNext() {
  if (this.pageNumber < this.lastPage) {
    this.pageNumber += 1;
    this.setIsFirstPage(false);
    this.setIsLastPage(this.pageNumber === this.lastPage);

    const startIndex = (this.pageNumber - 1) * this.itemCount;
    const endIndex = this.pageNumber * this.itemCount;
    this.myPreviousBookingsCurrentPage = this.myPreviousBookings.slice(startIndex, endIndex);
  }
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
