import { makeAutoObservable } from "mobx";
import ApiService from "@services/ApiService";
import bookingStore from "@stores/BookingStore";
import { Room, HistoryBooking } from "@/shared/types/entities";
import { ApiStatus } from "@/shared/types/enums";

const ITEMS_PER_PAGE = 5;

class HistoryStore {
  myActiveBookings: HistoryBooking[] = [];
  myPreviousBookings: HistoryBooking[] = [];
  myPreviousBookingsCurrentPage: HistoryBooking[] = [];

  openDialog = false;
  bookingIdToDelete!: number;

  pageNumber = 1;
  lastPage = 1;
  isFirstPage = true;
  isLastPage = false;
  rooms: Room[] = [];
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

  async fetchRoomsAndSeatsForRoomLookup() {
    try {
      const url = "/api/room/rooms";
      await ApiService.fetchData<Room[]>(url, "Get", null).then((response) => {
        this.setRooms(response);
      });
    } catch (error) {
      console.error("Error fetching bookings:", error);
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
      await this.fetchRoomsAndSeatsForRoomLookup();
      this.initPreviousBookings();
    } catch (error) {
      console.error("Error fetching bookings:", error);
      this.setApiStatus(ApiStatus.Error);
    }
  }

  async deleteBooking(bookingId: number) {
    if (this.apiStatus === ApiStatus.Pending) return;
    try {
      this.setApiStatus(ApiStatus.Pending);
      const url = `/api/Booking/${bookingId}`;

      await ApiService.fetchData<{ ok: boolean }>(url, "DELETE").then(
        (response) => {
          if (!response.ok) {
            console.error(`Failed to delete booking with ID ${bookingId}`);
            return;
          }
          this.setApiStatus(ApiStatus.Success);
          return response;
        }
      );

      this.updateBookings(bookingId);
    } catch (error) {
      console.error(
        `An error occurred while deleting booking with ID ${bookingId}:`,
        error
      );
      this.setApiStatus(ApiStatus.Error);
    }
  }

  async updateBookings(bookingId: number) {
    this.removeBookingById(bookingId);
    bookingStore.removeBookingById(bookingId);
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

  setApiStatus(status: ApiStatus) {
    this.apiStatus = status;
  }

  setRooms(rooms: Room[]) {
    this.rooms = rooms;
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
