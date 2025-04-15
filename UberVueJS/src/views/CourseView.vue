<script setup>
import { ref, onMounted } from 'vue';
import { getCoursesEnAttente } from '@/services/courseService';
import { getAdresseById } from '@/services/adresseService';
import { useRouter } from 'vue-router';


const CourseEnAttente = ref([]);
const adresse1 = ref([]);
const adresse2 = ref([]);
const router = useRouter();

const fetchCoursesEnAttente = async () => {
  try {
    const response = await getCoursesEnAttente();
    CourseEnAttente.value = response;
    console.log(response);

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

const refuserCourse = (idCourse) => {
  CourseEnAttente.value = CourseEnAttente.value.filter(course => course.idCourse !== idCourse);
};

onMounted(() => {
  fetchCoursesEnAttente();
});

const accepterCourse = (idCourse) => {
  router.push(`/course/detail-course/${idCourse}`);
};


</script>

<template>
  <div class="container">
    <h1 class="text-center mb-5">Courses en attente</h1>

    <div v-if="CourseEnAttente.length === 0">
      <p>Aucune course en attente.</p>
    </div>

    <div v-else>
      <div v-for="(course, index) in CourseEnAttente" :key="course.idCourse" class="course-item mb-5">
        <div class="course-details">
          <h3 class="mb-4"><b>Course n°{{ course.idCourse}}</b></h3>
          <p><b>Date de la course :</b> {{ course.dateCourse || 'Date non fournie' }}</p>
          <p><b>Adresse de départ :</b> {{ adresse1[index] || 'Adresse de départ non fournie' }}</p>
          <p><b>Adresse d'arrivée :</b> {{ adresse2[index] || 'Adresse d\'arrivée non fournie' }}</p>
          <p><b>Heure de la course :</b> {{ course.heureCourse || 'Heure non fournie' }}</p>
          <p><b>Distance de la course :</b> {{ course.distance }} km</p>
          <p><b>Prix estimé :</b> {{ course.prixCourse }} €</p>
        </div>
        <div class="course-actions">
            <button class="btn-valider" @click="accepterCourse(course.idCourse)">Accepter</button>
          <button class="btn-refuser" @click="refuserCourse(course.idCourse)">Refuser</button>
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
  font-size: 52px !important;
  font-weight: 700 !important;
  line-height: 64px !important;
}

.course-item {
  padding: 1rem;
  margin-bottom: 1rem;
  border-radius: 8px;
  background-color: #f9f9f9;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  transition: all 0.5s;
}
.course-item:hover {
  scale: 1.05;
}

.course-details {
  margin-bottom: 1rem;
}

.course-actions {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
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

@media (max-width: 768px) {
  .container {
    padding: 1rem;
  }
}
</style>
