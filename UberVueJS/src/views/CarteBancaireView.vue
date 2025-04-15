<template>
    <div class="container my-5">
        <div class="account mt-5">


            <div class="row">
                <div class="col-md-3">
                    <ul id="sidebar-menu" class="list-group shadow-sm">
                        <router-link class="list-group-item" to="/myaccount">
                            <i class="fa-solid fa-arrow-left-long"></i>Revenir sur le compte
                        </router-link>
                        <ul class="list-group">
                            <li class="list-group-item active" @click="setActiveContent('content-cartebancaire')">
                                <i class="fas fa-credit-card me-2"></i> Carte Bancaire
                            </li>
                        </ul>
                    </ul>
                </div>


                <div class="col-md-9">
                    <div class="p-4">
                        <div id="content-addcartebancaire"
                            :class="{ 'content-section': true, 'active': activeContent === 'content-addcartebancaire' }">
                            <h1>Ajouter une Carte Bancaire</h1>
                            <div class="mt-3">
                                <div class="mb-3">
                                    <label>Numéro de la carte</label>
                                    <input placeholder="1234 5678 9012 3456" maxlength="19" 
                                    v-model="formattedNumeroCarte" inputmode="numeric" class="form-control">
                                </div>
                                <div class="mb-3">
                                    <label class="date-expiration-add">Date d'expiration</label>
                                    <input type="month" class="form-control" ref="dateExpiration">
                                    <small>Format mm-aaaa</small>
                                </div>
                                <div class="mb-3">
                                    <label>Cryptogramme (3 chiffres)</label>
                                    <input placeholder="123" maxlength="3" required pattern="\d{3}" type="number"
                                        class="form-control">
                                </div>
                                <div class="mb-3">
                                    <label>Type de carte</label>
                                    <select id="typecarte" name="typecarte" class="form-select " required=""
                                        ref="typeCarte">
                                        <option value="" disabled="" selected="">Choisissez le type</option>
                                        <option value="Crédit">Crédit</option>
                                        <option value="Débit">Débit</option>
                                    </select>

                                </div>

                                <div class="mb-889">
                                    <button class="btn-compte text-decoration-none px-4 py-2"
                                        @click="AjouterCarte()">Ajouter la carte</button>
                                    <button class="btn-compte text-decoration-none px-4 py-2 ms-2">Annuler</button>
                                </div>
                            </div>
                        </div>

                        <div id="content-cartebancaire"
                            :class="{ 'content-section': true, 'active': activeContent === 'content-cartebancaire' }">
                            <h1>Mes cartes bancaires</h1>

                            <!-- Si l'utilisateur a des cartes bancaires -->
                            <div v-if="cartesBancaires.length > 0">
                                <div class="row">
                                    <div v-for="(carte, index) in cartesBancaires" :key="index" class="col-md-6 mb-4">
                                        <div class="card shadow-sm border-0">
                                            <div class="card-body">
                                                <h5 class="card-title" style="font-size: 1rem; font-weight: bold;">
                                                    Carte se terminant par <span class="text-dark">**** {{
                                                        carte.numeroCb.slice(-4) }}</span>
                                                </h5>
                                                <p class="card-text text-muted mb-2" style="font-size: 0.9rem;">
                                                    Expiration : {{ carte.dateExpireCb }}
                                                </p>
                                                <p class="card-text">
                                                    <span class="badge bg-light text-dark px-2 py-1"
                                                        style="font-size: 0.85rem;">
                                                        {{ carte.typeCarte }}
                                                    </span>
                                                    <span class="badge bg-dark text-white px-2 py-1"
                                                        style="font-size: 0.85rem;">
                                                        {{ carte.typeReseaux }}
                                                    </span>
                                                </p>
                                                <button @click="supprimerCarte(carte.idCb)" class="btn sup-icon"
                                                    title="Supprimer cette carte">
                                                    <i class="fas fa-trash-alt fa-lg"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Si l'utilisateur n'a pas encore de carte bancaire -->
                            <div v-else>
                                <p class="text-center">Aucune carte bancaire enregistrée.</p>
                            </div>

                            <div class="text-center mt-5">
                                <a class="btn-compte text-decoration-none px-4 py-2"
                                    @click="setActiveContent('content-addcartebancaire')">Ajouter une carte bancaire</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { useUserStore } from '@/stores/userStore';
