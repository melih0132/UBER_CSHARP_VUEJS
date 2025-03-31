<template>
    <div class="container" style="min-height: 100vh;padding: 2rem;">

      <input type="text" v-model="rechercheProduit" class="search-input" placeholder="Recherchez un produit ou un établissement..." />

      <div class="filter">
        <div class="filters-grid">
          <!-- <input type="text" v-model="filtreVille" id="ville" class="search-input" placeholder="Entrez une ville" /> -->
  
          <select v-model="filtreType" id="type" class="filter-select">
            <option value="">Tous les types</option>
            <option value="Restaurant">Restaurant</option>
            <option value="Café">Café</option>
            <option value="Boulangerie">Boulangerie</option>

          </select>

          <select v-model="selectedTypeAffichage" @change="handleSelectionChange" class="filter-select">
                <option value="all">Établissements et Produits</option>
                <option value="etablissements">Établissements</option>
                <option value="produits">Produits</option>
            </select>
  
          <select v-model="filtreLivraison" id="type_livraison" class="filter-select">
          <option value="">Tous</option>
          <option value="livraison">Livraison</option>
          <option value="retrait">Retrait</option>
        </select>
        </div>
      </div>
  
      <!-- Affichage des établissements et produits -->
  <div v-if="selectedTypeAffichage === 'all'" class="my-4">
    <!-- Affichage des établissements -->
    <div class="etablissements">
      <h1 class="div-title">Établissements</h1>
      <div class="etablissements-grid">
        <div v-for="(etablissement, index) in etablissementsFiltres" :key="etablissement.idetablissement">
          <form :method="'GET'" :action="'/etablissement/detail/' + etablissement.idEtablissement">
            <button type="submit" class="btn-etablissement">
              <div class="etablissement-card">
                <div class="etablissement-image">
                  <img :src="etablissement.imageEtablissement" :alt="etablissement.nomEtablissement" />
                </div>
                <div class="etablissement-details pt-4">
                  <h5 class="etablissement-name">{{ etablissement.nomEtablissement }}</h5>
                  <h6 class="etablissement-type"><p>{{ etablissement.typeEtablissement }}</p></h6>
                </div>
              </div>
            </button>
          </form>
        </div>
      </div>
    </div>

    <!-- Affichage des produits -->
    <div class="produits">
      <h1 class="div-title">Produits</h1>
      <div class="produits-grid">
        <div v-for="produit in produits" :key="produit.idProduit" class="produit">
          <div class="produit-card">
            <img :src="produit.imageProduit" :alt="produit.nomProduit" class="produit-img" />
            <h5 class="produit-name">{{ produit.nomProduit }}</h5>
            <p class="produit-price">{{ produit.prixProduit }} €</p>
            <button @click="ajouterAuPanier(produit)" class="btn-panier">Ajouter au panier</button>
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- Affichage des établissements uniquement -->
  <div v-if="selectedTypeAffichage === 'etablissements'" class="my-4">
    <h1 class="div-title">Établissements</h1>
    <div class="etablissements-grid">
      <div v-for="(etablissement, index) in etablissementsFiltres" :key="etablissement.idetablissement">
        <form :method="'GET'" :action="'/etablissement/detail/' + etablissement.idEtablissement">
          <button type="submit" class="btn-etablissement">
            <div class="etablissement-card">
              <div class="etablissement-image">
                <img :src="etablissement.imageEtablissement" :alt="etablissement.nomEtablissement" />
              </div>
              <div class="etablissement-details pt-4">
                <h5 class="etablissement-name">{{ etablissement.nomEtablissement }}</h5>
                <h6 class="etablissement-type"><p>{{ etablissement.typeEtablissement }}</p></h6>
              </div>
            </div>
          </button>
        </form>
      </div>
    </div>
  </div>

  <!-- Affichage des produits uniquement -->
  <div v-if="selectedTypeAffichage === 'produits'" class="produits">
    <h1 class="div-title">Produits</h1>
    <div class="produits-grid">
      <div v-for="produit in produits" :key="produit.idProduit" class="produit">
        <div class="produit-card">
          <img :src="produit.imageProduit" :alt="produit.nomProduit" class="produit-img" />
          <h5 class="produit-name">{{ produit.nomProduit }}</h5>
          <p class="produit-price">{{ produit.prixProduit }} €</p>
          <button @click="ajouterAuPanier(produit)" class="btn-panier">Ajouter au panier</button>
        </div>
      </div>
    </div>
  </div>

  </div>
  </template>
  

  <script>
  import { getEtablissementsByVille } from "@/services/etablissementService";
  import { getProduits } from "@/services/produitService.js";
  
  export default {
    data() {
      return {
        etablissements: [], // Liste des établissements
        produits: [], // Liste des produits
        errorMessage: "",
        successMessage: "",
        filtreVille: "", // Filtre par ville
        filtreType: "", // Filtre par type d'établissement
        filtreLivraison: "", // Filtre par livraison ou retrait
        rechercheProduit: "", // Recherche par produit ou établissement
        selectedTypeAffichage: 'all',
      };
    },
    computed: {
      // Filtrage des établissements
      etablissementsFiltres() {
        let resultats = this.etablissements;
  
        // Filtrage par nom d'établissement (barre de recherche)
        if (this.rechercheProduit) {
          resultats = resultats.filter(e =>
            e.nomEtablissement.toLowerCase().includes(this.rechercheProduit.toLowerCase())
          );
        }
  
        // Filtrage par ville
        if (this.filtreVille) {
          resultats = resultats.filter(e =>
            e.idAdresseNavigation && e.idAdresseNavigation.idVilleNavigation &&
            e.idAdresseNavigation.idVilleNavigation.nomVille.toLowerCase().includes(this.filtreVille.toLowerCase())
          );
        }
  
        // Filtrage par type d'établissement
        if (this.filtreType) {
          resultats = resultats.filter(e =>
            e.typeEtablissement && e.typeEtablissement.toLowerCase() === this.filtreType.toLowerCase()
          );
        }
  
        if (this.filtreLivraison) {
        if (this.filtreLivraison === "livraison") {
          resultats = resultats.filter((e) => e.livraison === true); 
        } else if (this.filtreLivraison === "retrait") {
          resultats = resultats.filter((e) => e.aemporter === true); 
        }
      }
  
        return resultats;
      }
    },

    produitsFiltres() {
    let resultats = this.produits;

    if (this.rechercheProduit === "all"){
        //Affiche les établissements et les produits
    }
    // Filtrage par nom de produit
    else if (this.rechercheProduit) {
      resultats = resultats.filter(p =>
        p.nomProduit.toLowerCase().includes(this.rechercheProduit.toLowerCase())
      );
    }

    return resultats;
  },


    methods: {
        
      async fetchData() {
        try {
          this.etablissements = await getEtablissementsByVille(this.$route.params.nomVille);
          this.produits = await getProduits(this.$route.params.nomVille);
          this.successMessage = "Données chargées avec succès";
        } catch (error) {
          this.errorMessage = "Une erreur est survenue lors du chargement des données";
        }
      },
      ajouterAuPanier(produit) {
    console.log("produit ajouté au panier");

    // Récupérer le panier actuel depuis localStorage ou initialiser un tableau vide si le panier n'existe pas
    let panier = JSON.parse(localStorage.getItem("panier")) || [];

    // Vérifier si le produit est déjà dans le panier
    const indexProduit = panier.findIndex(item => item.idProduit === produit.idProduit);

    if (indexProduit !== -1) {
      // Si le produit existe déjà, on met à jour la quantité
      panier[indexProduit].quantite += 1;
    } else {
      // Sinon, on ajoute le produit avec une quantité de 1
      produit.quantite = 1;
      panier.push(produit);
    }

    // Sauvegarder à nouveau le panier dans le localStorage
    localStorage.setItem("panier", JSON.stringify(panier));

    // Afficher le produit ajouté au panier
    console.log(panier);
  },
    },
    mounted() {
      this.fetchData();
      document.title = "Uber Eats";
    }
  };
  </script>
  

