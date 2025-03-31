<template>
  <h2>Paiement sécurisé</h2>
  <button @click="pay">Payer 10€</button>
</template>

<script>
import { loadStripe } from "@stripe/stripe-js";

export default {
  setup() {
    const stripePromise = loadStripe("clé_publique_stripe");

    const pay = async () => {
      const stripe = await stripePromise;
      stripe.redirectToCheckout({ sessionId: "session_id_stripe" });
    };

    return { pay };
  },
};
</script>
