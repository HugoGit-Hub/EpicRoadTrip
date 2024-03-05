import React, { useState } from "react";
import { City } from "../../services/cities";
import SubmitButtonSearchBar from "./SubmitButtonSearchBar";
import CitySearchBar from "./CitySearchBar";
import BudgetSearchBar from "./BudgetSearchBar";

interface SearchBarProps {
  onSubmit: (data: { depart: City; destination: City; budget: number }) => void;
}

function SearchBar({ onSubmit }: SearchBarProps) {
  const [budget, setBudget] = useState<number>(0);

  const [depart, setDepart] = useState<City | undefined>(undefined);

  const [destination, setDestination] = useState<City | undefined>(undefined);

  const [activeField, setActiveField] = useState("");

  return (
    <div className="flex flex-col gap-[12px] items-center w-fit absolute z-50">
      <div
        className={`flex flex-row items-center rounded-full shadow-md border flex flex-row overflow-hidden mt-10 w-fit ${
          activeField != "" ? "bg-zinc-100" : "bg-white"
        }`}
      >
        <CitySearchBar
          label="Départ"
          city={depart}
          placeholder="Rechercher un départ"
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
          placeholder="Rechercher une destination"
          active={activeField === "destination"}
          onClick={() => {
            setActiveField("destination");
          }}
          onCitySelected={(city) => {
            setDestination(city);
          }}
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
              });
            } else {
              alert("Il manque un filtre");
            }
          }}
          active={activeField != ""}
        />
      </div>
    </div>
  );
}

export default SearchBar;
