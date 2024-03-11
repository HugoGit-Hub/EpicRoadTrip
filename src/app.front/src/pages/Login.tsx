import { useState } from 'react';

export default function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const isValidEmail = (email: string) => {
        // Expression régulière pour valider le format de l'email
        const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        return emailPattern.test(email);
    };

    const handleLogin = () => {
        console.log("Email:", email);
        console.log("Mot de passe:", password);
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
                        <button
                            className={`py-2 px-4 rounded focus:outline-none focus:shadow-outline mr-2 ${!email || !password || !isValidEmail(email) ? 'bg-gray-400 cursor-not-allowed' : 'bg-pink-500 hover:bg-pink-700 text-white'}`}
                            type="button"
                            onClick={handleLogin}
                            disabled={!email || !password || !isValidEmail(email)}
                        >
                            Connexion
                        </button>
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
