<script setup>
import { ref } from "vue";
import { sendLogin } from "@/stores/auth";
const username = ref("");
const password = ref("");
async function login() {
  console.log("logging in");
  const success = await sendLogin(username.value, password.value);
  console.log(success);
}
</script>
<template>
  <form @submit.prevent="login">
    <input v-model="username" placeholder="username" type="text" />
    <input v-model="password" placeholder="password" type="password" />
    <button type="submit">login</button>
  </form>
</template>
