<template>
  <div class="container" style="padding-bottom: 2rem; padding-top: 4rem;">
    <section>
      <div class="main-container">
        <div class="row p-4">
          <div class="col-12 col-sm-6">
            <h1 class="pb-4">Commandez ou planifiez une course</h1>

            <div>
              <h2 v-if="userStore.isAuthenticated">Bonjour {{ userStore.user.prenomUser }}</h2>
              <h6>Ajoutez les détails de votre course, montez à bord et c'est parti.</h6>
            </div>

            <!-- Adresse départ -->
            <div class="address-input-container">
              <input type="text" id="startAddress" v-model="startAddress" placeholder="Adresse de départ" required
                @input="fetchSuggestions('start')" @focus="fetchSuggestions('start')">
              <ul v-if="startSuggestions.length" class="suggestions-list">
                <li v-for="s in startSuggestions" :key="s.place_id" @click="selectAddress('start', s)">
                  {{ s.description }}
                </li>
              </ul>

              <small>Veuillez renseigner votre adresse complète</small>
            </div>

            <!-- Adresse arrivée -->
            <div class="address-input-container">
              <input type="text" id="endAddress" v-model="endAddress" placeholder="Adresse d'arrivée" required
                @input="fetchSuggestions('end')" @focus="fetchSuggestions('end')">
              <ul v-if="endSuggestions.length" class="suggestions-list">
                <li v-for="s in startSuggestions" :key="s.place_id" @click="selectAddress('start', s)">
                  {{ s.description }}
                </li>
              </ul>

              <small>Veuillez renseigner votre adresse complète</small>
            </div>

            <!-- Date et heure -->
            <div class="date-container">
              <div class="date-time-container mt-3 mr-3" @click="$refs.dateInput.showPicker()">
                <label id="tripDateLabel" data-icon="📅" class="mr-1">
                  {{ formattedDate }}
                </label>
                <input type="date" id="tripDate" ref="dateInput" v-model="tripDate"
                  :min="new Date().toISOString().split('T')[0]">
              </div>

              <div id="customTimePicker" class="date-time-container mt-3" @click="showTimeDropdown = !showTimeDropdown">
                <label id="tripTimeLabel" data-icon="⏰">
                  {{ tripTime }}
                </label>
                <ul v-if="showTimeDropdown" id="customTimeDropdown" class="dropdown-list show">
                  <li v-for="(time, index) in timeOptions" :key="index" @click="selectTime(time)">
                    {{ time }}
                  </li>
                </ul>
              </div>
            </div>

            <!-- Distance et bouton -->
            <div v-if="distance" id="distanceResult" class="mt-3">
              Distance estimée : {{ distance }} km • Durée : {{ duration }}
            </div>
            <button v-if="isAuthenticated" @click="handleSubmit" class="btn btn-primary mt-4">Voir les
              prestations</button>
            <router-link v-else to="/login" class="mt-4">Voir les prestations</router-link>
          </div>

          <div class="col-12 col-sm-6">
            <MapView ref="mapView" />
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
import { useUserStore } from '@/stores/userStore';
import MapView from '@/components/MapView.vue';
import { mapStores } from 'pinia';
import axios from 'axios';


