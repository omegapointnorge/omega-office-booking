import { Logo } from "./Logo";
import { LogoutButton } from "../Buttons/LogoutButton";
import { useNavigate } from "react-router-dom";
import NavbarItem from "./NavbarItem";

export const Navbar = () => {
  const navigate = useNavigate();

  return (
    <nav className="fixed w-full bg-warmgray p-4 flex justify-between z-10 items-center top-0 shadow-sm xl:px-20 md:px-10 sm:px-2 px-4">
      <div className="flex items-center space-x-4">
        <Logo />
        <NavbarItem
          label="Oversikt"
          onClick={() => {
            navigate("/overview");
          }}
        />
        <NavbarItem
          label="Reservasjoner"
          onClick={() => {
            navigate("/history");
          }}
        />
      </div>
      <LogoutButton />
    </nav>
  );
};
