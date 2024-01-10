import { Logo } from "./Logo";
import { LogoutButton } from "../Buttons/LogoutButton";
import { useNavigate } from "react-router-dom";
import NavbarItem from "./NavbarItem";

export const Navbar = () => {
  const navigate = useNavigate();

  return (
    <div className="w-full bg-warmgray z-10 shadow-sm">
      <div className="py-4 border-b-[1px]">
        <div className="max-w-[2520px] mx-auto xl:px-20 md:px-10 sm:px-2 px-4">
          <div className="flex flex-row items-center justify-between gap-3 md:gap-0 cursor-pointer">
              <ul className="flex items-center justify-between gap-10">
                  <li>
                      <Logo/>
                  </li>
                  <li>
                      <NavbarItem
                          label="Overview"
                          onClick={() => {
                              navigate("/overview");
                          }}
                      />
                  </li>
                  <li>
                      <NavbarItem
                          label="History"
                          onClick={() => {
                              navigate("/history");
                          }}
                      />
                  </li>
              </ul>
              <LogoutButton />
          </div>
        </div>
      </div>
    </div>
  );
};