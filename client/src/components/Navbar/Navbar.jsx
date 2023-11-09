import {Link} from "react-router-dom";
import Button from "../Button";

type NavbarProps = {
    userName?: string | null; 
}

export const Navbar = ({userName} : NavbarProps) => {
    const LogoutUrl = "/api/Account/Logout";
    return (
        <div className="fixed w-full bg-white z-10 shadow-sm ">
            <div className="py-4 border-b-[1px]">
                <div className="max-w-[2520px] mx-auto xl:px-20 md:px-10 sm:px-2 px-4">
                    <div className="flex flex-row items-center justify-between gap-3 md:gap-0">
                        <div>{userName != null ? {userName} : "Welcome to overview!" }</div>
                        <div>
                            <Link
                                className="flex flex-row items-center gap-4 w-full z-5000"
                                to={LogoutUrl}
                                reloadDocument
                            >
                                <Button alert label="Logout" />
                            </Link>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}