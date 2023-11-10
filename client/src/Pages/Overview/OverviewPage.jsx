import Heading from "../../components/Heading";
import {useAuthContext} from "../../api/useAuthContext";
import { observer } from "mobx-react-lite";
import {useNavigate} from "react-router-dom";

const OverviewPage = observer(() => {
    const context = useAuthContext();
    const navigate = useNavigate();
    const name = context?.user?.claims?.find(x => x.key === 'name')?.value;
    
    return (
        <>
            <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 outline-none focus:outline-none" >
                <div className="flex flex-col gap-10">
                    <Heading title={`Velkommen ${name}`} subTitle="Vennligst velg rom for å booke"/>
                    <div className="flex flex-row gap-24">
                        <div>
                            <p className="font-bold text-center">Big room</p>
                            <img alt="big-room"
                                 className="hidden md:block cursor-pointer"
                                 src="/images/big-room.png"
                                 onClick={() => {
                                     navigate("/bigroom");
                                 }}
                            />
                        </div>
                        <div>
                            <p className="font-bold text-center">Small room</p>
                            <img alt="small-room"
                                 className="hidden md:block cursor-pointer"
                                 src="/images/small-room.png"
                            />
                        </div>
                    </div>
                </div>
            </div>
        </>)
});

export default OverviewPage;