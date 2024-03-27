import { PropsWithChildren, useEffect, useState } from "react";
import toast from "react-hot-toast";
import trashIcon from "../icons/delete.svg";
import loadingIcon from "../icons/loading.svg";
import { deleteRoadtrip } from "../services/api";
import { Button } from "./ui/button";

interface RoadTripCardProps extends PropsWithChildren {
    title: string;
    budget: number;
    dates: string;
    id: number;
    isViewOpen: any;
}

const mapImages = [
    "../../public/card-map-carvin.png",
    "../../public/card-map-pompignan.png",
    "../../public/card-map-antibes.png",
    "../../public/card-map-thanh.png",
    "../../public/card-map-montpellier.png",
];

function getRandomInt(max: number) {
    return Math.floor(Math.random() * max);
}

function RoadTripCard({ title, budget, dates, id, isViewOpen }: RoadTripCardProps) {
    const [image, setImage] = useState('')
    const [isLoading, setIsLoading] = useState(false);

    useEffect(() => {
        setImage(mapImages[getRandomInt(mapImages.length)])
    }, [])
    function handleView(): void {
        isViewOpen(id)
    }

    async function handleDelete(){
        setIsLoading(true);
        try {
            await toast.promise(
                deleteRoadtrip(id),
                {
                    loading: 'Suppression...',
                    success: () => {
                        return <b>Suppression réussie !</b>;
                    },
                    error: (error) => {
                        console.error("Erreur lors de la suppression :", error.message);
                        return <b>{"Erreur lors de la suppression : " + error.message}</b>;
                    },
                }
            );
        } catch (error: any) {
            console.error("Erreur lors de la suppression du roadtrip :", error.message);
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <div className="max-w-sm bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700 my-2 mr-1">
            <a href="#">
                <img className="rounded-t-lg" src={image} alt="" />
            </a>
            <div className="p-5">
                <a href="#">
                    <h5 className="mb-2 text-2xl font-bold tracking-tight text-gray-900 dark:text-white">{title}</h5>
                </a>
                <p className="mb-3 font-normal text-gray-700 dark:text-gray-400">Budget : {budget}€</p>
                <p className="mb-3 font-normal text-gray-700 dark:text-gray-400">Dates : {dates}</p>
                <div className="flex">
                    <Button variant="supprRoadTrip" size="icon" type="submit" onClick={() => handleDelete()}>
                        <img src={isLoading ? loadingIcon : trashIcon}/>
                    </Button>
                    <Button variant="pink" type="submit" onClick={() => handleView()}>Voir</Button>
                </div>
            </div>
        </div>
    );
}

export default RoadTripCard;
