<template>
  <div class="container">
    <h1 class="text-center">Votre Panier</h1>
    <div class="cart">
      <div v-if="panier.length > 0">

        <div v-for="(produit, index) in panier" :key="`${produit.idProduit}-${index}`" class="cart-item">
          <div class="item-info">
            <div class="item-img">
              <img :src="produit.imageProduit" :alt="produit.nomProduit" class="produit-img" />
            </div>
            <div class="item-details">
              <div class="item-name">{{ produit.nomProduit }}</div>
              <div class="item-price">{{ formatPrice(produit.prixProduit) }} € (x{{ produit.quantite }})</div>
            </div>
          </div>
          <div class="item-controls">
            <button @click="deleteOneProduct(produit)" class="btn-quantite">-</button>
            <input type="number" v-model.number="produit.quantite" @change="updateQuantity(produit)" min="1" max="99"
              class="quantity-input" />
            <button @click="addOneProduct(produit)" class="btn-quantite">+</button>
            <button type="button" @click="removeProduct(produit)" class="delete-btn">
              <svg viewBox="0 0 24 24">
                <path
                  d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
              </svg>
            </button>
          </div>
        </div>

        <div class="cart-summary">
          <div class="summary-content">
            <div class="total">
              Total : <span>{{ formatPrice(totalPrix) }} €</span>
            </div>
            <div class="actions">
              <button @click="clearCart" class="btn-panier">Vider le panier</button>
              <button v-if="!userStore.isAuthenticated" @click="checkout" class="btn-panier">Passer la commande</button>
              <router-link v-else to="/commande/panier/choix-livraison" class="btn-panier text-decoration-none">Passer
                la commande</router-link>
            </div>
          </div>
        </div>

      </div>
      <div v-else class="empty-cart">
        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
            d="M16 11V7a4 4 0 00-8 0v4M5 9h14l1 12H4L5 9z" />
        </svg>
        <p>Votre panier est vide</p>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue'
import { useUserStore } from '@/stores/userStore'
import { useRouter } from 'vue-router'
import {
  AjoutAuPanier,
  GetPanierById,
  MajQuantiteProduitPanier,
  SupprimerProduitDuPanier
} from '@/services/panierService'

const userStore = useUserStore()
const router = useRouter()

const panier = ref([])
const totalPrix = ref(0)

const fetchPanierFromAPI = async () => {
  if (!userStore.isAuthenticated) return

  try {
    const panierData = await GetPanierById(userStore.user.userId)

    if (panierData && panierData.contient2s) {
      panier.value = panierData.contient2s.map(item => ({
        idProduit: item.idProduit,
        idEtablissement: item.idEtablissement,
        nomProduit: item.idProduitNavigation.nomProduit,
        prixProduit: item.idProduitNavigation.prixProduit,
        imageProduit: item.idProduitNavigation.imageProduit,
        quantite: item.quantite
      }))
      calculateTotal()
    }
  } catch (error) {
    console.error("Erreur lors de la récupération du panier", error)
  }
}

const syncLocalCartWithAPI = async () => {
  if (!userStore.isAuthenticated || panier.value.length === 0) return

  try {
    for (const produit of panier.value) {
      await AjoutAuPanier(
        userStore.user.userId,
        produit.idProduit,
        produit.idEtablissement
      )

      if (produit.quantite > 1) {
        await MajQuantiteProduitPanier(
          userStore.user.userId,
          produit.idProduit,
          produit.idEtablissement,
          produit.quantite
        )
      }
    }

    await fetchPanierFromAPI()

    localStorage.removeItem('panier')
  } catch (error) {
    console.error("Erreur lors de la synchronisation du panier", error)
  }
}

const calculateTotal = () => {
  totalPrix.value = panier.value.reduce((total, produit) =>
    total + (produit.prixProduit * produit.quantite), 0)
}

const updateQuantity = async (produit) => {
  produit.quantite = parseInt(produit.quantite) || 1
  if (produit.quantite < 1) produit.quantite = 1
  if (produit.quantite > 99) produit.quantite = 99

  if (userStore.isAuthenticated) {
    try {
      await MajQuantiteProduitPanier(
        userStore.user.userId,
        produit.idProduit,
        produit.idEtablissement,
        produit.quantite
      )
      await fetchPanierFromAPI()
    } catch (error) {
      console.error("Échec de la mise à jour", error)
    }
  } else {
    calculateTotal()
    localStorage.setItem('panier', JSON.stringify(panier.value))
  }
}

const deleteOneProduct = async (produit) => {
  if (produit.quantite <= 1) return

  if (userStore.isAuthenticated) {
    try {
      await MajQuantiteProduitPanier(
        userStore.user.userId,
        produit.idProduit,
        produit.idEtablissement,
        produit.quantite - 1
      )
      await fetchPanierFromAPI()
    } catch (error) {
      console.error("Échec de la mise à jour", error)
    }
  } else {
    produit.quantite -= 1
    calculateTotal()
    localStorage.setItem('panier', JSON.stringify(panier.value))
  }
}

const addOneProduct = async (produit) => {
  if (produit.quantite >= 99) return

  if (userStore.isAuthenticated) {
    try {
      await MajQuantiteProduitPanier(
        userStore.user.userId,
        produit.idProduit,
        produit.idEtablissement,
        produit.quantite + 1
      )
      await fetchPanierFromAPI()
    } catch (error) {
      console.error("Échec de la mise à jour", error)
    }
  } else {
    produit.quantite += 1
    calculateTotal()
    localStorage.setItem('panier', JSON.stringify(panier.value))
  }
}

