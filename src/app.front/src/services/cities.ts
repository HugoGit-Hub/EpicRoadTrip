export interface City {
    name: string
    lat: string
    lng: string
    countryName: string
    countryCode: string
    adminName1: string
}

export interface ReponseCitiesSearch {
    geonames: City[]
}

export const getCitiesByWordSearch = async (search: string): Promise<ReponseCitiesSearch> => {
    const data = await (await fetch(`http://api.geonames.org/searchJSON?q=${search}&maxRows=5&username=matthiasgoup`)).json()
    return data
}
