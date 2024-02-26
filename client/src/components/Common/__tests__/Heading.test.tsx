import React from "react";
import { render, screen } from "@testing-library/react";
import { Heading } from "../Heading";

describe("Heading", () => {
  const defaultProps = {
    title: "Test Heading Comp",
    subTitle: "",
  };

  it("renders with given props", () => {
    render(<Heading {...defaultProps} />);

    expect(screen.getByText(defaultProps.title)).toBeInTheDocument();
    expect(screen.getByRole("heading")).toBeInTheDocument();
  });

  it("renders with subTitle", () => {
    const props = { ...defaultProps, subTitle: "Test subtitle" };
    render(<Heading {...props} />);

    expect(screen.getByText("Test Heading Comp"));
    expect(screen.getByText("Test subtitle"));
  });

  it("does't render if title is not defined", () => {
    const props = { ...defaultProps, title: "" };
    render(<Heading {...props} />);
    expect(screen.queryByText("Test Heading Comp")).not.toBeInTheDocument();
    expect(screen.queryByText("Test subtitle")).not.toBeInTheDocument();
  });
});
