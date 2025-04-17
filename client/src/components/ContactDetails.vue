<script lang="ts" setup>
import {tokenStorage} from "@/storage/tokenStorage.ts";
import {contactStorage} from "@/storage/contactStorage.ts";
import {ref} from "vue";
import Editor from "@/components/Editor.vue";

const props = defineProps({
    contact: Object,
});
const emit = defineEmits(["close"]);

const editedContact = ref(null)

function editContact() {
    editedContact.value = contactStorage.clone(props.contact);
}

function closeEditor() {
    editedContact.value = null;
    emit("close");
}
</script>


<template>
    <Editor v-if="editedContact" :contact="editedContact" :editing="true"
            @close="closeEditor"/>
    <h2>Details</h2>
    <ul>
        <li>
            <span>businessCategory</span> <br/><span>{{
                contact.businessCategory
            }}</span>
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
        @click.prevent="editContact"
    >
        edit
    </button>
    <button
        :disabled="!tokenStorage.isAuthenticated()"
        @click.prevent="contactStorage.deleteContact(contact)"
    >
        delete
    </button>
</template>
