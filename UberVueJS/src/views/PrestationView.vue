<script setup>
import { ref, onMounted, computed, nextTick } from 'vue';
import HomeContent from '@/components/HomeContent.vue';
import { getTypePrestations } from '@/services/typePrestationServices';
import { getAdresseByLibelleAdresse } from '@/services/adresseService';
import { useRideStore } from "@/stores/rideStore";
import axios from "axios";
import { useRoute, useRouter } from 'vue-router';

const typePrestations = ref([]);
const adresse1 = ref([]);
const adresse2 = ref([]);
const route = useRoute();
const router = useRouter();
const rideStore = useRideStore(); // Stocker le store pour éviter de l'appeler plusieurs fois

const startAddress = ref(route.query.start || 'Non spécifié');
const endAddress = ref(route.query.end || 'Non spécifié');
const tripDate = ref(route.query.date || new Date().toISOString().split('T')[0]);
const tripTime = ref(route.query.time || 'Maintenant');
const isLoading = ref(true);
const errorMessage = ref("");

onMounted(async () => {
  const { start, end, date, time } = route.query;

  if (!start || !end || !date || !time) {
    console.warn("❌ Informations manquantes :", { start, end, date, time });

    alert("Informations manquantes, retour à l'accueil.");
    await nextTick(); 
    router.push('/');
  }

  try {
    isLoading.value = true;
    typePrestations.value = await getTypePrestations();

    console.log("✅ Prestations disponibles :", typePrestations.value);
  } catch (error) {
    console.error("❌ Erreur lors du chargement des prestations :", error);
    errorMessage.value = "Impossible de charger les prestations. Veuillez réessayer plus tard.";
  } finally {
    isLoading.value = false;
  }
});

const prestationMessage = computed(() => {
  return `Pour une course le ${tripDate.value} à ${tripTime.value}, de ${startAddress.value} à ${endAddress.value}`;
});

const validerPrestation = async (prestation) => { 
  console.log("🚗 Réservation semi-confirmée :", prestation);

  try {
    await rideStore.NewAdresses({
      idCoursier: null,  
          idVille: 15, 
          libelleAdresse: startAddress.value,
          latitude: "50.6185789",  
          longitude: "3.0638805",  
          clients: [],  
          courseAdrIdAdresseNavigations: [],  
          courseIdAdresseNavigations: [],  
          coursiers: [],  
          entreprises: [],  
          etablissements: [],  
          idVilleNavigation: null,  
          lieuFavoris: [],  
          velos: [], 
    });

    await rideStore.NewAdresses({
      idCoursier: null,  
          idVille: 15, 
          libelleAdresse: endAddress.value,  
          latitude: "50.6185789",  
          longitude: "3.0638805",  
          clients: [],  
          courseAdrIdAdresseNavigations: [],  
          courseIdAdresseNavigations: [],  
          coursiers: [],  
          entreprises: [],  
          etablissements: [],  
          idVilleNavigation: null,  
          lieuFavoris: [],  
          velos: [], 
    });

    console.log("✅ adresse créée avec succès !");
    router.push('/search-driver');
  } catch (error) {
    console.error("❌ Erreur lors de la création de l'adresse :", error);
  }


  try {
    adresse1.value = await getAdresseByLibelleAdresse(startAddress.value);
    adresse2.value = await getAdresseByLibelleAdresse(endAddress.value);
    tripTime.value = tripTime.value + ':00'
    await rideStore.NewCourses({
      idCoursier: null,  
      idCb: null,  
      idAdresse: adresse1.value.idAdresse,
      idReservation: 1,
      adrIdAdresse: adresse2.value.idAdresse,
      idPrestation: prestation.idPrestation,  
      dateCourse: tripDate.value,  
      heureCourse: tripTime.value,  
      prixCourse: 25.5,
      statutCourse: "En attente",
      noteCourse: null,
      commentaireCourse: "",
      pourboire: 0,
      distance: 10.5,
      temps: 15
    });

    console.log("✅ course créée avec succès !");
    router.push('/search-driver');
  } catch (error) {
    console.error("❌ Erreur lors de la création de la course :", error);
  }
};
</script>

<template>
  <HomeContent />
  <div class="prestation-container">
    <div class="prestation-header">
      <p>{{ prestationMessage }}</p>
    </div>

    <div class="prestation-list">
      <div v-for="prestation in typePrestations" :key="prestation.idPrestation" class="li-prestation my-4">
        <div class="prestation-info">
          <h3 class="libelle-prestation">{{ prestation.libellePrestation }}</h3>
          <p class="p-prestation">{{ prestation.descriptionPrestation }}</p>
          <p class="p-prestation"><strong>Distance :</strong> 10 km</p>
          <p class="p-prestation"><strong>Temps estimé :</strong> 15 min</p>
          <p class="p-prestation"><strong>Prix estimé :</strong> 25 €</p>
          <button @click="validerPrestation(prestation)" class="btn-panier mt-4">Réserver</button>
        </div>
        <img :src="`/images/${prestation.imagePrestation}`" :alt="prestation.libellePrestation" class="img-prestation" />

      </div>
    </div>
  </div>
</template>


<style scoped>
.prestation-container {
  max-width: 78rem; 
  width: 92%;
  margin: 0 auto;
}
.p-prestation {
    color: black;
    font-weight: normal;
    font-size: 12px;
    line-height: 20px;
    margin-top: 8px;
    margin-bottom: 8px;
    padding-right: 8px;
}
.li-prestation
{
    background: rgb(255,255,255);
    -webkit-box-align: center;
    align-items: start;
    display: flex;
    -webkit-box-pack: justify;
    justify-content: space-between;
    padding: 15px;
    text-decoration: none !important;
    box-shadow: 0px 5px 7px 0px rgb(220, 220, 220);
    border-radius: 10px;
    border: rgb(255,255,255) 1px solid;
    transition: all 0.5s;
}
.prestation-header {
  font-size: 36px;
  font-weight: 700;
  line-height: 44px;
  text-align: center;
  margin-bottom: 20px;
}

.prestation-list {
  display: flex;
  flex-direction: column;
  gap: 16px;
  width: 100%;
}

.li-prestation:hover {
  transform: scale(1.02);
}

.prestation-info {
  flex: 1;
  text-align: left;
  padding-right: 20px;
}

.libelle-prestation {
    color: black;
    font-weight: 600;
    line-height: 20px;
    font-size: 18px;
}

.p-suggestion {
  font-size: 12px;
  color: black;
  margin: 8px 0;
}

.img-prestation {
    height: 200px;
    object-fit: contain;
    margin-right: 2rem;
    width: 200px;
}
.btn-panier {
    font-size: 14px;
    font-weight: 600;
    background: rgb(0, 0, 0);
    color: rgb(255, 255, 255);
    border-radius: 8px;
    padding: 0.5rem 1rem;
    border: none;
  }

  .btn-panier:hover {
    background: rgb(40, 40, 40);
    color: rgb(255, 255, 255);
  }

</style>
