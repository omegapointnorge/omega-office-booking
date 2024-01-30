import Heading from "@common-components/Heading";
import Button from "@common-components/Button";
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
    <>
      <div
        className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 z-50 outline-none focus:outline-none bg-warmgray"
      >
        <div className="relative w-full md:w-4/6 lg:w-3/6 xl:w-2/5 my-6 mx-auto h-full lg:h-auto md:h-auto">
          {/*CONTENT*/}
          <div
            className="translate h-full lg:h-auto md:h-auto border-0 relative 
                flex flex-col w-full  outline-none focus:outline-none my-48"
          >
            <NavbarLogoBig />
            {/* HEADER */}
            <div>
              <Heading
                title="Welcome to OfficeBooking"
                subTitle="Please login below"
              />
              <div className="flex flex-col gap-2 p-6">
                <div className="flex flex-row items-center gap-4">
                  <Link
                    className="flex flex-row items-center gap-4 w-full"
                    to={LoginUrl}
                    reloadDocument
                  >
                    <Button label="Login" />
                  </Link>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default LoginPage;
