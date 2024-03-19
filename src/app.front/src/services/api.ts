import bcrypt from "bcryptjs";

export interface UserLogin {
    email: string;
    password: string;
}

export interface tokenResponse {
    Token: string;
}

export interface registerDataType {
    email: string;
    password: string;
    firstName: string;
    lastName: string;
    age: number;
    gender: boolean | null;
}

const BASE_URL = "http://localhost:5000/";
const FIXED_SALT = "$2a$10$CwTycUXWue0Thq9StjUM0u";

export const login = async (
    email: string,
    password: string
): Promise<tokenResponse> => {
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
        return response.json() as Promise<tokenResponse>;
    }
    throw new Error(response.status + " " + response.statusText);
};

export const register = async (
    data: registerDataType
): Promise<tokenResponse> => {
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
        return response.json() as Promise<tokenResponse>;
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
