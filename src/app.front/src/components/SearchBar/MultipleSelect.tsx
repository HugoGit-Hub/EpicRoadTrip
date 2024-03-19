import React, { useRef } from "react";
import closeIcon from "../../icons/close.svg";
import { createPortal } from "react-dom";
import Box from "./Box";

interface IMultipleSelectProps {
  label: string;
  placeholder: string;
  value: string[];
  options: {
    icon: string;
    label: string;
    value: string;
    desactiveAll?: boolean;
  }[];
  active?: boolean;
  onChange: (value: string[]) => void;
  onClick: () => void;
}

function MultipleSelect({
  label,
  placeholder,
  value,
  active,
  options,
  onChange,
  onClick,
}: IMultipleSelectProps) {
  const clearButton = useRef(null);
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
        <p
          className={`text-[0.875rem] truncate ${
            !value.length ? "text-gray-400" : ""
          }`}
        >
          {Boolean(value.length)
            ? `${value
                .map(
                  (el) => options.find((option) => option.value === el)?.label
                )
                .join(", ")}`
            : placeholder}
        </p>
        {Boolean(value) && active && (
          <div
            ref={clearButton}
            className="absolute right-[20px] rounded-full hover:bg-zinc-100 p-[2px] cursor-pointer"
            onClick={() => {
              onChange([]);
            }}
          >
            <img width={17} src={closeIcon} />
          </div>
        )}
      </div>
      {active &&
        createPortal(
          <Box>
            <p className="font-bold text-[22px] mb-[16px]">
              Moyens de transport
            </p>

            <div className="relative mb-6 flex flex-row gap-[25px]">
              {options.map((option) => {
                const optionChoosen = value.includes(option.value);
                return (
                  <div
                    key={option.value}
                    onClick={() => {
                      if (optionChoosen) {
                        onChange(
                          value.filter((element) => element !== option.value)
                        );
                      } else {
                        if (option.desactiveAll) {
                          onChange([option.value]);
                        } else {
                          const valueDesactiveAllElement =
                            options.find((el) => el.desactiveAll)?.value ||
                            "NOT FIND";
                          if (value.includes(valueDesactiveAllElement)) {
                            onChange([option.value]);
                          } else {
                            onChange([...value, option.value]);
                          }
                        }
                      }
                    }}
                    className={`w-[80px] h-[80px] cursor-pointer rounded flex flex-col items-center justify-center hover:shadow ${
                      optionChoosen
                        ? "bg-gradient-to-r from-rose-500 to-fuchsia-700"
                        : " hover:bg-zinc-100"
                    }`}
                  >
                    <img
                      src={option.icon}
                      width={30}
                      className={
                        !optionChoosen ? "filter invert text-black" : ""
                      }
                    />
                    <p
                      className={`"font-bold" ${
                        optionChoosen ? "text-white" : ""
                      }`}
                    >
                      {option.label}
                    </p>
                  </div>
                );
              })}
            </div>
          </Box>,
          document.getElementById("searchBar") || document.body
        )}
    </>
  );
}

export default MultipleSelect;
