import { getTokenFromStorage } from "./storage"

const url = "http://localhost:5000/api/Route"

interface ICoordinate {
    item1: number,
    item2: number
}
interface IRouteBetweenPointsParameters {
    cityOneCoord: ICoordinate
    cityTwoCoord: ICoordinate
    transportationAllowedId: number[]
  }
export const getRouteBetweenPoints = async (data: IRouteBetweenPointsParameters) => {
    const token = getTokenFromStorage();

    if (!token) {
        throw new Error("No token found");
    }
    
    const response = await fetch(`${url}/GetRouteBetweenPoints`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`,
        },
        body: JSON.stringify(data),
    });
    return await response.json()
}