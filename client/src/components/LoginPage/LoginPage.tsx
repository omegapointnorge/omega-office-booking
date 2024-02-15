import React from "react";
import Heading from "@common-components/Heading";
import { Link, useNavigate } from "react-router-dom";
import { useAuthContext } from "@auth/useAuthContext";
import { useEffect } from "react";
import { NavbarLogoBig } from "@components/Navbar/NavbarLogo/NavbarLogo";

import "@/index.css";

const LoginPage = () => {
  const LoginUrl = "/api/Account/Login";
  const context = useAuthContext();
  const navigate = useNavigate();

  useEffect(() => {
    if (context?.user?.isAuthenticated) {
      navigate("/overview");
    }
  }, [context?.user?.isAuthenticated, navigate]);

  return (
    <main
      className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 z-50 outline-none focus:outline-none bg-warmgray"
    >
      <div className="relative w-full md:w-4/6 lg:w-3/6 xl:w-2/5 my-6 mx-auto h-full lg:h-auto md:h-auto">
        <div
          className="translate h-full lg:h-auto md:h-auto border-0 relative 
                flex flex-col w-full  outline-none focus:outline-none my-48"
        >
          <NavbarLogoBig />
          <div>
            <Heading title="Velkommen til kontorbooking" />
            <div className="flex flex-col gap-2 p-6">
              <div className="flex flex-row items-center gap-4">
                <Link
                  className={`text-center hover:opacity-80 transition relative rounded-lg w-full bg-marine bg-blue-900 border-blue-900 text-white py-3 text-md font-semibold border-2`}
                  to={LoginUrl}
                  reloadDocument
                >
                  Login inn
                </Link>
              </div>
            </div>
          </div>
        </div>
      </div>
    </main>
  );
};

export default LoginPage;
