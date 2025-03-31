import { defineStore } from 'pinia';
import apiClient from '@/axios';
import router from '@/router';
import { getClientById } from '@/services/clientService';

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

        this.user = {
          token: response.data.token,
          role: response.data.role,
          userId: response.data.userId,
          email: response.data.email
        };

        await this.fetchUserData();
        localStorage.setItem('user', JSON.stringify(this.user));
        this.setAuthHeader();

        return true;
      } catch (error) {
        this.clearAuth();
        throw error;
      }
    },

    async fetchUserData() {
      if (!this.user.userId) return;
      try {
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
      } catch (error) {
        console.error("Erreur lors de la récupération des données du client:", error);
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

    // register client - Melih qui nvm
    async register(clientData) {
      try {
        const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
        if (!emailPattern.test(clientData.emailUser)) {
          throw new Error('Invalid email format');
        }

        const response = await apiClient.post('clients/register', clientData);

        await this.login({
          email: clientData.emailUser,
          password: clientData.motDePasseUser
        });

        return response.data;
      } catch (error) {
        if (error.response && error.response.data.errors) {
          console.error("Validation errors:", error.response.data.errors);
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
