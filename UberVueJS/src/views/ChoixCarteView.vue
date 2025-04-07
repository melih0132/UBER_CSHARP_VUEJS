<template>
    <div class="container" style="min-height: 100vh; padding: 2rem;">
        <div class="container">
            <h1>Choisissez une carte bancaire</h1>

            <div v-if="cartesBancaires.length > 0">
                <div class="card-selection">
                    <div v-for="(carte, index) in cartesBancaires" :key="carte.idCb" class="col-md-6 mb-1">
                        <label>
                            <input type="radio" name="carte_id" :value="carte.idCb" v-model="selectedCarteId" />
                            <span>
                                **** **** **** {{ getLastDigits(carte.numeroCb) }} - Exp.
                                {{ formatDate(carte.dateExpireCb) }}
                            </span>
                        </label>
                    </div>
                </div>
            </div>

            <div v-else>
                <p>Aucune carte bancaire enregistrée.</p>
            </div>

            <router-link :to="{ name: 'Commandes', query: { carteId: selectedCarteId } }"
                class="btn-panier text-decoration-none ms-2">
                Utiliser cette carte
            </router-link>
            <router-link to="/myaccount/carte-bancaire" class="btn-panier text-decoration-none ms-2">
                Ajouter une nouvelle carte bancaire
            </router-link>
        </div>
    </div>
</template>

<script>
import { useUserStore } from '@/stores/userStore';
import { getCartesByClientId } from '@/services/carteBancaireService';
import { storeToRefs } from 'pinia';
import { ref, onMounted } from 'vue';

export default {
    setup() {
        const userStore = useUserStore();
        const { user } = storeToRefs(userStore);

        const cartesBancaires = ref([]);
        const selectedCarteId = ref(null);

        const fetchCartesBancaires = async () => {
            try {
                const cartes = await getCartesByClientId(user.value.userId);
                cartesBancaires.value = cartes;
                console.table(cartesBancaires.value);
            } catch (error) {
                console.error('Erreur lors de la récupération des cartes bancaires :', error);
            }
        };

        const formatDate = (date) => {
            const d = new Date(date);
            return `${String(d.getMonth() + 1).padStart(2, '0')}/${d.getFullYear()}`;
        };

        const getLastDigits = (numeroCb) => {
            return numeroCb.slice(-4);
        };

        onMounted(fetchCartesBancaires);

        return {
            cartesBancaires,
            selectedCarteId,
            formatDate,
            getLastDigits
        };
    }
};
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

    .actions {
        flex-direction: column;
        width: 100%;
    }

    .btn {
        width: 100%;
        text-align: center;
    }

    .summary-content {
        flex-direction: column;
        gap: 16px;
    }
}

/* CHOIX LIVRAISON  */
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

/* CHOIX CB */
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


/* POST COMMANDE */
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

.list-group {
    list-style: none;
    padding: 0;
}

.list-group-item {
    padding: 10px;
    border-bottom: 1px solid #eee;
}

.list-group-item:last-child {
    border-bottom: none;
}

.table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 10px;
}

.table th,
.table td {
    text-align: center;
    padding: 10px;
    border-bottom: 1px solid #eee;
}

.table th {
    font-size: 0.9rem;
    color: #666;
}

.table td {
    font-size: 0.85rem;
    color: #333;
}

.text-muted {
    color: #999;
}

.alert {
    padding: 10px;
    margin-bottom: 15px;
    border-radius: 5px;
    font-size: 0.9rem;
}

.form-inline {
    display: flex;
    gap: 1rem;
    align-items: center;

}

.form-inline .form-group {
    flex: 0.5;
}


.new-card-form {
    display: block;
}
</style>