<style scoped>

.container {
    cursor: pointer;
    max-width: 1200px;
    margin: 0 auto;
    padding: 20px;
}

h1 {
    color: #000;
    font-weight: bold;
    margin-bottom: 20px;
}
.search-input {
    width: 100%;
    padding: 1rem;
    font-size: 1rem;
    border: 2px solid #e2e8f0;
    border-radius: 8px;
    margin-bottom: 1.5rem;
    transition: border-color 0.2s ease;
    height: 50px;
}
.filter {
    margin-top: 2rem;
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    gap: 10px;
}
.filter-select {
    width: auto;
    padding: 0.75rem;
    border: 2px solid #e2e8f0;
    border-radius: 8px;
    background-color: white;
    font-size: 0.95rem;
    transition: border-color 0.2s ease;
    cursor: pointer;
    height: 50px;
}


.filters-grid {
    display: flex;
    gap: 1rem;
    margin-bottom: 1.5rem;
}

.filter {
    margin-top: 2rem;
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    gap: 10px;
}



.filter .btn-dark {
    background-color: #000;
    color: #fff;
    border-radius: 30px;
    padding: 10px 20px;
    border: none;
    cursor: pointer;
    font-weight: bold;
    transition: background-color 0.2s ease;
}

.filter .btn-dark:hover {
    background-color: #333;
}

