<script setup>
import { authStore } from "@/stores/auth";
import {ref} from "vue";
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;
const props = defineProps({
  contact: Object,
});
const emit = defineEmits(["close", "editContact", "reload"]);
const isSubmitting = ref(false);
async function deleteContact() {
  if (isSubmitting.value) {
    return;
  }
  isSubmitting.value = true;
  try {
    const response = await fetch(`${API_BASE_URL}/Contact/${props.contact.id}`, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${authStore.token}`,
      }
    });

    if (!response.ok) {
      const errorMessage = await response.text();
      throw new Error(`Failed to delete contact, ${errorMessage}`);
    }
    alert("Contact deleted!");
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
  <h2>Details</h2>
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
  <button
    @click.prevent="emit('editContact', contact)"
    :disabled="!authStore.isAuthenticated()"
  >
    edit
  </button>
  <button
      @click.prevent="deleteContact"
      :disabled="!authStore.isAuthenticated()"
  >
    delete
  </button>
</template>
