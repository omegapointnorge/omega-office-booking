import Heading from "../../components/Heading";
import {useAuthContext} from "../../api/useAuthContext";
import {useStores} from "../../stores";
import { observer } from "mobx-react-lite";
import BookingItem from "../../components/Bookings/BookingItem";

const OverviewPage = observer(() => {
    const context = useAuthContext();
    const name = context?.user?.claims?.find(x => x.key === 'name')?.value;
    const {overviewStore} = useStores();
    
    return (
        <>
            <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 outline-none focus:outline-none" >
                <div className="flex flex-col gap-10">
                    <Heading title="Overview page" subTitle={`Velkommen ${name}`}/>
                    
                    
                </div>
            </div>
        </>)
});

export default OverviewPage;