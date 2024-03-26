import React, { useRef } from "react";
import closeIcon from "../../icons/close.svg";
import { createPortal } from "react-dom";
import Box from "./Box";
import { Calendar } from "../ui/calendar";
import { formatDate } from "../../lib/utils";

interface DateRangePickerProps {
  active?: boolean;
  label: string;
  value: [Date | undefined, Date | undefined];
  onClick: () => void;
  placeholder: string;
  onChange: (value: [Date | undefined, Date | undefined]) => void;
}
function DateRangePicker({
  active,
  label,
  value,
  onClick,
  onChange,
  placeholder,
}: DateRangePickerProps) {
  const clearButton = useRef(null);
  return (
    <>
      <div
        className={`rounded-full h-[66px] px-[32px] text-[0.75rem] flex flex-col items-left justify-center w-[250px] ${
          !active ? "hover:bg-zinc-100 cursor-pointer" : "bg-white shadow-md"
        }  relative`}
        onClick={(event) => {
          if (!active && event.target != clearButton.current) {
            onClick();
          }
        }}
      >
        <p className="font-bold">{label}</p>
        <p
          className={`text-[0.875rem] ${
            !value[0] && !value[1] ? "text-gray-400" : ""
          }`}
        >
          {Boolean(value[0] && value[1])
            ? `${formatDate(value[0])} - ${formatDate(value[1])} `
            : placeholder}
        </p>
        {Boolean(value) && active && (
          <div
            ref={clearButton}
            className="absolute right-[20px] rounded-full hover:bg-zinc-100 p-[2px] cursor-pointer"
            onClick={() => {
              onChange([undefined, undefined]);
            }}
          >
            <img width={17} src={closeIcon} />
          </div>
        )}
      </div>
      {active &&
        createPortal(
          <Box>
            <div className="flex flex-row items-center justify-around">
              <div className="flex flex-col items-center">
                <p className="font-bold">Début</p>
                <Calendar
                  mode="single"
                  selected={value[0]}
                  onSelect={(date) => {
                    onChange([date, value[1]]);
                  }}
                  disabled={(date) => date < new Date()}
                  initialFocus
                />
              </div>
              <div className="flex flex-col items-center">
                <p className="font-bold">Fin</p>
                <Calendar
                  mode="single"
                  selected={value[1]}
                  onSelect={(date) => {
                    onChange([value[0], date]);
                  }}
                  disabled={(date) =>
                    date < new Date() || (value[0] ? date < value[0] : false)
                  }
                  initialFocus
                />
              </div>
            </div>
          </Box>,
          document.getElementById("searchBar") || document.body
        )}
    </>
  );
}

export default DateRangePicker;
