import Heading from "../../components/Heading";
import { observer } from "mobx-react-lite";
import BookingItem from "../../components/Bookings/BookingItem";
import historyStore from "../../stores/HistoryStore";

const HistoryPage = observer(() => {
  if (historyStore.myBookings.length === 0) {
    return (
      <>
        <div
          className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 outline-none focus:outline-none"
        >
          <Heading title="You dont have any bookings" />
        </div>
      </>
    );
  }

  return (
    <>
      <div
        className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 outline-none focus:outline-none"
      >
        <div className="flex flex-col gap-10">
          <Heading
            title="Your bookings"
            subTitle="A summary of your bookings"
          />
          {historyStore.myBookings.map((booking) => (
            <li key={booking.id} seatNr={booking.seatId}></li>
          ))}
        </div>
      </div>
    </>
  );
});

export default HistoryPage;
