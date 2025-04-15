<template>
    <div class="container my-5">
        <div class="account mt-5">
            <h1 v-if="showTitle" class="mb-4 text-center">Mon compte</h1>
            <div class="row">
                <div class="col-md-3">
                    <ul id="sidebar-menu" class="list-group shadow-sm">
                        <li class="list-group-item" @click="setActiveContent('content-informations')"
                            :class="{ active: activeContent === 'content-informations' }">
                            <i class="fas fa-user me-2"></i> Informations compte
                        </li>
                        <ul class="list-group">
                            <li class="list-group-item" @click="setActiveContent('content-confidentialite')"
                                :class="{ active: activeContent === 'content-confidentialite' }">
                                <i class="fas fa-user-shield me-2"></i> Confidentialité et données
                            </li>
                            <li class="list-group-item" @click="setActiveContent('content-securite')"
                                :class="{ active: activeContent === 'content-securite' }">
                                <i class="fas fa-shield-alt me-2"></i> Sécurité
                            </li>
                            <router-link to="/myaccount/carte-bancaire" class="list-group-item"
                                @click="setActiveContent('content-cartebancaire')"
                                :class="{ active: activeContent === 'content-cartebancaire' }">
                                <i class="fas fa-credit-card me-2"></i> Carte Bancaire
                            </router-link>
                        </ul>
                    </ul>
                </div>

                <div class="col-md-9">
                    <div class="p-4">
                        <div id="content-informations"
                            :class="{ 'content-section': true, 'active': activeContent === 'content-informations' }">
                            <div class="mb-4">
                                <img :src="userStore.user.PhotoProfile || profileImageUrl" alt="Photo de profil"
                                    title="Je taime" class="pdp_picture" id="profileImage">
                            </div>
                            <div class="row">
                                <div class="col-12" v-for="(value, key) in userFields" :key="key">
                                    <p><strong>{{ key }} :</strong> {{ formatUserField(value) }}</p>
                                </div>
                            </div>
                        </div>

                        <div id="content-securite"
                            :class="{ 'content-section': true, 'active': activeContent === 'content-securite' }">
                            <h1 class="h4-mb-4">Sécurité</h1>
                            <div>
                                <p class="text-center">Configurer vos paramètres de sécurité ici.</p>
                            </div>
                        </div>

                        <div id="content-addcartebancaire"
                            :class="{ 'content-section': true, 'active': activeContent === 'content-addcartebancaire' }">
                            <h1 class="h4-mb-4">Ajouter une Carte Bancaire</h1>
                            <div class="add-carte-bancaire">
                                <div class="mb-3">
                                    <label>Numéro de la carte</label>
                                    <input v-model="formattedNumeroCarte" placeholder="1234 5678 9012 3456"
                                        maxlength="19" inputmode="numeric" class="form-control" />
                                </div>
                                <div class="mb-3">
                                    <label class="date-expiration-add">Date d'expiration</label>
                                    <input type="month" class="form-control">
                                    <small>Format mm-aaaa</small>
                                </div>
                                <div class="mb-3">
                                    <label>Cryptogramme (3 chiffres)</label>
                                    <input placeholder="123" maxlength="3" required pattern="\d{3}" type="number"
                                        class="form-control">
                                </div>

                                <div class="mb-3">
                                    <label>Type de carte</label>
                                    <select id="typecarte" name="typecarte" class="form-select" required>
                                        <option value="" disabled selected>Choisissez le type</option>
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

                        <div id="content-confidentialite"
                            :class="{ 'content-section': true, 'active': activeContent === 'content-confidentialite' }">
                            <h2 class="h4 mb-4">Confidentialité et données</h2>

                            <table  class="table table-bordered">
                                <tbody>
                                    <tr v-for="(value, key) in userFields" :key="key">
                                        <td><strong>{{ key }}</strong></td>
                                        <td>{{ formatUserField(value) }}</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>

                        <div id="content-cartebancaire"
                            :class="{ 'content-section': true, 'active': activeContent === 'content-cartebancaire' }">
                            <h1 class="h4-mb-4">Mes cartes bancaires</h1>

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

                            <div v-else>
                                <p>Aucune carte bancaire enregistrée.</p>
                            </div>

                            <div class="text-center mt-5">
                                <a class="btn-compte text-decoration-none px-4 py-2"
                                    @click="setActiveContent('content-addcartebancaire')">
                                    Ajouter une carte bancaire
                                </a>
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

