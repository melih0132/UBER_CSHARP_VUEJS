<template>
    <div class="container">
      <h1 class="text-center">Votre Panier</h1>
      
      <div v-if="panier.length > 0">
        <div class="cart">
          <!-- Affichage des produits du panier -->
          <div v-for="(produit, index) in panier" :key="produit.idProduit" class="cart-item">
            <div class="item-info">
              <div class="item-img">
                <img :src="produit.imageProduit" :alt="produit.nomProduit" class="produit-img" />
              </div>
              <div class="item-details">
                <div class="item-name">{{ produit.nomProduit }}</div>
                <div class="item-price">{{ produit.prixProduit }} €</div>
                <div class="item-quantity">
                  <label for="quantity">Quantité: </label>&nbsp;&nbsp;
                  <button @click="deleteOneProduct(produit)" class="but-qauntite" >-</button>
                  <input type="number" v-model="produit.quantite" min="1" @change="updateQuantity(produit)" />&nbsp;&nbsp;
                  <button @click="AddOneProduct(produit)" class="but-qauntite" >+</button>
                </div>
              </div>
            </div>
            <div class="item-controls">
              <button @click="removeProduct(produit.idProduit)" class="remove-btn">Supprimer</button>
              
            </div>
          </div>
  
          <!-- Résumé du panier -->
          <div class="cart-summary">
            <div class="total">
              Total : <span>{{ formatPrice(totalPrix) }} €</span>
            </div>
            <div class="actions">
              <button @click="clearCart" class="btn-clear">Vider le panier</button>
              <button @click="checkout" class="btn-checkout">Passer la commande</button>
            </div>
          </div>
        </div>
      </div>
  
      <!-- Si le panier est vide -->
      <div v-else class="empty-cart">
        <svg viewBox="0 0 24 24" fill="none" stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M16 11V7a4 4 0 00-8 0v4M5 9h14l1 12H4L5 9z" />
        </svg>
        <p>Votre panier est vide</p>
      </div>
    </div>
  </template>
  
  <script>
  export default {
  data() {
    return {
      panier: [], // Tableau des produits dans le panier
      totalPrix: 0, // Total du panier
    };
  },

  mounted() {
    // Récupérer les produits du panier au moment où le composant est monté
    const storedCart = JSON.parse(localStorage.getItem('panier')) || [];
    this.panier = storedCart;
    this.calculateTotal();
  },

  methods: {
    // Mettre à jour la quantité d'un produit
    updateQuantity(produit) {
      produit.quantite = Math.max(1, produit.quantite); // S'assurer que la quantité est au moins 1
      this.calculateTotal();
    },

    deleteOneProduct(produit){
      produit.quantite -= 1;
      this.calculateTotal();
    },

    AddOneProduct(produit){
      produit.quantite += 1;
      this.calculateTotal();
    },

    // Supprimer un produit du panier
    removeProduct(idProduit) {
      this.panier = this.panier.filter(p => p.idProduit !== idProduit);
      localStorage.setItem("panier", JSON.stringify(this.panier)); // Sauvegarder les modifications dans le localStorage
      this.calculateTotal();
    },

    // Vider le panier
    clearCart() {
      this.panier = [];
      localStorage.setItem("panier", JSON.stringify(this.panier)); // Sauvegarder les modifications dans le localStorage
      this.calculateTotal();
    },

    // Calculer le total du panier
    calculateTotal() {
      this.totalPrix = this.panier.reduce((total, produit) => {
        return total + (produit.prixProduit * produit.quantite);
      }, 0);
    },

    // Formater le prix
    formatPrice(price) {
      return price.toFixed(2); // Formater le prix avec 2 décimales
    },

    // Passer à la page de paiement
    checkout() {
      this.$router.push({ name: 'checkout' }); // Assurez-vous d'avoir une route de commande configurée
    }
  }
};

  </script>
  
  <style scoped>
  .container {
    padding: 2rem;
  }
  
  .cart {
    margin-top: 2rem;
  }
  
  .cart-item {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 1rem;
    padding: 1rem;
    border: 1px solid #ccc;
    border-radius: 8px;
  }
  
  .item-info {
    display: flex;
    align-items: center;
  }
  
  .item-img {
    width: 60px;
    height: 60px;
    margin-right: 1rem;
  }
  
  .produit-img {
    width: 100%;
    height: 100%;
    object-fit: cover;
  }
  
  .item-details {
    display: flex;
    flex-direction: column;
  }
  
  .item-name {
    font-weight: bold;
  }
  
  .item-price {
    color: #888;
  }
  
  .item-quantity {
    display: flex;
    align-items: center;
  }
  
  .item-quantity input {
    width: 50px;
    margin-left: 0.5rem;
    padding: 0.3rem;
  }
  
  .item-controls {
    display: flex;
    align-items: center;
  }
  
  .remove-btn {
    background-color: black;
    color: white;
    padding: 0.5rem;
    border: none;
    border-radius: 4px;
    cursor: pointer;

  }
  
  .remove-btn:hover {
    background-color: black;
  }
  
  .cart-summary {
    margin-top: 2rem;
    padding: 1rem;
    border: 1px solid #ccc;
    border-radius: 8px;
  }
  
  .total {
    font-weight: bold;
  }
  
  .actions {
    display: flex;
    justify-content: space-between;
    margin-top: 1rem;
  }
  
  .btn-clear,
  .btn-checkout {
    padding: 0.7rem 1.5rem;
    border: none;
    border-radius: 4px;
    cursor: pointer;
  }
  
  .btn-clear {
    background-color: black;
    color: white;
  }
  
  .btn-checkout {
    background-color: black;
    color: white;
  }
  
  .btn-clear:hover {
    background-color: black;
  }
  
  .btn-checkout:hover {
    background-color: black;
  }
  
  .empty-cart {
    text-align: center;
    margin-top: 2rem;
    font-size: 1.2rem;
    color: #888;
  }
  
  .empty-cart svg {
    width: 40px;
    height: 40px;
    margin-bottom: 1rem;
  }

  .but-qauntite{
    background-color: black;
    border-radius: 5px;
    color: white;
  }
  </style>
  