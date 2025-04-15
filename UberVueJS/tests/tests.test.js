// DOC VITEST = "vitest assert" dans google/duckduckgo


// Pour l'installation :
//----------------------------------------------------INSTALL
// npm install vitest
// Ajout de 
//   test": "vitest""
//   dans package.json -> scripts

// Dossier de test avec fichiers nommés *.test.js
//------------------------------------------------------/INSTALL

// CHARGEMENT DE L'APP = Copier/Coller depuis main.js
//------------------------------------------------------APP
import { expect, vi, test } from 'vitest';

import { useUserStore } from '../src/stores/userStore';
import apiClient from '@/axios';
import { getClientById } from '@/services/clientService';

import { createApp } from 'vue';
import { createPinia } from 'pinia';

import App from './src/App.vue';


vi.mock('@/axios');
vi.mock('@/router');
vi.mock('@/services/clientService');

const localStorageMock = (function() {
  let store = {};
  return {
    getItem(key) {
      return store[key] || null;
    },
    setItem(key, value) {
      store[key] = value.toString();
    },
    removeItem(key) {
      delete store[key];
    },
    clear() {
      store = {};
    }
  };
})();

Object.defineProperty(global, 'localStorage', { value: localStorageMock });

const app = createApp(App)
app.use(createPinia())
const userStore = useUserStore();


// Test 1: Vérifier que l'utilisateur est initialisé correctement
test('User store est initialisé correctement', () => {
  expect(userStore.user.token).toBeNull();
  expect(userStore.user.role).toBeNull();
  expect(userStore.user.userId).toBeNull();
  expect(userStore.user.email).toBeNull();
  expect(userStore.isInitialized).toBe(false);
});




// Test 2: Vérifier la méthode de connexion
test('La méthode de connexion renvoie les bonnes données', async () => {
  const mockResponse = {
    data: {
      token: 'mock-token',
      role: 'Client',
      userId: '123',
      email: 'test@example.com'
    }
  };
  apiClient.post.mockResolvedValueOnce(mockResponse);
  getClientById.mockResolvedValueOnce({
    fullName: 'John Doe',
    prenomUser: 'John',
    nomUser: 'Doe',
    genreUser: 'Male',
    dateNaissance: '1990-01-01',
    telephone: '1234567890',
    photoProfile: 'profile.jpg'
  });

  await userStore.login({ email: 'test@example.com', password: 'password' });

  expect(userStore.user.token).toBe('mock-token');
  expect(userStore.user.role).toBe('Client');
  expect(userStore.user.userId).toBe('123');
  expect(userStore.user.email).toBe('test@example.com');
  expect(userStore.user.fullName).toBe('John Doe');
});


// Test 3: Vérifier la méthode de déconnexion
test('La méthode de déconnexion vide bien les user data', () => {
  userStore.user = {
    token: 'mock-token',
    role: 'Client',
    userId: '123',
    email: 'test@example.com',
    fullName: 'John Doe',
    prenomUser: 'John',
    nomUser: 'Doe',
    genreUser: 'Male',
    dateNaissance: '1990-01-01',
    telephone: '1234567890',
    photoProfile: 'profile.jpg'
  };

  userStore.logout();

  expect(userStore.user.token).toBeNull();
  expect(userStore.user.role).toBeNull();
  expect(userStore.user.userId).toBeNull();
  expect(userStore.user.email).toBeNull();
  expect(userStore.user.fullName).toBeNull();
  expect(localStorage.getItem('user')).toBeNull();
});


// Test 4: Vérifie la méthode d'initialisation
test('La méthode initialize ajoute les données utilisateur dans user data', async () => {
  const mockUserData = {
    token: 'mock-token',
    role: 'Client',
    userId: '123',
    email: 'test@example.com'
  };
  localStorage.setItem('user', JSON.stringify(mockUserData));
  getClientById.mockResolvedValueOnce({
    fullName: 'John Doe',
    prenomUser: 'John',
    nomUser: 'Doe',
    genreUser: 'Male',
    dateNaissance: '1990-01-01',
    telephone: '1234567890',
    photoProfile: 'profile.jpg'
  });

  await userStore.initialize();

  expect(userStore.user.token).toBe('mock-token');
  expect(userStore.user.role).toBe('Client');
  expect(userStore.user.userId).toBe('123');
  expect(userStore.user.email).toBe('test@example.com');
  expect(userStore.user.fullName).toBe('John Doe');
  expect(userStore.isInitialized).toBe(true);
});


