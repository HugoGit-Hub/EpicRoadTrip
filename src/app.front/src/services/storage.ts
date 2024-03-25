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

export const getTokenFromStorage = () : string | undefined =>{
    return getInformationsFromStorage()?.token;
}

export const getIdFromStorage = () : number | undefined =>{
    return getInformationsFromStorage()?.id;
}

export const isLoggedIn = () => {
    const data = getInformationsFromStorage();
    return data ? data.token ? true : false : false;
}

export const deleteAuthDataFromStorage = () => {
    localStorage.removeItem('authData');
}