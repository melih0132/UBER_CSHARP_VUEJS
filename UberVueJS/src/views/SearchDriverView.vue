<script setup>
import { ref, onMounted, onUnmounted } from 'vue';
import { getCourseById, putCourseById } from '@/services/courseService.js';
import { useRouter } from 'vue-router';
import { useRoute } from 'vue-router';

const successMessage = ref('')
const failMessage = ref('')
const isConfirmed = ref(false);
const router = useRouter();
const route = useRoute();
const course = ref([])
const idCourse = route.params.idCourse;

let intervalId = null;

const showSuccessMessage = (idcourse) => {
  successMessage.value = `Votre course N°${idcourse} a été réservé ! Patientez`;
  setTimeout(() => {
    successMessage.value = "";
  }, 3000);
}

const showFailedMessage = (idcourse) => {
  failMessage.value = `Votre course N°${idcourse} a été abandonné ! Patientez`;
  setTimeout(() => {
    failMessage.value = "";
  }, 3000);
}

const checkCourseStatus = async () => {
  try {

    const response = await getCourseById(idCourse);
    course.value = response;
    console.log(course)
    if (course.value.idCoursier !== null) {
      console.log(course.value.idCoursier);
      isConfirmed.value = true;
      clearInterval(intervalId);
    }
  } catch (error) {
    console.error("Erreur lors de la vérification du statut de la course :", error);
  }
};

onMounted(() => {
  intervalId = setInterval(checkCourseStatus, 3000);
});

onUnmounted(() => {
  clearInterval(intervalId);
});

const proceed = async () => {

  await putCourseById(course.value.idCourse, course.value.idCoursier, 'En cours');
  showSuccessMessage(course.value.idCourse)

  await new Promise(resolve => setTimeout(resolve, 3000));

  router.push('/');

};

const getBack = async () => {
  await putCourseById(course.value.idCourse, course.value.idCoursier, 'Annulée');
  { { console.log('caca', course.value) } }
  showFailedMessage(course.value.idCourse)
  await new Promise(resolve => setTimeout(resolve, 3000));
  router.push('/');
};
</script>

<template>

  <div v-if="failMessage" :class="['notification-failed', { hide: isHiding }]">
    <p>{{ failMessage }}</p>
  </div>

  <div v-if="successMessage" :class="['notification', { hide: isHiding }]">
    <p>{{ successMessage }}</p>
  </div>


  <div class="main-container">
    <div v-if="!isConfirmed" class="search-container">
      <p>Nous recherchons un coursier pour vous...</p>
      <div class="loader"></div>
    </div>

    <div v-if="isConfirmed" class="confirmation-container">
      <h1>Coursier trouvé ✅</h1>
      <p>Un coursier {{ }} a accepté votre demande.</p>
      <button @click="proceed" class="btn-next-step">Passer à l'étape suivante</button>
      <button @click="getBack" class="btn-next-step ms-3">Dernière chance d'annuler</button>
    </div>
  </div>
</template>

<style scoped>
.notification {
  position: fixed;
  top: 80px;
  right: 20px;
  background-color: #28a745;
  color: white;
  padding: 10px 20px;
  border-radius: 5px;
  font-size: 16px;
  z-index: 1000;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  opacity: 1;
  transition: opacity 0.3s ease;
}

.notification-failed {
  position: fixed;
  top: 80px;
  right: 20px;
  background-color: #cf1e18;
  color: white;
  padding: 10px 20px;
  border-radius: 5px;
  font-size: 16px;
  z-index: 1000;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  opacity: 1;
  transition: opacity 0.3s ease;
}

.notification.hide {
  opacity: 0;
}

.notification-failed.hide {
  opacity: 0;
}

.main-container {
  text-align: center;
  max-width: 600px;
  margin: 50px auto;
  background: white;
  padding: 20px;
  border-radius: 12px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}

.search-container {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.loader {
  margin-top: 20px;
  width: 50px;
  height: 50px;
  border: 5px solid #f3f3f3;
  border-top: 5px solid black;
  border-radius: 50%;
  animation: spin 1s linear infinite;
}

@keyframes spin {
  0% {
    transform: rotate(0deg);
  }

  100% {
    transform: rotate(360deg);
  }
}

.confirmation-container {
  text-align: center;
  margin-top: 20px;
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
</style>
