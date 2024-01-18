import {makeAutoObservable} from "mobx";
import {MyBookingsResponse} from "../domain/booking";
import ApiService from "../Services/ApiService.jsx";

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
            await this.fetchMyBookings();
            this.initPreviousBookings();

        } catch (error) {
            console.error(error);
        } finally {
            this.isLoading = false;
        }
    }

    async fetchMyBookings() {
        try {
            const url = "/api/Booking/Bookings/MyBookings";
            const response = await ApiService.fetchData(url, "Get", null);
            const data = await response.json();

      this.myActiveBookings = this.filterAndSortBookings(data, true);
      this.myPreviousBookings = this.filterAndSortBookings(data, false);
      this.lastPage = Math.ceil(this.myPreviousBookings.length / ITEMS_PER_PAGE);

        } catch (error) {
            console.error("Error fetching bookings:", error);
        }
    }

  async deleteBooking(bookingId) {
    try {
      const url = "/api/Booking/" + bookingId;
        await ApiService.fetchData(url, "Delete");
        this.removeBookingById(bookingId);
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


  removeBookingById(bookingId) {
    this.myActiveBookings = this.myActiveBookings.filter(booking => booking.id !== bookingId);
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
        } else {
            this.setIsLastPage(true);
            this.myPreviousBookingsCurrentPage = this.myPreviousBookings;
        }
    }

    filterAndSortBookings(bookings, isActive) {
        const currentDate = new Date();
        currentDate.setHours(0, 0, 0, 0); // Set hours, minutes, seconds, and milliseconds to 00:00:00.000

        const filteredBookings = bookings.filter((booking) => {
            const bookingDate = new Date(booking.bookingDateTime);
            return isActive ? bookingDate >= currentDate : bookingDate < currentDate;
        });

        const sortedBookings = filteredBookings.sort((a, b) => {
            const dateA = new Date(a.bookingDateTime);
            const dateB = new Date(b.bookingDateTime);
            return isActive ? dateA - dateB : dateB - dateA;
        });

        return sortedBookings.map((myBookingsResponse) => new MyBookingsResponse(myBookingsResponse.id, myBookingsResponse.userId, myBookingsResponse.seatId, myBookingsResponse.bookingDateTime, myBookingsResponse.roomId));
    }
}

const historyStore = new HistoryStore();
export default historyStore;
