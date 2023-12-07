import PrimaryModal from "./PrimaryModal";

const BookingModal = ({
  title,
  content,
  open,
  negativeAction,
  positiveAction,
  isTaken,
  ...props
}) => {
  if (!open) return null;

  return (
    <PrimaryModal
      positiveAction={positiveAction}
      negativeAction={negativeAction}
      {...props}
    >
      <div>
        <h3
          className="text-base font-semibold leading-6 text-gray-900"
          id="modal-title"
        >
          {title}
        </h3>
        <div className="mt-2">
          <p className="text-sm text-gray-500">{content}</p>
        </div>
      </div>
    </PrimaryModal>
  );
};

export default BookingModal;
