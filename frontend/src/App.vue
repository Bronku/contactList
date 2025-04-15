<script setup>
import { ref, onMounted } from "vue";
import Table from "./components/Table.vue";
import Details from "./components/Details.vue";
const contacts = ref(null);
const error = ref(null);
const selectedContact = ref(null);

const fetchData = async () => {
  try {
    const response = await fetch("http://localhost:8080/api/User");
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    contacts.value = await response.json();
  } catch (e) {
    error.value = e;
  }
};

onMounted(() => {
  fetchData();
});

const selectContact = (item) => {
  console.log(item);
};

const closeDetails = () => {
  selectedContact.value = null;
};
</script>

<template>
  <header>
    <h1>Hello World</h1>
  </header>

  <main>
    <Details
      v-if="selectedContact"
      :contact="selectedContact"
      @close="closeDetails"
    />
    <Table
      v-if="contacts"
      :contacts="contacts"
      @select-contact="selectContact"
    />
    <p v-else-if="error">Error fetching data: {{ error }}</p>
  </main>
</template>
