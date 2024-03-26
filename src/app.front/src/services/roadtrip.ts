import { getTokenFromStorage } from "./storage"

const url = "http://localhost:5000/api/Roadtrip"

interface ICoordinate {
    item1: number,
    item2: number
}

interface IRoadTrip {
    title: string
    budget: number
    startDate: string 
    endDate: string
    duration: number
    nbTransfers: number
    tags: string[]
    co2Emission: string
}

export const createRoadTrip = async (data: IRoadTrip) => {
    const token = getTokenFromStorage();

    if (!token) {
        throw new Error("No token found");
    }
    
    const response = await fetch(`${url}/Create`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`,
        },
        body: JSON.stringify(data),
    });
    return await response.json()
}
