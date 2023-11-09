import Heading from "../../components/Heading";
import {useAuthContext} from "../../api/useAuthContext";


export default function OverviewPage() {
    const context = useAuthContext();
    const name = context?.user?.claims?.find(x => x.key === 'name')?.value;
    
    return (
        <>
            <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 outline-none focus:outline-none" >
                <Heading title="Overview page" subTitle={`Velkommen ${name}`}/>
            </div>
        </>)
}