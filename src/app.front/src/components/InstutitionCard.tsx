import React from "react";
import { Button } from "./ui/button";

export interface IInstitution {
  name: string;
  address: string;
  lat: number;
  lng: number;
  email: string;
  phoneNumber: string;
  previewUrl: null | string;
  price: number;
  type: 0 | 1 | 2 | 3;
  webSite: string;
  geoJson: any,
  geoJsonData: any
}

interface IInstitutionProps extends IInstitution {
  onMap?: boolean;
  onClose?: () => void;
  selected?: boolean
  onSelected?: () => void
}
function InstutitionCard({
  name,
  address,
  phoneNumber,
  email,
  webSite,
  previewUrl,
  onMap,
  onClose,
  selected,
  onSelected
}: IInstitutionProps) {
  return (
    <div className="bg-white mb-10 p-4 rounded shadow flex flex-row gap-4 w-max-[400px]">
      {previewUrl && (
        <div className="overflow-hidden w-[100px] h-[150px]">
          <img src={previewUrl} />
        </div>
      )}
      <div className="flex flex-col justify-between w-full">
        <p className="font-bold text-xl bg-clip-text text-transparent bg-gradient-to-r from-rose-500 to-fuchsia-700">{name}</p>
        <p>{address}</p>
        {(phoneNumber || email || webSite) && (
          <div className="flex flex-col ">
            <p className="font-bold">Moyen de contact :</p>
            {phoneNumber && (
              <a className="ml-2" href={"tel:" + phoneNumber}>
                {phoneNumber}
              </a>
            )}
            {email && (
              <a className="ml-2" href={"mailto:" + email}>
                {email}
              </a>
            )}
            {webSite && (
              <a className="ml-2" href={webSite}>
                {webSite}
              </a>
            )}
          </div>
        )}
        <div className="flex flex-row justify-end mt-4 gap-4">
          {onSelected && <Button variant={selected ? 'red' : 'default'} onClick={onSelected}>{selected ? "Dé-selectionner" : "Choisir"}</Button>}
          {onMap ? (
            <Button variant="gradient" onClick={onClose}>
              Fermer
            </Button>
          ) : (
            <Button variant="gradient">Voir</Button>
          )}
        </div>
      </div>
    </div>
  );
}

export default InstutitionCard;
