import Heading from "../../components/Heading";
import Button from "../../components/Button";
import { Link } from "react-router-dom";

export default function LoginPage() {
  const LoginUrl = "https://localhost:5001/api/Account/Login";
  const LogoutUrl = "https://localhost:5001/api/Account/Logout";

  return (
    <>
      <div
        className="justify-center items-center flex overflow-x-hidden overflow-y-auto 
     fixed inset-0 z-50 outline-none focus:outline-none bg-neutral-800/70"
      >
        <div className="relative w-full md:w-4/6 lg:w-3/6 xl:w-2/5 my-6 mx-auto h-full lg:h-auto md:h-auto">
          {/*CONTENT*/}
          <div
            className="translate h-full lg:h-auto md:h-auto border-0 rounded-lg shadow-lg relative 
                flex flex-col w-full bg-white outline-none focus:outline-none"
          >
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
                  >
                    <Button label="Login" />
                  </Link>
                  <Link
                    className="flex flex-row items-center gap-4 w-full"
                    to={LogoutUrl}
                  >
                    <Button alert label="Logout" />
                  </Link>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
