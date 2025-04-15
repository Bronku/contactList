<script setup>
import { ref, onMounted, provide } from "vue";
import Table from "./components/Table.vue";
import Details from "./components/Details.vue";
const data = ref(null);
const error = ref(null);
const selected = ref({});
provide("data", data);
provide("selected", selected);
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
</script>

<template>
  <header>
    <h1>Hello World</h1>
  </header>

  <main>
    <Details v-if="selected.value" />
    <Table v-if="data" />
    <p v-else-if="error">Error fetching data: {{ error }}</p>
  </main>
</template>
