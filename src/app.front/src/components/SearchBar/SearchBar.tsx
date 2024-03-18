import React, { ForwardedRef, forwardRef, useState } from "react";
import { City } from "../../services/cities";
import SubmitButtonSearchBar from "./SubmitButtonSearchBar";
import CitySearchBar from "./CitySearchBar";
import BudgetSearchBar from "./BudgetSearchBar";
import DateRangePicker from "./DateRangePicker";
import MultipleSelect from "./MultipleSelect";
import bikeIcon from "../../icons/bike.svg"
import walkIcon from "../../icons/walk.svg"
import busIcon from "../../icons/bus.svg"
import carIcon from "../../icons/car.svg"
import asteriskIcon from "../../icons/asterisk.svg"


export interface IFilters {
  depart: City | undefined;
  destination: City | undefined;
  budget: number;
  periode: [Date | undefined, Date | undefined];
  transports: string[]
}

interface SearchBarProps {
  filters: IFilters;
  onChange: (data: IFilters) => void;
  onSubmit: () => void;
}

function SearchBar(
  { filters, onSubmit, onChange }: SearchBarProps,
  ref: ForwardedRef<HTMLDivElement>
) {
  const [activeField, setActiveField] = useState("");

  return (
    <div id="searchBar" className="flex flex-col gap-2">
      <div
        className={`"transition-all flex flex-row items-center w-fit`}
        ref={ref}
      >
        <div
          className={`flex flex-row items-center relative shadow-md border flex flex-row overflow-hidden w-fit rounded-full ${
            activeField != "" ? "bg-zinc-100" : "bg-white"
          }`}
        >
          <CitySearchBar
            label="Départ"
            city={filters.depart}
            placeholder="Votre départ"
            active={activeField === "depart"}
            onClick={() => {
              setActiveField("depart");
            }}
            onCitySelected={(depart) => {
              onChange({ ...filters, depart });
            }}
          />
          <CitySearchBar
            label="Destination"
            city={filters.destination}
            placeholder="Votre destination"
            active={activeField === "destination"}
            onClick={() => {
              setActiveField("destination");
            }}
            onCitySelected={(destination) => {
              onChange({ ...filters, destination });
            }}
          />
          <MultipleSelect
            label="Transports"
            placeholder="Vos transports"
            value={filters.transports}
            active={activeField === "transports"}
            options={[
              { icon: asteriskIcon, value: "All", label: "Tous", desactiveAll: true},
              { icon: busIcon, value: "Bus", label: "Bus" },
              { icon: walkIcon, value: "Marche", label: "Marche" },
              { icon: bikeIcon, value: "Velo", label: "Vélo" },
              { icon: carIcon, value: "Voiture", label: "Voiture" },
            ]}
            onChange={(transports) => {
              onChange({...filters, transports})
            }}
            onClick={() => {
              setActiveField("transports");
            }}
          />
          <DateRangePicker
            active={activeField === "periode"}
            label="Période"
            value={filters.periode}
            placeholder="Votre période"
            onClick={() => {
              setActiveField("periode");
            }}
            onChange={(periode) => {
              onChange({ ...filters, periode });
            }}
          />
          <BudgetSearchBar
            label="Budget"
            placeholder="Votre budget"
            value={filters.budget}
            active={activeField === "budget"}
            onChange={(budget) => {
              onChange({ ...filters, budget });
            }}
            onClick={() => {
              setActiveField("budget");
            }}
          />
          <SubmitButtonSearchBar
            text="Rechercher"
            onSubmit={() => {
              setActiveField("");
              if (
                filters.depart &&
                filters.destination &&
                filters.periode[0] &&
                filters.periode[1]
              ) {
                onSubmit();
              } else {
                alert("Il manque un ou plusieurs filtre");
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
