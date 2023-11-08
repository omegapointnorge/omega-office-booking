import Heading from "../../components/Heading";
import {useAuthContext} from "../../api/useAuthContext";
import Button from "../../components/Button";
import {Link} from "react-router-dom";


export default function OverviewPage() {
    const context = useAuthContext();
    const name = context?.user?.claims?.find(x => x.key === 'name')?.value;
    const LogoutUrl = "https://localhost:5001/api/Account/Logout";
    return (
        <>
            <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 z-50 outline-none focus:outline-none" >
                <Heading title="Overview page" subTitle={`Velkommen ${name}`}/>
                <div>
                    <Link
                        className="flex flex-row items-center gap-4 w-full"
                        to={LogoutUrl}
                    >
                        <Button alert label="Logout" />
                    </Link>
                </div>
                <div>
                    <link className="flex flex-row items-center gap-4 w-full">
                    


                    </link>
                </div>
            </div>
            
            
        </>)
}