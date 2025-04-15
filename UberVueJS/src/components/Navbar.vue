<template>
  <nav class="navbar-uber">
    <ul class="ul-links">
      <li v-if="!isCoursier" class="pr-1">
        <router-link to="/" class="header-logo" aria-label="Accueil">
          <img class="logo-png" src="/public/images/Uber.png" alt="Logo Uber">
        </router-link>
      </li>
      <li v-else class="pr-1">
        <div class="header-logo">
          <img class="logo-png" src="/public/images/Uber.png" alt="Logo Uber">
        </div>
      </li>
      <li v-if="!isCoursier" class="pr-1">
        <router-link to="/" class="header-links" aria-label="Déplacez-vous avec Uber">
          Déplacez-vous avec Uber
        </router-link>
      </li>
      <li v-if="!isCoursier" class="pr-1">
        <router-link to="/accueil" class="header-links" aria-label="Uber Eats">
          Uber&nbsp;Eats
        </router-link>
      </li>
      <li v-if="isCoursier" class="pr-1">
        <router-link to="/course" class="header-links" aria-label="Besoin d'aide">
          Courses
        </router-link>
      </li>
      <li v-if="!isCoursier" class="pr-1">
        <router-link to="/aide" class="header-links" aria-label="Besoin d'aide">
          Besoin&nbsp;d'aide ?
        </router-link>
      </li>
    </ul>
    <ul class="ul-link-login d-flex align-items-center">
      <li v-if="!isCoursier">
        <router-link to="/commande/panier" class="a-panier" aria-label="Panier">
          <i class="fas fa-shopping-cart panier"></i>
        </router-link>
      </li>
      <li v-if="isAuthenticated" class="pr-1">
        <router-link to="/myaccount" class="a-login" aria-label="Mon Compte">Mon Compte</router-link>
      </li>
      <li v-if="isAuthenticated" class="pr-1">
        <a @click="logout" class="a-register" aria-label="Se déconnecter">Se déconnecter</a>
      </li>
      <li v-if="!isAuthenticated" class="pr-1">
        <router-link to="/login" class="a-login" aria-label="Connexion">Connexion</router-link>
      </li>
      <li v-if="!isAuthenticated" class="pr-1">
        <router-link to="/register" class="a-register" aria-label="Inscription">Inscription</router-link>
      </li>
    </ul>
  </nav>
</template>

<script>
import { useUserStore } from '@/stores/userStore'
import { computed } from 'vue'

export default {
  setup() {
    const userStore = useUserStore()
    const isAuthenticated = computed(() => userStore.isAuthenticated)
    const isClient = computed(() => userStore.isClient)
    const isCoursier = computed(() => userStore.isCoursier)

    const logout = () => {
      userStore.logout()
    }

    return {
      isAuthenticated,
      isClient,
      isCoursier,
      logout,
      user: userStore.currentUser
    }
  }
}
</script>

<style scoped>
.pr-2 {
  background-color: white;
}

.header-logo {
  display: block;
  margin-top: 3px;
  margin-left: 125px;
  margin-right: 50px;
}

.logo-png {
  height: auto;
  width: 50px;
  border-style: none;
  background-color: none;
  pointer-events: none;
}

.navbar-uber {
  font-size: 16px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 12px 20px;
  border-bottom: 2px solid #000;
  background-color: #000;
  height: 64px;
  position: relative;
  top: 0;
  transition: 300ms;
  z-index: 2002;
  border-bottom: 2px solid rgb(255, 255, 255);
}

.ul-link-login {
  display: flex;
  align-items: center;
  margin-bottom: 0;
  gap: 10px;
  list-style-type: none;
  padding-left: 0;
}

.ul-links {
  display: flex;
  flex: 1;
  justify-content: flex-start;
  list-style: none;
  margin: 15px 24px;
  padding: 0;
}

.header-links,
.a-login,
.a-register {
  border-radius: 30px;
  padding: 10px 12px;
  font-size: 16px;
  font-weight: 600;
  text-decoration: none;
  display: inline-flex;
  align-items: center;
  white-space: nowrap;
  height: 36px;
}

.header-links {
  color: #fff;
  transition: background 0.3s ease-in-out;
}

.header-links:hover {
  background-color: #282828;
}

.a-register {
  cursor: pointer;
  background: #000;
  color: #fff;
  border-radius: 500px;
}

.a-register:hover {
  background-color: #282828;
}

.a-login {
  background: #fff;
  color: #000;
  border-radius: 500px;
}

.a-login:hover {
  background: #f3f3f3;
}

.a-panier {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  padding: 8px;
  border-radius: 8px;
  text-decoration: none;
  color: white;
}

.a-panier i {
  color: white;
}
</style>
