import React from "react";
import { render, screen } from "@testing-library/react";
import { Loading } from "../Loading";

describe("Loading", () => {
  const loadingText = "Loading...";

  it("renders with given props", () => {
    render(<Loading />);
    expect(screen.getByText(loadingText)).toBeInTheDocument();
    expect(screen.getByRole("progressbar")).toBeInTheDocument();
  });
});
