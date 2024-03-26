import { GeoJson, Map, Marker, Overlay } from "pigeon-maps";
import { useMemo, useState } from "react";
import RoadTripCard from "../components/ResumeRoadTripCard";
import SearchBar, { IFilters } from "../components/SearchBar/SearchBar";
import { getRouteBetweenPoints } from "../services/routes";

import { MultiPolygon } from "geojson";
import toast from "react-hot-toast";
import { useNavigate } from "react-router";
import LoaderIcon from "../../public/loader.svg";
import Card from "../components/Card";
import ItineraryCard from "../components/ItineraryCard";
import TourrismCard from "../components/TourrismCard";
import UserProfileDropdown from "../components/UserProfileDropdown";
import { Button } from "../components/ui/button";
import ProfileIcon from "../icons/profile.svg";
import { getInstitutionAround } from "../services/institutions";
import { getIsochrone } from "../services/isochrone";
import { isLoggedIn } from "../services/storage";
const colors = ["red", "blue", "green", "black", "purple", "brown", "pink", "orange", "grey"];

interface cityCoord {
  lng: number;
  lat: number;
}

import InstutitionCard, { IInstitution } from "../components/InstutitionCard";
import { formatDate } from "../lib/utils";

interface IInstitutions {
  0?: IInstitution[];
  1?: IInstitution[];
  2?: IInstitution[];
  3?: IInstitution[];
}

