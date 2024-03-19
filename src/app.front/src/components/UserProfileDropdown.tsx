import { useState } from 'react';
import { useNavigate } from 'react-router';
import { deleteAuthDataFromStorage, isLoggedIn } from '../services/storage';

const UserProfileDropdown = () => {
    const [isOpen, setIsOpen] = useState(false);
    const navigate = useNavigate();

    const toggleDropdown = () => {
        setIsOpen(!isOpen);
    };

    const handleLogout = () => {
        deleteAuthDataFromStorage()
        toggleDropdown();
    };

    return (
        <div className="relative">
            {/* Bouton rond avec l'icône utilisateur */}
            {(isLoggedIn() ?
                <button
                    className="w-10 h-10 rounded-full bg-white border border-gray-300 flex items-center justify-center focus:outline-none p-2"
                    onClick={toggleDropdown}
                >
                    {/* Icône utilisateur (Image) */}
                    <img src="../../public/free-user.png" alt="User profile picture" width={200} height={200} />
                </button>
                :
                <button className="bg-white rounded-full border border-gray-300 px-4 py-2 text-gray-800 font-semibold shadow hover:shadow-md focus:outline-none"
                onClick={() => navigate("/login")}>
                    Se connecter
                </button>

            )}

            {/* Dropdown */}
            {isOpen && (
                <div className="absolute right-0 mt-2 w-48 bg-white rounded-md shadow-lg z-10">
                    <div className="py-1">
                        {/* Option "Mon Profil" */}
                        <button
                            onClick={() => navigate("/profile")}
                            className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 w-full text-left"
                        >
                            Mon Profil
                        </button>
                        {/* Option "Se déconnecter" */}
                        <button
                            onClick={handleLogout}
                            className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 w-full text-left"
                        >
                            Se déconnecter
                        </button>
                    </div>
                </div>
            )}
        </div>
    );
};

export default UserProfileDropdown;
