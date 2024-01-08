import Heading from "../../components/Heading";
import {observer} from "mobx-react-lite";
import BookingItem from "../../components/Bookings/BookingItem";
import historyStore from "../../stores/HistoryStore";
import MyDialog from "../../components/Dialog";
import {IoIosArrowBack, IoIosArrowForward} from "react-icons/io";

const HistoryPage = observer(() => {
    if (historyStore.myUpcomingBookings.length === 0) {
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
                        <p className="text-left text-xl font-semibold heading mb-3">UPCOMING BOOKINGS</p>
                        <div className="flex flex-row gap-5">
                            {historyStore.myUpcomingBookings
                                .map((booking) => (
                                    <BookingItem
                                        key={booking.id}
                                        roomName={booking.roomName}
                                        seatId={booking.seatId}
                                        dateTime={booking.dateTime}
                                        showDeleteButton={true}
                                        onClick={() => {
                                            historyStore.handleOpenDialog(booking.id);
                                        }}
                                    ></BookingItem>
                                ))}
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
                        <p className="text-left text-xl font-semibold heading mb-3">PREVIOUS BOOKINGS</p>
                        <div className="flex flex-row gap-5">
                            <button onClick={() => historyStore.navigatePrevious()} className={`opacity-${historyStore.pageNumber < historyStore.lastPage ? '0' : '100'}`}>
                                <IoIosArrowBack/>
                            </button>
                            {historyStore.myPreviousBookings
                                .map((booking) => (
                                    <BookingItem key={booking.id} roomName={booking.roomName}
                                                 seatId={booking.seatId}
                                                 dateTime={booking.dateTime} showDeleteButton={false}
                                    ></BookingItem>
                                ))}
                            <button onClick={() => historyStore.navigateNext()}>
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
