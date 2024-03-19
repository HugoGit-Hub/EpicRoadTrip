import { useState } from 'react';
import toast from 'react-hot-toast';
import { useNavigate } from 'react-router-dom';
import { authResponse, login } from '../services/api';
import { setStorageFromResponse } from '../services/storage';

export default function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const navigate = useNavigate();

    const [isLoading, setIsLoading] = useState(false);

    const isValidEmail = (email: string) => {
        // Expression régulière pour valider le format de l'email
        const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        return emailPattern.test(email);
    };

    const handleLogin = async () => {
        setIsLoading(true);

        try {
            await toast.promise(
                login(email, password),
                {
                    loading: 'Connexion...',
                    success: (response) => {
                        const castResponse = response as authResponse;
                        setStorageFromResponse(castResponse);
                        navigate('/');
                        return <b>Connexion réussie !</b>;
                    },
                    error: (error) => {
                        console.error("Erreur lors de la connexion :", error.message);
                        return <b>{"Erreur lors de la connexion : " + error.message}</b>;
                    },
                }
            );
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="min-h-screen flex items-center justify-center bg-gradient-to-r from-pink-500 to-purple-500">
            <div className="bg-white p-8 rounded-lg shadow-lg w-full sm:w-3/4 md:w-2/4">
                <div className="flex items-center justify-between mb-4">
                    <h2 className="text-2xl font-bold">Epic Road Trip / Connexion</h2>
                    <a href="/">
                        <img src={"../../public/epic_road_trip.svg"} className="w-[50px] h-[50px] mr-2" alt="Logo" />
                    </a>
                </div>
                <div className="w-3/4 h-1 bg-gradient-to-r from-pink-500 to-purple-500 mb-2"></div>
                <form>
                    <div className="mb-4">
                        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="email">
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
                        <label className="block text-gray-700 text-sm font-bold mb-2" htmlFor="password">
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
                    <div className="flex items-center justify-between">
                        <div className="flex">
                            <button
                                className={`py-2 px-4 rounded focus:outline-none focus:shadow-outline mr-2 flex items-center justify-center ${!email || !password || !isValidEmail(email) ? 'bg-gray-400 cursor-not-allowed' : 'bg-pink-500 hover:bg-pink-700 text-white'}`}
                                type="button"
                                onClick={handleLogin}
                                disabled={!email || !password || !isValidEmail(email)}
                            >
                                <span>Connexion</span>
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
                        <div className="flex items-center justify-between mb-4">
                            <div className="block text-gray-700 text-sm font-bold mr-2 md:block hidden">Pas inscrit ?</div>
                            <a className="inline-block align-baseline font-bold text-sm text-pink-500 hover:text-pink-800" href="/register">
                                Créer un compte
                            </a>
                        </div>
                    </div>
                </form>
            </div>
            <div className="absolute bottom-0 right-0 text-white mr-8 mb-8 font-bold md:block hidden">
                © Epic Road Trip
            </div>
        </div>
    );
}
