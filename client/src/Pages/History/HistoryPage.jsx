import Heading from "../../components/Heading";
import {useAuthContext} from "../../api/useAuthContext";
import Button from "../../components/Button";
import {useState} from "react";
import {useStores} from "../../stores";
import { observer } from "mobx-react-lite";
import BookingItem from "../../components/Bookings/BookingItem";

const HistoryPage = observer(() => {
    const context = useAuthContext();
    const name = context?.user?.claims?.find(x => x.key === 'name')?.value;
    const {overviewStore} = useStores();

    return (
        <>
            <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 outline-none focus:outline-none" >
                <div className="flex flex-col gap-10">
                    <Heading title="Your bookings" subTitle="A summary of your bookings"/>
                    {overviewStore.bookings.map((booking) =>
                        (<BookingItem key={booking.id}
                                      name={booking.name}
                                      seatNr="Sete nr: 2"
                                      date="04.Oktober"
                                      roomName="Store rommet"

                        >
                        </BookingItem>))}
                </div>
            </div>
        </>)
});

export default HistoryPage;