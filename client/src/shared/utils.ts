export const isSameDate = (date1: Date, date2: Date) => {
  if (!(date1 instanceof Date) || !(date2 instanceof Date)) {
    throw new Error("Both arguments must be Date objects.");
  }

  return date1.toDateString() === date2.toDateString();
};

const getEarliestAllowedBookingTime = (date: Date): Date => {
  let earliestAllowedTime = new Date(date);

  // If it's Monday, set the time to the Friday before at 16:00
  if (earliestAllowedTime.getDay() === 1) {
    // Monday has index 1 in JavaScript (0 is Sunday)
    earliestAllowedTime.setDate(earliestAllowedTime.getDate() - 3); // 3 days back to Friday
  } else {
    // Otherwise, set the time to the day before at 16:00
    earliestAllowedTime.setDate(earliestAllowedTime.getDate() - 1);
  }
  earliestAllowedTime.setHours(16, 0, 0, 0);
  return earliestAllowedTime;
};

export const hasBookingOpened = (displayDate: Date): boolean => {
  let bookingOpeningTime = getEarliestAllowedBookingTime(displayDate);

  let currentDateTime = new Date();
  return currentDateTime > bookingOpeningTime;
};
