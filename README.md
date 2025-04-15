# UBER AVEC API CORE & VUE JS - Full Stack

## Overview
This project integrates Uber's API with Vue.js to create a user-friendly ride-sharing application.

## Features
- Uber Eats
- User authentication with JWT
- Ride history and booking

## Getting Started
### Prerequisites
- Node.js
- Vue CLI

### Installation
1. Clone the repository:
  ```bash
  git clone https://github.com/melih0132/UBER_S4.git
  ```

2. Install dependencies:
  ```bash
  npm install
  ```

3. Start the development server:
  ```bash
  npm run serve
  ```

## Usage
### API Integration
- Configure the Uber API keys in the `config.js` file.
- Ensure the API endpoints are correctly set up.

### Vue.js Components
- The project uses Vue.js components for modular development.
- Key components include `RideRequest.vue`, `RideHistory.vue`, and `UserAuth.vue`.

### Directory Structure
Ensure the repository has a clear directory structure. Here’s a suggested structure:
```
Directory structure:
└── melih0132-uber_s4/
    ├── README.md
    ├── UberApi/
    │   ├── README.md
    │   ├── UberApi.sln
    │   ├── .gitattributes
    │   ├── .gitignore
    │   ├── UberApi/
    │   │   ├── appsettings.Development.json
    │   │   ├── appsettings.json
    │   │   ├── Program.cs
    │   │   ├── UberApi.csproj
    │   │   ├── UberApi.http
    │   │   ├── Controllers/
    │   │   │   ├── AdressesController.cs
    │   │   │   ├── CarteBancaireController.cs
    │   │   │   ├── CategoriePrestationsController.cs
    │   │   │   ├── CategorieProduitsController.cs
    │   │   │   ├── ClientsController.cs
    │   │   │   ├── CodesPostauxController.cs
    │   │   │   ├── CommandesController.cs
    │   │   │   ├── CoursesController.cs
    │   │   │   ├── CoursiersController.cs
    │   │   │   ├── EntreprisesController.cs
    │   │   │   ├── EntretiensController.cs
    │   │   │   ├── EtablissementController.cs
    │   │   │   ├── EtablissementsController.cs
    │   │   │   ├── FacturesController.cs
    │   │   │   ├── LieuxFavorisController.cs
    │   │   │   ├── LivreursController.cs
    │   │   │   ├── LoginController.cs
    │   │   │   ├── PaniersController.cs
    │   │   │   ├── PaysController.cs
    │   │   │   ├── ProduitsController.cs
    │   │   │   ├── RestaurateursController.cs
    │   │   │   ├── TypePrestationsController.cs
    │   │   │   ├── UserController.cs
    │   │   │   ├── VehiculesController.cs
    │   │   │   └── VillesController.cs
    │   │   ├── Migrations/
    │   │   │   ├── 20250317083247_CreationBDUberApiTD1.cs
    │   │   │   ├── 20250317083247_CreationBDUberApiTD1.Designer.cs
    │   │   │   └── S221UberContextModelSnapshot.cs
    │   │   ├── Models/
    │   │   │   ├── Policies.cs
    │   │   │   ├── User.cs
    │   │   │   ├── DataManager/
    │   │   │   │   ├── AdresseManager.cs
    │   │   │   │   ├── CarteBancairesManager.cs
    │   │   │   │   ├── CategoriePrestationManager.cs
    │   │   │   │   ├── CategorieProduitManager.cs
    │   │   │   │   ├── ClientManager.cs
    │   │   │   │   ├── CodePostalManager.cs
    │   │   │   │   ├── CommandeManager.cs
    │   │   │   │   ├── CourseManager.cs
    │   │   │   │   ├── CoursierManager.cs
    │   │   │   │   ├── EntrepriseManager.cs
    │   │   │   │   ├── EntretienManager.cs
    │   │   │   │   ├── EtablissementManager.cs
    │   │   │   │   ├── FactureManager.cs
    │   │   │   │   ├── LieuFavoriManager.cs
    │   │   │   │   ├── LivreurManager.cs
    │   │   │   │   ├── PanierManager.cs
    │   │   │   │   ├── PaysManager.cs
    │   │   │   │   ├── ProduitManager.cs
    │   │   │   │   ├── RestaurateurManager.cs
    │   │   │   │   ├── TypePrestationManager.cs
    │   │   │   │   ├── VehiculeManager.cs
    │   │   │   │   └── VilleManager.cs
    │   │   │   ├── EntityFramework/
    │   │   │   │   ├── Adresse.cs
    │   │   │   │   ├── CarteBancaire.cs
    │   │   │   │   ├── CategoriePrestation.cs
    │   │   │   │   ├── CategorieProduit.cs
    │   │   │   │   ├── Client.cs
    │   │   │   │   ├── CodePostal.cs
    │   │   │   │   ├── Commande.cs
    │   │   │   │   ├── Contient2.cs
    │   │   │   │   ├── Course.cs
    │   │   │   │   ├── Coursier.cs
    │   │   │   │   ├── Entreprise.cs
    │   │   │   │   ├── Entretien.cs
    │   │   │   │   ├── Etablissement.cs
    │   │   │   │   ├── Facture.cs
    │   │   │   │   ├── GestionEtablissement.cs
    │   │   │   │   ├── Horaires.cs
    │   │   │   │   ├── LieuFavori.cs
    │   │   │   │   ├── Livreur.cs
    │   │   │   │   ├── Otp.cs
    │   │   │   │   ├── Panier.cs
    │   │   │   │   ├── Pays.cs
    │   │   │   │   ├── Produit.cs
    │   │   │   │   ├── ReglementSalaire.cs
    │   │   │   │   ├── Reservation.cs
    │   │   │   │   ├── ResponsableEnseigne.cs
    │   │   │   │   ├── Restaurateur.cs
    │   │   │   │   ├── S221UberContext.cs
    │   │   │   │   ├── TypePrestation.cs
    │   │   │   │   ├── Vehicule.cs
    │   │   │   │   ├── Velo.cs
    │   │   │   │   ├── VeloReservation.cs
    │   │   │   │   └── Ville.cs
    │   │   │   └── Repository/
    │   │   │       ├── ICarteBancaireRepository.cs
    │   │   │       ├── ICourseRepository.cs
    │   │   │       ├── IDataRepository.cs
    │   │   │       └── IPanierRepository.cs
    │   │   ├── Properties/
    │   │   │   └── launchSettings.json
    │   │   └── Views/
    │   │       ├── Clients/
    │   │       │   ├── Create.cshtml
    │   │       │   ├── Delete.cshtml
    │   │       │   ├── Details.cshtml
    │   │       │   ├── Edit.cshtml
    │   │       │   └── Index.cshtml
    │   │       ├── Commandes/
    │   │       │   ├── Create.cshtml
    │   │       │   ├── Delete.cshtml
    │   │       │   ├── Details.cshtml
    │   │       │   ├── Edit.cshtml
    │   │       │   └── Index.cshtml
    │   │       ├── Courses/
    │   │       │   ├── Create.cshtml
    │   │       │   ├── Delete.cshtml
    │   │       │   ├── Details.cshtml
    │   │       │   ├── Edit.cshtml
    │   │       │   └── Index.cshtml
    │   │       ├── Produits/
    │   │       │   ├── Create.cshtml
    │   │       │   ├── Delete.cshtml
    │   │       │   ├── Details.cshtml
    │   │       │   ├── Edit.cshtml
    │   │       │   └── Index.cshtml
    │   │       ├── Restaurateurs/
    │   │       │   ├── Create.cshtml
    │   │       │   ├── Delete.cshtml
    │   │       │   ├── Details.cshtml
    │   │       │   ├── Edit.cshtml
    │   │       │   └── Index.cshtml
    │   │       ├── Shared/
    │   │       │   └── _ValidationScriptsPartial.cshtml
    │   │       ├── TypePrestations/
    │   │       │   ├── Create.cshtml
    │   │       │   ├── Delete.cshtml
    │   │       │   ├── Details.cshtml
    │   │       │   ├── Edit.cshtml
    │   │       │   └── Index.cshtml
    │   │       └── Vehicules/
    │   │           ├── Create.cshtml
    │   │           ├── Delete.cshtml
    │   │           ├── Details.cshtml
    │   │           ├── Edit.cshtml
    │   │           └── Index.cshtml
    │   └── UberApiTests/
    │       ├── UberApiTests.csproj
    │       └── Controllers/
    │           ├── AdressesControllerTests.cs
    │           ├── CarteBancairesControllerTests.cs
    │           ├── CategoriePrestationsControllerTests.cs
    │           ├── ClientsControllerTests.cs
    │           ├── CommandesControllerTests.cs
    │           ├── CoursesControllerTests.cs
    │           ├── CoursiersControllerTests.cs
    │           ├── EntreprisesControllerTests.cs
    │           ├── EtablissementsControllerTests.cs
    │           ├── ProduitsControllerTests.cs
    │           ├── TypePrestationsControllerTests.cs
    │           └── VillesControllerTests.cs
    └── UberVueJS/
        ├── README.md
        ├── index.html
        ├── jsconfig.json
        ├── package-lock.json
        ├── package.json
        ├── vite.config.js
        ├── .gitignore
        ├── public/
        │   └── images/
        │       └── guide-uber/
        ├── src/
        │   ├── App.vue
        │   ├── axios.js
        │   ├── main.js
        │   ├── assets/
        │   │   ├── leaflet.js
        │   │   └── main.css
        │   ├── components/
        │   │   ├── HomeContent.vue
        │   │   ├── MapView.vue
        │   │   └── Navbar.vue
        │   ├── router/
        │   │   └── index.js
        │   ├── services/
        │   │   ├── adresseService.js
        │   │   ├── carteBancaireService.js
        │   │   ├── categoriePrestationService.js
        │   │   ├── clientService.js
        │   │   ├── commandeService.js
        │   │   ├── courseService.js
        │   │   ├── coursierService.js
        │   │   ├── etablissementService.js
        │   │   ├── panierService.js
        │   │   ├── produitService.js
        │   │   ├── typePrestationServices.js
        │   │   └── villesService.js
        │   ├── stores/
        │   │   ├── counter.js
        │   │   ├── rideStore.js
        │   │   └── userStore.js
        │   └── views/
        │       ├── BesoinAideView.vue
        │       ├── CarteBancaireView.vue
        │       ├── ChoixCarteView.vue
        │       ├── ChoixLivraisonView.vue
        │       ├── CommandesView.vue
        │       ├── CourseView.vue
        │       ├── DetailCourseView.vue
        │       ├── DetailEtablissementView.vue
        │       ├── EtablissementsVilleView.vue
        │       ├── ForgotPasswordView.vue
        │       ├── HomeView.vue
        │       ├── LoginView.vue
        │       ├── MyAccountView.vue
        │       ├── PanierView.vue
        │       ├── PrestationView.vue
        │       ├── RegisterView.vue
        │       ├── SearchDriverView.vue
        │       └── UberEatsView.vue
        └── tests/
            └── tests.test.js

```

### Issues and Pull Requests
Encourage users to report issues and submit pull requests. This will help improve the project and make it more collaborative.

### Releases
Consider creating releases for different versions of your project. This helps users track changes and updates.

### Packages
If you plan to publish any packages, make sure to follow the guidelines for publishing on npm or other package managers.

By following these suggestions, you can make your repository more complete and user-friendly. If you need further assistance with specific parts of the project, feel free to ask!
