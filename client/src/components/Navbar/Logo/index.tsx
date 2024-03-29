import React from "react";
import { useNavigate } from "react-router-dom";

export const NavbarLogo = () => {
  const navigate = useNavigate();
  return (
    <img
      onClick={() => {
        navigate("/");
      }}
      alt="Logo"
      className="hidden md:block cursor-pointer"
      height="40"
      width="105"
      src="/logo.png"
    />
  );
};

export const NavbarLogoBig = () => {
  return (
    <img
      alt="Big Logo"
      height="120"
      width="315"
      className=" mx-auto"
      src="/omegapoint-logo-black-1200.png"
    />
  );
};
