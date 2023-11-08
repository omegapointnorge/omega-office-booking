import { Routes, Route } from "react-router-dom";
import {IsAuthenticated} from "../Pages/Login/IsAuthenticated";
import LoginPage from "../Pages/Login/LoginPage";
import OverviewPage from "../Pages/Overview/OverviewPage";
import {ProtectedRoute} from "./ProtectedRoute";


const Routers = () => {
    return (<Routes>
        <Route path="/" element={<IsAuthenticated />}></Route>
        <Route path="/login" element={<LoginPage />}></Route>

        {/* PROTECTED ROUTES */}
        <Route path="/overview" element={<ProtectedRoute />}></Route>
    </Routes>)
}

export default Routers;