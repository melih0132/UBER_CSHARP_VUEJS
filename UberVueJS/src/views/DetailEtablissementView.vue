<template>
  <section class="etablissement-detail">
    <div class="etablissement-banner">
      <img :src="etablissement?.imageEtablissement" alt="Image de l'établissement" />
    </div>

    <div class="main d-flex">
      <div class="main-info">
        <h1 class="font-weight-bold text-uppercase">
          {{ etablissement?.nomEtablissement }}
        </h1>
        <div class="categories-section">
          <div class="categories">
            <span v-for="(categorie, index) in etablissement.idCategoriePrestations"
              :key="categorie.idCategoriePrestation">
              {{ categorie.libelleCategoriePrestation }}
              <span v-if="index < etablissement.idCategoriePrestations.length - 1">•</span>
            </span>
          </div>
        </div>
        <div class="etablissement-description">
          <p>{{ etablissement.description }}</p>
        </div>
        <div class="address-section">
          <p> {{ etablissement.idAdresseNavigation.libelleAdresse }}, {{
            etablissement.idAdresseNavigation.idVilleNavigation.nomVille }}
            ({{ etablissement.idAdresseNavigation.idVilleNavigation.idCodePostalNavigation.cp }})</p>
        </div>
      </div>

      <div class="etablissement-info">
        <div class="options-container">
          <div class="options">
            <span :class="['option', etablissement?.livraison ? 'active' : '']">Livraison</span>
            <span :class="['option', etablissement?.aEmporter ? 'active' : '']">À emporter</span>
          </div>
        </div>
        <div class="hours-section">
          <ul class="hours-list">
            <li v-for="(horaire, index) in horaires" :key="index" class="hours-item">
              <span class="days pe-2">{{ horaire.jourSemaine }}</span>
              <span class="time">
                <span v-if="isClosed(horaire)" class="closed">Fermé</span>
                <span v-else>{{ formatTime(horaire.heureDebut) }} - {{ formatTime(horaire.heureFin) }}</span>
              </span>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </section>
  <div class="products-grid my-5">
    <div v-for="produit in etablissement.idProduits" :key="produit.idProduit" class="product">
      <div class="product-card">
        <div class="product-image">
          <img :src="produit.imageProduit" :alt="produit.nomProduit" />
        </div>
        <div class="product-details">
          <h5 class="product-name">{{ produit.nomProduit }}</h5>
          <h5 class="product-price">{{ formatPrice(produit.prixProduit) }} €</h5>
          <!--           <form method="POST" action="{{ route('panier.ajouter') }}"> 
            <input name="product" value="{{ $produit->idproduit }}" type="hidden"> -->
          <button type="submit" @click="ajouterAuPanier(produit)" class="btn-panier">Ajouter au panier</button>
          <!-- </form> -->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { getEtablissementById } from "@/services/etablissementService";
import { getCategoriePrestations } from "@/services/categoriePrestationService";

export default {
  data() {
    return {
      etablissement: null,
      categories: [],
      horaires: []
    };
  },
  async created() {
    try {

      const idEtablissement = this.$route.params.idEtablissement;

      this.etablissement = await getEtablissementById(idEtablissement);
      if (this.etablissement) {
        this.categories = await getCategoriePrestations(this.etablissement.idEtablissement);
      }
    } catch (error) {
      console.error("Erreur lors de la récupération des données", error);
    }
    this.fetchHoraires();
  },
  methods: {
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
    formatCodePostal(codepostal) {
      return codepostal ? codepostal.substring(0, 2) : '';
    },
    formatHeure(dateString) {
      const date = new Date(dateString);
      const heures = String(date.getHours()).padStart(2, '0');
      const minutes = String(date.getMinutes()).padStart(2, '0');
      console.log(`${heures}:${minutes}`)
      return `${heures}:${minutes}`;
    },
    async fetchHoraires() {
      try {
        const idEtablissement = this.$route.params.idEtablissement;
        const response = await fetch('https://uberapi-azure-bceagvaug7cxa8hk.francecentral-01.azurewebsites.net/api/etablissements/getById/' + idEtablissement);
        const data = await response.json();

        this.horaires = data.horaires;
      } catch (error) {
        console.error('Erreur lors de la récupération des horaires:', error);
      }
    },

    isClosed(horaire) {
      return horaire.heureDebut === '0001-01-02T08:00:00+00:00' || horaire.heureFin === '0001-01-02T18:00:00+00:00';
    },
    formatTime(isoTime) {
      const date = new Date(isoTime);
      return date.toISOString().slice(11, 16);
    },
    formatPrice(price) {
      return price.toFixed(2);
    },
  },
  mounted() {
    document.title = this.etablissement?.nomEtablissement;
    this.fetchData();
  }
};
</script>

<style scoped>
@font-face {
  font-family: 'Uber Move Bold';
  src: url('/fonts/UberMoveBold.otf') format('opentype');
  font-weight: bold;
  font-style: normal;
}

