import ApiService from "@services/ApiService";
import { Booking } from "@/shared/types/entities";


export const isSameDate = (date1: Date, date2: Date) => {
  if (!(date1 instanceof Date) || !(date2 instanceof Date)) {
    throw new Error("Both arguments must be Date objects.");
  }

  return date1.toDateString() === date2.toDateString();
};

export const getEarliestAllowedBookingDate = (date: Date): Date => {
  let earliestAllowedTime = new Date(date);

  // If it's Monday, set the time to the Friday before
  if (earliestAllowedTime.getDay() === 1) {
    // Monday has index 1 in JavaScript (0 is Sunday)
    earliestAllowedTime.setDate(earliestAllowedTime.getDate() - 3); // 3 days back to Friday
  } else {
    // Otherwise, set the time to the day before
    earliestAllowedTime.setDate(earliestAllowedTime.getDate() - 1);
  }
  return earliestAllowedTime;
};


export const fetchOpeningTimeOfDay = async () => {
  try {
    const response = await ApiService.fetchData<string>(
      "/api/Booking/OpeningTime",
      "GET"
    );
    
    if (!response) {
      throw new Error("Failed to fetch opening time");
    }

    return response;

  } catch (error) {
    console.error("Error fetching opening time:", error);
    return "Error fetching opening time";
  }
};



const isBookingForUser = (
  booking: Booking,
  userId: string,
  seatId: number,
  date: Date,
  includeCurrentUser: boolean
): boolean => {
  const providedDate = new Date(date);
  providedDate.setHours(0, 0, 0, 0);
  const bookingDate = new Date(booking.bookingDateTime);
  bookingDate.setHours(0, 0, 0, 0);
  

  return (
    (includeCurrentUser ? booking.userId === userId : booking.userId !== userId) &&
    booking.seatId === seatId &&
    bookingDate.getTime() === providedDate.getTime()
  );
};

export const isBookedByUser = (
  bookingsList: Booking[],
  userId: string,
  seatId: number,
  date: Date
): boolean =>
  bookingsList.some(booking => isBookingForUser(booking, userId, seatId, date, true));

export const isBookedByOtherUser = (
  bookingsList: Booking[],
  userId: string,
  seatId: number,
  date: Date
): boolean =>
  bookingsList.some(booking => isBookingForUser(booking, userId, seatId, date, false));





