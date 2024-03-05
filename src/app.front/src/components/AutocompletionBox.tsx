import React, { PropsWithChildren } from "react";

function AutocompletionBox({ children }: PropsWithChildren) {
  return (
    <div className="rounded-[32px] shadow-md border p-[32px] w-full">
      {children}
    </div>
  );
}

export default AutocompletionBox;
