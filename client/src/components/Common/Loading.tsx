import { CircularProgress } from "@mui/material";

const Loading = () => {
  return (
    <div className="flex flex-col">
      <CircularProgress size={100} style={{ margin: "10vh auto" }} />
      <p className="text-lg text-center">Loading...</p>
    </div>
  );
};

export default Loading;
