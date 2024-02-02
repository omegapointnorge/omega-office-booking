import { useState, useEffect } from "react";
import { getUser } from "@services/userService";
import AuthContext from "@auth/AuthContext";

// TODO: trekk ut interface til type/
interface User {
  isAuthenticated: boolean;
}

interface AuthProviderType {
  children: React.ReactNode;
}
export const AuthProvider: React.FC<AuthProviderType> = ({ children }) => {
  const [user, setUser] = useState<User | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getUser()
      .then((response) => {
        setUser(response);
      })
      .catch((e) => {
        setUser({ isAuthenticated: false });
        console.error(e);
      })
      .finally(() => setLoading(false));
  }, []);

  return (
    <AuthContext.Provider value={{ user, loading }}>
      {children}
    </AuthContext.Provider>
  );
};