export default {
  components: {
    MapView
  },
  data() {
    return {
      tripDate: new Date().toISOString().split('T')[0],
      tripTime: 'Maintenant',
      startAddress: '',
      endAddress: '',
      startSuggestions: [],
      endSuggestions: [],
      showTimeDropdown: false,
      timeOptions: this.generateTimeOptions(),
      distance: null,
      duration: null,
      debounceTimer: null,
      isAuthenticated: false,
      startSuggestions: [],
      endSuggestions: [],
    };
  },
  computed: {
    ...mapStores(useUserStore),
    formattedDate() {
      const date = new Date(this.tripDate);
      return date.toLocaleDateString('fr-FR', { day: 'numeric', month: 'long', year: 'numeric' });
    },
  },
  methods: {
    async fetchSuggestions(type, input) {
      if (input.length < 3) return;

      try {
        const response = await fetch(`https://maps.googleapis.com/maps/api/place/autocomplete/json?input=${encodeURIComponent(input)}&key=TA_CLE_API&language=fr&components=country:fr`);
        const data = await response.json();

        if (data.status === 'OK') {
          const suggestions = data.predictions;
          if (type === 'start') {
            this.startSuggestions = suggestions;
          } else {
            this.endSuggestions = suggestions;
          }
        } else {
          console.error('Erreur API Google:', data.status, data.error_message);
        }
      } catch (error) {
        console.error('Erreur de récupération des suggestions :', error);
      }
    },

    selectAddress(type, suggestion) {
      if (type === 'start') {
        this.startAddress = suggestion.description;
        this.startSuggestions = [];
      } else {
        this.endAddress = suggestion.description;
        this.endSuggestions = [];
      }
      this.calculateRoute();
    },

    generateTimeOptions() {
      const options = ['Maintenant'];
      for (let h = 0; h < 24; h++) {
        for (let m = 0; m < 60; m += 15) {
          options.push(`${h.toString().padStart(2, '0')}:${m.toString().padStart(2, '0')}`);
        }
      }
      return options;
    },

    getNextHalfHour() {
      const now = new Date();
      const minutes = now.getMinutes();
      const halfHour = minutes < 30 ? 30 : 60;
      now.setMinutes(halfHour, 0, 0);
      return now.toLocaleTimeString('fr-FR', { hour: '2-digit', minute: '2-digit' });
    },

    selectTime(time) {
      this.tripTime = time === 'Maintenant' ? this.getNextHalfHour() : time;
      this.showTimeDropdown = false;
    },

    async calculateRoute() {
      if (!this.startAddress || !this.endAddress) return;

      try {
        const response = await axios.post('/api/calculate-route', {
          start: this.startAddress,
          end: this.endAddress
        });

        this.distance = (response.data.distance / 1000).toFixed(1);
        this.duration = response.data.duration;

        this.$refs.mapView.drawRoute(response.data.polyline);
      } catch (error) {
        console.error('Route calculation error:', error);
      }
    },

    handleSubmit() {
      if (!this.startAddress || !this.endAddress || !this.tripDate || !this.tripTime) {
        alert("Veuillez remplir tous les champs !");
        return;
      }

      this.selectTime(this.getNextHalfHour());

      console.log('Départ :', this.startAddress);
      console.log('Arrivée :', this.endAddress);
      console.log('Distance :', this.distance, 'km');

      this.$router.push({
        path: '/prestation',
        query: {
          start: this.startAddress,
          end: this.endAddress,
          date: this.tripDate,
          time: this.tripTime
        }
      });
    },

    checkAuth() {
      this.userStore = useUserStore();
      this.isAuthenticated = this.userStore.isAuthenticated;
    }
  },
  mounted() {
    this.checkAuth();
  }
};
</script>



<style scoped>
#map {
  height: 500px;
  width: 100%;
  border-radius: 5px;
}

.main-container {
  max-width: 78rem;
  width: 92%;
}

h1 {
  font-size: 52px !important;
  font-weight: 700 !important;
  line-height: 64px !important;
}

h3 {
  font-size: 36px !important;
  font-weight: 700 !important;
  line-height: 44px !important;
}

#startAddress {
  border-radius: 8px;
  background-color: #f3f3f3;
  border: 2px solid#F3F3F3;
  width: 100%;
  padding-top: 10px;
  padding-bottom: 10px;
  padding-left: 36px;
  padding-right: 0;
}

#endAddress {
  border-radius: 8px;
  background-color: #f3f3f3;
  border: 2px solid#F3F3F3;
  width: 100%;
  padding-top: 10px;
  padding-bottom: 10px;
  padding-left: 36px;
  padding-right: 0;
}

.date-container {
  display: flex;
}

.date-container label {
  display: flex;
  cursor: pointer;
}