import { storeToRefs } from 'pinia';
import { ref, onMounted, computed } from 'vue';
import { getClientById } from '@/services/clientService';
import { addCarteBancaire, deleteCarteBancaire } from '@/services/carteBancaireService';


const userStore = useUserStore();
const { user } = storeToRefs(userStore);

const activeContent = ref('content-cartebancaire');
const showTitle = ref(true);
const cartesBancaires = ref([]);
const rawNumeroCarte = ref('');

onMounted(async () => {
    const clientId = user.value.userId;
    if (clientId) {
        const clientData = await getClientById(clientId);
        cartesBancaires.value = clientData.idCbs || [];
    }
});

const setActiveContent = (contentId) => {
    activeContent.value = contentId;
    showTitle.value = contentId === 'content-cartebancaire';
};

const formattedNumeroCarte = computed({
    get() {
        return rawNumeroCarte.value.replace(/(.{4})/g, '$1 ').trim();
    },
    set(val) {
        rawNumeroCarte.value = val.replace(/\D/g, '').slice(0, 16); // garde seulement les chiffres
    }
});

const supprimerCarte = async (carteId) => {
    await deleteCarteBancaire(carteId);
    const clientData = await getClientById(userStore.user.userId);
    cartesBancaires.value = clientData.idCbs || [];
};

async function AjouterCarte() {
    const numeroCarte = rawNumeroCarte.value;
    const dateExpiration = document.querySelector('input[type="month"]').value;
    const cryptogramme = document.querySelector('input[type="number"][placeholder="123"]').value;
    const typeCarte = document.querySelector('select[name="typecarte"]').value;
    console.log(numeroCarte,dateExpiration,cryptogramme,typeCarte)

    if (!numeroCarte || !dateExpiration || !cryptogramme || !typeCarte) {
        alert("Veuillez remplir tous les champs");
        return;
    }

    try {
        const typeReseaux = determinerTypeReseau(numeroCarte);
        const [year, month] = dateExpiration.split('-');
        const lastDay = new Date(parseInt(year), parseInt(month), 0).getDate();
        const formattedDate = `${year}-${month}-${String(lastDay).padStart(2, '0')}`;

        const carteData = {
            userId: userStore.user.userId,
            numeroCb: numeroCarte,
            dateExpireCb: formattedDate,
            cryptogramme: cryptogramme,
            typeCarte: typeCarte,
            typeReseaux: typeReseaux
        };

        await addCarteBancaire(carteData);

        const clientData = await getClientById(userStore.user.userId);
        cartesBancaires.value = clientData.idCbs || [];

        setActiveContent('content-cartebancaire');

        alert("Carte ajoutée avec succès");
    } catch (error) {
        console.error("Erreur lors de l'ajout de la carte :", error);
        alert("Erreur lors de l'ajout de la carte");
    }
}

function determinerTypeReseau(numeroCarte) {
    const premier = numeroCarte.charAt(0);
    const deuxPremiers = numeroCarte.substring(0, 2);
    const quatrePremiers = numeroCarte.substring(0, 4);

    if (premier === '4') {
        return 'Visa';
    } else if ((deuxPremiers >= '51' && deuxPremiers <= '55') || (quatrePremiers >= '2221' && quatrePremiers <= '2720')) {
        return 'MasterCard';
    } else if (deuxPremiers === '34' || deuxPremiers === '37') {
        return 'American Express';
    } else if (deuxPremiers === '30' || deuxPremiers === '36' || deuxPremiers === '38') {
        return 'Diners Club';
    } else if (deuxPremiers === '62' || deuxPremiers === '81') {
        return 'UnionPay';
    } else {
        return 'Autre';
    }
};

</script>

<style scoped>
body {
    background-color: #fff;
    color: #000;
    margin: 0;
    padding: 0;
    line-height: 1.6;
}

.content-section {
    display: none;
}

