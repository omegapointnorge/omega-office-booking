import React from "react";

import { useNavigate } from "react-router-dom";
import { useAuthContext } from "@auth/useAuthContext";
import { useEffect } from "react";
import Loading from "@common-components/Loading";

export const IsAuthenticated = () => {
  const navigate = useNavigate();
  const context = useAuthContext();

  useEffect(() => {
    if (context?.user?.isAuthenticated) {
      console.log("User is authenticated, redirecting to overview..");
      navigate("/overview");
    } else {
      navigate("/login");
    }
  }, [context?.user?.isAuthenticated, navigate]);

  return <Loading />;
};
