import apiClient from "@/axios";

export const getCommandes = async () => {
  try {
    const response = await apiClient.get("/commandes");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des commandes :", error);
    return [];
  }
};
