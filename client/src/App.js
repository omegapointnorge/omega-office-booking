import './App.css';
import Routers from "./Routes/Routers";

import React from "react";
import {Navbar} from "./components/Navbar/Navbar";
import {useAuthContext} from "./api/useAuthContext";
export default function App() {
    
    const context = useAuthContext();
    return (
        <div className="App">
            <Navbar />
            <Routers />
        </div>)
}
App.displayName = App.name
