<script setup>
import { ref, onMounted } from 'vue';
import { getCoursesEnAttente } from '@/services/courseService';
import { getAdresseById } from '@/services/adresseService';
import { useRouter } from 'vue-router';


const CourseEnAttente = ref([]);
const adresse1 = ref([]);
const adresse2 = ref([]);
const router = useRouter();




// Fonction pour récupérer les courses en attente
const fetchCoursesEnAttente = async () => {
  try {
    const response = await getCoursesEnAttente();

    CourseEnAttente.value = response;
    console.log(response);

    // Récupérer uniquement les libellés d'adresse de départ (idAdresse) et d'arrivée (adrIdAdresse)
    adresse1.value = await Promise.all(
      response.map(course => getAdresseById(course.idAdresse).then(adresse => adresse.libelleAdresse))
    );

    adresse2.value = await Promise.all(
      response.map(course => getAdresseById(course.adrIdAdresse).then(adresse => adresse.libelleAdresse))
    );

  } catch (error) {
    console.error('Erreur lors de la récupération des courses en attente', error);
  }
};

// Fonction pour supprimer une course de la liste
const refuserCourse = (idCourse) => {
  // Supprimer la course de la liste en filtrant par id
  CourseEnAttente.value = CourseEnAttente.value.filter(course => course.idCourse !== idCourse);
};

// Charger les courses en attente lors du montage du composant
onMounted(() => {
  fetchCoursesEnAttente();
});

const accepterCourse = (idCourse) => {
  // Rediriger vers la page de détails de la course avec l'ID (si besoin)
  router.push(`/course/detail-course/${idCourse}`);
};

</script>

<template>
  <div class="container">
    <h1>Courses en attente</h1>

    <div v-if="CourseEnAttente.length === 0">
      <p>Aucune course en attente.</p>
    </div>

    <div v-else>
      <div v-for="(course, index) in CourseEnAttente" :key="course.idCourse" class="course-item">
        <div class="course-details">
          <h3><strong>Course Numéro  {{ course.idCourse}}</strong>
          </h3>
          <p><strong>Départ:</strong> {{ adresse1[index] || 'Adresse de départ non fournie' }}</p>
          <p><strong>Arrivée:</strong> {{ adresse2[index] || 'Adresse d\'arrivée non fournie' }}</p>
          <p><strong>Date:</strong> {{ course.dateCourse || 'Date non fournie' }}</p>
          <p><strong>Heure:</strong> {{ course.heureCourse || 'Heure non fournie' }}</p>
          <p><strong>Distance:</strong> {{ course.distance }} km</p>
          <p><strong>Prix:</strong> {{ course.prixCourse }} €</p>
        </div>
        <div class="course-actions">
          <button class="btn accept" @click="accepterCourse(course.idCourse)">Accepter</button>
          <button class="btn reject" @click="refuserCourse(course.idCourse)">Refuser</button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.container {
  padding: 2rem;
  max-width: 900px;
  margin: 0 auto;
}

h1 {
  font-size: 2rem;
  margin-bottom: 1rem;
  text-align: center;
}

.course-item {
  border: 1px solid #ddd;
  padding: 1rem;
  margin-bottom: 1rem;
  border-radius: 8px;
  background-color: #f9f9f9;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.course-details {
  margin-bottom: 1rem;
}

.course-actions {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
}

.btn {
  padding: 0.5rem 1rem;
  border: none;
  border-radius: 5px;
  font-size: 1rem;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.accept {
  background-color: #4caf50;
  color: white;
}

.accept:hover {
  background-color: #45a049;
}

.reject {
  background-color: #f44336;
  color: white;
}

.reject:hover {
  background-color: #e53935;
}

@media (max-width: 768px) {
  .container {
    padding: 1rem;
  }
}
</style>
