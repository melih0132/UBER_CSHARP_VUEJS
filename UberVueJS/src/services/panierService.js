import apiClient from "@/axios";

export const AjoutAuPanier = async (id, produitId, etablissementId) => {
  try {
    const response = await apiClient.post(
      `/Paniers?id=${id}&produitId=${produitId}&etablissementId=${etablissementId}`
    );
    return response.data;
  } catch (error) {
    console.error(error);
    return [];
  }
};

export const GetPanierById = async (id) => {
  try {
    const response = await apiClient.get(`/Paniers/GetById/${id}`);
    return response.data; 
  } catch (error) {
    console.error(error);
    return [];
  }
};

export const MajQuantiteProduitPanier = async (id, produitId, etablissementId, quantite) => {
  try {
    const response = await apiClient.put(
      `/Paniers/${id}?produitId=${produitId}&etablissementId=${etablissementId}&quantite=${quantite}`
    );
    return response.data;
  } catch (error) {
    console.error(error);
    return [];
  }
};

export const SupprimerProduitDuPanier = async (id, produitId, etablissementId) => {
  try {
    const response = await apiClient.delete(
      `/Paniers/${id}?produitId=${produitId}&etablissementId=${etablissementId}`
    );
    return response.data;
  } catch (error) {
    console.error(error);
    return [];
  }
};