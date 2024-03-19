import { useEffect, useState } from "react";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";
import { authResponse, register, registerDataType } from "../services/api";
import { isLoggedIn, setStorageFromResponse } from "../services/storage";

export default function Register() {
    useEffect(()=>{
        if(isLoggedIn()){
            navigate("/profile")
        }
    },[])
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirm_password, setConfirmPassword] = useState("");
    const [surname, setSurname] = useState("");
    const [name, setName] = useState("");
    const [gender, setGender] = useState<boolean | null>(null);
    const [age, setAge] = useState("");
    const [isLoading, setIsLoading] = useState(false);

    const navigate = useNavigate();

    const isValidEmail = (email: string) => {
        // Expression régulière pour valider le format de l'email
        const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        return emailPattern.test(email);
    };

    const handleGenderChange = (value: any) => {
        if (value == 'homme') {
            setGender(true);
        } else if (value == 'femme') {
            setGender(false);

        } else {
            setGender(null);
        }
    }

    const handleRegister = async () => {
        const data: registerDataType = {
            "email": email,
            "password": password,
            "firstName": name,
            "lastName": surname,
            "age": Number(age),
            "gender": gender
        }
        setIsLoading(true);

        try {
            await toast.promise(
                register(data),
                {
                    loading: 'Création du compte...',
                    success: (response) => {
                        const castResponse = response as authResponse;
                        setStorageFromResponse(castResponse);
                        navigate('/');
                        return <b>Création réussie !</b>;
                    },
                    error: (error) => {
                        console.error("Erreur lors de la création du compte :", error.message);
                        return <b>{"Erreur lors de la création du compte : " + error.message}</b>;
                    },
                }
            );
        } finally {
            setIsLoading(false);
        }
    };

    const passwordMatch = () => {
        return confirm_password === password;
    }

    const isButtonDisabled = !email || !password || !isValidEmail(email) || !passwordMatch() || !surname || !name || !age;

    return (
        <div className="relative">
            <div className="min-h-screen flex items-center justify-center bg-gradient-to-r from-pink-500 to-purple-500">
                <div className="bg-white p-8 rounded-lg shadow-lg w-full sm:w-3/4 md:w-2/4 my-8">
                    <div className="flex items-center justify-between mb-4">
                        <h2 className="text-2xl font-bold">Epic Road Trip / Création de compte</h2>
                        <a href="/">
                            <img src={"../../public/epic_road_trip.svg"} className="w-[50px] h-[50px] mr-2" alt="Logo" />
                        </a>
                    </div>
                    <div className="w-3/4 h-1 bg-gradient-to-r from-pink-500 to-purple-500 mb-2"></div>
                    <h4 className="font-bold mb-2">Informations de connexion</h4>
                    <form>
                        <div className="mb-4">
                            <label className="block text-gray-500 text-sm font-bold mb-2" htmlFor="email">
                                Email
                            </label>
                            <input
                                className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                id="email"
                                type="email"
                                placeholder="Email"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                            />
                            {!isValidEmail(email) && (
                                <p className="text-red-500 text-xs italic mt-1">Veuillez saisir un email valide.</p>
                            )}
                        </div>
                        <div className="mb-6">
                            <label className="block text-gray-500 text-sm font-bold mb-2" htmlFor="password">
                                Mot de passe
                            </label>
                            <input
                                className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                id="password"
                                type="password"
                                placeholder="********"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                            />
                        </div>
                        <div className="mb-6">
                            <label className="block text-gray-500 text-sm font-bold mb-2" htmlFor="confirm_password">
                                Confirmer le mot de passe
                            </label>
                            <input
                                className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                id="confirm_password"
                                type="password"
                                placeholder="********"
                                value={confirm_password}
                                onChange={(e) => setConfirmPassword(e.target.value)}
                            />
                            {!passwordMatch() && confirm_password !== "" && (
                                <p className="text-red-500 text-xs italic mt-1">Les mots de passes ne correspondent pas.</p>
                            )}
                        </div>
                        <div className="w-3/4 h-1 bg-gradient-to-r from-pink-500 to-purple-500 mb-2"></div>
                        <h4 className="font-bold mb-2">Informations personnelles</h4>
                        <div className="mb-6">
                            <label className="block text-gray-500 text-sm font-bold mb-2" htmlFor="name">
                                Nom
                            </label>
                            <input
                                className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                id="name"
                                type="text"
                                placeholder="Nom"
                                value={name}
                                onChange={(e) => setName(e.target.value)}
                            />
                        </div>
                        <div className="mb-6">
                            <label className="block text-gray-500 text-sm font-bold mb-2" htmlFor="surname">
                                Prénom
                            </label>
                            <input
                                className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                id="surname"
                                type="text"
                                placeholder="Prénom"
                                value={surname}
                                onChange={(e) => setSurname(e.target.value)}
                            />
                        </div>
                        <div className="mb-6">
                            <label className="block text-gray-500 text-sm font-bold mb-2">
                                Âge
                            </label>
                            <input
                                className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                                id="age"
                                type="text"
                                placeholder="Âge"
                                value={age}
                                onChange={(e) => setAge(e.target.value.replace(/\D/, ''))}
                            />
                        </div>
                        <div className="mb-6">
                            <div>
                                <label className="block text-gray-500 text-sm font-bold mb-2">
                                    Genre
                                </label>
                                <div>
                                    <div className="inline-flex items-center mr-4">
                                        <label className="relative flex items-center p-3 rounded-full cursor-pointer" htmlFor="homme">
                                            <input
                                                name="gender"
                                                type="radio"
                                                className="before:content[''] peer relative h-5 w-5 cursor-pointer appearance-none rounded-full border border-blue-gray-200 text-gray-900 transition-all before:absolute before:top-2/4 before:left-2/4 before:block before:h-12 before:w-12 before:-translate-y-2/4 before:-translate-x-2/4 before:rounded-full before:bg-gradient-to-r before:from-pink-500 before:to-purple-500 before:opacity-0 before:transition-opacity checked:border-gray-900 checked:before:bg-gray-900 hover:before:opacity-10"
                                                id="homme"
                                                value="homme"
                                                checked={gender == true}
                                                onChange={(e) => handleGenderChange(e.target.value)}
                                            />
                                            <span className="absolute text-gray-900 transition-opacity opacity-0 pointer-events-none top-2/4 left-2/4 -translate-y-2/4 -translate-x-2/4 peer-checked:opacity-100">
                                                <svg xmlns="http://www.w3.org/2000/svg" className="h-3.5 w-3.5" viewBox="0 0 16 16" fill="#a955f5">
                                                    <circle data-name="ellipse" cx="8" cy="8" r="8"></circle>
                                                </svg>
                                            </span>
                                        </label>
                                        <label className="mt-px font-light text-gray-700 cursor-pointer select-none" htmlFor="homme">
                                            Homme
                                        </label>
                                    </div>
                                    <div className="inline-flex items-center mr-4">
                                        <label className="relative flex items-center p-3 rounded-full cursor-pointer" htmlFor="femme">
                                            <input
                                                name="gender"
                                                type="radio"
                                                className="before:content[''] peer relative h-5 w-5 cursor-pointer appearance-none rounded-full border border-blue-gray-200 text-gray-900 transition-all before:absolute before:top-2/4 before:left-2/4 before:block before:h-12 before:w-12 before:-translate-y-2/4 before:-translate-x-2/4 before:rounded-full before:bg-gradient-to-r before:from-pink-500 before:to-purple-500 before:opacity-0 before:transition-opacity checked:border-gray-900 checked:before:bg-gray-900 hover:before:opacity-10"
                                                id="femme"
                                                value="femme"
                                                checked={gender == false}
                                                onChange={(e) => handleGenderChange(e.target.value)}
                                            />
                                            <span className="absolute text-gray-900 transition-opacity opacity-0 pointer-events-none top-2/4 left-2/4 -translate-y-2/4 -translate-x-2/4 peer-checked:opacity-100">
                                                <svg xmlns="http://www.w3.org/2000/svg" className="h-3.5 w-3.5" viewBox="0 0 16 16" fill="#a955f5">
                                                    <circle data-name="ellipse" cx="8" cy="8" r="8"></circle>
                                                </svg>
                                            </span>
                                        </label>
                                        <label className="mt-px font-light text-gray-700 cursor-pointer select-none" htmlFor="femme">
                                            Femme
                                        </label>
                                    </div>
                                    <div className="inline-flex items-center">
                                        <label className="relative flex items-center p-3 rounded-full cursor-pointer" htmlFor="autre">
                                            <input
                                                name="gender"
                                                type="radio"
                                                className="before:content[''] peer relative h-5 w-5 cursor-pointer appearance-none rounded-full border border-blue-gray-200 text-gray-900 transition-all before:absolute before:top-2/4 before:left-2/4 before:block before:h-12 before:w-12 before:-translate-y-2/4 before:-translate-x-2/4 before:rounded-full before:bg-gradient-to-r before:from-pink-500 before:to-purple-500 before:opacity-0 before:transition-opacity checked:border-gray-900 checked:before:bg-gray-900 hover:before:opacity-10"
                                                id="autre"
                                                value="autre"
                                                checked={gender == null}
                                                onChange={(e) => handleGenderChange(e.target.value)}
                                            />
                                            <span className="absolute text-gray-900 transition-opacity opacity-0 pointer-events-none top-2/4 left-2/4 -translate-y-2/4 -translate-x-2/4 peer-checked:opacity-100">
                                                <svg xmlns="http://www.w3.org/2000/svg" className="h-3.5 w-3.5" viewBox="0 0 16 16" fill="#a955f5">
                                                    <circle data-name="ellipse" cx="8" cy="8" r="8"></circle>
                                                </svg>
                                            </span>
                                        </label>
                                        <label className="mt-px font-light text-gray-700 cursor-pointer select-none" htmlFor="autre">
                                            Autre
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="flex items-center">
                            <button
                                className={`py-2 px-4 rounded focus:outline-none focus:shadow-outline mr-2 ${isButtonDisabled ? 'bg-gray-400 cursor-not-allowed' : 'bg-pink-500 hover:bg-pink-700 text-white'}`}
                                type="button"
                                onClick={handleRegister}
                                disabled={isButtonDisabled}
                            >
                                Enregistrer
                            </button>
                            {isLoading && (
                                <svg
                                    version="1.1"
                                    id="L9"
                                    xmlns="http://www.w3.org/2000/svg"
                                    xmlnsXlink="http://www.w3.org/1999/xlink"
                                    x="0px"
                                    y="0px"
                                    width="30px"
                                    height="30px"
                                    viewBox="0 0 100 100"
                                    enableBackground="new 0 0 0 0"
                                    xmlSpace="preserve"
                                    style={{ marginLeft: '5px', marginTop: '5px' }}
                                >
                                    <path
                                        fill="#000"
                                        d="M73,50c0-12.7-10.3-23-23-23S27,37.3,27,50 M30.9,50c0-10.5,8.5-19.1,19.1-19.1S69.1,39.5,69.1,50"
                                    >
                                        <animateTransform
                                            attributeName="transform"
                                            attributeType="XML"
                                            type="rotate"
                                            dur="1s"
                                            from="0 50 50"
                                            to="360 50 50"
                                            repeatCount="indefinite"
                                        />
                                    </path>
                                </svg>
                            )}
                        </div>
                    </form>
                </div>
            </div>
            <div className="absolute bottom-0 right-0 text-white mr-8 mb-8 font-bold md:block hidden">
                © Epic Road Trip
            </div>
        </div>
    );
}
