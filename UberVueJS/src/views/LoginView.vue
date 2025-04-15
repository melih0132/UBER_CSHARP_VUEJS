<template>
  <div>
    <div class="container mt-5">
      <h1 class="text-center mb-4">Connexion</h1>

      <form @submit.prevent="handleLogin" class="form-login d-flex flex-column justify-content-center">
        <div class="mb-3">
          <label for="email" class="form-label">Email</label>
          <input type="email" v-model="email" id="email" class="form-control" placeholder="Entrez votre email"
            required />
        </div>
        <div class="mb-3">
          <label for="password" class="form-label">Mot de passe</label>
          <input type="password" v-model="password" id="password" class="form-control"
            placeholder="Entrez votre mot de passe" required />
        </div>
        <button class="btn-login" :disabled="isLoading">
          {{ isLoading ? "Connexion en cours..." : "Se connecter" }}
        </button>
      </form>
      <div class="text-center mt-3">
        <router-link to="/forgot-password" class="link-forgot">Mot de passe oublié ?</router-link>
      </div>
      <p v-if="errorMessage" class="error-message">{{ errorMessage }}</p>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useUserStore } from '@/stores/userStore';

const router = useRouter();
const store = useUserStore();

const email = ref('');
const password = ref('');
const errorMessage = ref('');
const isLoading = ref(false);

const handleLogin = async () => {
  if (!email.value || !password.value) {
    errorMessage.value = 'Veuillez remplir tous les champs';
    return;
  }

  isLoading.value = true;
  errorMessage.value = '';

  try {
    await store.login({ email: email.value, password: password.value });

    if (store.isClient || store.isCoursier) {
      router.push({ name: 'MyAccount' });
    }
  } catch (error) {
    errorMessage.value =
      error.response?.data?.message ||
      'Identifiants incorrects ou problème de connexion';
  } finally {
    isLoading.value = false;
  }
};
</script>

<style scoped>
.container {
  max-width: 400px;
  margin: auto;
}

h1 {
  font-size: 2rem;
  font-weight: 700;
  margin-bottom: 1.5rem;
}

.form-login {
  background-color: #fff;
  border-radius: 10px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  padding: 2rem;
}

label {
  font-weight: 500;
  margin-bottom: 0.5rem;
}

input {
  margin-bottom: 1rem;
}

.btn-login {
  font-size: 1rem;
  font-weight: 600;
  background: #000;
  color: #fff;
  border-radius: 8px;
  padding: 0.75rem;
  border: none;
  cursor: pointer;
  transition: background 0.3s ease;
}

.btn-login:hover {
  background: #333;
}

.btn-login:disabled {
  background: #ccc;
  cursor: not-allowed;
}

.link-forgot {
  color: #8d8d8d;
  text-decoration: none;
  font-size: 0.9rem;
}

.link-forgot:hover {
  color: #cecece;
  text-decoration: none;
}

.error-message {
  color: red;
  text-align: center;
  margin-top: 1rem;
}
</style>
