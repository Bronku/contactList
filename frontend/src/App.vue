<script setup>
import { ref, onMounted } from "vue";
const data = ref(null);
const loading = ref(null);
const error = ref(null);
const fetchData = async () => {
  try {
    const response = await fetch("http://localhost:8080/api/User");
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    data.value = await response.json();
  } catch (e) {
    error.value = e;
  } finally {
    loading.value = false;
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
    <ul v-if="data">
      <li v-for="item in data" :key="item.id">
        {{ item.name }} {{ item.phoneNumber }}
      </li>
    </ul>
    <p v-else-if="loading">Loading data...</p>
    <p v-else-if="error">Error fetching data: {{ error }}</p>
  </main>
</template>
