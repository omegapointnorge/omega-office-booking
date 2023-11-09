import Heading from "../../components/Heading";

const RoomsPage = () => {
    return (
        <>
            <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 outline-none focus:outline-none" >
                <div className="flex flex-col gap-10">
                    <Heading title="Rooms page"/>
                    <div className="flex flex-row gap-32">
                        <img alt="big-room"
                             className="hidden md:block cursor-pointer"
                             src="/images/big-room.png"/>
                        <img alt="small-room"
                             className="hidden md:block cursor-pointer"
                             src="/images/small-room.png"
                        />
                    </div>
                </div>
            </div>
        </>)
}

export default RoomsPage;