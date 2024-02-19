import React from "react";
import { CircularProgress } from "@mui/material";

export const Loading = () => {
  return (
    <div className="flex flex-col">
      <CircularProgress size={100} style={{ margin: "10vh auto" }} />
      <p className="text-lg text-center">Loading...</p>
    </div>
  );
};
