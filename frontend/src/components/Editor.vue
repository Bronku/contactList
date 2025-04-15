<script setup>
const props = defineProps({
  contact: Object,
});
const emit = defineEmits(["close"]);
</script>

<template>
  <h2>Edit</h2>
  <ul>
    <li>
      <span>businessCategory</span> <br /><span>{{
        contact.businessCategory
      }}</span>
    </li>
    <li>
      <span>category</span> <br /><span>{{ contact.category }}</span>
    </li>
    <li>
      <span>dateOfBirth</span> <br /><span>{{ contact.dateOfBirth }}</span>
    </li>
    <li>
      <span>email</span> <br /><span>{{ contact.email }}</span>
    </li>
    <li>
      <span>id</span> <br /><span>{{ contact.id }}</span>
    </li>
    <li>
      <span>name</span> <br /><span>{{ contact.name }}</span>
    </li>
    <li>
      <span>password</span> <br /><span>{{ contact.password }}</span>
    </li>
    <li>
      <span>phoneNumber</span> <br /><span>{{ contact.phoneNumber }}</span>
    </li>
    <li>
      <span>surname</span> <br /><span>{{ contact.surname }}</span>
    </li>
  </ul>
  <button @click.prevent="emit('close')">close</button>
</template>
