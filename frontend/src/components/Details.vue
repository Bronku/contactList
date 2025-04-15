<script setup>
import { inject } from "vue";
const selected = inject("selected");
</script>

<template>
  <span>
    {{ selected.value.name }} {{ selected.value.surname }}
    {{ selected.value.phoneNumber }}
  </span>
  <button @click.prevent="selected.value = null">close</button>
</template>
