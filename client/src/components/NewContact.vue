<script lang="ts" setup>
import { contactStorage } from '@/storage/contactStorage.ts'
import { type Ref, ref } from 'vue'
import ContactEditor from '@/components/ContactEditor.vue'
import type { contactType } from '@/types/contact.ts'

const editedContact: Ref<contactType | null> = ref(null)

function openEditor() {
    contactStorage.newContact()
}
</script>

<template>
    <button v-if="!editedContact" @click.prevent="openEditor">new</button>
    <ContactEditor v-if="editedContact" />
</template>
