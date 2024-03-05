import { Map, Marker } from "pigeon-maps";
import { useEffect, useMemo, useState } from "react";
import InputTextSearchBar from "../components/InputTextSearchBar";
import SliderSearchBar from "../components/SliderSearchBar";
import SubmitButtonSearchBar from "../components/SubmitButtonSearchBar";

function Home() {
    const [data, setData] = useState({
        depart: {
            value: "",
            active: false,
        },
        destination: {
            value: "",
            active: false,
        },
        budget: {
            value: 0,
            active: false
        }
    });

    const active = useMemo(
        () => data.depart.active || data.destination.active || data.budget.active,
        [data.depart.active, data.destination.active, data.budget.active]
    );

    const [coord, setCoord] = useState([50.879, 4.6997]);

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
                <div className="flex flex-col gap-[12px] items-center w-fit absolute z-50">
                    <div
                        className={`flex flex-row items-center rounded-full shadow-md border flex flex-row overflow-hidden mt-10 w-fit ${active ? "bg-zinc-100" : "bg-white"
                            }`}
                    >
                        <InputTextSearchBar
                            label="Départ"
                            value={data.depart.value}
                            placeholder="Rechercher un départ"
                            active={data.depart.active}
                            onClick={() => {
                                setData({
                                    depart: {
                                        ...data.depart,
                                        active: true,
                                    },
                                    destination: {
                                        ...data.destination,
                                        active: false,
                                    },
                                    budget: {
                                        ...data.budget,
                                        active: false
                                    }
                                });
                            }}
                            onChange={(value) => {
                                setData({
                                    ...data,
                                    depart: {
                                        ...data.depart,
                                        value,
                                    },
                                });
                            }}
                        />
                        <InputTextSearchBar
                            label="Destination"
                            value={data.destination.value}
                            placeholder="Rechercher une destination"
                            active={data.destination.active}
                            onClick={() => {
                                setData({
                                    depart: {
                                        ...data.depart,
                                        active: false,
                                    },
                                    destination: {
                                        ...data.destination,
                                        active: true,
                                    },
                                    budget: {
                                        ...data.budget,
                                        active: false
                                    }
                                });
                            }}
                            onChange={(value) => {
                                setData({
                                    ...data,
                                    destination: {
                                        ...data.destination,
                                        value,
                                    },
                                });
                            }}
                        />
                        <SliderSearchBar
                            label="Budget"
                            placeholder="Votre budget"
                            value={data.budget.value}
                            active={data.budget.active}
                            onChange={(value) => {
                                setData({
                                    ...data,
                                    budget: {
                                        ...data.budget,
                                        value
                                    }
                                });
                            }}
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
                                        active: true
                                    }
                                });
                            }}
                        />
                        <SubmitButtonSearchBar
                            text="Rechercher"
                            onSubmit={() => {
                                console.log({
                                    depart: data.depart.value,
                                    destination: data.destination.value,
                                });
                            }}
                            active={active}
                        />
                    </div>
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
