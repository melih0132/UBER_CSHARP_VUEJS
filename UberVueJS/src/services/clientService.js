import apiClient from "@/axios";

export const getClients = async () => {
  try {
    const response = await apiClient.get("/clients");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des clients :", error);
    return [];
  }
};

export const getClientById = async (idClient) => {
  try {
    const response = await apiClient.get(`/clients/GetById/${idClient}`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération du client :", error);
    throw error;
  }
};