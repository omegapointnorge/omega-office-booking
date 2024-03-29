import React from "react";
import Routers from "./core/routes/Routers";
import { Navbar } from "@components/Navbar";
import { useAuthContext } from "@auth/useAuthContext";
import { Toaster } from "react-hot-toast";
import "@/index.css";

export default function App() {
  const context = useAuthContext();
  return (
    <div className="body">
      <div>
        <Toaster />
      </div>
      {context?.user?.isAuthenticated ? <Navbar /> : undefined}
      <Routers />
    </div>
  );
}
