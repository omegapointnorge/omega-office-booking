import React from "react";

import { Routes, Route } from "react-router-dom";
import { IsAuthenticated } from "@auth/IsAuthenticated";
import LoginPage from "@components/LoginPage/LoginPage";
import OverviewPage from "@components/OverviewPage/OverviewPage";
import { ProtectedRoute } from "@routes/ProtectedRoute";
import HistoryPage from "@components/HistoryPage/HistoryPage";

const Routers = () => {
  return (
    <Routes>
      <Route path="/" element={<IsAuthenticated />}></Route>
      <Route path="/login" element={<LoginPage />}></Route>

      {/* PROTECTED ROUTES */}
      <Route
        path="/overview"
        element={<ProtectedRoute outlet={<OverviewPage />} />}
      ></Route>

      <Route
        path="/history"
        element={<ProtectedRoute outlet={<HistoryPage />} />}
      ></Route>
    </Routes>
  );
};

export default Routers;
