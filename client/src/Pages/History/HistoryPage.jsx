import React, {useState} from "react";
import Heading from "../../components/Heading";
import {observer} from "mobx-react-lite";
import BookingItem from "../../components/Bookings/BookingItem";
import historyStore from "../../stores/HistoryStore";
import MyDialog from "../../components/Dialog";
import {IoIosArrowBack, IoIosArrowForward} from "react-icons/io";

const HistoryPage = observer(() => {
    const [isFirstPage, setIsFirstPage] = useState(true);
const [isLastPage, setIsLastPage] = useState(false);

const navigateNext = () => {
    if (!isLastPage) {
        historyStore.navigateNext();
        setIsFirstPage(false)
        if (historyStore.pageNumber === historyStore.lastPage) setIsLastPage(true)
    }
};

const navigatePrevious = () => {
    if (!isFirstPage) {
        historyStore.navigatePrevious();
        setIsLastPage(false)
        if(historyStore.pageNumber === 1) setIsFirstPage(true)
    }
};

    if (historyStore.myUpcomingBookings.length === 0 && historyStore.myPreviousBookings.length === 0) {
        return (
            <>
                <div
                    className="justify-center items-center flex overflow-x-hidden overflow-y-auto
     fixed inset-0 outline-none focus:outline-none"
                >
                    <Heading title="You dont have any bookings"/>
                </div>
            </>
        );
    }
    return (
        <>
            <div
                className="justify-center items-center flex flex-col fixed inset-0 mt-10"
            >
                <Heading
                    title="Your bookings"
                />
                <div className="container mt-3">
                    <div className="flex flex-col gap-4 mb-10">
                        <p className="text-left text-xl font-semibold heading mb-3 pl-11">UPCOMING BOOKINGS</p>
                        <div className="flex flex-row gap-5">
                            {/*Buttons for navigation when support is added for more than one booking. Currently only aligns the booking items correctly*/}
                            <button onClick={() => navigatePrevious()}
                                    className="opacity-0" disabled={true}>
                                <IoIosArrowBack/>
                            </button>
                            {historyStore.myUpcomingBookings
                                .map((booking) => (
                                    <BookingItem
                                        key={booking.id}
                                        roomId={booking.roomId}
                                        seatId={booking.seatId}
                                        bookingDateTime={booking.bookingDateTime}
                                        showDeleteButton={true}
                                        onClick={() => {
                                            historyStore.handleOpenDialog(booking.id);
                                        }}
                                    ></BookingItem>
                                ))}
                            <button onClick={() => navigateNext()}
                                    className={"opacity-0"} disabled={true}>
                                <IoIosArrowForward/>
                            </button>
                            <MyDialog
                                title="Delete your Seat?"
                                open={historyStore.openDialog}
                                handleClose={historyStore.handleCloseDialog}
                                onClick={() => {
                                    historyStore.deleteBooking(historyStore.bookingIdToDelete);
                                }}
                            />
                        </div>
                    </div>
                    <div className="flex flex-col gap-4">
                        <p className="text-left text-xl font-semibold heading mb-3 pl-11">PREVIOUS BOOKINGS</p>
                        <div className="flex flex-row gap-5">
                            <button onClick={() => navigatePrevious()} className={`${isFirstPage ? 'opacity-0' : 'opacity-100'}`} disabled={isFirstPage}>
                                <IoIosArrowBack/>
                            </button>
                            {historyStore.myPreviousBookings
                                .map((booking) => (
                                    <BookingItem key={booking.id} roomId={booking.roomId}
                                                 seatId={booking.seatId}
                                                 bookingDateTime={booking.bookingDateTime} showDeleteButton={false}
                                    ></BookingItem>
                                ))}
                            <button onClick={() => navigateNext()} className={`${isLastPage ? 'opacity-0' : 'opacity-100'}`} disabled={isLastPage}>
                                <IoIosArrowForward />
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
});

export default HistoryPage;