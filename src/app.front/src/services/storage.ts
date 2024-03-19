import { authResponse } from './api';

export const setStorageFromResponse = (data: authResponse): void => {
    localStorage.setItem('authData', JSON.stringify(data));
}

export const getInformationsFromStorage = (): authResponse | null => {
    const storedData = localStorage.getItem('authData');
    if (storedData) {
        return JSON.parse(storedData) as authResponse;
    }
    return null;
}

export const isLoggedIn = () => {
    const data = getInformationsFromStorage();
    return data ? data.token ? true : false : false;
}

export const deleteAuthDataFromStorage = () => {
    localStorage.removeItem('authData');
}