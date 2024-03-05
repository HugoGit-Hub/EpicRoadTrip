import React, { PropsWithChildren } from "react";

function AutocompletionBox({ children }: PropsWithChildren) {
  return (
    <div className="rounded-[32px] shadow-md border p-[32px] w-full absolute top-[120px] bg-white transition-all">
      {children}
    </div>
  );
}

export default AutocompletionBox;
