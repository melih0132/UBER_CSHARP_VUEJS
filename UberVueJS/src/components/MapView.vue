<template>
  <link rel="stylesheet" href="https://unpkg.com/leaflet@1.9.3/dist/leaflet.css">
  <div id="map" style="height: 550px;"></div>
</template>

<script>
import '@/assets/leaflet.js';
import { useRideStore } from "@/stores/rideStore";
import { useUserStore } from "@/stores/userStore";
import { defineComponent, onMounted, ref } from "vue";
import L from "leaflet";
import "leaflet-routing-machine";

export default defineComponent({
  name: "MapView",
  setup(_, { expose }) {
    const rideStore = useRideStore();
    const userStore = useUserStore();
    const map = ref(null);
    const userMarker = ref(null);
    const routeLayer = ref(null);
    const routingControl = ref(null);
    const routeControl = ref(null);

    const initMap = () => {
      if (map.value) return;

      map.value = L.map("map").setView([45.8992, 6.1294], 13);

      L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", {
        attribution: "&copy; OpenStreetMap contributors",
      }).addTo(map.value);

      if (navigator.geolocation) {
        navigator.geolocation.watchPosition(
          (position) => {
            const { latitude, longitude } = position.coords;
            const userLocation = [latitude, longitude];

            if (!userMarker.value) {
              userMarker.value = L.marker(userLocation)
                .addTo(map.value)
                .bindPopup("Votre position")
                .openPopup();
            } else {
              userMarker.value.setLatLng(userLocation);
            }

            map.value.setView(userLocation, 14);
            userStore.setPosition(latitude, longitude);
          },
          (error) => {
            console.error("Erreur de géolocalisation :", error);
          },
          {
            enableHighAccuracy: true,
            timeout: 5000,
            maximumAge: 0,
          }
        );
      }
    };

    const drawRoute = (startLatLng, endLatLng) => {
      if (!map.value) return;

      if (routeControl.value) {
        map.value.removeControl(routeControl.value);
      }

      routeControl.value = L.Routing.control({
        waypoints: [
          L.latLng(startLatLng),
          L.latLng(endLatLng),
        ],
        routeWhileDragging: false,
        addWaypoints: false,
        draggableWaypoints: false,
        fitSelectedRoutes: true,
        createMarker: () => null,
        show: false,
      }).addTo(map.value);

      setTimeout(() => {
        const panel = document.querySelector(".leaflet-routing-container");
        if (panel) panel.remove();
      }, 100);
    };

    const addMarker = (latLng, label) => {
      if (!map.value) return;
      L.marker(latLng)
        .addTo(map.value)
        .bindPopup(label)
        .openPopup();
    };

    const calculateRoute = (startLatLng, endLatLng, callback) => {
      if (!map.value) return;

      if (routingControl.value) {
        map.value.removeControl(routingControl.value);
      }

      routingControl.value = L.Routing.control({
        waypoints: [
          L.latLng(startLatLng),
          L.latLng(endLatLng),
        ],
        routeWhileDragging: false,
        addWaypoints: false,
        draggableWaypoints: false,
        createMarker: () => null,
        show: false,
      }).addTo(map.value);

      routingControl.value.on("routesfound", (event) => {
        const route = event.routes[0];
        const distance = (route.summary.totalDistance / 1000).toFixed(2);
        const duration = Math.ceil(route.summary.totalTime / 60);

        if (callback && typeof callback === "function") {
          callback(distance, duration);
        }
      });
    };

    onMounted(() => {
      initMap();
    });

    expose({ map, addMarker, calculateRoute });

    return { drawRoute };
  },
});
</script>



<style scoped>
#map {
  height: 500px;
  width: 100%;
  border-radius: 5px;
  position: relative; 
}
</style>