function Home() {
  const navigate = useNavigate();
  const [citiesGeoJson, setCitiesGeoJson] = useState<MultiPolygon[] | null>(null);
  let citiesCoords : cityCoord[] = [];
  const [coord, setCoord] = useState<[number, number]>([43.6, 3.894]);
  const [isLoading, setIsLoading] = useState(false);
  const [filters, setFilters] = useState<IFilters>({
    depart: undefined,
    destination: undefined,
    periode: [undefined, undefined],
    budget: 0,
    transports: [],
    loisirs: [],
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
  let promises: Promise<any>[] = [];

  const [loisirDepart, setLoisirDepart] = useState<IInstitutions>({});
  const [loisirDestination, setLoisirDestination] = useState<IInstitutions>({});
  const [selectedLoisir, setSelectedLoisir] = useState<
    IInstitution | undefined
  >(undefined);
  
  return (
    <>
      <div
        className={`w-full h-screen relative flex flex-col ${data ? "" : "items-center"
          } ${isLoading ? 'filter blur-sm' : ''}`}
      >
        {!data ? (
          <div
            className={`flex items-center absolute z-50 mt-[32px] w-full justify-center mx-auto ${isLoading ? 'h-full' : ''}`}
            onClick={() => setActiveSearchBar(true)}>
            {!isLoading ? (
              <>
                <div className="absolute left-0 top-0 mt-1.5 ml-2 w-14 h-14 rounded-full bg-white border border-gray-300 flex items-center justify-center focus:outline-none p-2">
                  <img src="../../public/epic_road_trip.svg" alt="" />
                </div>
                <SearchBar
                  filters={filters}
                  onChange={(filters) => {
                    setFilters(filters);
                  }}
                  onSubmit={async () => {
                    if (filters.depart && filters.destination) {
                      if (isLoggedIn()) {
                        setIsLoading(true);
                        const json = await getRouteBetweenPoints({
                          cityOneCoord: {
                            item1: Number(filters.depart?.lng),
                            item2: Number(filters.depart?.lat),
                          },
                          cityTwoCoord: {
                            item1: Number(filters.destination?.lng),
                            item2: Number(filters.destination?.lat),
                          },
                          transportationAllowedId: filters.transports[0] === -1 ? [0, 1, 4, 5] : filters.transports,
                        });
                        setIsLoading(false);
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
                        citiesCoords = ([
                          {
                            "lng": Number(filters.depart?.lng),
                            "lat": Number(filters.depart?.lat)
                          },
                          {
                            "lng": Number(filters.destination?.lng),
                            "lat": Number(filters.destination?.lat)
                          },
                        ]);
                        console.log(citiesCoords);
                        citiesCoords?.forEach((c) => {
                          promises.push(getIsochrone(Number(c.lng), Number(c.lat)));
                        });
                        console.log(promises);
                        const res = await Promise.all(promises);
                        const formatRes = res as MultiPolygon[];
                        setCitiesGeoJson(formatRes);
                        if (filters.loisirs.length > 0) {
                          const jsonDepart: IInstitution[] =
                            await getInstitutionAround({
                              lat: Number(filters.depart?.lat),
                              lng: Number(filters.depart?.lng),
                              checkin:
                                "20" +
                                formatDate(filters.periode[0])
                                  .split("/")
                                  .reverse()
                                  .join("-"),
                              checkout:
                                "20" +
                                formatDate(filters.periode[1])
                                  .split("/")
                                  .reverse()
                                  .join("-"),
                              institutionTypes:
                                filters.loisirs[0] === -1
                                  ? [0, 1, 2, 3]
                                  : filters.loisirs,
                            });
      
                          setLoisirDepart(
                            jsonDepart.reduce(
                              (acc: IInstitutions, curr: IInstitution) => {
                                const { type } = curr;
                                console.log(curr);
                                if (!acc[type]) {
                                  acc[type] = [];
                                }
                                acc[type]?.push({
                                  ...curr,
                                  ...(curr.geoJson && {
                                    geoJsonData: {
                                      type: "Loisir",
                                      geometry: JSON.parse(curr.geoJson),
                                    },
                                  }),
                                });
                                return acc;
                              },
                              {}
                            )
                          );
                          const jsonDestination: IInstitution[] =
                            await getInstitutionAround({
                              lat: Number(filters.destination?.lat),
                              lng: Number(filters.destination?.lng),
                              checkin:
                                "20" +
                                formatDate(filters.periode[0])
                                  .split("/")
                                  .reverse()
                                  .join("-"),
                              checkout:
                                "20" +
                                formatDate(filters.periode[1])
                                  .split("/")
                                  .reverse()
                                  .join("-"),
                              institutionTypes:
                                filters.loisirs[0] === -1
                                  ? [0, 1, 2, 3]
                                  : filters.loisirs,
                            });
                          setLoisirDestination(
                            jsonDestination.reduce(
                              (acc: IInstitutions, curr: IInstitution) => {
                                const { type } = curr;
                                console.log(curr);
                                if (!acc[type]) {
                                  acc[type] = [];
                                }
                                acc[type]?.push({
                                  ...curr,
                                  ...(curr.geoJson && {
                                    geoJsonData: {
                                      type: "Loisir",
                                      geometry: JSON.parse(curr.geoJson),
                                    },
                                  }),
                                });
                                return acc;
                              },
                              {}
                            )
                          );
                        }
                        console.log('Tous les résultats :', formatRes);
                      }else{
                        toast.error("Vous devez être connecté afin de rechercher !")
                      }
                    }
                  }}
                  active={activeSearchBar}
                />
                <div className="absolute right-0 top-0 mt-1.5 z-10">
                  <UserProfileDropdown></UserProfileDropdown>
                </div>
              </>
            ) : (
              <div className="flex justify-center items-center w-full h-full">
                <img src={LoaderIcon} alt="" />
              </div>
            )}
            {/* <SearchBar
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
                    transportationAllowedId:
                      filters.transports[0] === -1
                        ? [0, 1, 4, 5]
                        : filters.transports,
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
                  if (filters.loisirs.length > 0) {
                    const jsonDepart: IInstitution[] =
                      await getInstitutionAround({
                        lat: Number(filters.depart?.lat),
                        lng: Number(filters.depart?.lng),
                        checkin:
                          "20" +
                          formatDate(filters.periode[0])
                            .split("/")
                            .reverse()
                            .join("-"),
                        checkout:
                          "20" +
                          formatDate(filters.periode[1])
                            .split("/")
                            .reverse()
                            .join("-"),
                        institutionTypes:
                          filters.loisirs[0] === -1
                            ? [0, 1, 2, 3]
                            : filters.loisirs,
                      });

                    setLoisirDepart(
                      jsonDepart.reduce(
                        (acc: IInstitutions, curr: IInstitution) => {
                          const { type } = curr;
                          console.log(curr);
                          if (!acc[type]) {
                            acc[type] = [];
                          }
                          acc[type]?.push({
                            ...curr,
                            ...(curr.geoJson && {
                              geoJsonData: {
                                type: "Loisir",
                                geometry: JSON.parse(curr.geoJson),
                              },
                            }),
                          });
                          return acc;
                        },
                        {}
                      )
                    );
                    const jsonDestination: IInstitution[] =
                      await getInstitutionAround({
                        lat: Number(filters.destination?.lat),
                        lng: Number(filters.destination?.lng),
                        checkin:
                          "20" +
                          formatDate(filters.periode[0])
                            .split("/")
                            .reverse()
                            .join("-"),
                        checkout:
                          "20" +
                          formatDate(filters.periode[1])
                            .split("/")
                            .reverse()
                            .join("-"),
                        institutionTypes:
                          filters.loisirs[0] === -1
                            ? [0, 1, 2, 3]
                            : filters.loisirs,
                      });
                    setLoisirDestination(
                      jsonDestination.reduce(
                        (acc: IInstitutions, curr: IInstitution) => {
                          const { type } = curr;
                          console.log(curr);
                          if (!acc[type]) {
                            acc[type] = [];
                          }
                          acc[type]?.push({
                            ...curr,
                            ...(curr.geoJson && {
                              geoJsonData: {
                                type: "Loisir",
                                geometry: JSON.parse(curr.geoJson),
                              },
                            }),
                          });
                          return acc;
                        },
                        {}
                      )
                    );
                  }
                }
              }}
              active={activeSearchBar}
            /> */}
          </div>

        ) : (
          <div
            className={`flex flex-col gap-[15px] h-full absolute z-50 p-[15px] backdrop-blur-md overflow-y-auto`}
          >
            <Card title="Epic Road Trip">
              <div className="mt-2 flex justify-between items-center">
                <div className="w-14 h-14 rounded-full bg-white border border-gray-300 flex items-center justify-start focus:outline-none p-2">
                  <img src="../../public/epic_road_trip.svg" alt="" />
                </div>
                <Button variant="gradient" onClick={() => navigate("/profile")}>
                  <img src={ProfileIcon} width={20} className="filter invert text-white" />
                  <span className="ml-[10px]">Mon profil</span>
                </Button>
              </div>
            </Card>
            <RoadTripCard
              filters={filters}
              onEdit={() => {
                setData(undefined);
              }}
              onSave={() => { }}
            />
            <ItineraryCard data={data} />

            <TourrismCard
              cityDepart={filters.depart?.name || "Ville départ"}
              cityDestination={filters.destination?.name || "Ville destination"}
              loisirDestination={loisirDestination}
              loisirDepart={loisirDepart}
            />
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
            data={geoJson}
            styleCallback={(trajet, hover) => {
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
          {citiesGeoJson?.map((geoJsonData, index) => (
            <GeoJson
              key={index} // Assurez-vous d'ajouter une clé unique pour chaque composant GeoJson
              data={geoJsonData}
              styleCallback={(feature: any, hover: any) => {
                if (feature.geometry.type === "LineString") {
                  return { strokeWidth: "1", stroke: "black" };
                }
                return {
                  fill: "#318CE744",
                  strokeWidth: "1",
                  stroke: "white",
                  r: "20",
                };
              }}
            />
          ))}

          {loisirDepart[0] &&
            loisirDepart[0].map((loisir: IInstitution) => {
              return (
                <Marker
                  width={50}
                  anchor={[loisir.lat, loisir.lng]}
                  color={"#FF0000"}
                  onClick={() => setSelectedLoisir(loisir)}
                />
              );
            })}

          {loisirDepart[1] &&
            loisirDepart[1].map((loisir: IInstitution) => {
              return (
                <Marker
                  width={50}
                  anchor={[loisir.lat, loisir.lng]}
                  color={"#FFFF00"}
                  onClick={() => setSelectedLoisir(loisir)}
                />
              );
            })}

          {loisirDepart[2] &&
            loisirDepart[2].map((loisir: IInstitution) => {
              return (
                <Marker
                  width={50}
                  anchor={[loisir.lat, loisir.lng]}
                  color={"#00FFFF"}
                  onClick={() => setSelectedLoisir(loisir)}
                />
              );
            })}

          {loisirDepart[3] &&
            loisirDepart[3].map((loisir: IInstitution) => {
              return (
                <Marker
                  width={50}
                  anchor={[loisir.lat, loisir.lng]}
                  color={"#FF00FF"}
                  onClick={() => setSelectedLoisir(loisir)}
                />
              );
            })}

          {loisirDestination[0] &&
            loisirDestination[0].map((loisir: IInstitution) => {
              return (
                <Marker
                  width={50}
                  anchor={[loisir.lat, loisir.lng]}
                  color={"#FF0000"}
                  onClick={() => setSelectedLoisir(loisir)}
                />
              );
            })}

          {loisirDestination[1] &&
            loisirDestination[1].map((loisir: IInstitution) => {
              return (
                <Marker
                  width={50}
                  anchor={[loisir.lat, loisir.lng]}
                  color={"#FFFF00"}
                  onClick={() => setSelectedLoisir(loisir)}
                />
              );
            })}

          {loisirDestination[2] &&
            loisirDestination[2].map((loisir: IInstitution) => {
              return (
                <Marker
                  width={50}
                  anchor={[loisir.lat, loisir.lng]}
                  color={"#00FFFF"}
                  onClick={() => setSelectedLoisir(loisir)}
                />
              );
            })}

          {loisirDestination[3] &&
            loisirDestination[3].map((loisir: IInstitution) => {
              return (
                <Marker
                  width={50}
                  anchor={[loisir.lat, loisir.lng]}
                  color={"#FF00FF"}
                  onClick={() => setSelectedLoisir(loisir)}
                />
              );
            })}
          {selectedLoisir && (
            <Overlay anchor={[selectedLoisir.lat, selectedLoisir.lng]}>
              <InstutitionCard
                {...selectedLoisir}
                onMap
                onClose={() => setSelectedLoisir(undefined)}
              />
            </Overlay>
          )}
        </Map>
      </div>
    </>
  );
}

export default Home;
