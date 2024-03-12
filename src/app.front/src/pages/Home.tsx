import { Map, Marker } from "pigeon-maps";
import { useEffect, useState } from "react";
import SearchBar from "../components/SearchBar/SearchBar";
import Card from "../components/Card";

function Home() {
  const [coord, setCoord] = useState([50.879, 4.6997]);
  const [filters, setFilters] = useState<any>(null);

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
      <div
        className="w-full h-screen relative flex flex-col items-center"
      >
        <div className="flex flex-col items-center absolute z-50 p-[32px]">
          <SearchBar
            onSubmit={(data) => {
              setFilters(data);
            }}
          />
          {/* {right && (
            bg-gray-500 bg-opacity-50 backdrop-blur-10
            <div
              className={`flex flex-col gap-[15px] w-full mt-[32px] overflow-y-auto`}
              style={{
                height: `calc(100vh - 108px - 32px - 32px)`,
              }}
            >
              <Card title="Villes" />

              <Card title="Restaurants" />
              <Card title="Loisirs" />
              <Card title="Lieu à visiter" />
            </div>
          )} */}
        </div>

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
