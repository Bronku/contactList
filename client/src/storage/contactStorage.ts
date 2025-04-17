// src/storage/contactStorage.ts
import { ref } from 'vue'
import type { contactType } from '@/types/contact.ts'
import { contactService } from '@/services/contactService.ts'
import { isExpired } from '@/services/authServices.ts'
import { tokenStorage } from '@/storage/tokenStorage.ts'

export const contactStorage = {
    contacts: ref<contactType[]>([]),
    selectedContact: ref<contactType | null>(null),
    editingContact: ref(false),
    async refresh() {
        this.contacts.value = await contactService.fetchContacts()
        this.selectedContact.value = null
    },
    selectContact(contact: contactType) {
        this.selectedContact.value = { ...contact }
    },
    clearSelectedContact() {
        this.selectedContact.value = null
    },
    async saveContact() {
        if (isExpired(tokenStorage.token)) {
            tokenStorage.clearToken()
            return
        }
        if (!this.selectedContact.value) return
        if (this.selectedContact.value.id == 0) {
            await contactService.createContact(this.selectedContact.value)
        } else {
            await contactService.updateContact(this.selectedContact.value)
        }
        this.selectedContact.value = null
        this.editingContact.value = false
        await this.refresh()
    },
    async deleteContact() {
        if (isExpired(tokenStorage.token)) {
            tokenStorage.clearToken()
            return
        }
        if (!this.selectedContact.value) return
        await contactService.deleteContact(this.selectedContact.value.id)
        this.selectedContact.value = null
        this.editingContact.value = false
        await this.refresh()
    },
    newContact() {
        this.selectedContact.value = {
            businessCategoryId: 0,
            contactCategoryId: 0,
            dateOfBirth: '',
            email: '',
            id: 0,
            name: '',
            otherCategory: '',
            password: '',
            phoneNumber: '',
            surname: '',
        }
        this.editingContact.value = true
    },
}
