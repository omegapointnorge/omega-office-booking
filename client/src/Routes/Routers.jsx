import { Routes, Route } from "react-router-dom";
import { IsAuthenticated } from "../Pages/Login/IsAuthenticated";
import LoginPage from "../Pages/Login/LoginPage";
import OverviewPage from "../Pages/Overview/OverviewPage";
import { ProtectedRoute } from "./ProtectedRoute";
import HistoryPage from "../Pages/History/HistoryPage";
import RoomPage from "../Pages/Rooms/RoomPage";

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
        path="/rooms/:roomId"
        element={<ProtectedRoute outlet={<RoomPage />} />}
      ></Route>
      <Route
        path="/history"
        element={<ProtectedRoute outlet={<HistoryPage />} />}
      ></Route>
    </Routes>
  );
};

export default Routers;
