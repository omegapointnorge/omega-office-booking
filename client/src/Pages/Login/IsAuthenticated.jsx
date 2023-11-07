import { CircularProgress } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useIsAuthenticated } from "../../api/useIsAuthenticated";
export const IsAuthenticated = () => {
  const navigate = useNavigate();

  const { isSuccess, isFetched, error, isError } = useIsAuthenticated();

  useEffect(() => {
    if (isError) {
      navigate("/login");
      console.log(error);
    }

    if (isFetched) {
      if (isSuccess) {
        navigate("/overview");
      }
    } else {
      console.error("Couldnt authenticate");
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
