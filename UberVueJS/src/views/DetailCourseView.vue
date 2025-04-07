<script setup>
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';
import { getCourseById } from '@/services/courseService.js';
import { getAdresseById } from '@/services/adresseService';

const route = useRoute();
const courseId = route.params.idCourse;

const course = ref([]);
const adresseDepart = ref('');
const adresseArrivee = ref('');
console.log('ok',route.params.idCourse)

onMounted(async () => {
  try {
    const response = await getCourseById(courseId);
    course.value = response;

    // Récupérer les adresses
    if (course.value) {
      console.log('fgd',course)
      const adr1 = await getAdresseById(course.value.idAdresse);
      const adr2 = await getAdresseById(course.value.adrIdAdresse);
      adresseDepart.value = adr1?.libelleAdresse || 'Adresse non trouvée';
      adresseArrivee.value = adr2?.libelleAdresse || 'Adresse non trouvée';
    }

  } catch (error) {
    console.error('Erreur lors du chargement de la course', error);
  }
});
</script>

<template>
  <div class="container">
    <h1>Détail de la course</h1>

    <div v-if="course">
      <p><strong>Date :</strong> {{ course.dateCourse }}</p>
      <p><strong>Heure :</strong> {{ course.heureCourse }}</p>
      <p><strong>Départ :</strong> {{ adresseDepart }}</p>
      <p><strong>Arrivée :</strong> {{ adresseArrivee }}</p>
      <p><strong>Distance :</strong> {{ course.distance }} km</p>
      <p><strong>Prix :</strong> {{ course.prixCourse }} €</p>
    </div>

    <div v-else>
      <p>Chargement des informations de la course...</p>
    </div>
  </div>
</template>

<style scoped>
.container {
  max-width: 800px;
  margin: 2rem auto;
  padding: 2rem;
  background: #f9f9f9;
  border-radius: 12px;
  box-shadow: 0 0 12px rgba(0, 0, 0, 0.1);
}

h1 {
  text-align: center;
  margin-bottom: 2rem;
}

p {
  font-size: 1.1rem;
  margin-bottom: 1rem;
}
</style>
