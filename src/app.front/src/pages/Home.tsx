import { Map, Marker } from "pigeon-maps";
import { useState } from "react";
import SearchBar, { IFilters } from "../components/SearchBar/SearchBar";
import Card from "../components/Card";
import RoadTripCard from "../components/RoadTripCard";

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
              onSubmit={() => {
                setData({});
              }}
              active={activeSearchBar}
            />
          </div>
        ) : (
          <div
            className={`flex flex-col gap-[15px] h-full absolute z-50 p-[15px] backdrop-blur-md goverflow-y-auto`}
          >
            <RoadTripCard
              filters={filters}
              onEdit={() => {
                setData(undefined);
              }}
              onSave={() => {}}
            />

            <Card title="Villes"></Card>
            <Card title="Restaurants" />
            <Card title="Loisirs" />
            <Card title="Lieu à visiter" />
          </div>
        )}
        <Map
          defaultCenter={coord}
          defaultZoom={16}
          onClick={() => {
            setActiveSearchBar(false);
          }}
        >
          <Marker width={50} anchor={coord} />
        </Map>
      </div>
    </>
  );
}

export default Home;
