<template>

    <main class="main-content">
        <section class="section-container">
            <header class="section-header">
                <h1 class="header-title">Vos restos locaux livrés chez vous</h1>
                <p class="header-description">Trouvez et faites-vous livrer les meilleurs plats des restaurants proches de chez vous.</p>
            </header>
            <div class="form-section">
                <form class="form-container">
                    <div class="form-grid">
                        <!-- Ville Input -->
                        <div class="form-group d-flex justify-content-center">
                            <label for="recherche_ville" class="form-label">Ville</label>
                            <input type="text" v-model="rechercheVille" id="recherche_ville" class="form-input" required 
                                placeholder="Recherchez une ville" @keyup.enter="handleRecherche" />
                        </div>

                        <!-- <div class="form-group date-picker-container">
                            <label for="selected_jour" class="form-label">Date</label>
                            <div class="input-group">
                                <input type="text" v-model="selectedJour" id="selected_jour" name="selected_jour"
                                    class="form-input flatpickr-date" placeholder="jj/mm/aaaa" />
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="selected_horaires" class="form-label">Créneau horaire</label>
                            <select v-model="selectedHoraire" id="selected_horaires" class="form-select">
                                <option value="" disabled>Sélectionnez un créneau</option>
                                <option v-for="slot in slots" :key="slot" :value="slot">
                                    {{ slot }}
                                </option>
                            </select>
                        </div> -->

                        <!-- Submit Button -->
                        <div class="form-row">
                            <RouterLink to="/etablissements" @click.capture="handleRecherche" class="form-button">Rechercher</RouterLink>
                        </div>
                    </div>
                </form>
            </div>
        </section>
    </main>

</template>

<script>
import { RouterLink } from "vue-router";
import Fuse from "fuse.js"; // Import de Fuse.js

export default {
  data() {
    return {
      villes: ['Paris', 'Lyon', 'Marseille', 'Bordeaux', 'Lille', 'Toulouse', 'Nice', 'Nantes', 'Strasbourg', 'Annecy'], // Liste des villes
      rechercheVille: "", // Modèle pour la ville
    };
  },
  methods: {
    async handleRecherche() {
      const ville = this.rechercheVille;

      // Configuration de Fuse.js
      const fuse = new Fuse(this.villes, {
        includeScore: true, // Si vous voulez la "qualité" de la correspondance
        threshold: 0.3, // Définissez un seuil pour permettre les correspondances floues
        keys: ["name"], // Si vous avez des objets, ajustez cette clé
      });

      // Effectuer la recherche
      const result = fuse.search(ville);

      // Si un résultat a été trouvé
      if (result.length > 0) {
        const villeCorrecte = result[0].item;
        window.location.href = `/etablissements/${villeCorrecte}`;
      } else {
        alert("Aucune ville trouvée.");
      }
    },
  },
  mounted() {
    const link = document.querySelector("link[rel='icon']");
    if (link) {
      link.href = 'public/images/UberEatsPetit.png';
    }
    this.fetchData();
    document.title = "Uber Eats";
  },
};
</script>


<style scoped>
.main-content {
    background-image: url("/images/ubereat.png") !important;
    background-size: cover;
    background-position: center;
    background-repeat: no-repeat;
    background-attachment: fixed;
    height: 100vh;
    width: 100%;
}
.main-content {
    padding: 50px;
}
.section-container {
    margin: 0 auto;
    max-width: 1100px;
    border-radius: 16px;
    backdrop-filter: blur(5px);
    box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
    padding: 40px;
}

.section-header {
    text-align: center;
    margin-bottom: 35px;
}

.header-title {
    font-size: 32px;
    font-weight: 700;
    color: #1a1a1a;
}

.header-description {
    font-size: 18px;
    color: #4a4a4a;
    margin-top: 10px;
}

.form-section {
    margin-top: 40px;
}

.form-container {
    display: flex;
    flex-direction: column;
}

.form-grid {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    gap: 24px;
    position: relative;
}

@media (min-width: 768px) {
    .form-grid {
        grid-template-columns: repeat(3, 1fr);
    }
}

.form-row {
    grid-column: 1 / -1;
    display: flex;
    justify-content: flex-end;
    margin-top: 10px;
}

.form-group {
    display: flex;
    flex-direction: column;
    background-color: #fdfdfd;
    padding: 20px;
    border-radius: 8px;
    border: 1px solid #e4e4e4;
    transition: box-shadow 0.3s;
}

.form-group:hover {
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}

.form-label {
    font-size: 14px;
    font-weight: 600;
    color: #555555;
    margin-left: 4px;
    margin-bottom: 8px;
    text-transform: uppercase;
    letter-spacing: 0.5px;
}

.form-input,
.form-select {
    padding: 14px;
    border: 1px solid #dcdcdc;
    border-radius: 8px;
    font-size: 16px;
    color: #333333;
    background-color: #ffffff;
    transition: border-color 0.3s, box-shadow 0.3s;
}

.form-input:focus,
.form-select:focus {
    border-color: #333333;
    box-shadow: 0 0 5px rgba(51, 51, 51, 0.4);
    outline: none;
}

.form-button {
    padding: 16px;
    background-color: rgb(0, 0, 0);
    color: #ffffff;
    border: none;
    border-radius: 8px;
    cursor: pointer;
    font-size: 16px;
    font-weight: bold;
    transition: background-color 0.3s, box-shadow 0.3s;
    text-decoration: none;
}

.form-button:hover {
    background-color: #2b2b2b;
    box-shadow: 0 4px 10px rgba(43, 43, 43, 0.4);
}

.form-button:active {
    background-color: #1a1a1a;
    box-shadow: inset 0 4px 8px rgba(0, 0, 0, 0.2);
}
</style>