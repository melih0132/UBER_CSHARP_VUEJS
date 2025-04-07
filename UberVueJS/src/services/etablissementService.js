import apiClient from "@/axios";

export const getEtablissements = async () => {
  try {
    const response = await apiClient.get("/etablissements");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des établissements :", error);
    return [];
  }
};

export const getEtablissementById = async (idEtablissement) => {
  try {
    const response = await apiClient.get(`/etablissements/GetById/${idEtablissement}`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération de l'établissement", error);
    throw error;
  }
};

const extractHourMinutes = (isoString) => {
  const date = new Date(isoString);
  // Utilisation des heures/minutes en UTC (les horaires étant définis avec une date fictive)
  const hours = date.getUTCHours().toString().padStart(2, '0');
  const minutes = date.getUTCMinutes().toString().padStart(2, '0');
  return `${hours}:${minutes}`;
};

export const getEtablissementsByVille = async (ville, date = null, time = null) => {
  try {
    const response = await apiClient.get("/etablissements");

    // Filtrer par ville
    const all = response.data.filter(e =>
      e.idAdresseNavigation?.idVilleNavigation?.nomVille === ville
    );

    // Si date ou time ne sont pas fournis, on retourne tous les établissements
    if (!date || !time) return all;

    // Récupération du jour de la semaine au format complet (exemple: "lundi")
    const jourSemaine = new Date(date)
      .toLocaleDateString("fr-FR", { weekday: "long" })
      .toLowerCase();

    // Filtrer pour ne garder que les établissements ouverts à l'heure donnée
    return all.filter(etab => {
      if (!etab.horaires || etab.horaires.length === 0) return false;

      // Recherche de l'horaire correspondant au jour (on compare en minuscule)
      return etab.horaires.some(h => {
        if (h.jourSemaine.toLowerCase() !== jourSemaine) return false;
        const heureDebut = extractHourMinutes(h.heureDebut);
        const heureFin = extractHourMinutes(h.heureFin);
        return time >= heureDebut && time <= heureFin;
      });
    });

  } catch (error) {
    console.error("Erreur lors de la récupération des établissements :", error);
    return [];
  }
};

export const getHorairesByEtablissementId = async (idEtablissement) => {
  try {
    const response = await apiClient.get(`/etablissements/GetById/${idEtablissement}`);
    return response.data.horaires;
  } catch (error) {
    console.error("Erreur lors de la récupération des horaires :", error);
    return [];
  }
};
