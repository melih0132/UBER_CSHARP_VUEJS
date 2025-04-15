<template>
  <div v-if="successMessage" class="notification">
    <p>{{ successMessage }}</p>
  </div>

  <div class="container" style="min-height: 100vh; padding: 2rem;">
    <div v-if="etablissements.length > 0" class="my-4">
      <input type="text" v-model="rechercheProduit" class="search-input"
        placeholder="Recherchez un produit ou un établissement..." />
      <div class="filter">
        <div class="filters-grid">
          <select v-model="filtreType" class="filter-select">
            <option value="">Tous les types</option>
            <option value="Restaurant">Restaurant</option>
            <option value="Café">Café</option>
            <option value="Boulangerie">Boulangerie</option>
          </select>

          <select v-model="selectedTypeAffichage" class="filter-select">
            <option value="all">Établissements et Produits</option>
            <option value="etablissements">Établissements</option>
            <option value="produits">Produits</option>
          </select>

          <select v-model="filtreLivraison" class="filter-select">
            <option value="">Type de livraison</option>
            <option value="livraison">Livraison</option>
            <option value="retrait">Retrait</option>
          </select>
        </div>
      </div>
    </div>

    <template v-if="selectedTypeAffichage === 'all'">
      <div class="etablissements my-4">
        <h1 class="div-title">Établissements</h1>
        <div v-if="etablissementsFiltres.length === 0" class="my-4 text-center">
          <p class="div-paragraph">Aucun établissement trouvé.</p>
        </div>
        <div v-else class="etablissements-grid">
          <div v-for="etablissement in etablissementsFiltres" :key="etablissement.idEtablissement">
            <form :method="'GET'" :action="'/etablissement/detail/' + etablissement.idEtablissement">
              <button type="submit" class="btn-etablissement">
                <div class="etablissement-card">
                  <div class="etablissement-image">
                    <img :src="etablissement.imageEtablissement" :alt="etablissement.nomEtablissement" />
                  </div>
                  <div class="etablissement-details pt-4">
                    <h5 class="etablissement-name">{{ etablissement.nomEtablissement }}</h5>
                    <div v-if="!isEtablissementOpen(etablissement.horaires)" class="closed-badge">
                      Fermé à cette heure
                    </div>
                    <h6 class="etablissement-type">
                      <p>{{ etablissement.typeEtablissement }}</p>
                    </h6>
                  </div>
                </div>
              </button>
            </form>
          </div>
        </div>
      </div>

      <div class="produits">
        <h1 class="div-title">Produits</h1>
        <div v-if="produitsFiltres.length === 0" class="my-4 text-center">
          <p class="div-paragraph">Aucun produit trouvé.</p>
        </div>
        <div v-else class="produits-grid">
          <div v-for="produit in produitsFiltres" :key="produit.idProduit" class="produit">
            <div class="produit-card">
              <img :src="produit.imageProduit" :alt="produit.nomProduit" class="produit-img" />
              <h5 class="produit-name">{{ produit.nomProduit }}</h5>
              <p class="produit-price">{{ formatPrice(produit.prixProduit) }} €</p>
              <button @click="ajouterAuPanier(produit)" class="btn-panier">Ajouter au panier</button>
            </div>
          </div>
        </div>
      </div>
    </template>

    <div v-else-if="selectedTypeAffichage === 'etablissements'" class="my-4">
      <h1 class="div-title">Établissements</h1>
      <div class="etablissements-grid">
        <div v-for="etablissement in etablissementsFiltres" :key="etablissement.idEtablissement">
          <form :method="'GET'" :action="'/etablissement/detail/' + etablissement.idEtablissement">
            <button type="submit" class="btn-etablissement">
              <div class="etablissement-card">
                <div class="etablissement-image">
                  <img :src="etablissement.imageEtablissement" :alt="etablissement.nomEtablissement" />
                </div>
                <div class="etablissement-details pt-4">
                  <h5 class="etablissement-name">{{ etablissement.nomEtablissement }}</h5>
                  <div v-if="!isEtablissementOpen(etablissement.horaires)" class="closed-badge">
                    Fermé à cette heure
                  </div>
                  <h6 class="etablissement-type">
                    <p>{{ etablissement.typeEtablissement }}</p>
                  </h6>
                </div>
              </div>
            </button>
          </form>
        </div>
      </div>
    </div>

    <div v-else-if="selectedTypeAffichage === 'produits'" class="produits">
      <h1 class="div-title">Produits</h1>
      <div class="produits-grid">
        <div v-for="produit in produitsFiltres" :key="produit.idProduit" class="produit">
          <div class="produit-card">
            <img :src="produit.imageProduit" :alt="produit.nomProduit" class="produit-img" />
            <h5 class="produit-name">{{ produit.nomProduit }}</h5>
            <p class="produit-price">{{ formatPrice(produit.prixProduit) }} €</p>
            <button @click="ajouterAuPanier(produit)" class="btn-panier">Ajouter au panier</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { useUserStore } from '@/stores/userStore'
import { getEtablissementsByVille } from '@/services/etablissementService.js'
import { getProduitsVille } from '@/services/produitService.js'
import { AjoutAuPanier, MajQuantiteProduitPanier, GetPanierById } from '@/services/panierService'

const route = useRoute();
const userStore = useUserStore();

const etablissements = ref([]);
const produits = ref([]);
const successMessage = ref('');
const filtreType = ref('');
const filtreLivraison = ref('');
const rechercheProduit = ref('');
const selectedTypeAffichage = ref('all');

const selectedDate = ref(route.query.date || new Date().toISOString().split('T')[0]);
const selectedTime = ref(
  route.query.time || new Date().toLocaleTimeString('fr-FR', { hour: '2-digit', minute: '2-digit' })
);

