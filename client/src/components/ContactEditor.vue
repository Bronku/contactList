<script lang="ts" setup>
import { ref } from 'vue'
import { contactStorage } from '@/storage/contactStorage.ts'
import { optionStorage } from '@/storage/optionStorage.ts'

const isSubmitting = ref(false)

function closeEditor() {
    contactStorage.editingContact.value = false
    contactStorage.selectedContact.value = null
}

async function handleSubmit() {
    if (isSubmitting.value) return
    isSubmitting.value = true
    try {
        await contactStorage.saveContact()
    } catch (e) {
        console.error(e)
    }
    isSubmitting.value = false
}
</script>

<template>
    <h2>Edit</h2>
    <form v-if="contactStorage.selectedContact.value" @submit.prevent="handleSubmit">
        <input v-model="contactStorage.selectedContact.value.id" hidden type="number" />
        <ul>
            <li>
                <label>Category</label><br />
                <select v-model="contactStorage.selectedContact.value.contactCategoryId">
                    <option disabled value="0">Please select a category</option>
                    <option
                        v-for="option in optionStorage.categories.value"
                        :key="option.id"
                        :value="option.id"
                    >
                        {{ option.name }}
                    </option>
                </select>
            </li>
            <li v-if="contactStorage.selectedContact.value.contactCategoryId == 1">
                <label>Business category</label><br />
                <select v-model="contactStorage.selectedContact.value.businessCategoryId">
                    <option disabled value="0">Please select a category</option>
                    <option
                        v-for="option in optionStorage.businessCategories.value"
                        :key="option.id"
                        :value="option.id"
                    >
                        {{ option.name }}
                    </option>
                </select>
            </li>
            <li v-if="contactStorage.selectedContact.value.contactCategoryId == 3">
                <label>CategoryName</label><br />
                <input v-model="contactStorage.selectedContact.value.otherCategory" />
            </li>
            <li>
                <label>Date of Birth</label><br />
                <input v-model="contactStorage.selectedContact.value.dateOfBirth" type="date" />
            </li>
            <li>
                <label>Email</label><br />
                <input v-model="contactStorage.selectedContact.value.email" type="email" />
            </li>
            <li>
                <label>Name</label><br />
                <input v-model="contactStorage.selectedContact.value.name" />
            </li>
            <li>
                <label>Password</label><br />
                <input v-model="contactStorage.selectedContact.value.password" type="password" />
            </li>
            <li>
                <label>Phone Number</label><br />
                <input v-model="contactStorage.selectedContact.value.phoneNumber" />
            </li>
            <li>
                <label>Surname</label><br />
                <input v-model="contactStorage.selectedContact.value.surname" />
            </li>
        </ul>
        <button :disabled="isSubmitting" type="submit">
            {{ isSubmitting ? 'Saving' : 'Save' }}
        </button>
        <button :disabled="isSubmitting" @click.prevent="contactStorage.deleteContact()">
            delete
        </button>
        <button type="button" @click="closeEditor">Close</button>
    </form>
</template>
