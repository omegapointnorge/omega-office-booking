import { useAuthContext } from "../auth/useAuthContext";
import Heading from "../../components/Common/Heading";

export const ProtectedRoute = ({ outlet }) => {
  const context = useAuthContext();

  if(context?.loading) {
    return "Loading...";
  }

  if (context?.user?.isAuthenticated) {
    return outlet;
  }

  console.log("You are not authenticated, please login first");

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
