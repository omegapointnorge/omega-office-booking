import bookingStore from "@stores/BookingStore";
import { isBookedByOtherUser, isBookedByUser } from "@utils/utils";
import React from "react";
import { Calendar, DateObject} from "react-multi-date-picker";
import DatePanel from "react-multi-date-picker/plugins/date_panel";


interface SeatAssignedToUserCalendarProps {
    userGuid: string;
    selectedSeatId: number;
}



const SeatAssignedToUserCalendar: React.FC<SeatAssignedToUserCalendarProps> = ({ userGuid, selectedSeatId }) => {

    return (
        <Calendar
            onChange={(newDates: DateObject[]) =>
                
                bookingStore.setPreAssignedBookingsToDelete(newDates)
            }
            minDate={new Date()}
            maxDate={new Date(new Date().setMonth(new Date().getMonth() + 1))}
            multiple={true}
            value={bookingStore.preAssignedBookingsToDelete}
            plugins={[<DatePanel sort="date" />]}
            mapDays={({ date }) => {
                let color;
                let disabled = true;

                if (
                    isBookedByUser(
                        bookingStore.activeBookings,
                        userGuid,
                        selectedSeatId,
                        date.toDate()
                    )
                ) {
                    color = "green";
                    disabled = false;
                } else if (
                    isBookedByOtherUser(
                        bookingStore.activeBookings,
                        userGuid,
                        selectedSeatId,
                        date.toDate()
                    )
                ) {
                    color = "red";
                }

                return {
                    className: "highlight highlight-" + color,
                    disabled: disabled
                };
            }}
        />
    );
};

export default SeatAssignedToUserCalendar;
