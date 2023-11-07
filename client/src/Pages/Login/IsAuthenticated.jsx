import { CircularProgress } from "@mui/material";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
export const IsAuthenticated = ()=> {
    const navigate = useNavigate();
    
    return (
        <div className="flex flex-col">
            <CircularProgress size={100} style={{ margin: '10vh auto' }} />
            <p className="text-lg text-center">Loading...</p>
        </div>
    )
}