import apiClient from "@/axios";

export const getProduits = async () => {
  try {
    const response = await apiClient.get("/produits");
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la récupération des clients :", error);
    return [];
  }
};

export const getProduitsVille = async (ville) => {
  try {
    const response = await apiClient.get("/produits");
    
    const produitsDansVille = response.data.filter(produit => {
      return produit?.idEtablissements?.some(etablissement => {
        const villeProduit = etablissement?.idAdresseNavigation?.idVilleNavigation?.nomVille;
        return villeProduit?.toLowerCase() === ville.toLowerCase();
      });
    });

    return produitsDansVille;
  } catch (error) {
    console.error("Erreur lors de la récupération des produits :", error);
    return [];
  }
};

