<script lang="ts" setup>
import { tokenStorage } from '@/storage/tokenStorage.ts'
import { contactStorage } from '@/storage/contactStorage.ts'
import { optionStorage } from '../storage/optionStorage.ts'
</script>

<template>
    <h2>Details</h2>
    <ul v-if="contactStorage.selectedContact.value">
        <li>
            <span>category</span> <br /><span>{{
                optionStorage.getCategoryById(
                    contactStorage.selectedContact.value.contactCategoryId,
                )
            }}</span>
        </li>
        <li v-if="contactStorage.selectedContact.value.contactCategoryId == 1">
            <span>businessCategory</span>
            <br /><span
                >{{
                    optionStorage.getBusinessCategoryById(
                        contactStorage.selectedContact.value.businessCategoryId,
                    )
                }}
            </span>
        </li>
        <li v-if="contactStorage.selectedContact.value.contactCategoryId == 3">
            <span>other category</span>
            <br /><span>{{ contactStorage.selectedContact.value.otherCategory }}</span>
        </li>
        <li>
            <span>dateOfBirth</span> <br /><span>{{
                contactStorage.selectedContact.value.dateOfBirth
            }}</span>
        </li>
        <li>
            <span>email</span> <br /><span>{{ contactStorage.selectedContact.value.email }}</span>
        </li>
        <li>
            <span>id</span> <br /><span>{{ contactStorage.selectedContact.value.id }}</span>
        </li>
        <li>
            <span>name</span> <br /><span>{{ contactStorage.selectedContact.value.name }}</span>
        </li>
        <li>
            <span>password</span> <br /><span>{{
                contactStorage.selectedContact.value.password
            }}</span>
        </li>
        <li>
            <span>phoneNumber</span> <br /><span>{{
                contactStorage.selectedContact.value.phoneNumber
            }}</span>
        </li>
        <li>
            <span>surname</span> <br /><span>{{
                contactStorage.selectedContact.value.surname
            }}</span>
        </li>
    </ul>
    <button @click.prevent="contactStorage.clearSelectedContact()">close</button>
    <button
        :disabled="!tokenStorage.isAuthenticated()"
        @click.prevent="contactStorage.editingContact.value = true"
    >
        edit
    </button>
</template>
