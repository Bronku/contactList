<script setup>
import { reactive, watch } from "vue";
const props = defineProps({
  contact: Object,
  creatingContact: Boolean,
});

const emit = defineEmits(["close", "reload"]);
const form = reactive({});
watch(
  () => props.contact,
  (newContact) => {
    Object.assign(form, newContact);
  },
  { immediate: true },
);
async function handleSubmit() {
  try {
    console.log(JSON.parse(JSON.stringify(form)));
    const response = await fetch("http://localhost:8080/api/User", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(form),
    });

    if (!response.ok) {
      const errorMessage = await response.text();
      throw new Error(`Failed to save contact, ${errorMessage}`);
    }
    alert("Contact saved!");
    emit("reload");
    emit("close");
  } catch (error) {
    console.error(error);
  }
}
</script>

<template>
  <h2>{{ creatingContact ? "New" : "Edit" }}</h2>
  <form @submit.prevent="handleSubmit">
    <ul>
      <li>
        <label>Business Category</label><br />
        <input v-model="form.businessCategory" type="number" />
      </li>
      <li>
        <label>Category</label><br />
        <input v-model="form.category" type="number" />
      </li>
      <li>
        <label>Date of Birth</label><br />
        <input v-model="form.dateOfBirth" type="date" />
      </li>
      <li>
        <label>Email</label><br />
        <input v-model="form.email" type="email" />
      </li>
      <li>
        <label>ID</label><br />
        <input v-model="form.id" :readonly="!creatingContact" type="number" />
      </li>
      <li>
        <label>Name</label><br />
        <input v-model="form.name" />
      </li>
      <li>
        <label>Password</label><br />
        <input v-model="form.password" type="password" />
      </li>
      <li>
        <label>Phone Number</label><br />
        <input v-model="form.phoneNumber" />
      </li>
      <li>
        <label>Surname</label><br />
        <input v-model="form.surname" />
      </li>
    </ul>
    <button type="submit">
      {{ creatingContact ? "Create" : "Update" }}
    </button>
    <button type="button" @click="emit('close')">Close</button>
  </form>
</template>
