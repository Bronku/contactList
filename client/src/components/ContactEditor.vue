<script lang="ts" setup>
import {reactive, ref, watch} from "vue";
import {contactStorage} from "@/storage/contactStorage.ts";
import type {contactType} from "@/types/contact.ts";

const props = defineProps<{
    contact: contactType | null,
    editing: boolean
}>();

const isSubmitting = ref(false);
const emit = defineEmits(["close"]);

const localContact = reactive(contactStorage.newContact());
watch(() => props.contact, (newContact: contactType | null) => {
    Object.assign(localContact, newContact);
}, {immediate: true});

async function handleSubmit() {
    if (isSubmitting.value) {
        return;
    }
    isSubmitting.value = true;
    if (props.editing) {
        await contactStorage.updateContact(localContact);
    } else {
        await contactStorage.createContact(localContact);
    }
    isSubmitting.value = false;
    emit("close");
}
</script>

<template>
    <h2> Edit</h2>
    <form @submit.prevent="handleSubmit">
        <ul>
            <li>
                <label>Business Category</label><br/>
                <input v-model="localContact.businessCategory" type="number"/>
            </li>
            <li>
                <label>Category</label><br/>
                <input v-model="localContact.category" type="number"/>
            </li>
            <li>
                <label>Date of Birth</label><br/>
                <input v-model="localContact.dateOfBirth" type="date"/>
            </li>
            <li>
                <label>Email</label><br/>
                <input v-model="localContact.email" type="email"/>
            </li>
            <li>
                <label>ID</label><br/>
                <input v-model="localContact.id" :readonly="localContact.id != 0" type="number"/>
            </li>
            <li>
                <label>Name</label><br/>
                <input v-model="localContact.name"/>
            </li>
            <li>
                <label>Password</label><br/>
                <input v-model="localContact.password" type="password"/>
            </li>
            <li>
                <label>Phone Number</label><br/>
                <input v-model="localContact.phoneNumber"/>
            </li>
            <li>
                <label>Surname</label><br/>
                <input v-model="localContact.surname"/>
            </li>
        </ul>
        <button :disabled="isSubmitting" type="submit">
            {{ isSubmitting ? "Saving" : "Save" }}
        </button>
        <button type="button" @click="emit('close')">Close</button>
    </form>
</template>