.date-time-container {
  display: flex;
  align-items: center;
  justify-content: space-between;
  background-color: #f3f3f3;
  border: 1px solid #e0e0e0;
  border-radius: 12px;
  padding: 12px 16px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  width: 200px;
  font-size: 16px;
  margin-right: 1rem;
  cursor: pointer;
  position: relative;
  transition:
    background-color 0.3s,
    box-shadow 0.3s;
}

.date-time-container label {
  display: flex;
  align-items: center;
  font-size: 16px;
  font-weight: 500;
  color: #333;
  gap: 8px;
  flex-shrink: 0;
  margin-bottom: 0;
}

.date-time-container label:before {
  content: attr(data-icon);

  font-size: 18px;
  color: #666;
  flex-shrink: 0;
}

.date-time-container input {
  appearance: none;
  -webkit-appearance: none;
  opacity: 0;
  position: absolute;
  pointer-events: none;
}

.date-time-container input {
  border: none;
  background: transparent;
  font-size: 16px;
  color: #333;
  flex: 1;
  text-align: right;
  outline: none;
  cursor: pointer;
  padding-left: 10px;
}

.date-time-container:hover {
  background-color: #e8e8e8;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.15);
}

.date-time-container::after {
  content: "\25BC";
  font-size: 12px;
  color: #666;
  margin-left: auto;
}

#customTimeDropdown.show {
  display: block;
}

#customTimeDropdown {
  position: absolute;
  top: 100%;
  left: 0px;
  right: 0px;
  background-color: white;
  box-shadow: rgba(0, 0, 0, 0.1) 0px 4px 6px;
  max-height: 240px;
  overflow-y: auto;
  z-index: 1000;
  display: none;
  border-width: 1px;
  border-style: solid;
  border-color: rgb(204, 204, 204);
  border-image: initial;
  border-radius: 8px;
}

#customTimeDropdown li {
  font-size: 14px;
  color: rgb(51, 51, 51);
  cursor: pointer;
  padding: 10px 16px;
  list-style: none;
}

#app .card-suggestion {
  -webkit-box-align: center;
  align-items: center;
  display: flex;
  -webkit-box-pack: justify;
  justify-content: space-between;
  padding: 16px;
  text-decoration: none !important;
  box-shadow: 0px 5px 7px 0px rgb(220, 220, 220);
  border-radius: 10px;
  border: #f3f3f3 1px solid;
  background: #F3F3F3;
}

#app .title-prestation {
  color: rgb(0, 0, 0);
  font-weight: 600;
  font-size: 16px;
  line-height: 20px;
}

#app .p-suggestion {
  color: rgb(0, 0, 0);
  font-weight: normal;
  font-size: 12px;
  line-height: 20px;
  margin-top: 8px;
  margin-bottom: 8px;
  padding-right: 8px;
}

#app .img-suggestion {
  height: 128px;
  object-fit: contain;
  width: 128px;
}

#app .suggestions-list li {
  padding: 10px;
  cursor: pointer;
  font-size: 14px;
  color: #333;
}

.suggestions-list li:hover {
  background-color: #f0f0f0;
}

button.mt-4 {
  background-color: black;
  color: white;
  padding: 12px 16px;
  border: none;
  border-radius: 8px;
  font-size: 16px;
  font-weight: bold;
  cursor: pointer;
  width: 51.5%;
  transition:
    background-color 0.3s,
    box-shadow 0.3s;
  text-align: center;
}

a.mt-4 {
  display: inline-block;
  background-color: black;
  color: white;
  padding: 12px 16px;
  border: none;
  border-radius: 8px;
  font-size: 16px;
  font-weight: bold;
  cursor: pointer;
  width: 51.5%;
  transition:
    background-color 0.3s,
    box-shadow 0.3s;
  text-align: center;
  text-decoration: none;
}

button.mt-4:hover,
a.mt-4:hover {
  background-color: #333;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.2);
  text-decoration: none;
}
</style>