<script lang="ts" setup>
import LoginDialog from '@/components/LoginDialog.vue'
import ContactList from '@/components/ContactList.vue'
import { tokenStorage } from '@/storage/tokenStorage.ts'
import { contactStorage } from '@/storage/contactStorage.ts'
import { optionStorage } from '@/storage/optionStorage.ts'
import RegisterUser from '@/components/RegisterUser.vue'

contactStorage.refresh()
optionStorage.refresh()
</script>

<template>
    <main>
        <RegisterUser v-if="tokenStorage.isAuthenticated()" />
        <LoginDialog v-if="!tokenStorage.isAuthenticated()" />
        <button v-if="tokenStorage.isAuthenticated()" @click.prevent="tokenStorage.clearToken">
            logout
        </button>
        <ContactList />
    </main>
</template>
