import React, { PropsWithChildren } from "react";

function Box({ children }: PropsWithChildren) {
  return (
    <div className="rounded-[32px] shadow-md border p-[32px] w-full bg-white transition-all">
      {children}
    </div>
  );
}

export default Box;
