<template>
    <div class="container" style="min-height: 100vh; padding: 2rem;">
        <div class="container">
            <h1>Choisissez une carte bancaire</h1>

            <div v-if="cartesBancaires.length > 0" class="card-selection row">
                <div v-for="(carte, index) in cartesBancaires" :key="carte.idCb" class="col-12 col-md-6 mb-3">
                    <label>
                        <input type="radio" name="carte_id" :value="carte.idCb" v-model="selectedCarteId"/>
                        <span class="fs-6">
                            **** **** **** {{ carte.numeroCb.slice(-4) }} — Exp. {{ formatDate(carte.dateExpireCb) }}
                        </span>
                    </label>
                </div>
            </div>

            <div v-else>
                <p>Aucune carte bancaire enregistrée.</p>
            </div>

            <button v-if="cartesBancaires.length > 0" class="btn-panier text-decoration-none me-2"
                @click="goToCommande">
                Utiliser cette carte
            </button>
            <router-link to="/myaccount/carte-bancaire" class="btn-panier text-decoration-none">
                Ajouter une nouvelle carte bancaire
            </router-link>
        </div>
    </div>
</template>

<script setup>
import { useRoute, useRouter } from 'vue-router';
import { ref, onMounted } from 'vue';
import { useUserStore } from '@/stores/userStore';
import { getCartesByClientId } from '@/services/carteBancaireService';
import { storeToRefs } from 'pinia';

const router = useRouter();
const route = useRoute();

const { user } = storeToRefs(useUserStore());
const cartesBancaires = ref([]);
const selectedCarteId = ref(null);

const formatDate = (date) => {
    const d = new Date(date);
    const month = String(d.getMonth() + 1).padStart(2, '0');
    const year = String(d.getFullYear());
    return `${month}/${year}`;
}

const fetchCartesBancaires = async () => {
    try {
        const cartes = await getCartesByClientId(user.value.userId);
        cartesBancaires.value = cartes;
    } catch (error) {
        console.error('Erreur chargement cartes :', error);
    }
};

const goToCommande = () => {
    if (!selectedCarteId.value) {
        alert("Veuillez sélectionner une carte.");
        return;
    }

    router.push({
        name: 'Commandes',
        query: {
            ...route.query,
            carteId: selectedCarteId.value,
        },
    });
};

onMounted(fetchCartesBancaires);
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

.card-selection {
    display: flex;
    flex-direction: column;
    gap: 15px;
    margin-bottom: 20px;
}

.card-selection label {
    display: flex;
    align-items: center;
    gap: 15px;
    font-size: 1rem;
    color: #555;
    cursor: pointer;
    padding: 10px 15px;
    border: 1px solid #ddd;
    border-radius: 8px;
    background: #f9f9f9;
    transition: background-color 0.2s ease-in-out;
}

.card-selection label:hover {
    background: #f1f1f1;
}

.card-selection input[type="radio"] {
    accent-color: #28a745;
    transform: scale(1.2);
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

@media (max-width: 768px) {
    .container {
        padding: 16px;
    }

    .cart-item {
        flex-direction: column;
        align-items: flex-start;
        gap: 16px;
    }

    .item-controls {
        width: 100%;
        justify-content: space-between;
    }

    .summary-content {
        flex-direction: column;
        gap: 16px;
    }
}

.form-group {
    margin-bottom: 20px;
}

.radio-group {
    display: flex;
    flex-direction: column;
    gap: 5px;
    margin: 15px 0;
}

.radio-group label {
    display: flex;
    align-items: center;
    font-size: 1rem;
    color: #555;
    cursor: pointer;
    transition: background-color 0.2s ease-in-out;
    padding: 10px 15px;
    border: 1px solid #ddd;
    border-radius: 8px;
    background: #f9f9f9;
}

.radio-group input {
    margin-right: 15px;
    accent-color: #28a745;
}

.radio-group label:hover {
    background: #f1f1f1;
}

.card-selection {
    display: flex;
    flex-direction: column;
    gap: 15px;
    margin-bottom: 20px;
}

.card-selection label {
    display: flex;
    align-items: center;
    gap: 15px;
    font-size: 1rem;
    color: #555;
    cursor: pointer;
    padding: 10px 15px;
    border: 1px solid #ddd;
    border-radius: 8px;
    background: #f9f9f9;
    transition: background-color 0.2s ease-in-out;
}

.card-selection label:hover {
    background: #f1f1f1;
}

.card-selection input[type="radio"] {
    accent-color: #28a745;
}

.card {
    background: #fff;
    border: none;
    border-radius: 10px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    margin-bottom: 20px;
}

.card-header {
    font-weight: bold;
    font-size: 1.2rem;
    padding: 15px;
    border-bottom: 1px solid #eee;
}

.card-body {
    padding: 15px;
}

.alert {
    padding: 10px;
    margin-bottom: 15px;
    border-radius: 5px;
    font-size: 0.9rem;
}
</style>