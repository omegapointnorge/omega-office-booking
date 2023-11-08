import logo from './logo.svg';
import './App.css';
import Routers from "./Routes/Routers";

import React from "react";
import LoginPage from './pages/Login/LoginPage'
import  {Route, Routes} from 'react-router-dom';
import OverviewPage from "./pages/Overview/OverviewPage";
import {IsAuthenticated} from "./pages/Login/IsAuthenticated";
import {BigRoom} from "./pages/Rooms/BigRoom";

export default function App() {

    return (
        <div className="App">
            <Routers />
        </div>)
}
App.displayName = App.name
