<script setup>
import { ref, onMounted, provide } from "vue";
import Table from "./components/Table.vue";
const data = ref(null);
const error = ref(null);
const fetchData = async () => {
  try {
    const response = await fetch("http://localhost:8080/api/User");
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    data.value = await response.json();
    console.log(data.value);
  } catch (e) {
    error.value = e;
  }
};

onMounted(() => {
  fetchData();
});
provide("data", data);
</script>

<template>
  <header>
    <h1>Hello World</h1>
  </header>

  <main>
    <Table v-if="data" />
    <p v-else-if="error">Error fetching data: {{ error }}</p>
  </main>
</template>
