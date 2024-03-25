const BASE_URL = "https://api.traveltimeapp.com/v4/time-map";

export interface isoChroneBody {
    arrival_searches: arrivalSearch[];
}

export interface arrivalSearch {
    id: string;
    coords: {
        lat: number;
        lng: number;
    };
    arrival_time: string;
    travel_time: number;
    transportation: {
        type: string;
    };
}

const XApplicationId = "0c15f32b";
const XApiKey = "8aaa43da725d1091021b5a94fa053c95";
const Accept = "application/geo+json";

export const getIsochrone = async (lng: number, lat: number) => {
    const body: isoChroneBody = {
        arrival_searches: [
            {
                id: "isochrone-0",
                coords: {
                    lat: lat,
                    lng: lng,
                },
                arrival_time: "2024-03-25T08:00:00.000Z",
                travel_time: 1800,
                transportation: {
                    type: "walking",
                },
            },
        ],
    };

    const response = await fetch(BASE_URL, {
        method: "POST",
        headers: {
            "X-Application-Id": XApplicationId,
            "X-Api-Key" : XApiKey,
            "Accept" : Accept,
            "Content-Type" : "application/json"
        },
        body: JSON.stringify(body),
    });
    if (response.ok) {
        return response.json();
    }
    throw new Error(response.status + " " + response.statusText);
};
