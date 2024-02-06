import { makeAutoObservable } from "mobx";
import ApiService from "@services/ApiService";
import bookingStore from "@stores/BookingStore";
import { Room, Booking } from "@/shared/types/entities";

const ITEMS_PER_PAGE = 5;

class HistoryStore {
  myActiveBookings: Booking[] = [];
  myPreviousBookings: Booking[] = [];
  myPreviousBookingsCurrentPage: Booking[] = [];

  openDialog = false;
  bookingIdToDelete?: number;

  pageNumber = 1;
  lastPage = 1;
  isFirstPage = true;
  isLastPage = false;
  isLoading = false;
  rooms: Room[] = [];

  constructor() {
    this.initBookings();
    makeAutoObservable(this);
  }

  async initBookings() {
    try {
      this.isLoading = true;
      await this.fetchMyBookings();
      await this.fetchRoomsAndSeatsForRoomLookup();
      this.initPreviousBookings();
    } catch (error) {
      console.error(error);
    } finally {
      this.isLoading = false;
    }
  }

  async fetchRoomsAndSeatsForRoomLookup() {
    try {
      const url = "/api/room/rooms";
      const response = await ApiService.fetchData<Room[]>(url, "Get", null);
      const data = await response.json() as Room[];
      this.rooms = data;

    } catch (error) {
      console.error("Error fetching bookings:", error);
    }
  }

  async fetchMyBookings() {
    try {
      const url = "/api/Booking/Bookings/MyBookings";
      const response = await ApiService.fetchData<Booking[]>(url, "Get", null);
      const data = (await response.json()) as Booking[];

      this.myActiveBookings = this.filterAndSortBookings(data, true);
      this.myPreviousBookings = this.filterAndSortBookings(data, false);
      this.lastPage = Math.ceil(
        this.myPreviousBookings.length / ITEMS_PER_PAGE
      );
    } catch (error) {
      console.error("Error fetching bookings:", error);
    }
  }

  async deleteBooking(bookingId: number) {
    try {
      const url = `/api/Booking/${bookingId}`;

      const response = await ApiService.fetchData(url, "DELETE");

      if (!response.ok) {
        console.error(`Failed to delete booking with ID ${bookingId}`);
        return;
      }

      this.removeBookingById(bookingId);
      bookingStore.removeBookingById(bookingId);
    } catch (error) {
      console.error(
        `An error occurred while deleting booking with ID ${bookingId}:`,
        error
      );
    }
  }

  setIsFirstPage(data: boolean) {
    this.isFirstPage = data;
  }

  setIsLastPage(data: boolean) {
    this.isLastPage = data;
  }

  removeBookingById(bookingId: number) {
    this.myActiveBookings = this.myActiveBookings.filter(
      (booking) => booking.id !== bookingId
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
  handleOpenDialog(bookingId: number) {
    this.openDialog = !this.openDialog;
    this.bookingIdToDelete = bookingId;
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

  filterAndSortBookings(bookings: Booking[], isActive: boolean) {
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

    // return sortedBookings.map(
    //   (booking) =>
    //     new Booking(
    //       booking.id,
    //       booking.userId,
    //       booking.userName,
    //       booking.seatId,
    //       booking.bookingDateTime
    //     )
    // );
    return sortedBookings;
  }

  getRoomIdBySeatId(seatId: number) {
    for (const room of this.rooms) {
      const foundSeat = room.seats.find((seat) => seat.id === seatId);
      if (foundSeat) {
        return room.id;
      }
    }
    console.error(`Seat with ID ${seatId} not found in any room.`);
    return null;
  }
}

const historyStore = new HistoryStore();
export default historyStore;
