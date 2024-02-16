import React from "react";

import { useState, useEffect } from "react";
import { getUser } from "@services/userService";
import AuthContext from "@auth/AuthContext";
import { UserRole } from "@/shared/types/enums";

interface UserContextType {
  isAuthenticated: boolean;
  claims?: {
    userName: string;
    objectidentifier: string;
    email: string;
    role: UserRole;
  };
}
export const AuthProvider: React.FC = ({ children }) => {
  const [user, setUser] = useState<UserContextType | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getUser()
      .then((response) => {
        setUser(response);
      })
      .catch(() => setUser({ isAuthenticated: false }))
      .finally(() => setLoading(false));
  }, []);

  return (
    <AuthContext.Provider value={{ user, loading }}>
      {children}
    </AuthContext.Provider>
  );
};
