import { useState, useEffect } from 'react';
import { getUser } from '../api/userService.js'
import AuthContext from './AuthContext'
export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);
    
    useEffect(() => {
        getUser()
            .then(response => { setUser(response) })
            .catch(e => setUser({ isAuthenticated: false }))
            .finally(() => setLoading(false));
    }, []);

    return (
        <AuthContext.Provider value={{ user, loading }}>{children}</AuthContext.Provider>
    );
};