const activeContent = ref('content-informations');
const showTitle = ref(true);
const rawNumeroCarte = ref('');
const cartesBancaires = ref([]);
const profileImageUrl = 'https://static.vecteezy.com/ti/vetor-gratis/p2/9734564-default-avatar-profile-icon-of-social-media-user-vetor.jpg';

onMounted(async () => {
    const clientId = user.value.userId;
    if (clientId) {
        const clientData = await getClientById(clientId);
        cartesBancaires.value = clientData.idCbs || [];
    }
});

const setActiveContent = (contentId) => {
    activeContent.value = contentId;
    showTitle.value = contentId === 'content-informations';
};

const formattedDateNaissance = computed(() => {
    const options = { day: 'numeric', month: 'long', year: 'numeric' };
    const dateNaissance = new Date(user.value.dateNaissance);
    return dateNaissance.toLocaleDateString('fr-FR', options);
});

const formattedTelephone = computed(() => {
    const telephone = user.value.telephone;
    if (!telephone) return 'Non renseigné';
    const formatted = telephone.replace(/(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})/, '$1 $2 $3 $4 $5');
    return formatted;
});

const userFields = computed(() => ({
    "Prénom": user.value.prenomUser,
    "Nom": user.value.nomUser,
    "Date de naissance": formattedDateNaissance.value,
    "Numéro de téléphone": formattedTelephone.value,
    "Adresse mail": user.value.email,
    "Rôle": user.value.role,
}));

const formatUserField = (value) => {
    if (!value) return 'Non renseigné';
    if (value instanceof Date) return value.toLocaleDateString();
    return value;
};

const formattedNumeroCarte = computed({
    get() {
        return rawNumeroCarte.value.replace(/(.{4})/g, '$1 ').trim();
    },
    set(val) {
        rawNumeroCarte.value = val.replace(/\D/g, '').slice(0, 16);
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
    const cryptogramme = document.querySelector('input[type="number"]:not([placeholder])').value;
    const typeCarte = document.querySelector('select[name="typecarte"]').value;


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

    if (premier === '4') return 'Visa';
    if ((deuxPremiers >= '51' && deuxPremiers <= '55') || (quatrePremiers >= '2221' && quatrePremiers <= '2720')) return 'MasterCard';
    if (deuxPremiers === '34' || deuxPremiers === '37') return 'American Express';
    if (['30', '36', '38'].includes(deuxPremiers)) return 'Diners Club';
    if (['62', '81'].includes(deuxPremiers)) return 'UnionPay';
    return 'Autre';
}
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
.btn-details,
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

.btn-retour {
    font-size: 14px;
    font-weight: 600;
    background: #b8b8b8;
    color: #000000;
    border-radius: 8px;
    padding: 0.5rem 1rem;
    border: none;
    transition: background-color 0.3s ease;
}

.btn-retour:hover {
    background: #cbcbcb;
    color: #282828;
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

.pdp_picture {
    width: 100px;
    height: 100px;
    border-radius: 50%;
    object-fit: cover;
    border: 3px solid #ddd;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.pp_container .btn {
    font-size: 0.9rem;
    font-weight: 500;
    border-radius: 5px;
    margin-left: 20px;
    padding: 10px 15px;
    background-color: transparent;
    border: 2px solid #000;
    transition: background-color 0.3s ease, color 0.3s ease;
}

.pp_container .btn:hover {
    background-color: #444;
    color: #fff;
    border-color: #444;
}

#fileInput {
    display: none;
}

.link-photo {
    cursor: pointer;
}

.link-photo:hover {
    text-decoration: underline;
}

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

.success-container {
    position: fixed;
    top: 80px;
    left: 50%;
    transform: translateX(-50%);
    background-color: #d4edda;
    color: #155724;
    border: 1px solid #c3e6cb;
    border-radius: 8px;
    padding: 15px;
    text-align: center;
    font-size: 1rem;
    font-weight: bold;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    animation: fadeInFromTop 0.5s ease-out forwards;
    z-index: 9999;
}

@keyframes fadeInFromTop {
    from {
        opacity: 0;
        transform: translate(-50%, -20px);
    }

    to {
        opacity: 1;
        transform: translate(-50%, 0);
    }
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

.h4-mb-4 {
    text-align: center;
    font-weight: bold;
}

.card-body {
    line-height: 1.8;
    color: #495057;
}

.add-carte-bancaire {
    margin: 50px;
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