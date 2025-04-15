import { defineStore } from 'pinia';
import apiClient from '@/axios';
import router from '@/router';
import { getClientById } from '@/services/clientService';
import { getCoursierById } from '@/services/coursierService';

export const useUserStore = defineStore('user', {
  state: () => ({
    user: {
      token: null,
      role: null,
      userId: null,
      email: null,
      fullName: null,
      prenomUser: null,
      nomUser: null,
      genreUser: null,
      dateNaissance: null,
      telephone: null,
      photoProfile: null
    },
    isInitialized: false
  }),

  actions: {
    async login(credentials) {
      try {
        const response = await apiClient.post('login', {
          email: credentials.email,
          password: credentials.password
        });

        console.log(response.data);

        this.user = {
          token: response.data.token,
          role: response.data.role,
          userId: response.data.userId,
          email: response.data.email
        };

        console.log(this.user)

        this.setAuthHeader();

        await this.fetchUserData();
        localStorage.setItem('user', JSON.stringify(this.user));

        return true;
      } catch (error) {
        this.clearAuth();
        throw error;
      }
    },

    async fetchUserData() {
      if (!this.user.userId || !this.user.role) return;

      try {
        if (this.user.role === 'Client') {
          const clientData = await getClientById(this.user.userId);
          if (clientData) {
            Object.assign(this.user, {
              fullName: clientData.fullName || "",
              prenomUser: clientData.prenomUser || "",
              nomUser: clientData.nomUser || "",
              genreUser: clientData.genreUser || "",
              dateNaissance: clientData.dateNaissance || "",
              telephone: clientData.telephone || "",
              photoProfile: clientData.photoProfile || ""
            });
          }
        } else if (this.user.role === 'Coursier') {
          const coursierData = await getCoursierById(this.user.userId);
          if (coursierData) {
            Object.assign(this.user, {
              fullName: coursierData.fullName || "",
              prenomUser: coursierData.prenomUser || "",
              nomUser: coursierData.nomUser || "",
              genreUser: coursierData.genreUser || "",
              dateNaissance: coursierData.dateNaissance || "",
              telephone: coursierData.telephone || "",
              photoProfile: coursierData.photoProfile || ""
            });
          }
        }
      } catch (error) {
        console.error("Erreur lors de la récupération des données utilisateur :", error);
      }
    },

    logout() {
      this.clearAuth();
      router.push({ name: 'Login' });
    },

    async initialize() {
      if (this.isInitialized) return;
      try {
        const userData = localStorage.getItem('user');
        if (userData) {
          this.user = JSON.parse(userData);
          this.setAuthHeader();
          await this.fetchUserData();
        }
      } catch (error) {
        this.clearAuth();
        console.error('Initialization error:', error);
      } finally {
        this.isInitialized = true;
      }
    },

    setAuthHeader() {
      if (this.user.token) {
        apiClient.defaults.headers.common['Authorization'] = `Bearer ${this.user.token}`;
      }
    },

    clearAuth() {
      this.user = {
        token: null,
        role: null,
        userId: null,
        email: null,
        fullName: null,
        prenomUser: null,
        nomUser: null,
        genreUser: null,
        dateNaissance: null,
        telephone: null,
        photoProfile: null
      };
      delete apiClient.defaults.headers.common['Authorization'];
      localStorage.removeItem('user');
    },

    async register(clientData) {
      try {
        const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
        if (!emailPattern.test(clientData.emailUser)) {
          throw new Error('Format email invalide');
        }

        const response = await apiClient.post('clients/register', clientData);
        return response.data;

      } catch (error) {
        if (error.response?.data?.errors) {
          console.error("Erreurs de validation:", error.response.data.errors);
        }
        throw error;
      }
    }
  },

  getters: {
    isAuthenticated: (state) => !!state.user.token,
    currentUser: (state) => state.user,
    isClient: (state) => state.user.role === 'Client',
    isCoursier: (state) => state.user.role === 'Coursier',
    authHeader: (state) => state.user.token ? { Authorization: `Bearer ${state.user.token}` } : {}
  }
});
