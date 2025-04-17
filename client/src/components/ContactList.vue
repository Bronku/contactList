<script lang="ts" setup>
import ContactDetails from "@/components/ContactDetails.vue";
import type {contactType} from "@/types/contact.ts";
import {contactStorage} from "@/storage/contactStorage.ts";
import ContactEditor from "@/components/ContactEditor.vue";
</script>

<template>
    <div>
        <ContactDetails
            v-if="contactStorage.selectedContact.value && !contactStorage.editingContact.value"/>
        <ContactEditor v-if="contactStorage.editingContact.value"/>
        <button v-if="!contactStorage.editingContact.value && !contactStorage.selectedContact.value"
                @click.prevent="contactStorage.newContact()">new
        </button>
        <table>
            <thead>
            <tr>
                <th>Id</th>
                <th>Name</th>
                <th>Surname</th>
                <th>PhoneNumber</th>
            </tr>
            </thead>
            <tr v-for="contact in contactStorage.contacts.value as contactType[]"
                :key="contact.id">
                <th>
                    <button @click.prevent="contactStorage.selectContact(contact)">
                        {{ contact.id }}
                    </button>
                </th>
                <th>{{ contact.name }}</th>
                <th>{{ contact.surname }}</th>
                <th>{{ contact.phoneNumber }}</th>
            </tr>
        </table>
    </div>
</template>