const etablissementsFiltres = computed(() => {
  return etablissements.value.filter(etab => {
    const matchesSearch = etab.nomEtablissement?.toLowerCase().includes(rechercheProduit.value.toLowerCase());
    const matchesType = !filtreType.value || etab.typeEtablissement === filtreType.value;
    const matchesLivraison = checkLivraison(etab);
    const isOpen = isEtablissementOpen(etab.horaires);
    return matchesSearch && matchesType && matchesLivraison && isOpen;
  });
});

const produitsFiltres = computed(() => {
  const etabIds = etablissementsFiltres.value.map(e => e.idEtablissement);
  return produits.value.filter(produit => {
    const matchesSearch = produit.nomProduit?.toLowerCase().includes(rechercheProduit.value.toLowerCase());
    return matchesSearch && produit.idEtablissements.some(e => etabIds.includes(e.idEtablissement));
  });
});

const fetchData = async () => {
  try {
    const ville = route.params.nomVille;
    etablissements.value = await getEtablissementsByVille(ville, selectedDate.value, selectedTime.value);
    produits.value = await getProduitsVille(ville);
  } catch (error) {
    console.error('Erreur de chargement:', error);
  }
};

const checkLivraison = (etab) => {
  if (filtreLivraison.value === 'livraison') return etab.livraison;
  if (filtreLivraison.value === 'retrait') return etab.aEmporter;
  return true;
};

const ajouterAuPanier = async (produit) => {
  try {
    if (!produit.idEtablissements || produit.idEtablissements.length === 0) {
      console.error("Le produit n'a pas d'établissement associé");
      return;
    }
    
    const etablissementId = produit.idEtablissements[0].idEtablissement;
    
    updateLocalStoragePanier(produit, etablissementId);
    
    if (userStore.isAuthenticated && userStore.user) {
      await updateDatabasePanier(produit, etablissementId);
    }
    
    showSuccessMessage(produit.nomProduit);
  } catch (error) {
    console.error("Erreur lors de l'ajout au panier:", error);
  }
};

const updateLocalStoragePanier = (produit, etablissementId) => {
  try {
    const panier = JSON.parse(localStorage.getItem('panier')) || [];
    
    const existingIndex = panier.findIndex(p => 
      p.idProduit === produit.idProduit && p.idEtablissement === etablissementId
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
        quantite: 1
      });
    }

    localStorage.setItem('panier', JSON.stringify(panier));
  } catch (error) {
    console.error("Erreur lors de la mise à jour du panier local:", error);
  }
};

const updateDatabasePanier = async (produit, etablissementId) => {
  try {
    const panierData = await GetPanierById(userStore.user.userId);
    
    if (panierData && panierData.contient2s) {
      const existingProduct = panierData.contient2s.find(
        item => item.idProduit === produit.idProduit && item.idEtablissement === etablissementId
      );
      
      if (existingProduct) {
        const nouvelleQuantite = existingProduct.quantite + 1;
        await MajQuantiteProduitPanier(
          userStore.user.userId,
          produit.idProduit,
          etablissementId,
          nouvelleQuantite
        );
      } else {
        await AjoutAuPanier(
          userStore.user.userId,
          produit.idProduit,
          etablissementId
        );
      }
    } else {
      await AjoutAuPanier(
        userStore.user.userId,
        produit.idProduit,
        etablissementId
      );
    }
  } catch (error) {
    console.error("Erreur lors de la mise à jour du panier dans la base de données:", error);
  }
};

const showSuccessMessage = (nomProduit) => {
  successMessage.value = `${nomProduit} ajouté au panier !`;
  setTimeout(() => (successMessage.value = ''), 3000);
};

const formatPrice = (price) => {
  return price.toFixed(2);
};

const isEtablissementOpen = (horaires) => {
  if (!horaires || horaires.length === 0) return false;
  const dateFilter = new Date(selectedDate.value);
  const jourSemaine = new Intl.DateTimeFormat('fr-FR', { weekday: 'long' }).format(dateFilter);
  const horairesDuJour = horaires.find(
    h => h.jourSemaine.toLowerCase() === jourSemaine.toLowerCase()
  );
  if (!horairesDuJour) return false;
  const extractHourMinutes = (isoString) => {
    const date = new Date(isoString);
    const hours = date.getUTCHours().toString().padStart(2, '0');
    const minutes = date.getUTCMinutes().toString().padStart(2, '0');
    return `${hours}:${minutes}`;
  };

  const heureOuverture = extractHourMinutes(horairesDuJour.heureDebut);
  const heureFermeture = extractHourMinutes(horairesDuJour.heureFin);

  const [heureActuelle, minuteActuelle] = selectedTime.value.split(':').map(Number);
  const toMinutes = (h, m) => h * 60 + m;
  const now = toMinutes(heureActuelle, minuteActuelle);
  const [openH, openM] = heureOuverture.split(':').map(Number);
  const [closeH, closeM] = heureFermeture.split(':').map(Number);
  const start = toMinutes(openH, openM);
  const end = toMinutes(closeH, closeM);

  return now >= start && now <= end;
};

onMounted(() => {
  document.title = `Uber Eats | ${route.params.nomVille}`;
  fetchData();
});
</script>

<style scoped>
.notification {
  position: fixed;
  top: 20px;
  right: 20px;
  background-color: rgb(43, 43, 43);
  color: white;
  padding: 15px 25px;
  border-radius: 10px;
  font-size: 18px;
  z-index: 1000;
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
  opacity: 1;
  transition: opacity 0.3s ease, transform 0.3s ease;
  transform: translateY(0);
}

.notification.hide {
  opacity: 0;
  transform: translateY(-20px);
}

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
