import { PropsWithChildren } from "react";
import { Button } from "./ui/button";

interface RoadTripCardProps extends PropsWithChildren {
    title: string;
    description: string;
}

const mapImages = [
    "../../public/card-map-carvin.png",
    "../../public/card-map-pompignan.png",
    "../../public/card-map-antibes.png",
    "../../public/card-map-thanh.png",
    "../../public/card-map-montpellier.png",
];

function getRandomInt(max : number) {
    return Math.floor(Math.random() * max);
  }

function RoadTripCard({ title, description }: RoadTripCardProps) {
    return (
        <div className="max-w-sm bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700 my-2 mr-1">
            <a href="#">
                <img className="rounded-t-lg" src={mapImages[getRandomInt(mapImages.length)]} alt=""/>
            </a>
            <div className="p-5">
                <a href="#">
                    <h5 className="mb-2 text-2xl font-bold tracking-tight text-gray-900 dark:text-white">{title}</h5>
                </a>
                <p className="mb-3 font-normal text-gray-700 dark:text-gray-400">{description}</p>
                <Button variant="pink" type="submit" onClick={() => console.log("suppr")}>Voir</Button>

            </div>
        </div>
    );
}

export default RoadTripCard;
