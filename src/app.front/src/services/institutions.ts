import { IInstitution } from "../components/InstutitionCard"
import { getTokenFromStorage } from "./storage"

const url = "http://localhost:5000/api/Institution"

interface IParams{
    lat: number
    lng: number,
    checkin: string,
    checkout: string,
    institutionTypes: number[]
}


export const getInstitutionAround = async (data: IParams) => {
    const token = getTokenFromStorage()
    
    const response = await fetch(`${url}/GetInstitutionAround`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },
        body: JSON.stringify({...data, radius: 1000}),
    })
    return await response.json()
}

interface IInstitutionParams extends Omit<IInstitution, "geoJson" | "geoJsonData">{roadTripId: number} 

export const createInsitution = async (data: IInstitutionParams) => {
    const token = getTokenFromStorage()
    
    const response = await fetch(`${url}/Create`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },
        body: JSON.stringify({...data, radius: 1000}),
    })
    return await response.json()
}