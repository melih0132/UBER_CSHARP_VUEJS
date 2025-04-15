import apiClient from "@/axios";

export const getCarteBancaire = async () => {
  try {
    const response = await apiClient.get("/CarteBancaires");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des cartes :", error);
    return [];
  }
};

export const getCartesByClientId = async (clientId) => {
  try {
    const response = await apiClient.get(`/clients/getbyid/${clientId}`);

    if (response.data?.idCbs) {
      const cartes = Array.isArray(response.data.idCbs)
        ? response.data.idCbs
        : Object.values(response.data.idCbs);
      return cartes;
    }

    return [];
  } catch (error) {
    console.error("Erreur lors de la récupération des cartes du client :", error);
    return [];
  }
};

export const deleteCarteBancaire = async (idCarte) => {
  try {
    const response = await apiClient.delete(`/CarteBancaires/${idCarte}`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la suppression de la carte :", error);
    return [];
  }
};

export const addCarteBancaire = async (carteData) => {
  try {
    const formattedData = {
      numeroCb: carteData.numeroCb,
      dateExpireCb: carteData.dateExpireCb,
      cryptogramme: carteData.cryptogramme,
      typeCarte: carteData.typeCarte,
      typeReseaux: carteData.typeReseaux,
      idClients: []
    };

    console.log("Données envoyées à l'API:", formattedData);
    const response = await apiClient.post("/cartebancaires?clientId=" + carteData.userId, formattedData);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de l'ajout de la carte :", error);
    throw error;
  }
};

