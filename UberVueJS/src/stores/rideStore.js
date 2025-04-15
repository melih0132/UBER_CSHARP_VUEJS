import { defineStore } from "pinia";
import apiClient from '@/axios';

export const useRideStore = defineStore("ride", {
  actions: {
    async NewCourses(courseData) {
      try {
        const response = await apiClient.post("Courses", {
          idCoursier: null,
          idCb: null,
          idAdresse: courseData.idAdresse,
          idReservation: courseData.idReservation,
          adrIdAdresse: courseData.adrIdAdresse,
          idPrestation: courseData.idPrestation,
          dateCourse: courseData.dateCourse,
          heureCourse: courseData.heureCourse,
          prixCourse: courseData.prixCourse,
          statutCourse: "En attente",
          noteCourse: null,
          commentaireCourse: "",
          pourboire: 0,
          distance: courseData.distance,
          temps: courseData.temps
        });

        return response.data;
      } catch (error) {
        console.error("Erreur lors de la création de la course :", error);
        throw error;
      }
    },
    async NewAdresses(adresseData) {
      try {
        const response = await apiClient.post("Adresses", {
          idCoursier: null,
          idVille: 15,
          libelleAdresse: adresseData.libelleAdresse,
          latitude: "50.6185789",
          longitude: "3.0638805",
          clients: [],
          courseAdrIdAdresseNavigations: [],
          courseIdAdresseNavigations: [],
          coursiers: [],
          entreprises: [],
          etablissements: [],
          idVilleNavigation: null,
          lieuFavoris: [],
          velos: [],
        });

        return response.data.idAdresse;
      } catch (error) {
        console.error("Erreur lors de la création de la adresse :", error);
        throw error;
      }
    }
  }
})