const removeProduct = async (produit) => {
  if (userStore.isAuthenticated) {
    try {
      const response = await SupprimerProduitDuPanier(
        userStore.user.userId,
        produit.idProduit,
        produit.idEtablissement
      )
      console.log('Produit supprimé avec succès:', response)
      await fetchPanierFromAPI()
    } catch (error) {
      console.error("Échec de la suppression du produit", error)
    }
  } else {
    panier.value = panier.value.filter(p => p.idProduit !== produit.idProduit)
    localStorage.setItem("panier", JSON.stringify(panier.value))
    calculateTotal()
  }
}

const clearCart = async () => {
  if (userStore.isAuthenticated && panier.value.length > 0) {
    try {
      const produitsCopy = [...panier.value]

      for (const produit of produitsCopy) {
        console.log(`Suppression du produit ${produit.idProduit} de l'établissement ${produit.idEtablissement}`)
        await SupprimerProduitDuPanier(
          userStore.user.userId,
          produit.idProduit,
          produit.idEtablissement
        )
      }

      await fetchPanierFromAPI()
    } catch (error) {
      console.error("Échec du vidage du panier", error)
    }
  } else {
    panier.value = []
    localStorage.setItem("panier", JSON.stringify(panier.value))
    calculateTotal()
  }
}

const formatPrice = (price) => {
  return parseFloat(price).toFixed(2)
}

const checkout = () => {
  if (!userStore.isAuthenticated) {
    router.push({ name: 'Login' })
  } else {
    router.push('/commande/panier/choix-livraison')
  }
}

watch(() => userStore.isAuthenticated, async (newValue, oldValue) => {
  if (newValue && !oldValue) {
    await syncLocalCartWithAPI()
  }
})

onMounted(async () => {
  if (userStore.isAuthenticated) {
    await fetchPanierFromAPI()
  } else {
    try {
      const storedCart = localStorage.getItem('panier')
      panier.value = storedCart ? JSON.parse(storedCart) : []
      calculateTotal()
    } catch (error) {
      console.error("Erreur lors du chargement du panier local", error)
      panier.value = []
    }
  }
  const link = document.querySelector("link[rel='icon']")
  if (link) {
    link.href = '/public/images/UberEatsPetit.png'
  }
  document.title = "Mon panier"
})
</script>

<style scoped>
.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 20px;
}

label {
  margin-top: 0.5rem;
}

.cart {
  background: white;
  border-radius: 16px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  margin-top: 24px;
  overflow: hidden;
}

.cart-item {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 20px;
  border-bottom: 1px solid #f0f0f0;
}

.item-info {
  flex: 1;
}

.item-img {
  flex: 0 0 80px;
  height: 80px;
  margin-inline-end: 16px;
  border-radius: 8px;
  overflow: hidden;
  display: flex;
  align-items: center;
  justify-content: center;
  max-width: 10rem;
  max-height: 10rem;
}

.item-img img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.item-name {
  font-weight: 500;
  margin-bottom: 4px;
}

.item-price {
  color: #666;
  font-size: 0.9em;
  font-weight: bold;
}

.item-controls {
  display: flex;
  align-items: center;
  gap: 20px;
}

.quantity-form {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.quantity-input {
  padding: 8px;
  border: 1px solid #e0e0e0;
  border-radius: 8px;
  font-size: 14px;
  width: 80px;
  text-align: center;
  background-color: white;
  cursor: text;
}

.quantity-input:focus {
  outline: none;
  border-color: black;
  box-shadow: 0 0 0 2px rgba(0, 0, 0, 0.1);
}

.quantity-input::-webkit-inner-spin-button,
.quantity-input::-webkit-outer-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

.delete-form {
  display: flex;
  justify-content: center;
  align-items: center;
  text-align: center;
}

.delete-btn {
  background: none;
  border: none;
  cursor: pointer;
  padding: 6px 8px 8px 8px;
  border-radius: 12px;
  transition: all 0.2s;
}

.delete-btn:hover {
  background-color: rgb(247, 247, 247);
}

.delete-btn svg {
  width: 20px;
  height: 20px;
  fill: none;
  stroke: rgb(0, 0, 0);
  stroke-width: 2;
}

.cart-summary {
  bottom: 0;
  left: 0;
  right: 0;
  background: white;
  padding: 20px;
  backdrop-filter: blur(8px);
}

.summary-content {
  max-width: 1200px;
  margin: 0 auto;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.total {
  font-size: 1.2em;
  font-weight: 600;
}

.actions {
  display: flex;
  gap: 16px;
}

.btn-panier {
  font-size: 14px;
  font-weight: 600;
  background: #000;
  color: #fff;
  border-radius: 8px;
  padding: 0.5rem 1rem;
  border: none;
  transition: background-color 0.3s ease;
}

.btn-panier:hover {
  background: #282828;
  color: #fff;
}

.empty-cart {
  text-align: center;
  padding: 48px 20px;
  color: #666;
}

.empty-cart svg {
  width: 64px;
  height: 64px;
  margin-bottom: 16px;
  color: #ccc;
}

.btn-quantite {
  color: white;
  background-color: black;
  border-radius: 5px;
  width: 30px;
  text-align: center;
  font-weight: bold;
}
</style>