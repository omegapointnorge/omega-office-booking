import React from "react";

interface HeadingProps {
  title: string;
  subTitle?: string;
}

export const Heading = ({ title, subTitle }: HeadingProps) => {
  return (
    <div className="text-center">
      <h1 className="text-2xl font-bold heading">{title}</h1>
      {subTitle && <div className="font-light mt-2">{subTitle}</div>}
    </div>
  );
};
