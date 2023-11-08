import { CircularProgress } from "@mui/material";
import { useEffect, useRef } from "react";
import { useNavigate } from "react-router-dom";
import { useAuthContext } from "../../api/useAuthContext";
export const IsAuthenticated = () => {
  const navigate = useNavigate();
  const context = useAuthContext();
  const isMounted = useRef(false);
  useEffect(() => {
    isMounted.current = true;
    if (context?.user?.isAuthenticated) {
      console.log("User is authenticated, redirecting to overview..");
      isMounted.current = false;
      navigate("/overview");
    } else {
      isMounted.current = false;
      navigate("/login");
    }
  });

  return (
    <div className="flex flex-col">
      <CircularProgress size={100} style={{ margin: "10vh auto" }} />
      <p className="text-lg text-center">Loading...</p>
    </div>
  );
};
