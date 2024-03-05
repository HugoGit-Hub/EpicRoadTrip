import React, { useRef } from "react";
import closeIcon from "../icons/close.svg";

interface InputTextSearchBarProps {
  label: string;
  value: string;
  placeholder: string;
  active?: boolean;
  onChange: (value: string) => void;
  onClick: () => void;
}

function InputTextSearchBar({
  label,
  placeholder,
  value,
  active,
  onChange,
  onClick,
}: InputTextSearchBarProps) {
  const inputRef = useRef<HTMLInputElement>(null);
  const clearButton = useRef<HTMLDivElement>(null);

  return (
    <div
      className={`rounded-full h-[66px] px-[32px] flex flex-col items-left justify-center w-[300px] ${
        !active ? "hover:bg-zinc-100 cursor-pointer" : "bg-white shadow-md"
      }  relative`}
      onClick={(event) => {
        console.log(event.target);
        if (!active && event.target != clearButton.current) {
          inputRef.current?.focus();
          onClick();
        }
      }}
    >
      <p className="font-bold">{label}</p>
      <input
        ref={inputRef}
        type="text"
        value={value}
        placeholder={placeholder}
        onChange={(event) => {
          onChange(event.target.value);
        }}
        onFocus={() => {
          onClick();
        }}
        className="border-0 focus:border-0 bg-transparent outline-none"
      />

      {value.length > 0 && active && (
        <div
          ref={clearButton}
          className="absolute right-[20px] rounded-full hover:bg-zinc-100 p-[2px] cursor-pointer"
          onClick={() => {
            onChange("");
          }}
        >
          <img width={17} src={closeIcon} />
        </div>
      )}
    </div>
  );
}

export default InputTextSearchBar;
