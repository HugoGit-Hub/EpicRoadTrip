import React, { useRef } from "react";
import AutocompletionBox from "./Box";
import closeIcon from "../../icons/close.svg";
import { createPortal } from "react-dom";

interface BudgetSearchBarProps {
  active?: boolean;
  label: string;
  value: number;
  placeholder?: string;
  min?: number;
  max?: number;
  onClick: () => void;
  onChange: (value: number) => void;
}

function BudgetSearchBar({
  active,
  label,
  value,
  placeholder,
  min = 1,
  max = 1500,
  onClick,
  onChange,
}: BudgetSearchBarProps) {
  const clearButton = useRef<HTMLDivElement>(null);

  return (
    <>
      <div
        className={`rounded-full h-[66px] px-[32px] flex flex-col items-left justify-center w-[200px] ${
          !active ? "hover:bg-zinc-100 cursor-pointer" : "bg-white shadow-md"
        }  relative`}
        onClick={(event) => {
          if (!active && event.target != clearButton.current) {
            onClick();
          }
        }}
      >
        <p className="font-bold text-[0.75rem]">{label}</p>
        <p className={`text-[0.875rem] ${!value ? "text-gray-400" : ""}`}>
          {Boolean(value) ? `${value} €` : placeholder}
        </p>
        {Boolean(value) && active && (
          <div
            ref={clearButton}
            className="absolute right-[20px] rounded-full hover:bg-zinc-100 p-[2px] cursor-pointer"
            onClick={() => {
              onChange(0);
            }}
          >
            <img width={17} src={closeIcon} />
          </div>
        )}
      </div>
      {active &&
        createPortal(
          <AutocompletionBox>
            <p className="font-bold text-[22px] mb-[16px]">Votre budget</p>

            <div className="relative mb-6">
              <input
                className="bg-gradient-to-r from-rose-500 to-fuchsia-700 w-full h-3 rounded-lg appearance-none cursor-pointer range-sm"
                type="range"
                min={min}
                max={max}
                value={value}
                onChange={(event) => {
                  onChange(Number(event.target.value));
                }}
              />
              <span className="text-sm text-gray-500 dark:text-gray-400 absolute start-0 -bottom-6">
                Min (1 €)
              </span>
              <span className="text-sm text-gray-500 dark:text-gray-400 absolute start-1/3 -translate-x-1/2 rtl:translate-x-1/2 -bottom-6">
                500 €
              </span>
              <span className="text-sm text-gray-500 dark:text-gray-400 absolute start-2/3 -translate-x-1/2 rtl:translate-x-1/2 -bottom-6">
                1000 €
              </span>
              <span className="text-sm text-gray-500 dark:text-gray-400 absolute end-0 -bottom-6">
                Max (1500 €)
              </span>
            </div>
          </AutocompletionBox>,
          document.getElementById("searchBar") || document.body
        )}
    </>
  );
}

export default BudgetSearchBar;
