<script lang="ts" setup>
import { ref } from 'vue'
import { sendRegister } from '@/services/authServices.ts'

const username = ref('')
const password = ref('')

const submitting = ref(false)
const message = ref('')

async function login() {
    submitting.value = true
    message.value = await sendRegister(username.value, password.value)
    submitting.value = false
}
</script>

<template>
    <h2>Register</h2>
    <form @submit.prevent="login">
        <input v-model="username" placeholder="username" type="text" />
        <input v-model="password" placeholder="password" type="password" />
        <button :disabled="submitting" type="submit">{{ submitting ? 'loading' : 'login' }}</button>
        <span>{{ message }}</span>
    </form>
</template>

<style scoped>
span {
    color: red;
}
</style>
