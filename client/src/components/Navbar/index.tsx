import React from "react";

import { NavbarLogo } from "@components/Navbar/Logo";
import { useNavigate } from "react-router-dom";
import { NavbarItem } from "@components/Navbar/Item";
import { Link } from "react-router-dom";

export const Navbar = () => {
  const navigate = useNavigate();

  const LogoutButton = () => {
    const LogoutUrl = "/api/Account/Logout";

    return (
      <div>
        <Link
          className="flex flex-row items-center gap-4 w-full z-5000 font-bold hover:text-white transition"
          to={LogoutUrl}
          reloadDocument
        >
          Logg ut
        </Link>
      </div>
    );
  };

  return (
    <nav className="fixed w-full bg-warmgray p-4 flex justify-between z-10 items-center top-0 shadow-sm xl:px-20 md:px-10 sm:px-2 px-4">
      <div className="flex items-center space-x-4">
        <NavbarLogo />
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
