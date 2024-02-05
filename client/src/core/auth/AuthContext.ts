import { createContext } from "react";

interface AuthContextType {
  user: any;
  loading: boolean;
}
const AuthContext = createContext<AuthContextType | undefined>(undefined);
export default AuthContext;
