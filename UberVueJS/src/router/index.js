import { createRouter, createWebHistory } from "vue-router";
import { useUserStore } from "@/stores/userStore";

// Vues principales
const HomeView = () => import("@/views/HomeView.vue");
const RideView = () => import("@/views/RideView.vue");
const LoginView = () => import("@/views/LoginView.vue");
const RegisterView = () => import("@/views/RegisterView.vue");
const EtablissementsView = () => import("@/views/EtablissementsView.vue");
const EtablissementsVilleView = () => import("@/views/EtablissementsVilleView.vue");
const DetailEtablissementView = () => import("@/views/DetailEtablissementView.vue");
const MyAccountView = () => import("@/views/MyAccountView.vue");
const BesoinAideView = () => import("@/views/BesoinAideView.vue");
const PanierView = () => import("@/views/PanierView.vue");
const UberEatsView = () => import("@/views/UberEatsView.vue");
const PrestationView = () => import("@/views/PrestationView.vue");

const CommandesView = () => import("@/views/CommandesView.vue");

const routes = [
  // Routes publiques
  { path: "/", name: "Home", component: HomeView },
  { path: "/prestation", name: "prestation", component: PrestationView },
  { path: "/ride", name: "Ride", component: RideView },
  { path: "/login", name: "Login", component: LoginView, meta: { requiresAuth: false } },
  { path: "/register", name: "Register", component: RegisterView, meta: { requiresAuth: false } },
  { path: "/etablissements", name: "Etablissements", component: EtablissementsView },
  { path: "/etablissements/:nomVille", name: "EtablissementsParVille", component: EtablissementsVilleView },
  { path: "/etablissement/detail/:idEtablissement", name: "DetailEtablissement", component: DetailEtablissementView },
  { path: "/aide", name: "Besoin-aide", component: BesoinAideView },
  { path: "/panier", name: "Panier", component: PanierView, meta: { requiresAuth: false } },
  { path: "/accueil", name: "AccueilUberEats", component: UberEatsView },

  // Routes protégées
  { 
    path: "/myaccount", 
    name: "MyAccount", 
    component: MyAccountView,
    meta: { requiresAuth: true } 
  },
  { 
    path: "/commandes", 
    name: "Commandes", 
    component: CommandesView,
    meta: { requiresAuth: true, allowedRoles: ['Client'] } 
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach(async (to, from, next) => {
  const store = useUserStore();
  
  if (!store.isInitialized) {
    await store.initialize();
  }

  if (to.meta.requiresAuth) {
    if (!store.isAuthenticated) {
      return next({ 
        name: 'Login',
        query: { redirect: to.fullPath } 
      });
    }

    if (to.meta.allowedRoles && !to.meta.allowedRoles.includes(store.user.role)) {
      return next(store.isClient ? { name: 'MyAccount' } : { name: '/' });
    }
  }

  if (to.name === 'Login' && store.isAuthenticated) {
    return next(store.isClient ? { name: 'MyAccount' } : { name: '/' });
  }

  next();
});

export default router;