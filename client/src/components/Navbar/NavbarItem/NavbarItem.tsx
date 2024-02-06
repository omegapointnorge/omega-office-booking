import React from "react";

interface NavbarItemProps {
  onClick: () => void;
  label: string;
}
const NavbarItem = ({ onClick, label }: NavbarItemProps) => {
  return (
    <div
      onClick={onClick}
      className="px-4 py-3 hover:text-white transition font-semibold cursor-pointer"
    >
      {label}
    </div>
  );
};

export default NavbarItem;
