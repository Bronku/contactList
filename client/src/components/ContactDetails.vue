<script lang="ts" setup>
import {tokenStorage} from "@/storage/tokenStorage.ts";
import {contactStorage} from "@/storage/contactStorage.ts";
import type {contactType} from "@/types/contact.ts";
import {ref, type Ref} from "vue";
import ContactEditor from "@/components/ContactEditor.vue";

const props = defineProps<{
    contact: contactType | null
}>();
const emit = defineEmits(["close"]);

const editedContact: Ref<contactType | null> = ref(null)

function editContact() {
    if (!props.contact) {
        return;
    }
    editedContact.value = props.contact;
}

function closeEditor() {
    editedContact.value = null;
    emit("close");
}

function deleteContact() {
    contactStorage.deleteContact(props.contact);
    emit("close")
}
</script>


<template>
    <ContactEditor v-if="editedContact" :contact="editedContact" :editing="true"
                   @close="closeEditor"/>
    <h2>Details</h2>
    <ul v-if="contact">
        <li>
            <span>businessCategory</span> <br/><span>{{ contact.businessCategory }}</span>
        </li>
        <li>
            <span>category</span> <br/><span>{{ contact.category }}</span>
        </li>
        <li>
            <span>dateOfBirth</span> <br/><span>{{ contact.dateOfBirth }}</span>
        </li>
        <li>
            <span>email</span> <br/><span>{{ contact.email }}</span>
        </li>
        <li>
            <span>id</span> <br/><span>{{ contact.id }}</span>
        </li>
        <li>
            <span>name</span> <br/><span>{{ contact.name }}</span>
        </li>
        <li>
            <span>password</span> <br/><span>{{ contact.password }}</span>
        </li>
        <li>
            <span>phoneNumber</span> <br/><span>{{ contact.phoneNumber }}</span>
        </li>
        <li>
            <span>surname</span> <br/><span>{{ contact.surname }}</span>
        </li>
    </ul>
    <button @click.prevent="emit('close')">close</button>
    <button
        :disabled="!tokenStorage.isAuthenticated()"
        @click.prevent="editContact">
        edit
    </button>
    <button
        :disabled="!tokenStorage.isAuthenticated()"
        @click.prevent="deleteContact"
    >
        delete
    </button>
</template>
