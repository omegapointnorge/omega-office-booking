interface HeadingProps {
  title: string;
  subTitle?: string;
}

const Heading = ({ title, subTitle }: HeadingProps) => {
  return (
    <div className="text-center">
      <div className="text-2xl font-bold heading">{title}</div>
      <div className="font-light mt-2">{subTitle}</div>
    </div>
  );
};

export default Heading;
