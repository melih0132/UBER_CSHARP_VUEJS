<template>
  <div v-if="successMessage" class="notification">
    <p>{{ successMessage }}</p>
  </div>

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
            <span v-if="etablissement && etablissement.idCategoriePrestations"
              v-for="(categorie, index) in etablissement.idCategoriePrestations" :key="categorie.idCategoriePrestation">
              {{ categorie.libelleCategoriePrestation }}
              <span v-if="index < etablissement.idCategoriePrestations.length - 1">•</span>
            </span>
          </div>
        </div>

        <div class="etablissement-description">
          <p>{{ etablissement.description }}</p>
        </div>
        <div class="address-section">
          <p>
            {{ etablissement.idAdresseNavigation.libelleAdresse }},
            {{
              etablissement.idAdresseNavigation.idVilleNavigation.nomVille
            }}
            ({{
              etablissement.idAdresseNavigation.idVilleNavigation
                .idCodePostalNavigation.cp
            }})
          </p>
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
          <button type="submit" @click="ajouterAuPanier(produit)" class="btn-panier">
            Ajouter au panier
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { getEtablissementById, getHorairesByEtablissementId } from "@/services/etablissementService";
import { getCategoriePrestations } from "@/services/categoriePrestationService";
import { useUserStore } from '@/stores/userStore';
import {
  AjoutAuPanier,
  MajQuantiteProduitPanier,
  GetPanierById,
} from "@/services/panierService";

export default {
  data() {
    return {
      etablissement: null,
      successMessage: "",
      categories: [],
      horaires: [],
      userStore: useUserStore(),
    };
  },
  async created() {
    const idEtablissement = this.$route.params.idEtablissement;
    try {
      this.etablissement = await getEtablissementById(idEtablissement);

      if (this.etablissement) {
        this.categories = await getCategoriePrestations(this.etablissement.idEtablissement);
        document.title = this.etablissement.nomEtablissement;
      }

      this.horaires = await getHorairesByEtablissementId(idEtablissement);
      console.table(this.horaires);
    } catch (error) {
      console.error("Erreur lors de la récupération des données :", error);
    }
  },
  methods: {
    ajouterAuPanier(produit) {
      try {
        const etablissementId = this.etablissement.idEtablissement;

        this.updateLocalStoragePanier(produit, etablissementId);

        if (this.userStore.isAuthenticated && this.userStore.user) {
          this.updateDatabasePanier(produit, etablissementId);
        }

        this.showSuccessMessage(produit.nomProduit);
      } catch (error) {
        console.error("Erreur lors de l'ajout au panier:", error);
      }
    },
    updateLocalStoragePanier(produit, etablissementId) {
      try {
        let panier = JSON.parse(localStorage.getItem("panier")) || [];

        const existingIndex = panier.findIndex(
          (item) =>
            item.idProduit === produit.idProduit &&
            item.idEtablissement === etablissementId
        );

        if (existingIndex !== -1) {
          panier[existingIndex].quantite += 1;
        } else {
          panier.push({
            idProduit: produit.idProduit,
            idEtablissement: etablissementId,
            nomProduit: produit.nomProduit,
            prixProduit: produit.prixProduit,
            imageProduit: produit.imageProduit,
            quantite: 1,
          });
        }

        localStorage.setItem("panier", JSON.stringify(panier));
      } catch (error) {
        console.error("Erreur lors de la mise à jour du panier local:", error);
      }
    },
    async updateDatabasePanier(produit, etablissementId) {
      try {
        const userId = this.userStore.user.userId;
        const panierData = await GetPanierById(userId);

        if (panierData && panierData.contient2s) {
          const existingProduct = panierData.contient2s.find(
            (item) =>
              item.idProduit === produit.idProduit &&
              item.idEtablissement === etablissementId
          );

          if (existingProduct) {
            const nouvelleQuantite = existingProduct.quantite + 1;
            await MajQuantiteProduitPanier(
              userId,
              produit.idProduit,
              etablissementId,
              nouvelleQuantite
            );
          } else {
            await AjoutAuPanier(userId, produit.idProduit, etablissementId);
          }
        } else {
          await AjoutAuPanier(userId, produit.idProduit, etablissementId);
        }
      } catch (error) {
        console.error("Erreur lors de la mise à jour du panier dans la base de données:", error);
      }
    },
    showSuccessMessage(nomProduit) {
      this.successMessage = `${nomProduit} ajouté au panier !`;
      setTimeout(() => {
        this.successMessage = "";
      }, 3000);
    },
    formatCodePostal(codepostal) {
      return codepostal ? codepostal.substring(0, 2) : "";
    },
    formatHeure(dateString) {
      const date = new Date(dateString);
      const heures = String(date.getHours()).padStart(2, "0");
      const minutes = String(date.getMinutes()).padStart(2, "0");
      return `${heures}:${minutes}`;
    },
    isClosed(horaire) {
      return (
        horaire.heureDebut === "0001-01-02T08:00:00+00:00" ||
        horaire.heureFin === "0001-01-02T18:00:00+00:00"
      );
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
    const link = document.querySelector("link[rel='icon']");
    if (link) {
      link.href = "/public/images/UberEatsPetit.png";
    }
  },
};
</script>

<style scoped>
.notification {
  position: fixed;
  top: 80px;
  right: 20px;
  background-color: #28a745;
  color: white;
  padding: 10px 20px;
  border-radius: 5px;
  font-size: 16px;
  z-index: 1000;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  opacity: 1;
  transition: opacity 0.3s ease;
}

.notification.hide {
  opacity: 0;
}

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
