import apiClient from "@/axios";
import { ref } from 'vue';

export const getCourseById = async (idCourse) => {
  try {
    const response = await apiClient.get(`/Courses/GetById/${idCourse}`);
    return response.data;

  } catch (error) {
    console.error(`Erreur lors de la récupération de la course ${idCourse}:`, error);
    return [];
  }
};

export const getCoursesEnAttente = async () => {
  try {
    const response = await apiClient.get("/Courses/GetByStatut/en%20attente");
    return response.data;

  } catch (error) {
    console.error("Erreur lors de la récupération des Courses en attente :", error);
    return [];
  }
};

export const putCourseById = async (idCourse, idcoursier, sstatutCourse) => {
  try {

    const courseData = ref(null);
    const data = await getCourseById(idCourse);
    courseData.value = data;

    console.log(courseData.value.idCoursier)
    if (!courseData) {
      throw new Error(`Course avec id ${idCourse} non trouvée.`);
    }

    courseData.value.idCoursier = idcoursier

    console.log(courseData.value)
    const response = await apiClient.put(`/Courses/${idCourse}`, {
      idCourse: courseData.value.idCourse,
      idCoursier: courseData.value.idCoursier,
      idCb: courseData.value.idCb,
      idAdresse: courseData.value.idAdresse,
      idReservation: courseData.value.idReservation,
      adrIdAdresse: courseData.value.adrIdAdresse,
      idPrestation: courseData.value.idPrestation,
      dateCourse: courseData.value.dateCourse,
      heureCourse: courseData.value.heureCourse,
      prixCourse: courseData.value.prixCourse,
      statutCourse: sstatutCourse,
      noteCourse: courseData.value.noteCourse,
      commentaireCourse: courseData.value.commentaireCourse,
      pourboire: courseData.value.pourboire,
      distance: courseData.value.distance,
      temps: courseData.value.temps,
      adrIdAdresseNavigation: courseData.value.adrIdAdresseNavigation,
      idAdresseNavigation: courseData.value.idAdresseNavigation,
      idCbNavigation: courseData.value.idCbNavigation,
      idCoursierNavigation: courseData.value.idCoursierNavigation,
      idPrestationNavigation: courseData.value.idPrestationNavigation,
      idReservationNavigation: courseData.value.idReservationNavigation
    });

    return response.data;
  } catch (error) {
    console.error(`Erreur lors de la modification de la course ${idCourse} pour l'id coursier:`, error);
    return [];
  }
};

