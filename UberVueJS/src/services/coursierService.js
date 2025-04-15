import apiClient from "@/axios";

export const getCoursiers = async () => {
  try {
    const response = await apiClient.get("/coursiers");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des coursiers :", error);
    return [];
  }
};

export const getCoursierById = async (idCoursier) => {
  try {
    const response = await apiClient.get(`/coursiers/GetById/${idCoursier}`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération du coursiers :", error);
    throw error;
  }
};