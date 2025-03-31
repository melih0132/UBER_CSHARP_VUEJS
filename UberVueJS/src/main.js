import './assets/main.css';

import { createApp } from 'vue';
import { createPinia } from 'pinia';
import App from './App.vue';
import router from './router';
import apiClient from '@/axios';
import { useUserStore } from '@/stores/userStore';

import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap';
import '@fortawesome/fontawesome-free/css/all.min.css';

const pinia = createPinia();
const app = createApp(App);

pinia.use(({ store }) => {
  if (store.$id === 'user') {
    store.initialize();
  }
});

app.use(pinia);
app.use(router);

app.config.globalProperties.$api = apiClient;

app.mount('#app');
