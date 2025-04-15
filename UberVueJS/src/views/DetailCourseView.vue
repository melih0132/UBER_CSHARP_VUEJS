<script setup>
import { ref, onMounted, onUnmounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { getCourseById, putCourseById } from '@/services/courseService.js';
import { getAdresseById } from '@/services/adresseService';
import { useUserStore } from '@/stores/userStore.js';

const route = useRoute();
const router = useRouter();
const courseId = route.params.idCourse;
const userStore = useUserStore();
const idCoursier = userStore.user.userId;

const course = ref(null);
const adresseDepart = ref('');
const adresseArrivee = ref('');
const isLoading = ref(false);
const successMessage = ref('');
const failedMessage = ref('');
const errorMessage = ref('');
const attenteClient = ref(false);

let intervalId = null;

const accepterCourse = async () => {
  try {
    isLoading.value = true;
    errorMessage.value = '';

    const updatedCourse = await putCourseById(courseId, idCoursier, 'En attente');
    course.value = updatedCourse;
    successMessage.value = `La course n°${courseId} a été réservée ! En attente du client...`;
    failedMessage.value = `La course n°${courseId} a été abandonnée ! Le client a refusé `;
    attenteClient.value = true;
  } catch (error) {
    console.error('Erreur lors de l\'acceptation de la course:', error);
    errorMessage.value = 'Erreur lors de l\'acceptation de la course.';
  } finally {
    isLoading.value = false;
  }
};

const refuserCourse = () => {
  router.push('/course');
};

const passerASuite = () => {
  router.push('/course');
};

const checkCourseStatus = async () => {
  try {
    const response = await getCourseById(courseId);
    course.value = response;
    console.log(course.value.statutCourse)
    if (course.value.statutCourse === 'En cours') {
      clearInterval(intervalId);
      attenteClient.value = false;
    }
    else if (course.value.statutCourse === 'Annulée') {

      clearInterval(intervalId);
      attenteClient.value = false;
    }
  } catch (error) {
    console.error("Erreur lors de la vérification du statut de la course :", error);
  }
};

onMounted(async () => {
  try {
    const response = await getCourseById(courseId);
    course.value = response;

    if (course.value) {
      const adr1 = await getAdresseById(course.value.idAdresse);
      const adr2 = await getAdresseById(course.value.adrIdAdresse);
      adresseDepart.value = adr1?.libelleAdresse || 'Adresse non trouvée';
      adresseArrivee.value = adr2?.libelleAdresse || 'Adresse non trouvée';
    }
  } catch (error) {
    console.error('Erreur lors du chargement de la course', error);
  }

  intervalId = setInterval(checkCourseStatus, 3000);
});

onUnmounted(() => {
  clearInterval(intervalId);
});
</script>


<template>
  <div class="container">

    <div v-if="course?.statutCourse === 'Annulée'" class="text-center">
      <h1>{{failedMessage}}</h1>
      <button class="btn-next-step" @click="passerASuite">
        Revenir en arrière
      </button>
    </div>

    <div v-else-if="attenteClient && course?.statutCourse !== 'En cours'" class="text-center">
      <h1>En attente de validation du client...</h1>
      {{ console.log(course?.idCoursier) }}
      <p class="success-message">{{ successMessage }}</p>
    </div>

    <div v-else-if="course?.statutCourse === 'En cours'" class="text-center">
      <h1 class="mb-4">Course en cours ⏳</h1>
      <div class="text-start">
        <p><b>Date :</b> {{ course.dateCourse }}</p>
        <p><b>Heure :</b> {{ course.heureCourse }}</p>
        <p><b>Départ :</b> {{ adresseDepart }}</p>
        <p><b>Arrivée :</b> {{ adresseArrivee }}</p>
        <p><b>Distance :</b> {{ course.distance }} km</p>
        <p><b>Prix :</b> {{ course.prixCourse }} €</p>
      </div>
      <button class="btn-next-step" @click="passerASuite">
        Valider fin de course
      </button>
    </div>



    <div v-else-if="course" class="text-center">
      <h1 class="mb-3">Détails de la course</h1>
      <p><b>Date :</b> {{ course.dateCourse }}</p>
      <p><b>Heure :</b> {{ course.heureCourse }}</p>
      <p><b>Statut :</b> {{ course.statutCourse }}</p>
      <p><b>Départ :</b> {{ adresseDepart }}</p>
      <p><b>Arrivée :</b> {{ adresseArrivee }}</p>
      <p><b>Distance :</b> {{ course.distance }} km</p>
      <p><b>Prix :</b> {{ course.prixCourse }} €</p>

      <p v-if="errorMessage" class="error-message">{{ errorMessage }}</p>

      <button class="btn-valider" @click="accepterCourse" :disabled="isLoading">
        {{ isLoading ? 'Chargement...' : 'Accepter' }}
      </button>
      <button class="ms-3 btn-refuser" @click="refuserCourse">
        Refuser
      </button>
    </div>


    <div v-else>
      <p>Chargement des informations de la course...</p>
    </div>
  </div>
</template>


<style scoped>
.container {
  max-width: 700px;
  margin: 3rem auto;
  padding: 2.5rem;
  background: #fff;
  border-radius: 16px;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
}

h1 {
  text-align: center;
  font-size: 2rem;
  color: #333;
}

p {
  font-size: 1rem;
  color: rgb(0, 0, 0);
}

.btn-valider {
  font-size: 14px;
  font-weight: 600;
  background: #7AA95C;
  color: rgb(255, 255, 255);
  border-radius: 8px;
  padding: 0.5rem 1rem;
  border: none;
}

.btn-valider:hover {
  background: #7AA95A;
  color: rgb(255, 255, 255);
}

.btn-refuser {
  font-size: 14px;
  font-weight: 600;
  background: #ff3232;
  color: rgb(255, 255, 255);
  border-radius: 8px;
  padding: 0.5rem 1rem;
  border: none;
}

.btn-refuser:hover {
  background: #ff3030;
  color: rgb(255, 255, 255);
}

.btn-next-step {
  font-size: 14px;
  font-weight: 600;
  background: #000;
  color: #fff;
  border-radius: 8px;
  padding: 0.5rem 1rem;
  border: none;
  transition: background-color 0.3s ease;
}

.btn-next-step:hover {
  background: #282828;
  color: #fff;
}

.validation-message {
  font-size: 1.2rem;
  margin-bottom: 15px;
}
</style>
