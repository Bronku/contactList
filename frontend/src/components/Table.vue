<script setup>
defineProps({
  contacts: Array,
});
const emit = defineEmits(["selectContact"]);
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
    <tr v-for="contact in contacts" :key="contact.id">
      <th>
        <button @click.prevent="emit('selectContact', contact)">
          {{ contact.id }}
        </button>
      </th>
      <th>{{ contact.name }}</th>
      <th>{{ contact.surname }}</th>
      <th>{{ contact.phoneNumber }}</th>
    </tr>
  </table>
</template>
