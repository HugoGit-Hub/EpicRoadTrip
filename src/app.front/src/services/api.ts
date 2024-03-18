
export interface UserLogin {
    "email": string,
    "password": string
}

export interface tokenResponse{
    "Token": string
}

export interface registerDataType
{
    "email": string,
    "password": string,
    "firstName": string,
    "lastName": string,
    "age": number,
    "gender": boolean
}

const BASE_URL = "http://localhost:5000/";

export const login = async (email : string, password : string) : Promise<tokenResponse> => {

    const url = BASE_URL + "api/Authentication/Login";
        const data = { email, password };
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });
    if(response.ok){
        return response.json() as Promise<tokenResponse>;
    }
    throw new Error(response.status + " " + response.statusText);
}

export const register = async (data : registerDataType) : Promise<tokenResponse> => {

    const url = BASE_URL + "api/Authentication/Register";
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });
    if(response.ok){
        return response.json() as Promise<tokenResponse>;
    }
    throw new Error(response.status + " " + response.statusText);
}