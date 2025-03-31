import apiClient from "@/axios";

export const getTypePrestations = async () => {
  try {
    const response = await apiClient.get("/typePrestations");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des clients :", error);
    return [];
  }
};