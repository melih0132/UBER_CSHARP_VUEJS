import { createRouter, createWebHistory, createMemoryHistory } from "vue-router";
import { useUserStore } from "@/stores/userStore";

const HomeView = () => import("@/views/HomeView.vue");
const SearchDriverView = () => import("@/views/SearchDriverView.vue");
const Course = () => import("@/views/CourseView.vue");
const Detail = () => import("@/views/DetailCourseView.vue");
const LoginView = () => import("@/views/LoginView.vue");
const RegisterView = () => import("@/views/RegisterView.vue");
const EtablissementsVilleView = () => import("@/views/EtablissementsVilleView.vue");
const DetailEtablissementView = () => import("@/views/DetailEtablissementView.vue");
const MyAccountView = () => import("@/views/MyAccountView.vue");
const BesoinAideView = () => import("@/views/BesoinAideView.vue");
const PanierView = () => import("@/views/PanierView.vue");
const UberEatsView = () => import("@/views/UberEatsView.vue");
const PrestationView = () => import("@/views/PrestationView.vue");
const ChoixLivraisonView = () => import("@/views/ChoixLivraisonView.vue");
const ChoixCarteView = () => import("@/views/ChoixCarteView.vue");
const CommandesView = () => import("@/views/CommandesView.vue");
const CarteBancaireView = () => import("@/views/CarteBancaireView.vue");
const ForgotPasswordView = () => import("@/views/ForgotPasswordView.vue");

const routes = [
  { path: "/", name: "Home", component: HomeView },
  { path: "/prestation", name: "prestation", component: PrestationView },
  { path: "/search-driver/:idCourse", name: "search-driver", component: SearchDriverView },
  { path: "/course", name: "course", component: Course },
  { path: "/course/detail-course/:idCourse", name: "detail-course", component: Detail },
  { path: "/login", name: "Login", component: LoginView, meta: { requiresAuth: false } },
  { path: "/register", name: "Register", component: RegisterView, meta: { requiresAuth: false } },
  { path: "/etablissements/:nomVille", name: "EtablissementsParVille", component: EtablissementsVilleView },
  { path: "/etablissement/detail/:idEtablissement", name: "DetailEtablissement", component: DetailEtablissementView },
  { path: "/aide", name: "Besoin-aide", component: BesoinAideView },
  { path: "/commande/panier", name: "Panier", component: PanierView, meta: { requiresAuth: false } },
  { path: "/accueil", name: "AccueilUberEats", component: UberEatsView },
  { path: "/commande/panier/choix-livraison", name: "Choix-livraison", component: ChoixLivraisonView },
  { path: "/commande/panier/choix-carte", name: "Choix-carte", component: ChoixCarteView },
  { path: "/myaccount/carte-bancaire", name: "CarteBancaire", component: CarteBancaireView },
  { path: "/forgot-password", name: "ForgotPassword", component: ForgotPasswordView, meta: { requiresAuth: false } },
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
const isTest = process.env.NODE_ENV === 'test'

const router = createRouter({
  history: isTest ? createMemoryHistory() : createWebHistory(),
  routes,
});

router.beforeEach(async (to, from, next) => {
  const store = useUserStore()

  if (!store.isInitialized) {
    await store.initialize()
  }

  if (to.meta.requiresAuth && !store.isAuthenticated) {
    return next({ name: 'Login', query: { redirect: to.fullPath } })
  }

  if (to.meta.allowedRoles && !to.meta.allowedRoles.includes(store.user.role)) {
    return next(store.isClient ? { name: 'MyAccount' } : { name: 'Home' })
  }

  if (to.name === 'Login' && store.isAuthenticated) {
    return next(store.isClient ? { name: 'MyAccount' } : { name: 'Home' })
  }

  next()
})

export default router;