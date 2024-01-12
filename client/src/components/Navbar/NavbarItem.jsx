const NavbarItem = ({ onClick, label }) => {
  return (
    <div
      onClick={onClick}
      className="hover:text-black transition font-semibold text-lg"
    >
      {label}
    </div>
  );
};

export default NavbarItem;
