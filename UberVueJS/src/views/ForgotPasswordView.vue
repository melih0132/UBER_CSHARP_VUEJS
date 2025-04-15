<template>
    <div class="container mt-5">
        <h1 class="text-center mb-4">Mot de passe oublié ?</h1>
        <h2 class="text-center mb-4">Fallait pas oublier 🤡</h2>
    </div>
</template>

<style scoped>
.container {
    max-width: 400px;
    margin: auto;
}

h1 {
    font-size: 2rem;
    font-weight: 700;
}

h2 {
    font-size: 1rem;
    font-weight: 600;
}

.form-reset {
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

.btn-reset {
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

.btn-reset:hover {
    background: #333;
}

.btn-reset:disabled {
    background: #ccc;
    cursor: not-allowed;
}

.success-message {
    color: green;
    text-align: center;
    margin-top: 1rem;
}

.error-message {
    color: red;
    text-align: center;
    margin-top: 1rem;
}
</style>