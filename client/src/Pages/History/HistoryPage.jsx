import Heading from "../../components/Heading";
import { observer } from "mobx-react-lite";
import BookingItem from "../../components/Bookings/BookingItem";
import historyStore from "../../stores/HistoryStore";
import MyDialog from "../../components/Dialog";


const HistoryPage = observer(() => {
  if (historyStore.myBookings.length === 0) {
    return (
      <>
        <div
          className="justify-center items-center flex mt-10 overflow-x-hidden overflow-y-auto
            inset-0 outline-none focus:outline-none"
        >
          <Heading title="You dont have any bookings" />
        </div>
      </>
    );
  }

  return (
    <>
      <div
        className="justify-center items-center flex mt-10 overflow-x-hidden overflow-y-auto
        inset-0 outline-none focus:outline-none"
      >
        <div className="flex flex-col gap-10">
          <Heading
            title="Your bookings"
            subTitle="A summary of your bookings"
          />
          {historyStore.myBookings.map((booking) => (
            <BookingItem key={booking.id} seatId={booking.seatId} name={booking.dateTime}
              onClick={() => {
                historyStore.handleOpenDialog(booking.id);
              }}></BookingItem>

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
    </>
  );
});

export default HistoryPage;
