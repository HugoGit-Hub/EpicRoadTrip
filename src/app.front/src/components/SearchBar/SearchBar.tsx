import React, { ForwardedRef, forwardRef, useState } from "react";
import { City } from "../../services/cities";
import SubmitButtonSearchBar from "./SubmitButtonSearchBar";
import CitySearchBar from "./CitySearchBar";
import BudgetSearchBar from "./BudgetSearchBar";
import ArrowRightIcon from "../../icons/arrow-right.svg";
import DateRangePicker from "./DateRangePicker";

interface SearchBarProps {
  onSubmit: (data: {
    depart: City;
    destination: City;
    budget: number;
    periode: [Date | undefined, Date | undefined];
  }) => void;
}

function SearchBar(
  { onSubmit }: SearchBarProps,
  ref: ForwardedRef<HTMLDivElement>
) {
  const [budget, setBudget] = useState<number>(0);
  const [depart, setDepart] = useState<City | undefined>(undefined);
  const [destination, setDestination] = useState<City | undefined>(undefined);
  const [periode, setPeriode] = useState<[Date | undefined, Date | undefined]>([
    undefined,
    undefined,
  ]);
  const [activeField, setActiveField] = useState("");

  return (
    <div id="searchBar" className="flex flex-col gap-2">
      <div
        className={`"transition-all flex flex-row items-center w-fit`}
        ref={ref}
      >
        <div
          className={`flex flex-row items-center relative shadow-md border flex flex-row overflow-hidden w-fit rounded-full ${activeField != "" ? "bg-zinc-100" : "bg-white"}`}
        >
          <CitySearchBar
            label="Départ"
            city={depart}
            placeholder="Votre départ"
            active={activeField === "depart"}
            onClick={() => {
              setActiveField("depart");
            }}
            onCitySelected={(city) => {
              setDepart(city);
            }}
          />
          <CitySearchBar
            label="Destination"
            city={destination}
            placeholder="Votre destination"
            active={activeField === "destination"}
            onClick={() => {
              setActiveField("destination");
            }}
            onCitySelected={(city) => {
              setDestination(city);
            }}
          />
          <DateRangePicker
            active={activeField === "periode"}
            label="Période"
            value={periode}
            placeholder="Votre période"
            onClick={() => {
              setActiveField("periode");
            }}
            onChange={setPeriode}
          />
          <BudgetSearchBar
            label="Budget"
            placeholder="Votre budget"
            value={budget}
            active={activeField === "budget"}
            onChange={(value) => {
              setBudget(value);
            }}
            onClick={() => {
              setActiveField("budget");
            }}
          />
          <SubmitButtonSearchBar
            text="Rechercher"
            onSubmit={() => {
              setActiveField("");
              if (depart && destination) {
                onSubmit({
                  depart,
                  destination,
                  budget,
                  periode,
                });
              } else {
                alert("Il manque un filtre");
              }
            }}
            active={activeField != ""}
          />
        </div>
      </div>
    </div>
  );
}

export default forwardRef<HTMLDivElement, SearchBarProps>(SearchBar);
