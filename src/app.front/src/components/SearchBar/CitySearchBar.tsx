import React, { useRef, useState } from "react";
import closeIcon from "../../icons/close.svg";
import AutocompletionBox from "./Box";
import { City, getCitiesByWordSearch } from "../../services/cities";
import { getFlagUrlByCountryCode } from "../../services/country";
import { createPortal } from "react-dom";

interface CitySearchBarProps {
  label: string;
  placeholder: string;
  active?: boolean;
  city?: City;
  onCitySelected: (city: City | undefined) => void;
  onClick: () => void;
}

function CitySearchBar({
  label,
  placeholder,
  active,
  city,
  onCitySelected,
  onClick,
}: CitySearchBarProps) {
  const inputRef = useRef<HTMLInputElement>(null);
  const clearButton = useRef<HTMLDivElement>(null);
  const [propositions, setPropositions] = useState<City[]>([]);
  const [value, setValue] = useState(city?.name || "");

  return (
    <>
      <div
        className={`rounded-full h-[66px] px-[32px] text-[0.75rem] flex flex-col items-left justify-center w-fit ${
          !active ? "hover:bg-zinc-100 cursor-pointer" : "bg-white shadow-md"
        }  relative`}
        onClick={(event) => {
          if (!active && event.target != clearButton.current) {
            inputRef.current?.focus();
            onClick();
          }
        }}
      >
        <p className="font-bold">{label}</p>
        <div className="flex flex-row items-center">
          <input
            ref={inputRef}
            type="text"
            value={value}
            placeholder={placeholder}
            onChange={async (event) => {
              const { value } = event.target;
              setValue(value);
              if (city) onCitySelected(undefined);
              if (value.length > 2) {
                try {
                  setPropositions(
                    (await getCitiesByWordSearch(event.target.value)).geonames
                  );
                } catch (e) {
                  console.log(e);
                }
              } else {
                setPropositions([]);
              }
            }}
            onFocus={() => {
              onClick();
            }}
            className="border-0 focus:border-0 bg-transparent outline-none text-[0.875rem]"
          />
        </div>
        {value.length > 0 && active && (
          <div
            ref={clearButton}
            className="absolute right-[20px] rounded-full hover:bg-zinc-100 p-[2px] cursor-pointer"
            onClick={() => {
              setPropositions([]);
              setValue("");
              onCitySelected(undefined);
            }}
          >
            <img width={17} src={closeIcon} />
          </div>
        )}
      </div>
      {active && value.length > 2 && !city && (
        createPortal(<AutocompletionBox>
          {propositions.map((city) => {
            return (
              <div
                key={city.lat + city.lng}
                className="flex flex-row gap-[10px] cursor-pointer hover:bg-zinc-100"
                onClick={() => {
                  onCitySelected(city);
                  setValue(city.name);
                  setPropositions([]);
                }}
              >
                <img
                  src={getFlagUrlByCountryCode(city.countryCode)}
                  alt=""
                  className="rounded"
                />
                <div className="flex flex-col justify-center">
                  <p className="font-bold text-[0.875rem]">{city.name}</p>
                  <p className="text-gray-400 text-[0.75rem]">
                    {city.adminName1}
                    {city.adminName1 && ","} {city.countryName}
                  </p>
                </div>
              </div>
            );
          })}
          {propositions.length === 0 && (
            <p className="text-gray-400">Aucun résultat</p>
          )}
        </AutocompletionBox>, document.getElementById('searchBar') || document.body)
      )}
    </>
  );
}

export default CitySearchBar;
