<script setup lang="ts" xmlns:Details="http://www.w3.org/1999/html">
import {ref} from "vue";
import ContactDetails from "@/components/ContactDetails.vue";

defineProps({
  contacts: Array,
})

const selectedContact = ref(null)
function selectContact(contact) {
  selectedContact.value = contact;
}
function closeContact(){
  selectedContact.value = null;
}
</script>

<template>
  <div>
  <ContactDetails v-if="selectedContact" :contact="selectedContact" @close="closeContact"/>
  <table>
    <thead>
      <tr>
        <th>Id</th>
        <th>Name</th>
        <th>Surname</th>
        <th>PhoneNumber</th>
      </tr>
    </thead>
    <tr v-for="contact in contacts" :key="contact.id">
      <th>
        <button @click.prevent="selectContact(contact)">
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
