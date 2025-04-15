<template>
    <div class="container" style="min-height: 100vh; padding: 2rem;">
        <div class="container">
            <h1>Choisissez votre mode de livraison</h1>
            <form @submit.prevent="submitForm">
                <div class="radio-group mb-4">
                    <label>
                        <input type="radio" name="modeLivraison" value="livraison" v-model="selectedMode" required />
                        Livraison à domicile
                    </label>
                    <label>
                        <input type="radio" name="modeLivraison" value="retrait" v-model="selectedMode" />
                        Retrait sur place
                    </label>
                </div>

                <div v-if="isDelivery" id="adresseLivraisonContainer" class="my-3">
                    <label for="adresse_livraison"><b>Adresse de livraison :</b></label>
                    <input type="text" id="adresse_livraison" class="form-control" placeholder="Entrez votre adresse"
                        v-model="adresseLivraison" :required="isDelivery" />

                    <label for="ville"><b>Ville :</b></label>
                    <input type="text" id="ville" class="form-control" placeholder="Entrez votre ville" v-model="ville"
                        :required="isDelivery" />

                    <label for="code_postal"><b>Code Postal :</b></label>
                    <input type="text" id="code_postal" class="form-control" placeholder="Entrez votre code postal"
                        v-model="codePostal" :required="isDelivery" />
                </div>

                <button type="submit" class="btn-panier">Continuer</button>
            </form>
        </div>
    </div>
</template>

<script setup>
import { ref, computed } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();

const selectedMode = ref('retrait');
const adresseLivraison = ref('');
const ville = ref('');
const codePostal = ref('');

const isDelivery = computed(() => selectedMode.value === 'livraison');

const submitForm = () => {
    router.push({
        name: 'Choix-carte',
        query: {
            livraison: selectedMode.value,
            adresse: adresseLivraison.value,
            ville: ville.value,
            codePostal: codePostal.value,
        },
    });
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
    margin-bottom: 5px;
    font-weight: 400;
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
}

.item-controls {
    display: flex;
    align-items: center;
    gap: 20px;
}

.form-control {
    display: block;
    width: 100%;
    height: calc(1.5em + 2px + 0.75rem);
    font-size: 1rem;
    font-weight: 400;
    line-height: 1.5;
    color: rgb(73, 80, 87);
    background-color: rgb(255, 255, 255);
    background-clip: padding-box;
    padding: 0.375rem 0.75rem;
    border-width: 1px;
    border-style: solid;
    border-color: rgb(206, 212, 218);
    border-image: initial;
    border-radius: 0.25rem;
    transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
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