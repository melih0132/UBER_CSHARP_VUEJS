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

    // Vérification de l'existence des données avant d'essayer d'y accéder
    response.data.forEach(produit => {
      console.log(produit); // Afficher chaque produit pour déboguer
    });

    // Filtrage des produits en fonction de la ville
    const produitsDansVille = response.data.filter(produit => {
      // Vérifier que l'objet idEtablissements, idAdresseNavigation et idVilleNavigation existent
      const villeProduit = produit?.idEtablissements?.idAdresseNavigation?.idVilleNavigation;

      // Si l'objet existe, on compare le nom de la ville, sinon on ignore ce produit
      return villeProduit?.nomVille === ville;
    });

    return produitsDansVille;
  } catch (error) {
    console.error("Erreur lors de la récupération des produits :", error);
    return [];
  }
};
