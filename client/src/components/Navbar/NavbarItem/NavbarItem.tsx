interface NavbarItemType {
  onClick: () => void;
  label: string;
}
const NavbarItem: React.FC<NavbarItemType> = ({ onClick, label }) => {
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
