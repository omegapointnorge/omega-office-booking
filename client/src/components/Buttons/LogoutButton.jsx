import {Link} from "react-router-dom";
import Button from "../Button";


export const LogoutButton = () => {
    const LogoutUrl = "/api/Account/Logout";
    
    return <div>
        <Link
            className="flex flex-row items-center gap-4 w-full z-5000"
            to={LogoutUrl}
            reloadDocument
        >
            <Button alert label="Logout" />
        </Link>
    </div>
}