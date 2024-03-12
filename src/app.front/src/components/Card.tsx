import React, { PropsWithChildren } from "react";

interface CardProps extends PropsWithChildren {
  title: string;
}
function Card({ title, children }: CardProps) {
  return (
    <div className="rounded-[32px] shadow-md border p-[32px] w-[500px] bg-white">
      <h1 className="font-bold">{title}</h1>
      {children}
    </div>
  );
}

export default Card;
