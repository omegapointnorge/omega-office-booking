import './App.css';
import Routers from "./Routes/Routers";

import React from "react";
import {Navbar} from "./components/Navbar/Navbar";
import {useAuthContext} from "./api/useAuthContext";
import {Toaster} from "react-hot-toast";
export default function App() {
    
    const context = useAuthContext();
    return (
        <div className="App">
            <div><Toaster/></div>
            {context?.user?.isAuthenticated ? <Navbar /> : undefined}
            <Routers />
        </div>)
}