// Test 5: Vérifie la méthode d'enregistrement d'un nouvel utilisateur
test('La méthode d\'inscription valide bien le mail et enregistre bien l\'utilisateur', async () => {
  const mockClientData = {
    emailUser: 'newuser@example.com',
    password: 'password'
  };
  const mockResponse = {
    data: {
      userId: '456',
      email: 'newuser@example.com'
    }
  };
  apiClient.post.mockResolvedValueOnce(mockResponse);

  const result = await userStore.register(mockClientData);

  expect(result.userId).toBe('456');
  expect(result.email).toBe('newuser@example.com');
});


test('Vérifie l\'inscription d\'un utilisateur', async () => {
  const userStore = useUserStore();
  const clientData = {
    emailUser: 'newuser@example.com',
    fullName: 'New User',
    prenomUser: 'New',
    nomUser: 'User',
  };

  // Simuler la réponse de l'API pour l'enregistrement
  vi.spyOn(apiClient, 'post').mockResolvedValue({
    data: { success: true },
  });

  const registerResponse = await userStore.register(clientData);

  expect(registerResponse.success).toBe(true);
});





// Test 6: Vérifie la gestion des erreurs lors de la connexion
test('La méthode de connexion gère bien les erreurs de connexion et n\'enregistre rien dans user store', async () => {
  const errorMessage = 'Invalid credentials';
  apiClient.post.mockRejectedValueOnce(new Error(errorMessage));

  try {
    await userStore.login({ email: 'test@example.com', password: 'wrongpassword' });
  } catch (error) {
    expect(error.message).toBe(errorMessage);
  }

  expect(userStore.user.token).toBeNull();
  expect(userStore.user.role).toBeNull();
  expect(userStore.user.userId).toBeNull();
  expect(userStore.user.email).toBeNull();
  expect(localStorage.getItem('user')).toBeNull();
});



// Test 7: Vérifie la réinitialisation des données utilisateur après une déconnexion
test('La déconnexion vide toutes les données utilisateur après s\'être déconnecté', async () => {
  // Initialisation de l'utilisateur avec des données
  userStore.user = {
    token: 'mock-token',
    role: 'Client',
    userId: '123',
    email: 'test@example.com',
    fullName: 'John Doe',
    prenomUser: 'John',
    nomUser: 'Doe',
    genreUser: 'Male',
    dateNaissance: '1990-01-01',
    telephone: '1234567890',
    photoProfile: 'profile.jpg'
  };
  localStorage.setItem('user', JSON.stringify(userStore.user));

  // Déconnexion
  userStore.logout();

  // Vérification que toutes les données utilisateur sont réinitialisées
  expect(userStore.user.token).toBeNull();
  expect(userStore.user.role).toBeNull();
  expect(userStore.user.userId).toBeNull();
  expect(userStore.user.email).toBeNull();
  expect(userStore.user.fullName).toBeNull();
  expect(userStore.user.prenomUser).toBeNull();
  expect(userStore.user.nomUser).toBeNull();
  expect(userStore.user.genreUser).toBeNull();
  expect(userStore.user.dateNaissance).toBeNull();
  expect(userStore.user.telephone).toBeNull();
  expect(userStore.user.photoProfile).toBeNull();
  expect(localStorage.getItem('user')).toBeNull();
  expect(apiClient.defaults.headers.common['Authorization']).toBeUndefined();
});


// Test 9: Vérifie la validation de l'email lors de l'enregistrement
test('La méthode d inscriptionvalide le format d\'email correctement', async () => {
  const invalidClientData = {
    emailUser: 'invalid-email',
    password: 'password'
  };

  try {
    await userStore.register(invalidClientData);
  } catch (error) {
    expect(error.message).toBe('Format email invalide');
  }

  const validClientData = {
    emailUser: 'newuser@example.com',
    password: 'password'
  };
  const mockResponse = {
    data: {
      userId: '456',
      email: 'newuser@example.com'
    }
  };
  apiClient.post.mockResolvedValueOnce(mockResponse);

  const result = await userStore.register(validClientData);

  expect(result.userId).toBe('456');
  expect(result.email).toBe('newuser@example.com');
});
