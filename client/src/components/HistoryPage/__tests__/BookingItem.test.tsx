import React from "react";
import { render, screen } from "@testing-library/react";
import { BookingItem } from "../BookingItem";

describe.skip("PrimaryDialog", () => {
  const defaultProps = {
    bookingDateTime: new Date(),
    seatId: 2,
    showDeleteButton: false,
    roomId: 1,
  };

  it("renders with given props", () => {
    render(<BookingItem {...defaultProps} />);
    expect(screen.getByText(/Sete 2/i)).toBeInTheDocument();
  });
});
