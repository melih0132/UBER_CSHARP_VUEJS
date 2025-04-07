import apiClient from "@/axios";

export const getVilles = async () => {
  try {
    const response = await apiClient.get("/villes");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des villes :", error);
    return [];
  }
};