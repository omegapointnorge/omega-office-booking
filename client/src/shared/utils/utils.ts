import ApiService from "@services/ApiService";

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

export const fetchUnavailableSeatsIds = async () => {
   try {
    const response = await ApiService.fetchData<string>(
      "/api/Seat/GetUnavailableSeatIds",
      "GET"
    );
    
    if (!response) {
      throw new Error("Failed to fetch unavailable seats");
    }
    return response;

  } catch (error) {
    console.error("Failed to fetch unavailable seats", error);
    return "Failed to fetch unavailable seats";
  }
};

