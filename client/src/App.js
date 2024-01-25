import './index.css';
import Routers from "./core/routes/Routers";
import React from "react";
import { Navbar } from "./components/Navbar/Navbar";
import { useAuthContext } from "./core/auth/useAuthContext";
import { Toaster } from "react-hot-toast";
export default function App() {
    
    const context = useAuthContext();
    return (
        <div className="body">
            <div><Toaster/></div>
            {context?.user?.isAuthenticated ? <Navbar /> : undefined}
            <Routers />
        </div>)
}
