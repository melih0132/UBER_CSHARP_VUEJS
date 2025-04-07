
<script setup>
import { useUserStore } from '@/stores/userStore';
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';

const isConfirmed = ref(false);
const router = useRouter();

onMounted(() => {
  
});

const proceed = () => {
  router.push('/suivant'); 
};

const getBack = () => {
  router.push('/'); 
};



</script>



<template>
  <div class="main-container">
    <h1>Recherche d'un coursier...</h1>
    
    <div v-if="!isConfirmed" class="search-container">
      <p>Nous recherchons un coursier pour vous...</p>
      <div class="loader"></div>
    </div>

    <div v-if="isConfirmed" class="confirmation-container">
      <h3>Coursier trouvé ✅</h3>
      <p>Votre coursier {{ userStore.user.prenomUser }} a accepté votre demande.</p>
      <button @click="proceed" class="confirm-button">Passer à l'étape suivante</button>
      <button @click="getBack" class="confirm-button">Dernière chance d'annuler</button>
    </div>
  </div>
</template>



<style scoped>
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
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}

.confirmation-container {
  text-align: center;
  margin-top: 20px;
}

.confirm-button {
  background: black;
  color: white;
  padding: 12px 20px;
  border-radius: 8px;
  font-size: 1.1rem;
  font-weight: bold;
  border: none;
  cursor: pointer;
  transition: background 0.3s;
}

.confirm-button:hover {
  background: #333;
}
</style>
