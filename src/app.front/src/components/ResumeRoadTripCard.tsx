import React, { PropsWithChildren, SyntheticEvent, useState } from "react";
import Card from "./Card";
import { Button } from "./ui/button";
import SaveIcon from "../icons/save.svg";
import EditIcon from "../icons/edit.svg";
import moneyBagIcon from "../icons/moneyBag.svg";
import calendarIcon from "../icons/calendar.svg";
import mapIcon from "../icons/map.svg";
import busGradientIcon from "../icons/bus-gradient.svg";
import barGradientIcon from "../icons/bar-gradient.svg";

import { formatDate } from "../lib/utils";
import { IFilters, loisirs, transports } from "./SearchBar/SearchBar";
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "./ui/dialog";

interface IRoadTripCardProps {
  filters: IFilters;
  onEdit: () => void;
  onSave: (title: string) => void;
}

interface ITailwindRowProps extends PropsWithChildren {
  label: string;
  value: string;
}

function TailwindRow({ children, label, value }: ITailwindRowProps) {
  return (
    <div className="flex flex-row gap-[10px] mt-[15px]">
      {children}
      <div className="flex flex-col gap-[4px]">
        <p className="font-bold">{label}</p>
        <p>{value}</p>
      </div>
    </div>
  );
}

function RoadTripCard({ filters, onEdit, onSave }: IRoadTripCardProps) {
  const [title, setTitle] = useState('');

  return (
    <Card title="Votre trajet">
      <TailwindRow
        label="Itinéraire"
        value={`${filters.depart?.name} ➜ ${filters.destination?.name}`}
      >
        <img src={mapIcon} width={45} className="mx-[8px]" />
      </TailwindRow>
      <TailwindRow
        label="Période"
        value={`Du ${formatDate(filters.periode[0])} au ${formatDate(
          filters.periode[1]
        )}`}
      >
        <img src={calendarIcon} width={45} className="mx-[8px]" />
      </TailwindRow>
      <TailwindRow label="Budget" value={`${filters.budget} €`}>
        <img src={moneyBagIcon} width={60} />
      </TailwindRow>
      <TailwindRow
        label="Transports"
        value={filters.transports
          .map(
            (transport) =>
              transports.find(({ value }) => value === transport)?.label
          )
          .join(", ")}
      >
        <img src={busGradientIcon} width={50} className="mx-[6px]" />
      </TailwindRow>

      <TailwindRow
        label="Loisirs"
        value={filters.loisirs
          .map((loisir) => loisirs.find(({ value }) => value === loisir)?.label)
          .join(", ")}
      >
        <img src={barGradientIcon} width={50} className="mx-[6px]" />
      </TailwindRow>

      <div className="flex flex-row gap-[15px] items-center mt-[32px] justify-end">
        <Button variant="gradient" onClick={onEdit}>
          <img src={EditIcon} width={20} className="filter invert text-white" />
          <span className="ml-[10px]">Modifier</span>
        </Button>
        <Dialog>
          <DialogTrigger>
            <Button variant="gradient">
              <img
                src={SaveIcon}
                width={20}
                className="filter invert text-white"
              />
              Enregistrer
            </Button>
          </DialogTrigger>
          <DialogContent>
            <DialogHeader>
              <DialogTitle>Enregistrer le roadTrip</DialogTitle>
              <DialogDescription>
                <form onSubmit={(e) => {
                  e.preventDefault()
                  onSave(title)
                }}>
                  <input value={title} onChange={((e) => {
                    setTitle(e.target.value)
                  })}/>
                  <DialogClose>
                  <Button type="submit" variant="gradient">Enregistrer</Button>
                  </DialogClose>
                </form>
              </DialogDescription>
            </DialogHeader>
          </DialogContent>
        </Dialog>
      </div>
    </Card>
  );
}

export default RoadTripCard;
