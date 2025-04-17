<script lang="ts" setup>
import {ref} from "vue";
import {contactStorage} from "@/storage/contactStorage.ts";

const isSubmitting = ref(false);

function closeEditor() {
    contactStorage.editingContact.value = false;
    contactStorage.selectedContact.value = null;
}

async function handleSubmit() {
    if (isSubmitting.value) return;
    isSubmitting.value = true;
    try {
        await contactStorage.saveContact();
    } catch (e) {
        console.error(e);
    }
    isSubmitting.value = false;
}
</script>

<template>
    <h2> Edit</h2>
    <form v-if="contactStorage.selectedContact.value" @submit.prevent="handleSubmit">
        <ul>
            <li>
                <label>Business Category</label><br/>
                <input v-model="contactStorage.selectedContact.value.businessCategory"
                       type="number"/>
            </li>
            <li>
                <label>Category</label><br/>
                <input v-model="contactStorage.selectedContact.value.category" type="number"/>
            </li>
            <li>
                <label>Date of Birth</label><br/>
                <input v-model="contactStorage.selectedContact.value.dateOfBirth" type="date"/>
            </li>
            <li>
                <label>Email</label><br/>
                <input v-model="contactStorage.selectedContact.value.email" type="email"/>
            </li>
            <li>
                <label>ID</label><br/>
                <input v-model="contactStorage.selectedContact.value.id" type="number"/>
            </li>
            <li>
                <label>Name</label><br/>
                <input v-model="contactStorage.selectedContact.value.name"/>
            </li>
            <li>
                <label>Password</label><br/>
                <input v-model="contactStorage.selectedContact.value.password" type="password"/>
            </li>
            <li>
                <label>Phone Number</label><br/>
                <input v-model="contactStorage.selectedContact.value.phoneNumber"/>
            </li>
            <li>
                <label>Surname</label><br/>
                <input v-model="contactStorage.selectedContact.value.surname"/>
            </li>
        </ul>
        <button :disabled="isSubmitting" type="submit">
            {{ isSubmitting ? "Saving" : "Save" }}
        </button>
        <button :disabled="isSubmitting" @click.prevent="contactStorage.deleteContact()">
            delete
        </button>
        <button type="button" @click="closeEditor">Close</button>
    </form>
</template>
