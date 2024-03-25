import { Map, GeoJson, Marker } from "pigeon-maps";
import { useEffect, useMemo, useState } from "react";
import SearchBar, { IFilters, transports } from "../components/SearchBar/SearchBar";
import Card from "../components/Card";
import RoadTripCard from "../components/ResumeRoadTripCard";
import { getRouteBetweenPoints } from "../services/routes";
import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "../components/ui/accordion";

import iconTest from "../icons/bus-gradient.svg";
import ItineraryCard from "../components/ItineraryCard";
import TourrismCard from "../components/TourrismCard";
const colors = ["red", "blue", "green", "black", "purple", "brown"];

function Home() {
  const [coord, setCoord] = useState<[number, number]>([43.6, 3.894]);
  const [filters, setFilters] = useState<IFilters>({
    depart: undefined,
    destination: undefined,
    periode: [undefined, undefined],
    budget: 0,
    transports: [],
  });
  const [data, setData] = useState<any>(undefined);
  const geoJson = useMemo(() => {
    if (!data) return undefined;
    return {
      type: "Trajets",
      features: Object.values(data)
        .map((trajet) => trajet.map((el) => el.geoJsonData))
        .flat(),
    };
  }, [data]);
  const [activeSearchBar, setActiveSearchBar] = useState(true);


  return (
    <>
      <div
        className={`w-full h-screen relative flex flex-col ${
          data ? "" : "items-center"
        } `}
      >
        {!data ? (
          <div
            className="flex flex-col items-center absolute z-50 mt-[32px]"
            onClick={() => setActiveSearchBar(true)}
          >
            <SearchBar
              filters={filters}
              onChange={(filters) => {
                setFilters(filters);
              }}
              onSubmit={async () => {
                if (filters.depart && filters.destination) {
                  const json = await getRouteBetweenPoints({
                    cityOneCoord: {
                      item1: Number(filters.depart?.lng),
                      item2: Number(filters.depart?.lat),
                    },
                    cityTwoCoord: {
                      item1: Number(filters.destination?.lng),
                      item2: Number(filters.destination?.lat),
                    },
                    transportationAllowedId: filters.transports[0] === -1 ? [0,1,4,5] : filters.transports,
                  });
                  const newObj = (json as any[]).reduce((acc, curr) => {
                    const { routeGroup } = curr;
                    if (!acc[routeGroup]) {
                      acc[routeGroup] = [];
                    }
                    acc[routeGroup].push({
                      ...curr,
                      geoJsonData: {
                        type: routeGroup,
                        geometry: JSON.parse(curr.geoJson),
                      },
                    });
                    return acc;
                  }, {});
                  setData(newObj);
                }
              }}
              active={activeSearchBar}
            />
          </div>
        ) : (
          <div
            className={`flex flex-col gap-[15px] h-full absolute z-50 p-[15px] backdrop-blur-md overflow-y-auto`}
          >
            <RoadTripCard
              filters={filters}
              onEdit={() => {
                setData(undefined);
              }}
              onSave={() => {}}
            />
            <ItineraryCard data={data}/>
            <TourrismCard city={filters.depart?.name || ""}/>
            <TourrismCard city={filters.destination?.name || ""}/>
          </div>
        )}
        <Map
          defaultCenter={coord}
          defaultZoom={16}
          onClick={() => {
            setActiveSearchBar(false);
          }}
        >
          {data && (
            <>
              <Marker
                width={50}
                anchor={[filters.depart?.lat, filters.depart?.long]}
                color={`hsl(${0 % 360}deg 39% 70%)`}
                // onClick={() => setHue(hue + 20)}
              />
              <Marker
                width={50}
                anchor={[filters.destination?.lat, filters.destination?.long]}
                color={`hsl(${0 % 360}deg 39% 70%)`}
                // onClick={() => setHue(hue + 20)}
              />
            </>
          )}
          <GeoJson
            // data={{
            //   type: "FeatureCollection",
            //   features: [
            //     {
            //       type: "Feature",
            //       geometry: { type: "Point", coordinates: [2.0, 48.5] },
            //       properties: { prop0: "value0" },
            //     },
            //   ],
            // }}
            data={geoJson}
            styleCallback={(trajet, hover) => {
              // console.log(Object.values(data))
              if (trajet.geometry.type === "LineString") {
                return {
                  strokeWidth: "2",
                  stroke: colors[Object.keys(data as any).indexOf(trajet.type)],
                };
              }
              return {
                fill: "#d4e6ec99",
                strokeWidth: "1",
                stroke: "white",
                r: "20",
              };
            }}
          />

          {/* <Marker width={50} anchor={coord} /> */}
        </Map>
      </div>
    </>
  );
}

export default Home;