@font-face {
  font-family: 'Uber Move Medium';
  src: url('/fonts/UberMoveMedium.otf') format('opentype');
  font-weight: 500;
  font-style: normal;
}

body {
  font-family: 'Uber Move Bold', sans-serif;
  box-sizing: border-box;
  letter-spacing: 0.5px;
  position: relative;
  overflow-x: hidden;
  overflow-y: scroll;
}

.main {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  align-items: flex-start;
  background-color: #fff;
  border-radius: 10px;
  padding: 20px;
  max-width: 1200px;
  margin: 20px auto;
  gap: 20px;
  font-size: 16px;
}

.main-info {
  flex: 1;
  min-width: 300px;
}

.main-info h1 {
  font-size: 28px;
  color: #222;
  margin-bottom: 20px;
  font-weight: 700;
  text-transform: uppercase;
  letter-spacing: 0.5px;
  font-family: 'Uber Move Bold', sans-serif;
}

.categories-section .categories {
  font-size: 16px;
  font-weight: 600;
  color: #555;
  margin-bottom: 10px;
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  align-items: center;
}

.product-description p {
  font-size: 16px;
  line-height: 1.8;
  color: #666;
  margin-bottom: 15px;
}

.etablissement-description {
  font-size: 16px;
  color: rgb(102, 102, 102);
  margin-bottom: 10px;
}

.etablissement-detail {
  max-width: 1200px;
  margin: 20px auto;
  background: #fff;
  border-radius: 10px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  overflow: hidden;
}

.etablissement-banner img {
  width: 100%;
  height: 300px;
  object-fit: cover;
  border-bottom: 1px solid #ddd;
}

.address-section p {
  font-size: 16px;
  color: #444;
  margin: 10px 0;
}

.address-section strong {
  font-weight: bold;
  color: #000;
}

.product-info {
  flex: 1;
  min-width: 300px;
  padding: 15px;
  font-size: 16px;
}

.options-container {
  display: flex;
  justify-content: flex-end;
}

.options {
  display: flex;
  justify-content: flex-end;
  border-radius: 20px;
  background-color: #f0f0f0;
  padding: 5px;
  gap: 5px;
}

.option {
  display: flex;
  padding: 8px 16px;
  border-radius: 20px;
  font-size: 14px;
  color: #666;
  background-color: transparent;
  cursor: pointer;
  transition: all 0.3s ease;
}

.option.active {
  background-color: #fff;
  color: #000;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  font-weight: bold;
}

.option:not(.active):hover {
  cursor: not-allowed;
}

.hours-section {
  margin-top: 20px;
  padding: 15px 20px;
  background-color: #f9f9f9;
  border-radius: 10px;
  border: 1px solid #e6e6e6;
  font-size: 16px;
}

.hours-list {
  list-style: none;
  padding: 0;
  margin: 0;
}

.hours-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 6px 0 10px;
  border-bottom: 1px solid #ddd;
  font-size: 16px;
  color: #555;
}

.hours-item:last-child {
  border-bottom: none;
}

.hours-item .days {
  font-weight: bold;
  color: #333;
  text-align: left;
}

.hours-item .time {
  text-align: right;
  color: #666;
  font-weight: 500;
}

.hours-item .closed {
  color: #ff0000;
  font-style: italic;
}

@media (max-width: 768px) {
  .hours-item {
    flex-direction: column;
    align-items: flex-start;
  }

  .hours-item .days,
  .hours-item .time {
    text-align: left;
    width: 100%;
    margin-bottom: 5px;
  }

  .hours-item .time {
    margin-bottom: 0;
  }
}

.products-grid {
  display: flex;
  flex-wrap: wrap;
  gap: 15px;
  justify-content: center;
  margin: 20px 0;
}

.product-card {
  width: 220px;
  border-radius: 8px;
  background-color: #fff;
  box-shadow: 0 3px 6px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  font-size: 14px;
}

.product-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 6px 10px rgba(0, 0, 0, 0.15);
}

.product-image img {
  width: 100%;
  height: 140px;
  object-fit: cover;
  border-bottom: 1px solid #ddd;
}


.product-details {
  padding: 10px;
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 8px;
  text-align: center;
}

.product-name {
  font-size: 1rem;
  max-width: 10rem;
  font-weight: 600;
  color: #333;
  margin-bottom: 5px;

  white-space: normal;
  word-wrap: break-word;
  overflow: hidden;
  text-overflow: ellipsis;
}


.product-price {
  font-size: 14px;
  font-weight: 600;
  color: rgb(0, 0, 0);
  margin-bottom: 10px;
}

.btn-panier {
  display: inline-block;
  background-color: rgb(0, 0, 0);
  color: #fff;
  font-size: 12px;
  font-weight: bold;
  padding: 8px 15px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.btn-panier:hover {
  background-color: #282828;
}
</style>
