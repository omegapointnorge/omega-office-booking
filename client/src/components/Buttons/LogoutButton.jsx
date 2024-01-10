import {Link} from "react-router-dom";

export const LogoutButton = () => {
    const LogoutUrl = "/api/Account/Logout";
    
    return <div>
        <Link
            className="text-lg flex flex-row items-center font-semibold gap-4 w-full z-5000"
            to={LogoutUrl}
            reloadDocument
        >
            Logout
        </Link>
    </div>
}