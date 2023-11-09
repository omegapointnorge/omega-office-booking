const Heading = ({ title, subTitle })=> {
  return (
    <div className="text-center">
      <div className="text-2xl font-bold">{title}</div>
      <div className="font-light mt-2">{subTitle}</div>
    </div>
  );
};

export default Heading;
