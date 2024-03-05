import { useState } from "react";

export default function Register() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [confirm_password, setConfirmPassword] = useState("");
    const [surname, setSurname] = useState("");
    const [name, setName] = useState("");
    const [gender, setGender] = useState("");
    const [age, setAge] = useState("");

    const isValidEmail = (email: string) => {
        // Expression régulière pour valider le format de l'email
        const emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        return emailPattern.test(email);
    };

    const handleRegister = () => {
        console.log("Email:", email);
        console.log("Mot de passe:", password);
        console.log("Mot de passe:", confirm_password);
        console.log("Nom:", name);
        console.log("Prénom:", surname);
        console.log("Sexe:", gender);
        console.log("Âge:", age);
    };

    const passwordMatch = () => {
        return confirm_password === password;
    }

    const isButtonDisabled = !email || !password || !isValidEmail(email) || !passwordMatch() || !surname || !name || !gender || !age;

    return (
        <div className="min-h-screen flex items-center justify-center bg-gradient-to-r from-pink-500 to-purple-500">
            <div className="bg-white p-8 rounded-lg shadow-lg w-1/2 my-8">
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
                        {!passwordMatch() && confirm_password !== "" &&(
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
                            placeholder="Prénom"
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
                            Sexe
                        </label>
                        <div>
                            <label className="inline-flex items-center mr-4">
                                <input
                                    type="radio"
                                    className="form-radio text-pink-500"
                                    name="gender"
                                    value="homme"
                                    onChange={(e) => setGender(e.target.value)}
                                />
                                <span className="ml-2 text-gray-700">Homme</span>
                            </label>
                            <label className="inline-flex items-center mr-4">
                                <input
                                    type="radio"
                                    className="form-radio text-pink-500"
                                    name="gender"
                                    value="femme"
                                    onChange={(e) => setGender(e.target.value)}
                                />
                                <span className="ml-2 text-gray-700">Femme</span>
                            </label>
                            <label className="inline-flex items-center mr-4">
                                <input
                                    type="radio"
                                    className="form-radio text-pink-500"
                                    name="gender"
                                    value="autre"
                                    onChange={(e) => setGender(e.target.value)}
                                />
                                <span className="ml-2 text-gray-700">Autre</span>
                            </label>
                        </div>
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
                    <div className="flex items-center justify-between">
                        <button
                            className={`py-2 px-4 rounded focus:outline-none focus:shadow-outline mr-2 ${isButtonDisabled? 'bg-gray-400 cursor-not-allowed' : 'bg-pink-500 hover:bg-pink-700 text-white'}`}
                            type="button"
                            onClick={handleRegister}
                            disabled={isButtonDisabled}
                        >
                            Enregistrer
                        </button>
                    </div>
                </form>
            </div>
            <div className="absolute bottom-0 right-0 text-white mr-8 mb-8 font-bold md:block hidden">
                © Epic Road Trip
            </div>
        </div>
    );
}
