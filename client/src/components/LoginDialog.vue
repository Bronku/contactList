<script setup lang="ts">
import {ref} from "vue";
import {sendLogin} from "@/auth/authServices.ts";

const username = ref("");
const password = ref("");

const loggingIn = ref(false);
const message = ref("");
async function login() {
  loggingIn.value = true;
  message.value = await sendLogin(username.value, password.value);
  loggingIn.value = false;
}

</script>

<template>
  <form @submit.prevent="login">
    <input v-model="username" placeholder="username" type="text"/>
    <input v-model="password" placeholder="password" type="password"/>
    <button type="submit" :disabled="loggingIn">{{loggingIn?"loading":"login"}}</button>
    <span>{{message}}</span>
  </form>
</template>

<style scoped>
span{
  color: red;
}
</style>
