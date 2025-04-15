<template>
    <div class="container div-commande text-center py-4">
        <h2 class="pb-3">Détails de la commande</h2>
        <p v-if="livraison === 'livraison'">
            <b>Mode de livraison :</b> Livraison à domicile
        </p>
        <p v-else>
           <b>Mode de livraison :</b> Retrait sur place
        </p>
        <p v-if="livraison === 'livraison'">
            <b>Adresse de livraison :</b> {{ adresse }}, {{ ville }} {{ codePostal }}
        </p>
        <p>
            <b>Montant de la commande :</b> {{ montantPanier ? (montantPanier / 100).toFixed(2) : '0.00' }} €
        </p>
        <p v-if="livraison === 'livraison'">
            <b>Frais de livraison (3%) :</b> {{ prixLivraison ? (prixLivraison / 100).toFixed(2) : '0.00' }} €
        </p>
        <p v-if="livraison === 'livraison'">
            <b>Total :</b> {{ totalPrix ? (totalPrix / 100).toFixed(2) : '0.00' }} €
        </p>
        <p v-else>
            <b>Total :</b> {{ totalPrix ? (totalPrix / 100).toFixed(2) : '0.00' }} €
        </p>

        <button class="btn btn-primary btn-pay" @click="handleStripeCheckout">
            Payer maintenant
        </button>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { useUserStore } from '@/stores/userStore';
import { getClientById } from '@/services/clientService';

const route = useRoute();
const userStore = useUserStore();

const livraison = ref(null);
const adresse = ref('');
const ville = ref('');
const codePostal = ref('');
const montantPanier = ref(0);
const prixLivraison = ref(0);
const totalPrix = ref(0);

const fetchClientDetails = async () => {
    const { livraison: livraisonQuery, adresse: adresseQuery, ville: villeQuery, codePostal: codePostalQuery } = route.query;

    try {
        if (!userStore.isAuthenticated) {
            throw new Error('Utilisateur non authentifié');
        }

        const userId = userStore.user.userId;

        const clientData = await getClientById(userId);

        if (clientData && clientData.paniers && clientData.paniers.length > 0) {
            const panier = clientData.paniers[0];
            montantPanier.value = panier.prix * 100;
        }

        if (livraisonQuery === 'livraison') {
            livraison.value = 'livraison';
            adresse.value = adresseQuery || '';
            ville.value = villeQuery || '';
            codePostal.value = codePostalQuery || '';

            prixLivraison.value = montantPanier.value * 0.03;

            totalPrix.value = montantPanier.value + prixLivraison.value;
        } else {
            livraison.value = 'retrait';
            prixLivraison.value = 0;
            totalPrix.value = montantPanier.value;
        }
    } catch (error) {
        console.error("Erreur lors de la récupération des détails du client :", error);
    }
};

onMounted(() => {
    fetchClientDetails();
});

const handleStripeCheckout = () => {
    console.log("Processus de paiement lancé");
};
</script>

<style scoped>

.container {
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
}
.div-commande {
  background: white;
  border-radius: 16px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  margin-top: 24px;
  overflow: hidden;
}
.card {
    background: #fff;
    border-radius: 12px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.btn-pay {
    background-color: #6772e5;
    color: #fff;
    border: none;
    border-radius: 8px;
    padding: 10px 20px;
    font-weight: bold;
    font-size: 16px;
    cursor: pointer;
    transition: background-color 0.3s ease;
}

.btn-pay:hover {
    background-color: #5469d4;
}
</style>