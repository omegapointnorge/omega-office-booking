import React from "react";
import { Link } from "react-router-dom";

export const LogoutButton = () => {
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