.content-section.active {
    display: block;
}

.div-cb,
.account,
.form-login,
.form-register,
.card {
    max-width: 1200px;
    margin: auto;
    background-color: #fff;
    border-radius: 10px;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
    padding: 20px;
}

h1,
h2,
h5 {
    color: #000;
    margin-bottom: 20px;
}

h1 {
    font-size: 2rem;
    font-weight: 700;
    text-align: center;
}

h2 {
    font-size: 1.5rem;
    font-weight: 600;
}

h5 {
    font-size: 1.1rem;
    font-weight: bold;
}

.list-group {
    cursor: pointer;
    border-radius: 10px;
    overflow: hidden;
    background-color: #333;
}

.list-group-item {
    font-size: 1rem;
    padding: 15px 20px;
    background-color: #444;
    color: #fff;
    border: none;
    transition: background-color 0.3s ease, color 0.3s ease;
    display: flex;
    align-items: center;
    gap: 10px;
}

.list-group-item a {
    color: #fff;

    gap: 10px;
}

.list-group-item:hover {
    background-color: #555;
    box-shadow: inset 0 0 10px rgba(0, 0, 0, 0.3);
}

.list-group-item.active {
    background-color: #222;
    box-shadow: inset 0 0 10px rgba(0, 0, 0, 0.5);
}

.list-group-item i {
    font-size: 1.2rem;
}

.list-item-flex {
    font-size: 1rem;
    padding: 15px 20px;
    background-color: #444;
    color: #fff;
    border: none;
    border-radius: 10px;
    display: flex;
    align-items: center;
    gap: 10px;
    transition: background-color 0.3s ease, color 0.3s ease, box-shadow 0.3s ease;
    cursor: pointer;
}

.list-item-flex:hover {
    background-color: #555;
    color: #fff;
    box-shadow: inset 0 0 10px rgba(0, 0, 0, 0.3);
}

.list-item-flex.active {
    background-color: #222;
    color: #fff;
    box-shadow: inset 0 0 10px rgba(0, 0, 0, 0.5);
}

.list-item-flex a {
    text-decoration: none;
    color: inherit;
    transition: color 0.3s ease;
}

.list-item-flex i {
    padding-right: 10px;
    font-size: 1.2rem;
}

.btn-cb,
.btn-compte {
    font-size: 14px;
    font-weight: 600;
    border-radius: 8px;
    background-color: #000;
    color: #fff;
    padding: 0.5rem 1rem;
    border: none;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    text-align: center;
    cursor: pointer;
    transition: all 0.3s ease;
    text-decoration: none;
}

.btn-cb:hover,
.btn-compte:hover {
    background: #282828;
    color: #fff;
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

label {
    font-weight: 500;
    margin-bottom: 5px;
    display: block;
}

/* Alerts */
.alert {
    padding: 10px;
    border-radius: 8px;
    text-align: center;
    margin-bottom: 20px;
    font-weight: 500;
}

.alert-danger {
    background-color: rgba(248, 215, 218, 0.9);
    color: #721c24;
    border: 1px solid #f5c6cb;
}

@media screen and (max-width: 768px) {
    h2 {
        font-size: 1.5rem;
    }

    .list-group-item {
        text-align: center;
        font-size: 0.9rem;
        padding: 10px 15px;
        flex-direction: column;
    }

    .pdp_picture {
        width: 80px;
        height: 80px;
    }

    .pp_container .btn {
        margin-left: 0;
        margin-top: 10px;
    }
}

.sup-item {
    display: flex;
    justify-content: flex-end;
}

.sup-icon {
    padding: 8px;
    border-radius: 10px;
}

.table-uber {
    background-color: rgb(0, 0, 0);
    color: #fff;
}

.card-body {
    line-height: 1.8;
    color: #495057;
}

input[type="number"]::-webkit-outer-spin-button,
input[type="number"]::-webkit-inner-spin-button {
    -webkit-appearance: none;
    margin: 0;
}

.date-expiration-add {
    margin-top: 30px;
}

.mb-889 {
    position: relative;
    left: 30%;
    margin: 5px;

}
</style>