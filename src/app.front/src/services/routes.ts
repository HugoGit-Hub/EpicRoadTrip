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
    const response = await fetch(`${url}/GetRouteBetweenPoints`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwiZW1haWwiOiJtYXR0aGlhcy5nb3VwaWxAZ21haWwuY29tIiwibmJmIjoxNzExMzc0ODI4LCJleHAiOjE3MTEzODU2MjgsImlhdCI6MTcxMTM3NDgyOCwiaXNzIjoiRXBpY1JvYWRUcmlwIiwiYXVkIjoiRXBpY1JvYWRUcmlwIn0.OOO5vPmSmiqHlvQz1gPQCDQWQokV_GYGGSWAldXBtXI"
        },
        body: JSON.stringify(data),
    });
    return await response.json()
}