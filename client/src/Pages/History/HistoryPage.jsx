import Heading from "../../components/Heading";
import {useAuthContext} from "../../api/useAuthContext";
import Button from "../../components/Button";
import {useStores} from "../../stores";
import { observer } from "mobx-react-lite";
import BookingItem from "../../components/Bookings/BookingItem";

const HistoryPage = observer(() => {
    const {historyStore} = useStores();
    const context = useAuthContext();
    const name = context?.user?.claims?.find(x => x.key === 'name')?.value;


    if(historyStore.myBookings.length === 0){
        return <>
            <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 outline-none focus:outline-none" >
                <Heading title="You dont have any bookings"/>
            </div>
        </>
    }
    
    return (
        <>
            <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 outline-none focus:outline-none" >
                <div className="flex flex-col gap-10">
                    <Heading title="Your bookings" subTitle="A summary of your bookings"/>
                    {historyStore.myBookings.map((booking) =>
                        (<BookingItem key={booking.id}
                                      seatNr={booking.seatId}

                        >
                        </BookingItem>))}
                </div>
            </div>
        </>)
});

export default HistoryPage;