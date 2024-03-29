import { makeAutoObservable } from "mobx";
import ApiService from "@services/ApiService";
import bookingStore from "@stores/BookingStore";
import { HistoryBooking } from "@/shared/types/entities";
import { ApiStatus } from "@/shared/types/enums";

const ITEMS_PER_PAGE = 5;

class HistoryStore {
  myActiveBookings: HistoryBooking[] = [];
  myPreviousBookings: HistoryBooking[] = [];
  myPreviousBookingsCurrentPage: HistoryBooking[] = [];

  openDialog = false;
  historyBookingIdToDelete!: number;
  eventIdToDelete!: number;

  pageNumber = 1;
  lastPage = 1;
  isFirstPage = true;
  isLastPage = false;
  apiStatus: ApiStatus = ApiStatus.Idle;

  constructor() {
    makeAutoObservable(this);
  }

  async initialize() {
    try {
      await this.fetchMyBookings();
    } catch (error) {
      console.error(error);
    } finally {
    }
  }

  
  async fetchMyBookings() {
    if (this.apiStatus === ApiStatus.Pending) return;
    try {
      this.setApiStatus(ApiStatus.Pending);

      const url = "/api/Booking/Bookings/MyBookings";
      const bookings = await ApiService.fetchData<HistoryBooking[]>(
        url,
        "Get",
        null
      ).then((response) => {
        this.setApiStatus(ApiStatus.Success);
        return response;
      });
      this.setMyActiveBookings(bookings);
      this.setMyPreviousBookings(bookings);
      this.lastPage = Math.ceil(
        this.myPreviousBookings.length / ITEMS_PER_PAGE
      );
      this.initPreviousBookings();
    } catch (error) {
      console.error("Error fetching bookings:", error);
      this.setApiStatus(ApiStatus.Error);
    }
  }

async deleteResource(resourceType: string, resourceId: number) {
    if (this.apiStatus === ApiStatus.Pending) return;
    try {
        this.setApiStatus(ApiStatus.Pending);
        const url = `/api/${resourceType}/${resourceId}`;

        const response = await ApiService.fetchData<{ ok: boolean }>(url, "DELETE");

        if (!response.ok) {
            console.error(`Failed to delete ${resourceType} with ID ${resourceId}`);
            return;
        }

        this.setApiStatus(ApiStatus.Success);
        this.updateBookings(resourceId);
    } catch (error) {
        console.error(
            `An error occurred while deleting ${resourceType} with ID ${resourceId}:`,
            error
        );
        this.setApiStatus(ApiStatus.Error);
    }
}

async deleteBooking(bookingId: number) {
    await this.deleteResource('Booking', bookingId);
}

async deleteEvent(eventId: number) {
    await this.deleteResource('Event', eventId);
}

  async updateBookings(bookingId: number) {
    this.removeBookingById(bookingId);
    bookingStore.fetchAllActiveBookings()
  }

  setIsFirstPage(data: boolean) {
    this.isFirstPage = data;
  }

  setIsLastPage(data: boolean) {
    this.isLastPage = data;
  }

  removeBookingById(historyBookingId: number) {
    this.myActiveBookings = this.myActiveBookings.filter(
      (booking) => booking.id !== historyBookingId
    );
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
    this.myPreviousBookingsCurrentPage = this.myPreviousBookings.slice(
      startIndex,
      endIndex
    );
  }

  get isEmpty() {
    return (
      this.myActiveBookings.length === 0 && this.myPreviousBookings.length === 0
    );
  }

  /* Utils */
  handleOpenDialog(historyBookingId: number) {
    this.openDialog = !this.openDialog;
    this.historyBookingIdToDelete = historyBookingId;
  }

  handleCloseDialog = (): void => {
    this.openDialog = !this.openDialog;
  };

  initPreviousBookings() {
    if (this.myPreviousBookings.length > ITEMS_PER_PAGE) {
      this.setIsLastPage(false);
      this.myPreviousBookingsCurrentPage = this.myPreviousBookings.slice(
        0,
        ITEMS_PER_PAGE
      );
    } else {
      this.setIsLastPage(true);
      this.myPreviousBookingsCurrentPage = this.myPreviousBookings;
    }
  }

  filterAndSortBookings(bookings: HistoryBooking[], isActive: boolean) {
    const currentDate = new Date();
    currentDate.setHours(0, 0, 0, 0); // Set hours, minutes, seconds, and milliseconds to 00:00:00.000

    const filteredBookings = bookings.filter((booking) => {
      const bookingDate = new Date(booking.bookingDateTime);
      return isActive ? bookingDate >= currentDate : bookingDate < currentDate;
    });

    const sortedBookings = filteredBookings.sort(
      (
        a: { bookingDateTime: string | number | Date },
        b: { bookingDateTime: string | number | Date }
      ) => {
        const dateA = new Date(a.bookingDateTime).getTime();
        const dateB = new Date(b.bookingDateTime).getTime();
        return isActive ? dateA - dateB : dateB - dateA;
      }
    );

    return sortedBookings;
  }



  setApiStatus(status: ApiStatus) {
    this.apiStatus = status;
  }

  setMyActiveBookings(bookings: HistoryBooking[]) {
    this.myActiveBookings = this.filterAndSortBookings(bookings, true);
  }

  setMyPreviousBookings(bookings: HistoryBooking[]) {
    this.myPreviousBookings = this.filterAndSortBookings(bookings, false);
    this.lastPage = Math.ceil(this.myPreviousBookings.length / ITEMS_PER_PAGE);
  }
}

const historyStore = new HistoryStore();
export default historyStore;
