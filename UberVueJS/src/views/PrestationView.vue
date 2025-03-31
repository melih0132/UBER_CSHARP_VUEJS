<script setup>
import { ref, onMounted } from 'vue';
import HomeContent from '@/components/HomeContent.vue';
import { getTypePrestations } from '@/services/typePrestationServices';

const typePrestations = ref([]);

onMounted(async () => {
  typePrestations.value = await getTypePrestations();
  console.log(typePrestations.value);
});
</script>

<template>
  <HomeContent />
  <div class="prestation-list">
    <div v-for="typePrestation in typePrestations" :key="typePrestation.idPrestation" class="prestation-card">
      <div class="prestation-info">
        <h3>{{ typePrestation.libellePrestation }}</h3>
        <p>{{ typePrestation.descriptionPrestation }}</p>
        <p><strong>Distance :</strong> km</p>
        <p><strong>Temps estimé :</strong> h minutes</p>
        <p><strong>Prix estimé :</strong> {{ typePrestation.prix }} €</p>
        <button class="reserve-button">Réserver</button>
      </div>
      <img :src="typePrestation.imagePrestation" :alt="typePrestation.libellePrestation" class="prestation-image" />
    </div>
  </div>
</template>


<style scoped>
.prestation-list {
  display: flex;
  flex-direction: column;
  gap: 20px;
  padding: 2rem;
  justify-content: center;
  align-items: center;
}

.prestation-card {
  display: flex;
  align-items: center;
  justify-content: space-between;
  background: white;
  padding: 20px;
  border-radius: 12px;
  box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s ease-in-out;
  width: 100%;
  max-width: 800px;
}

.prestation-card:hover {
  transform: scale(1.02);
}

.prestation-info {
  flex: 1;
  text-align: left;
}

.prestation-info h3 {
  font-size: 1.5rem;
  font-weight: bold;
  margin-bottom: 10px;
  color: black;
}

.prestation-info p {
  font-size: 1.1rem;
  color: #555;
  margin-bottom: 10px;
}

.prestation-info strong {
  color: black;
}

.prestation-image {
  width: 150px;
  height: auto;
  border-radius: 12px;
}

.reserve-button {
  background: black;
  color: white;
  padding: 12px 20px;
  border-radius: 6px;
  font-size: 1.1rem;
  font-weight: bold;
  text-decoration: none;
  display: inline-block;
  transition: background 0.3s ease;
  border: none;
  cursor: pointer;
  margin-top: 15px;
}

.reserve-button:hover {
  background: #333;
}

@media (max-width: 768px) {
  .prestation-card {
    flex-direction: column;
    text-align: center;
  }
}
</style>
