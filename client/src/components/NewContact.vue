<script lang="ts" setup>
import {contactStorage} from "@/storage/contactStorage.ts";
import {ref} from "vue";
import Editor from "@/components/Editor.vue";

const editedContact = ref(null);

function openEditor() {
    editedContact.value = contactStorage.newContact();
}

function closeEditor() {
    editedContact.value = null;
}

</script>

<template>
    <button v-if="!editedContact" @click.prevent="openEditor">new</button>
    <Editor v-if="editedContact" :contact="editedContact" :editing="false" @close="closeEditor"/>
</template>
