import { Map, Marker } from "pigeon-maps";
import { useEffect, useState } from "react";
import SearchBar, { IFilters } from "../components/SearchBar/SearchBar";
import Card from "../components/Card";
import { differenceInDays, formatDate } from "../lib/utils";

function Home() {
  const [coord, setCoord] = useState([50.879, 4.6997]);
  const [filters, setFilters] = useState<IFilters>({
    depart: undefined,
    destination: undefined,
    periode: [undefined, undefined],
    budget: 0,
    transports: []
  });
  const [data, setData] = useState<any>(undefined);

  useEffect(() => {
    const options = {
      enableHighAccuracy: true,
      timeout: 5000,
      maximumAge: 0,
    };

    function success(pos) {
      const crd = pos.coords;
      setCoord([crd.latitude, crd.longitude]);
      console.log("Your current position is:");
      console.log(`Latitude : ${crd.latitude}`);
      console.log(`Longitude: ${crd.longitude}`);
      console.log(`More or less ${crd.accuracy} meters.`);
    }

    function error(err) {
      console.warn(`ERROR(${err.code}): ${err.message}`);
    }

    navigator.geolocation.getCurrentPosition(success, error, options);
  }, []);
  return (
    <>
      <div className="w-full h-screen relative flex flex-col items-center">
        {!data ? (
          <div className="flex flex-col items-center absolute z-50 p-[32px]">
            <SearchBar
              filters={filters}
              onChange={(filters) => {
                setFilters(filters);
              }}
              onSubmit={() => {
                setData({});
              }}
            />
          </div>
        ) : (
          <div
            className={`flex flex-col gap-[15px] absolute z-50 w-full mt-[32px] overflow-y-auto`}
            style={{
              height: `calc(100vh - 108px - 32px - 32px)`,
            }}
          >
            <Card title="Votre trajet">
              <div className="flex flex-row gap-[20px]">
                <div>
                  <p>Départ</p>
                  <p>{filters.depart?.name}</p>
                </div>
                <div>
                  <p>Destination</p>
                  <p>{filters.destination?.name}</p>
                </div>
              </div>
              <p>
                Du {formatDate(filters.periode[0])} au{" "}
                {formatDate(filters.periode[1])} ({differenceInDays(filters.periode[1],filters.periode[0])} jours)
              </p>
              {filters.budget && <p>Pour un budget de {filters.budget} €</p>}
              <div>
                <button
                  onClick={() => {
                    setData(undefined);
                  }}
                >
                  Modifier
                </button>
                <button>Enregistrer</button>
              </div>
            </Card>

            <Card title="Villes"></Card>

            <Card title="Restaurants" />
            <Card title="Loisirs" />
            <Card title="Lieu à visiter" />
          </div>
        )}
        {/* 
        {data && (
            bg-gray-500 bg-opacity-50 backdrop-blur-10
            
          )} */}

        <Map
          defaultCenter={coord}
          defaultZoom={11}
          onClick={() => {
            setData({
              depart: {
                ...data.depart,
                active: false,
              },
              destination: {
                ...data.destination,
                active: false,
              },
              budget: {
                ...data.budget,
                active: false,
              },
            });
          }}
        >
          <Marker width={50} anchor={coord} />
        </Map>
      </div>
    </>
  );
}

export default Home;
