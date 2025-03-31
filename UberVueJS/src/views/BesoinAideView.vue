<template>
    <section class="guide-uber my-5">
      <h1 class="text-center">Guide utilisateur</h1>
      <div class="container">
        <p class="text-center">
          Bienvenue sur le guide utilisateur Uber qui va vous permettre de réserver une course en toute simplicité
        </p>
  
        <!-- Barre de menu -->
        <h2 class="text-decoration-underline mt-5 mb-2">Barre de menu</h2>
        <p>Avec la barre menu vous allez pouvoir accéder aux différents services d'Uber.</br>
          Vous pourrez aussi vous connecter ou bien vous inscrire.
        </p>
        <div class="row my-4">
          <div class="col-6 d-flex justify-content-center">
            <img class="img-fluid" :src="imgUrls.navuber" alt="Menu Uber">
          </div>
          <div class="col-6 d-flex justify-content-center">
            <img class="img-fluid" :src="imgUrls.loginuber" alt="Login Uber">
          </div>
        </div>
        <hr />
  
        <!-- Connexion / Inscription -->
        <h2 class="text-decoration-underline mt-5 mb-2">Connexion / Inscription</h2>
        <p>Il vous faut vous connecter ou vous inscrire si vous n'avez pas de compte pour réserver des courses.</p>
        <div class="row my-4">
          <div class="col-6 d-flex justify-content-center">
            <a :href="links.login">
              <img class="img-fluid" :src="imgUrls.login" alt="Login">
            </a>
          </div>
          <div class="col-6 d-flex justify-content-center">
            <a :href="links.register">
              <img class="img-fluid" :src="imgUrls.register" alt="Register">
            </a>
          </div>
        </div>
        <hr />
  
        <!-- Page d'accueil -->
        <h2 class="text-decoration-underline mt-4 mb-2">Page d'accueil</h2>
        <p>Voici la page d'accueil, point de départ de votre course.</p>
        <a :href="links.home">
          <img class="img-fluid my-3" :src="imgUrls.accueil" alt="Page d'accueil">
        </a>
        <hr />
  
        <!-- Prestations -->
        <h2 class="text-decoration-underline mt-4 mb-2">Prestations</h2>
        <p>En fonction de vos paramètres de course vous verrez tous les types de prestation disponibles.</p>
        <img class="img-fluid w-100 my-3" :src="imgUrls.prestation" alt="Prestations">
        <hr />
  
        <!-- Détails de la course -->
        <h2 class="text-decoration-underline mt-4 mb-2">Détails de la course</h2>
        <p>Une fois votre type de prestation choisi vous accéderez à une page récapitulative des détails de la réservation.</br>
          Vous pourrez alors valider celle-ci ou l'annuler.
        </p>
        <img class="img-fluid w-100 my-3" :src="imgUrls.detailcourse" alt="Détails de la course">
        <hr />
  
        <!-- Recherche de coursier -->
        <h2 class="text-decoration-underline mt-5 mb-2">Recherche de coursier</h2>
        <p>Après avoir validé les détails de la course, votre demande de course sera transmise à tous les coursiers<br> du secteur,
          vous serez alors en attente de coursier.
        </p>
        <div class="row my-4">
          <div class="col-12 d-flex justify-content-center">
            <img class="img-fluid" :src="imgUrls.chauffeurwait" alt="Attente coursier">
          </div>
          <p class="ml-4 mt-4">Lorsqu'un coursier aura accepté votre course, l'interface changera et vous pourrez valider le coursier.</p>
          <div class="col-12 d-flex justify-content-center mt-2">
            <img class="img-fluid" :src="imgUrls.chauffeurfind" alt="Coursier trouvé">
          </div>
        </div>
        <hr />
  
        <!-- Fin de la course -->
        <h2 class="text-decoration-underline mt-4 mb-2">Fin de la course</h2>
        <p>Une fois votre course terminée vous accéderez à ce petit bandeau afin de valider la fin de votre course.</p>
        <img class="img-fluid w-100 my-3" :src="imgUrls.fincourse" alt="Fin de la course">
        <hr />
  
        <!-- Page facture -->
        <h2 class="text-decoration-underline mt-4 mb-2">Page facture</h2>
        <p>Enfin vous pourrez évaluer la course en la notant et en donnant un pourboire.</br>
          Vous pourrez ensuite recevoir votre facture afin de mieux comprendre la prestation.
        </p>
        <img class="img-fluid my-3" :src="imgUrls.facture" alt="Facture">
      </div>
    </section>
  </template>
  
  <script>
  export default {
    data() {
      return {
        imgUrls: {
          navuber: 'public/images/guide-uber/navuber.png',
          loginuber: 'public/images/guide-uber/loginuber.png',
          login: 'public/images/guide-uber/login.png',
          register: 'public/images/guide-uber/register.png',
          accueil: 'public/images/guide-uber/accueiluber.png',
          prestation: 'public/images/guide-uber/prestation.png',
          detailcourse: 'public/images/guide-uber/detailcourse.png',
          chauffeurwait: 'public/images/guide-uber/chauffeurwait.png',
          chauffeurfind: 'public/images/guide-uber/chauffeurfind.png',
          fincourse: 'public/images/guide-uber/fincourse.png',
          facture: 'public/images/guide-uber/facture.png',
        },
        links: {
          login: '/login',
          register: '/register/passenger',
          home: '/',
        },
      };
    },
  };
  </script>
  
  <style scoped>
body {
    background-color: #f9f9f9;
}

h2 {
    font-weight: 700;
}
p {
    font-size: 1.2rem;
    font-weight: 700;
}
.guide-uber img {
    border-radius: 5px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}
hr {
    border: none;
    border-top: 3px solid #333;
    color: #333;
    overflow: visible;
    text-align: center;
    height: 5px;
}
  </style>
  