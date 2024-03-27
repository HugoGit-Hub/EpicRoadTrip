import { type ClassValue, clsx } from "clsx"
import { twMerge } from "tailwind-merge"

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export function formatDate(date: Date | undefined) {
  // Obtenir le jour, le mois et l'année
  if (date) {
    let jour = date.getDate();
    let mois = date.getMonth() + 1; // Les mois commencent à 0, donc on ajoute 1
    let annee = date.getFullYear() % 100; // Obtenez les deux derniers chiffres de l'année

    // Ajouter un zéro devant le jour et le mois si nécessaire
    jour = jour < 10 ? "0" + jour : jour;
    mois = mois < 10 ? "0" + mois : mois;
    annee = annee < 10 ? "0" + annee : annee;

    // Concaténer pour obtenir le format "dd/mm/aa"
    return `${jour}/${mois}/${annee}`;
  }
  return "";
}

export function differenceInDays(date1: Date, date2: Date) {
  // Soustrayez les deux dates pour obtenir la différence en millisecondes
  const differenceInMilliseconds = date1 - date2;

  // Convertissez la différence en jours
  const differenceInDays = differenceInMilliseconds / (1000 * 60 * 60 * 24);

  // Arrondissez la valeur au nombre entier si nécessaire
  return Math.round(differenceInDays);
}
 
export function formatDateToISO8601(date: Date) {
  let year = date.getFullYear();
  let month = (date.getMonth() + 1).toString().padStart(2, '0');
  let day = date.getDate().toString().padStart(2, '0');
  let hours = date.getHours().toString().padStart(2, '0');
  let minutes = date.getMinutes().toString().padStart(2, '0');
  let seconds = date.getSeconds().toString().padStart(2, '0');
  let milliseconds = date.getMilliseconds().toString().padStart(3, '0');

  return `${year}-${month}-${day}T${hours}:${minutes}:${seconds}.${milliseconds}Z`;
}