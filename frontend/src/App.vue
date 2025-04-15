<script setup>
import { ref, onMounted } from "vue";
import Table from "./components/Table.vue";
import Details from "./components/Details.vue";
import Editor from "./components/Editor.vue";
const contacts = ref(null);
const error = ref(null);
const selectedContact = ref(null);
const editedContact = ref(null);
const creatingContact = ref(false);

async function fetchData() {
  try {
    const response = await fetch("http://localhost:8080/api/User");
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    contacts.value = await response.json();
  } catch (e) {
    error.value = e;
  }
}

function selectContact(item) {
  selectedContact.value = item;
}

function closeDetails() {
  selectedContact.value = null;
}

function editContact(item) {
  const contact = {};
  contact.businessCategory = item.businessCategory;
  contact.category = item.category;
  contact.dateOfBirth = item.dateOfBirth;
  contact.email = item.email;
  contact.id = item.id;
  contact.name = item.name;
  contact.password = item.password;
  contact.phoneNumber = item.phoneNumber;
  contact.surname = item.surname;
  editedContact.value = contact;
  creatingContact.value = false;
}

function newContact() {
  const contact = {};
  contact.businessCategory = "";
  contact.category = "";
  contact.dateOfBirth = "";
  contact.email = "";
  contact.id = "";
  contact.name = "";
  contact.password = "";
  contact.phoneNumber = "";
  contact.surname = "";
  editedContact.value = contact;
  creatingContact.value = true;
}

function closeEditor() {
  editedContact.value = null;
}

onMounted(() => {
  fetchData();
});
</script>

<template>
  <header>
    <h1>Hello World</h1>
  </header>

  <main>
    <button v-if="!editedContact" @click.prevent="newContact">new</button>
    <Editor
      v-if="editedContact"
      :contact="editedContact"
      :creating-contact="creatingContact"
      @close="closeEditor"
    />
    <Details
      v-if="selectedContact"
      :contact="selectedContact"
      @close="closeDetails"
      @edit-contact="editContact"
    />
    <Table
      v-if="contacts"
      :contacts="contacts"
      @select-contact="selectContact"
    />
    <p v-else-if="error">Error fetching data: {{ error }}</p>
  </main>
</template>
