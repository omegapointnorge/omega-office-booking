import React from "react";
import { render, screen } from "@testing-library/react";
import { BookingItem } from "../BookingItem";
import { Rooms } from "../../../shared/types/enums";

describe("BookingItem", () => {
  const defaultProps = {
    bookingDateTime: new Date("2024-02-27T10:08:01.8760000Z"),
    seatId: 2,
    roomId: 1,
  };

  it("renders with right date format, seat text seat 2 and room 1 filled", () => {
    render(<BookingItem {...defaultProps} />);
    expect(screen.getByText("27.2.2024")).toBeInTheDocument();
    expect(screen.getByText(/Sete 2/i)).toBeInTheDocument();
    expect(screen.getByTestId(Rooms.Large)).toBeInTheDocument();
    expect(screen.queryByTestId(Rooms.Large)).toHaveAttribute("fill", "red");
    expect(screen.queryByTestId(Rooms.Small)).toHaveAttribute("fill", "black");
    expect(screen.queryByTestId(Rooms.Sales)).toHaveAttribute("fill", "black");
  });
  it("renders with right date format, seat text seat 2 and room 2 filled", () => {
    render(<BookingItem {...{ ...defaultProps, seatId: 12, roomId: 2 }} />);
    expect(screen.getByText(/Sete 12/i)).toBeInTheDocument();
    expect(screen.getByTestId(Rooms.Large)).toBeInTheDocument();
    expect(screen.queryByTestId(Rooms.Large)).toHaveAttribute("fill", "black");
    expect(screen.queryByTestId(Rooms.Small)).toHaveAttribute("fill", "red");
    expect(screen.queryByTestId(Rooms.Sales)).toHaveAttribute("fill", "black");
    expect(screen.queryByTestId("delete-btn")).not.toBeInTheDocument();
  });

  it("renders with delete button if onDelete is sent in", () => {
    const propsWithDelete = {
      ...defaultProps,
      onDelete: jest.fn(),
    };
    render(<BookingItem {...propsWithDelete} />);
    expect(screen.queryByTestId("delete-btn")).toBeInTheDocument();
  });
});
