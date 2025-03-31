import apiClient from "@/axios";

export const getCategoriePrestations = async () => {
  try {
    const response = await apiClient.get("/categoriePrestations");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des clients :", error);
    return [];
  }
};
