import apiClient from "@/axios";

export const getAdresseById = async (idAdresse) => {
  try {
    const response = await apiClient.get("/Adresses/GetById/" + idAdresse);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des adresses :", error);
    return [];
  }
};

export const getAdresseByLibelleAdresse = async (libelleAdresse) => {
    try {
      const response = await apiClient.get("/Adresses/GetByLibelleAdresse/" + libelleAdresse);
      return response.data;
    } catch (error) {
      console.error("Erreur lors de la récupération des adresses :", error);
      return [];
    }
  };