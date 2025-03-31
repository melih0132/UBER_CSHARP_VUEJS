import { defineStore } from "pinia";

export const useRideStore = defineStore("ride", {
  state: () => ({
    rideRequests: [],
    drivers: [],
  }),
  actions: {
    requestRide(passenger) {
      this.rideRequests.push(passenger);
    },
    acceptRide(driver) {
      if (this.rideRequests.length > 0) {
        const ride = this.rideRequests.shift();
        this.drivers.push({ driver, ride });
      }
    },
  },
});
