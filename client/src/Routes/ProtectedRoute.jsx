import { useAuthContext } from "../api/useAuthContext";
import { Navigate } from "react-router-dom";
import Heading from "../components/Heading";

type ProtectedRouteProps = {
    outlet: JSX.element;
}
export const ProtectedRoute = ({outlet}) => {
  const context = useAuthContext();
  
  if (context?.user?.isAuthenticated) {
    return outlet;
  }

  return (
    <div className="h-[60vh] flex flex-col gap-2 justify-center items-center">
      <Heading
        center
        title="Unauthorized!"
        subTitle="Please return and login"
      />
    </div>
  );
};
