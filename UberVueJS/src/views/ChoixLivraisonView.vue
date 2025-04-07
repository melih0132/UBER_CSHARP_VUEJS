<template>
    <div class="container" style="min-height: 100vh; padding: 2rem;">
        <div class="container">
            <h1>Choisissez votre mode de livraison</h1>
            <form @submit.prevent="submitForm">
                <div class="radio-group mb-4">
                    <label>
                        <input type="radio" name="modeLivraison" value="livraison" v-model="selectedMode"
                            required>Livraison à domicile
                    </label>
                    <label>
                        <input type="radio" name="modeLivraison" value="retrait" v-model="selectedMode">Retrait sur place
                    </label>
                </div>
                <div v-if="isDelivery" id="adresseLivraisonContainer" class="my-3">
                    <label for="adresse_livraison"><b>Adresse de livraison :</b></label>
                    <input type="text" id="adresse_livraison" name="adresse_livraison" class="form-control"
                        placeholder="Entrez votre adresse" :required="isDelivery">
                    <label for="ville"><b>Ville :</b></label>
                    <input type="text" id="ville" name="ville" class="form-control" placeholder="Entrez votre ville"
                         :required="isDelivery">

                    <label for="code_postal"><b>Code Postal :</b></label>
                    <input type="text" id="code_postal" name="code_postal" class="form-control"
                        placeholder="Entrez votre code postal" :required="isDelivery">
                </div>

                <button type="submit" class="btn-panier">Continuer</button>
            </form>
        </div>
    </div>

</template>

<script>
export default {
    data() {
        return {
            selectedMode: 'retrait', // Default mode
            adresseLivraison: '',
            ville: '',
            codePostal: '',
        };
    },
    computed: {
        isDelivery() {
            return this.selectedMode === 'livraison';
        },
    },
    watch: {
        selectedMode() {
            // Automatically updates when the selected mode changes
        },
    },
    methods: {
        submitForm() {
            this.$router.push('/commande/panier/choix-carte');
        },
    },
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