<script lang="ts" setup>
import {ref} from "vue";
import {contactStorage} from "@/storage/contactStorage.ts";

const props = defineProps({
    contact: Object,
    editing: Boolean
})
const isSubmitting = ref(false);
const emit = defineEmits(["close"]);

async function handleSubmit() {
    if (isSubmitting.value) {
        return;
    }
    isSubmitting.value = true;
    if (props.editing) {
        await contactStorage.updateContact(props.contact);
    } else {
        await contactStorage.createContact(props.contact);
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
                <input v-model="contact.businessCategory" type="number"/>
            </li>
            <li>
                <label>Category</label><br/>
                <input v-model="contact.category" type="number"/>
            </li>
            <li>
                <label>Date of Birth</label><br/>
                <input v-model="contact.dateOfBirth" type="date"/>
            </li>
            <li>
                <label>Email</label><br/>
                <input v-model="contact.email" type="email"/>
            </li>
            <li>
                <label>ID</label><br/>
                <input v-model="contact.id" :readonly="contact.id != 0" type="number"/>
            </li>
            <li>
                <label>Name</label><br/>
                <input v-model="contact.name"/>
            </li>
            <li>
                <label>Password</label><br/>
                <input v-model="contact.password" type="password"/>
            </li>
            <li>
                <label>Phone Number</label><br/>
                <input v-model="contact.phoneNumber"/>
            </li>
            <li>
                <label>Surname</label><br/>
                <input v-model="contact.surname"/>
            </li>
        </ul>
        <button :disabled="isSubmitting" type="submit">
            {{ isSubmitting ? "Saving" : "Save" }}
        </button>
        <button type="button" @click="emit('close')">Close</button>
    </form>
</template>
