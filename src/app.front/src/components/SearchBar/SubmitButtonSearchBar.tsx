import React from "react";
import SearchIcon from "../../icons/search.svg";

interface SubmitButtonSearchBarProps {
  text: string;
  active?: boolean;
  onSubmit: () => void;
}
function SubmitButtonSearchBar({
  text,
  active,
  onSubmit,
}: SubmitButtonSearchBarProps) {
  return (
    <button
      onClick={onSubmit}
      className={`rounded-full bg-gradient-to-r from-rose-500 to-fuchsia-700  hover:from-rose-600 hover:to-fuchsia-700 flex flex-row justify-center items-center ml-[32px] mr-[10px] ${
        !active ? "w-[50px] h-[50px]" : "px-[32px] py-[15px]"
      }`}
    >
      <img width={25} src={SearchIcon} />
      <span
        className={`text-white transition-all font-bold overflow-hidden ${
          active ? "ml-[10px] w-fit" : "w-0"
        }`}
      >
        {text}
      </span>
    </button>
  );
}

export default SubmitButtonSearchBar;
