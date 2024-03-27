import Card from "./Card";
import { transports } from "./SearchBar/SearchBar";
import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "./ui/accordion";
import { Button } from "./ui/button";

function addTimes(timeList) {
  // Initialiser les compteurs
  let totalHours = 0;
  let totalMinutes = 0;
  let totalSeconds = 0;

  // Parcourir la liste de temps
  timeList.forEach((time) => {
    // Convertir chaque temps en tableaux d'entiers
    const [hours, minutes, seconds] = time.split(":").map(Number);

    // Ajouter les heures, les minutes et les secondes
    totalHours += hours;
    totalMinutes += minutes;
    totalSeconds += seconds;
  });

  // Traiter les retenues
  totalMinutes += Math.floor(totalSeconds / 60);
  totalSeconds %= 60;

  totalHours += Math.floor(totalMinutes / 60);
  totalMinutes %= 60;

  totalHours %= 24; // Assurer que le total ne dépasse pas 24 heures

  // Formater le résultat
  const result = [
    String(totalHours).padStart(2, "0"),
    String(totalMinutes).padStart(2, "0"),
    String(totalSeconds).padStart(2, "0"),
  ].join(":");

  return result;
}

interface IItineraryCardProps {
  data: any[];
  selectedItinary?: any;
  onChange: (itinary: any) => void;
  isSelectable : boolean;
}

function ItineraryCard({
  data,
  selectedItinary,
  onChange,
  isSelectable
}: IItineraryCardProps) {
  return (
    <Card title={`Itinéraires (${Object.entries(data).length})`}>
      <Accordion type="single" collapsible>
        {Object.values(data).map((trajet: any, id) => {
          return (
            <AccordionItem value={`item-${id}`}>
              <AccordionTrigger>
                Itinéraire {id + 1} (
                {addTimes(trajet.map((e: any) => e.duration))})
              </AccordionTrigger>
              <AccordionContent>
                {trajet.map((e: any) => {
                  return (
                    <div className=" flex flex-row gap-[10px] items-center mt-[20px]">
                      <img
                        src={
                          transports
                            .find(({ value }) => value === e.transportType)
                            ?.icon.split(".svg")[0] + "-gradient.svg"
                        }
                        width={60}
                      />
                      <div className="flex flex-col gap-[3px]">
                        <p>{e.cityOneName} </p>
                        <p>{e.cityTwoName}</p>
                        <p className="font-bold">{e.duration}</p>
                      </div>
                    </div>
                  );
                })}
                {isSelectable && onChange && (
                  <div className="mt-4 flex flex-row items-center justify-end">
                    <Button
                      variant={
                        selectedItinary !== undefined &&
                        selectedItinary[0].routeGroup === trajet[0].routeGroup
                          ? "red"
                          : "default"
                      }
                      onClick={() => {
                        onChange(
                          selectedItinary !== undefined &&
                            selectedItinary[0].routeGroup ===
                              trajet[0].routeGroup
                            ? undefined
                            : trajet
                        );
                      }}
                    >
                      {selectedItinary !== undefined &&
                      selectedItinary[0].routeGroup === trajet[0].routeGroup
                        ? "Dé-selectionner"
                        : "Choisir"}
                    </Button>
                  </div>
                )}
              </AccordionContent>
            </AccordionItem>
          );
        })}
      </Accordion>
    </Card>
  );
}

export default ItineraryCard;
