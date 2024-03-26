import { ForwardedRef, forwardRef, useState } from "react";
import toast from "react-hot-toast";
import asteriskIcon from "../../icons/asterisk.svg";
import barIcon from "../../icons/bar.svg";
import bikeIcon from "../../icons/bike.svg";
import carIcon from "../../icons/car.svg";
import eventIcon from "../../icons/event.svg";
import hotelIcon from "../../icons/hotel.svg";
import restaurantIcon from "../../icons/restaurant.svg";
import subwayIcon from "../../icons/subway.svg";
import walkIcon from "../../icons/walk.svg";
import { City } from "../../services/cities";
import BudgetSearchBar from "./BudgetSearchBar";
import CitySearchBar from "./CitySearchBar";
import DateRangePicker from "./DateRangePicker";
import MultipleSelect from "./MultipleSelect";
import SubmitButtonSearchBar from "./SubmitButtonSearchBar";

export interface IFilters {
  depart: City | undefined;
  destination: City | undefined;
  budget: number;
  periode: [Date | undefined, Date | undefined];
  transports: (number)[];
  loisirs: (number)[];
}

interface SearchBarProps {
  filters: IFilters;
  onChange: (data: IFilters) => void;
  onSubmit: () => void;
  active?: boolean;
}

export const transports = [
  { icon: subwayIcon, value: 5, label: "Train" },
  { icon: walkIcon, value: 0, label: "A pieds" },
  { icon: bikeIcon, value: 1, label: "Vélo" },
  { icon: carIcon, value: 4, label: "Voiture" },
];

export const loisirs = [
  { icon: barIcon, value: 0, label: "Bar" },
  { icon: eventIcon, value: 1, label: "Evènements" },
  { icon: hotelIcon, value: 2, label: "Hôtel" },
  { icon: restaurantIcon, value: 3, label: "Restaurant" },
];

function SearchBar(
  { filters, active, onSubmit, onChange }: SearchBarProps,
  ref: ForwardedRef<HTMLDivElement>
) {
  const [activeField, setActiveField] = useState("");

  return (
    <div id="searchBar" className="flex flex-col gap-2 z-50">
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
            active={activeField === "depart" && active}
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
            active={activeField === "destination" && active}
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
            active={activeField === "transports" && active}
            options={[
              {
                icon: asteriskIcon,
                value: -1,
                label: "Tous",
                desactiveAll: true,
              },
              ...transports
            ]}
            onChange={(transports: number[]) => {
              onChange({ ...filters, transports });
            }}
            onClick={() => {
              setActiveField("transports");
            }}
          />
          <MultipleSelect
            label="Loisirs"
            placeholder="Vos loisirs"
            value={filters.loisirs}
            active={activeField === "loisirs" && active}
            options={[
              {
                icon: asteriskIcon,
                value: -1,
                label: "Tous",
                desactiveAll: true,
              },
              ...loisirs
            ]}
            onChange={(loisirs: number[]) => {
              onChange({ ...filters, loisirs });
            }}
            onClick={() => {
              setActiveField("loisirs");
            }}
          />
          <DateRangePicker
            active={activeField === "periode" && active}
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
            active={activeField === "budget" && active}
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
                toast.error("Il manque un ou plusieurs filtres !")
              }
            }}
            active={activeField != "" && active}
          />
        </div>
      </div>
    </div>
  );
}

export default forwardRef<HTMLDivElement, SearchBarProps>(SearchBar);