.div-title {
    text-align: center;
    margin: 3rem;
}

.btn-etablissement {
    border: none;
    background-color: transparent;
}

.btn-etablissement:hover {
    transform: scale(1.02);
    border: none;
    background-color: transparent;
}

.btn-etablissement:active {
    border: none;
    background-color: white;
}

.etablissements-grid {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
    gap: 4em;
    margin-top: 20px;
    justify-content: center;
    padding: 0 20px;
}

.etablissement-card {
    border: 2px solid white;
    border-radius: 8px;
    overflow: hidden;
    background-color: white;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    display: flex;
    text-decoration: none;
    flex-direction: column;
    justify-content: space-between;
    text-align: center;
    cursor: pointer;
    height: 240px;
    width: 300px;
}

.etablissement-container {
    display: flex;
    justify-content: center;
    align-items: center;
    text-align: center;
}

.etablissement-card:hover {
    box-shadow: 0 6px 10px rgba(0, 0, 0, 0.15);
}

.etablissement-image img {
    width: 100%;
    height: 150px;
    object-fit: cover;
    border-bottom: 1px solid #ddd;
}

.etablissement-details {
    padding: 15px;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: center;
    flex-grow: 1;
}

.etablissement-name {
    font-size: 18px;
    font-weight: bold;
    margin-bottom: 10px;
    color: #333;
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
}

.etablissement-type {
    font-size: 14px;
    margin-bottom: 10px;
    color: #555;
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
}

.produits-grid {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
    gap: 4em;
    margin-top: 20px;
    justify-content: center;
    padding: 0 20px;
}

.produit-card {
    margin: 0;
    padding: 15px;
    box-sizing: border-box;
    text-align: center;
    border-radius: 8px;
    background-color: #f6f6f6;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    transition: transform 0.2s ease, box-shadow 0.2s ease;
    cursor: pointer;
}

.produit-card:hover {
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.produit-name {
    font-size: 18px;
    font-weight: bold;
    color: #333;
    margin-bottom: 10px;
}

.produit-etablissement {
    font-size: 15px;
    font-weight: bold;
    color: #333;
}

.produit-price {
    font-size: 14px;
    color: #777;
    font-weight: bold;
    margin-bottom: 0.2rem;
}

.produit-img {
    width: 100%;
    height: 150px;
    object-fit: cover;
    border-radius: 8px;
    border: 1px solid #ddd;
    margin-bottom: 10px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.btn-panier {
    font-size: 14px;
    font-weight: 600;
    background: rgb(0, 0, 0);
    color: rgb(255, 255, 255);
    border-radius: 8px;
    padding: 0.5rem 1rem;
    border: none;
}

.btn-panier:hover {
    background: rgb(40, 40, 40);
    color: rgb(255, 255, 255);
}

/* Additional Uber Eats-specific styles */
.header {
    background-color: #000;
    color: #fff;
    padding: 20px;
    text-align: center;
}

.footer {
    background-color: #000;
    color: #fff;
    padding: 20px;
    text-align: center;
    margin-top: 20px;
}

</style>
