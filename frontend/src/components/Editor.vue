<script setup>
import { reactive, watch, ref } from "vue";
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;
const props = defineProps({
  contact: Object,
  creatingContact: Boolean,
});

const emit = defineEmits(["close", "reload"]);
const form = reactive({});
const isSubmitting = ref(false);
watch(
  () => props.contact,
  (newContact) => {
    Object.assign(form, newContact);
  },
  { immediate: true },
);
async function handleSubmit() {
  if (isSubmitting.value) {
    return;
  }
  isSubmitting.value = true;
  try {
    const response = await fetch(`${API_BASE_URL}/User`, {
      method: props.creatingContact ? "POST" : "PUT",
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
  } finally {
    isSubmitting.value = false;
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
    <button type="submit" :disabled="isSubmitting">
      {{ isSubmitting ? "Saving" : "Save" }}
    </button>
    <button type="button" @click="emit('close')">Close</button>
  </form>
</template>
