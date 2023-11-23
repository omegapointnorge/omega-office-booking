import { useAuthContext } from "../api/useAuthContext";
import Heading from "../components/Heading";

export const ProtectedRoute = ({ outlet }) => {
  const context = useAuthContext();

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
