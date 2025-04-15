<script setup>
import { inject } from "vue";
const data = inject("data");
</script>

<template>
  <table>
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
</template>
