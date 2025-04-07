import apiClient from "@/axios";
import { useUserStore } from '@/stores/userStore';

export const getCarteBancaire = async () => {
  try {
    const response = await apiClient.get("/CarteBancaires");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des cartes :", error);
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
    const userStore = useUserStore();
    const user = userStore.user;
    
    // Format attendu par l'API basé sur l'erreur et l'exemple JSON
    const formattedData = {
      numeroCb: carteData.numeroCb,
      dateExpireCb: carteData.dateExpireCb,
      cryptogramme: carteData.cryptogramme,
      typeCarte: carteData.typeCarte,
      typeReseaux: carteData.typeReseaux,
      idClients: []
    };
    
    console.log("Données envoyées à l'API:", formattedData);
    const response = await apiClient.post("/cartebancaires?clientId="+carteData.userId, formattedData);
    return response.data;
  } catch (error) {
    console.error("Erreur lors de l'ajout de la carte :", error);
    throw error;
  }
};

export const getCartesByClientId = async (clientId) => {
  try {
    // Vérifie si clientId est défini avant de faire la requête
    if (!clientId) {
      throw new Error("Client ID non défini");
    }

    const response = await apiClient.get(`/clients/getbyid/${clientId}`);

    // Vérifie si la réponse contient bien les cartes bancaires
    if (response.data && response.data.idCbs) {
      return response.data.idCbs;
    } else {
      console.error("Aucune carte bancaire trouvée pour ce client.");
      return []; // Retourne un tableau vide si aucune carte n'est trouvée
    }
  } catch (error) {
    console.error("Erreur lors de la récupération des cartes du client :", error);
    return []; // Retourne un tableau vide en cas d'erreur
  }
};

