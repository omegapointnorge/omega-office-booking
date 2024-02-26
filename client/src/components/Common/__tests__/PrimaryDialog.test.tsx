import React from "react";
import { render, screen, fireEvent } from "@testing-library/react";
import { PrimaryDialog } from "../PrimaryDialog";
describe("PrimaryDialog", () => {
  const defaultProps = {
    title: "Test Dialog",
    open: true,
    handleClose: jest.fn(),
    onClick: jest.fn(),
    content: "This is a test content",
  };

  it("renders with given props", () => {
    render(<PrimaryDialog {...defaultProps} />);

    // Check if the dialog title and content are rendered
    expect(screen.getByText("Test Dialog")).toBeInTheDocument();
    expect(screen.getByText("This is a test content")).toBeInTheDocument();

    // Check if the buttons are rendered
    expect(screen.getByText("Bekreft")).toBeInTheDocument();
    expect(screen.getByText("Avbryt")).toBeInTheDocument();
  });

  it("calls handleClose and onClick when buttons are clicked", () => {
    render(<PrimaryDialog {...defaultProps} />);

    // Click the "Bekreft" button
    fireEvent.click(screen.getByText("Bekreft"));
    // Check if the onClick function is called
    expect(defaultProps.onClick).toHaveBeenCalled();

    // Click the "Avbryt" button
    fireEvent.click(screen.getByText("Avbryt"));
    // Check if the handleClose function is called
    expect(defaultProps.handleClose).toHaveBeenCalled();
  });

  it("renders without content when content prop is not provided", () => {
    const { rerender } = render(
      <PrimaryDialog {...defaultProps} content={undefined} />
    );

    // Check if the content is not rendered when content prop is not provided
    expect(
      screen.queryByText("This is a test content")
    ).not.toBeInTheDocument();

    // Update props to have content
    rerender(<PrimaryDialog {...defaultProps} content="New Content" />);

    // Check if the new content is rendered
    expect(screen.getByText("New Content")).toBeInTheDocument();
  });
});
