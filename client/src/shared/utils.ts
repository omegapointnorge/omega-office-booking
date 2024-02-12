export const isSameDate = (date1: Date, date2: Date) => {
  if (!(date1 instanceof Date) || !(date2 instanceof Date)) {
    throw new Error("Both arguments must be Date objects.");
  }

  return date1.toDateString() === date2.toDateString();
};


