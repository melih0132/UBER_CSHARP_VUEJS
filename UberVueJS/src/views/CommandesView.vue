<template>
    <div class="container py-4">
        <h1 class="mb-3">Vos Commandes</h1>

        <div class="mb-4">
            <p v-if="carteId && typeof carteId === 'string'">
                Carte utilisée : **** {{ carteId.slice(-4) }}
            </p>
            <p v-else class="text-muted">
                Aucune carte sélectionnée.
            </p>
        </div>

        <div class="card mb-4">
            <div class="card-header">
                Historique des commandes
            </div>
            <div class="card-body">
                <ul class="list-group list-group-flush">
                    <li class="list-group-item">Commande n°2 - Nathan arrête</li>
                </ul>
            </div>
        </div>

        <button class="btn btn-primary btn-pay" @click="handleStripeCheckout">
            Payer maintenant
        </button>
    </div>
</template>

<script setup>
import { useRoute } from 'vue-router';
import { ref, onMounted } from 'vue';

const route = useRoute();
const carteId = ref(null);

onMounted(() => {
    const queryCarteId = route.query.carteId;
    if (queryCarteId && typeof queryCarteId === 'string') {
        carteId.value = queryCarteId;
    }
});

const handleStripeCheckout = async () => {
    if (!carteId.value) {
        alert("Veuillez sélectionner une carte avant de procéder au paiement.");
        return;
    }

    try {
        const response = await fetch('lien-api', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                amount: 2590,
                carteId: carteId.value,
            }),
        });

        const data = await response.json();

        if (data.url) {
            window.location.href = data.url;
        } else {
            console.error('Aucune URL Stripe retournée');
        }
    } catch (error) {
        console.error('Erreur lors de la redirection vers Stripe :', error);
    }
};

</script>

<style scoped>
.container {
    max-width: 800px;
    margin: 0 auto;
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