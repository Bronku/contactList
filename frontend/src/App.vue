<script setup>
import { ref, onMounted } from "vue";
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
</script>

<template>
  <header>
    <h1>Hello World</h1>
  </header>

  <main>
    <table v-if="data">
      <thead>
        <tr>
          <th>Id</th>
          <th>Name</th>
          <th>Surname</th>
          <th>PhoneNumber</th>
        </tr>
      </thead>
      <tr v-for="item in data" :key="item.id">
        <th>{{ item.id }}</th>
        <th>{{ item.name }}</th>
        <th>{{ item.surname }}</th>
        <th>{{ item.phoneNumber }}</th>
      </tr>
    </table>
    <p v-else-if="error">Error fetching data: {{ error }}</p>
  </main>
</template>
