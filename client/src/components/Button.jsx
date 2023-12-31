const Button = ({
  label,
  onClick,
  disabled,
  outline,
  small,
  alert,
  icon: Icon,
}) => {
  return (
    <button
      onClick={onClick}
      disabled={disabled}
      className={`
      relative
      disabled:opacity-70
      disabled:cursor-not-allowed
      rounded-lg
      hover:opacity-80
      transition
      w-full
      bg-marine
      ${outline ? "bg-white" : "bg-blue-900"}
      ${outline ? "#D6D6D6" : "border-blue-900"}
      ${outline ? "text-black" : "text-white"}
      ${small ? "py-1" : "py-3"}
      ${small ? "text-sm" : "text-md"}
      ${small ? "font-light" : "font-semibold"}
      ${small ? "border-[1px]" : "border-2"}
      ${alert ? "bg-red-800" : "bg-blue-900"}
      ${alert ? "border-red-800" : "border-blue-900"}
`}
    >
      {label}
      {Icon && <Icon size={24} className="absolute left-4 top-3" />}
    </button>
  );
};

export default Button;
