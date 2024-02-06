import React from "react";

import { useState, useEffect } from "react";
import { getUser } from "@services/userService";
import AuthContext from "@auth/AuthContext";

export const AuthProvider: React.FC = ({ children }) => {
  //TODO: define user
  const [user, setUser] = useState<{ isAuthenticated: boolean } | any | null>(
    null
  );
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getUser()
      .then((response) => {
        setUser(response);
      })
      .catch((e) => setUser({ isAuthenticated: false }))
      .finally(() => setLoading(false));
  }, []);

  return (
    <AuthContext.Provider value={{ user, loading }}>
      {children}
    </AuthContext.Provider>
  );
};
