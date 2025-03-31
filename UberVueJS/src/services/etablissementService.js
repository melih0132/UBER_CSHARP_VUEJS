import apiClient from "@/axios";
import axios from "@/axios";

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
export const getEtablissementsByVille = async (ville) => {
  try {
    const response = await apiClient.get("/etablissements");
    const etablissementsDansVille = response.data.filter(etablissement => etablissement.idAdresseNavigation.idVilleNavigation.nomVille === ville);
    
    return etablissementsDansVille;
  } catch (error) {
    console.error("Erreur lors de la récupération des établissements :", error);
    return [];
  }
};

