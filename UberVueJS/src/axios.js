import axios from "axios";
import { useUserStore } from '@/stores/userStore';

const apiClient = axios.create({
  baseURL: "https://uberapi-azure-bceagvaug7cxa8hk.francecentral-01.azurewebsites.net/api/",
  headers: {
    "Content-Type": "application/json",
  },
});

apiClient.interceptors.request.use(config => {
  const user = JSON.parse(localStorage.getItem('user'));
  if (user?.token) {
    config.headers.Authorization = `Bearer ${user.token}`;
  }
  return config;
});

apiClient.interceptors.response.use(
  response => response,
  error => {
    if (error.response?.status === 401) {
      const getUserStore = () => useUserStore();
      const userStore = getUserStore();
      userStore.logout();
      window.location.href = '/login';
    }
    
    console.error("Erreur API :", error.response?.data || error.message);
    return Promise.reject(error);
  }
);

export default apiClient;