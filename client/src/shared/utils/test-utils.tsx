import React, { ReactElement } from "react";
import { render, RenderOptions } from "@testing-library/react";

import { AuthProvider } from "@auth/AuthProvider";

//TODO: use on tests
const AllTheProviders = ({ children }: { children: React.ReactNode }) => {
  return <AuthProvider>{children}</AuthProvider>;
};

const customRender = (
  ui: ReactElement,
  options?: Omit<RenderOptions, "wrapper">
) => render(ui, { wrapper: AllTheProviders, ...options });

export * from "@testing-library/react";
export { customRender as render };
