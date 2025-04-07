import apiClient from "@/axios";

export const getCoursesEnAttente = async () => {
  try {
    const response = await apiClient.get("/Courses/GetByStatut/en%20attente");
    return response.data;

  } catch (error) {
    console.error("Erreur lors de la récupération des Courses en attente :", error);
    return [];
  }
};

export const getCourseById = async (idCourse) => {
  try {
    const response = await apiClient.get(`/Courses/GetById/${idCourse}`);
    return response.data;

  } catch (error) {
    console.error(`Erreur lors de la récupération de la course ${idCourse}:`, error);
    return [];
  }
};

