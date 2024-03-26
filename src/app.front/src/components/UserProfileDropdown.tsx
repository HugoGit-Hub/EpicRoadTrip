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
        <div className="relative mr-4">
            {/* Bouton rond avec l'icône utilisateur */}
            {(isLoggedIn() ?
                <button
                    className="w-14 h-14 rounded-full bg-white border border-gray-300 flex items-center justify-center focus:outline-none p-2"
                    onClick={toggleDropdown}
                >
                    {/* Icône utilisateur (Image) */}
                    <img src="../../public/free-user.png" alt="User profile picture" width={200} height={200} />
                </button>
                :
                <button
                    className="w-14 h-14 rounded-full bg-white border border-gray-300 flex items-center justify-center focus:outline-none p-2"
                    onClick={() => navigate("/login")}>
                    <img src="../../public/point-d'interrogation.png" alt="User profile picture" width={200} height={200} />
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
