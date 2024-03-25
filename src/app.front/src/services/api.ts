import bcrypt from "bcryptjs";
import { getTokenFromStorage } from "./storage";

export interface UserLogin {
    email: string;
    password: string;
}

export interface authResponse {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    age: number;
    gender: boolean | null;
    token: string;
}

export interface registerDataType {
    email: string;
    password: string;
    firstName: string;
    lastName: string;
    age: number;
    gender: boolean | null;
}

export interface UserInformations {
    id: number;
    email: string;
    password: string;
    firstName: string;
    lastName: string;
    age: number;
    gender: boolean | null;
}

export interface roadTripData {
    title : string;
    id: number;
    budget: number;
    startDate: string;
    endDate: string;
}

const BASE_URL = "http://localhost:5000/";
const FIXED_SALT = "$2a$10$CwTycUXWue0Thq9StjUM0u";

export const login = async (
    email: string,
    password: string
): Promise<authResponse> => {
    try {
        const hashedPassword = await hashPassword(password);
        password = hashedPassword;
    } catch (error) {
        console.error("Error:", error);
    }

    const url = BASE_URL + "api/Authentication/Login";
    const data = { email, password };
    const response = await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
    });
    if (response.ok) {
        return response.json() as Promise<authResponse>;
    }
    throw new Error(response.status + " " + response.statusText);
};

export const register = async (
    data: registerDataType
): Promise<authResponse> => {
    try {
        const hashedPassword = await hashPassword(data.password);
        data.password = hashedPassword;
    } catch (error) {
        console.error("Error:", error);
    }

    const url = BASE_URL + "api/Authentication/Register";
    const response = await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
    });
    if (response.ok) {
        return response.json() as Promise<authResponse>;
    }
    throw new Error(response.status + " " + response.statusText);
};

const hashPassword = async (password: string) => {
    try {
        const hashedPassword = await bcrypt.hash(password, FIXED_SALT);
        return hashedPassword;
    } catch (error) {
        console.error("Error hashing password:", error);
        throw error;
    }
};

export const updateUserInformations = async (
    data: UserInformations
): Promise<UserInformations> => {
    try {
        const hashedPassword = await hashPassword(data.password);
        data.password = hashedPassword;
    } catch (error) {
        console.error("Error:", error);
    }

    const url = BASE_URL + "api/User/Update";
    const token = getTokenFromStorage();

    if (!token) {
        throw new Error("No token found");
    }

    const response = await fetch(url, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
        },
        body: JSON.stringify(data),
    });

    if (response.ok) {
        return response.json() as Promise<UserInformations>;
    }
    throw new Error(response.status + " " + response.statusText);
};

export const deleteAccount = async (id: number) => {
    const url = BASE_URL + `api/User/Delete?id=${id}`;

    const token = getTokenFromStorage();

    if (!token) {
        throw new Error("No token found");
    }

    await fetch(url, {
        method: "DELETE",
        headers: {
            Authorization: `Bearer ${token}`,
        },
    });
};

export const getAllRoadtrips = async (
): Promise<roadTripData[]> => {

    const url = BASE_URL + "api/Roadtrip/GetAll";
    const token = getTokenFromStorage();

    if (!token) {
        throw new Error("No token found");
    }

    const response = await fetch(url, {
        method: "GET",
        headers: {
            Authorization: `Bearer ${token}`,
        },
    });

    if (response.ok) {
        return response.json();
    }
    throw new Error(response.status + " " + response.statusText);
};

export const deleteRoadtrip = async (id: number) => {
    const url = BASE_URL + `api/Roadtrip/Delete?id=${id}`;

    const token = getTokenFromStorage();

    if (!token) {
        throw new Error("No token found");
    }

    await fetch(url, {
        method: "DELETE",
        headers: {
            Authorization: `Bearer ${token}`,
        },
    });
};