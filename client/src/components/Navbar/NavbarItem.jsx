const NavbarItem = ({ onClick, label }) => {
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
