import {reactive} from "vue";
import {tokenStorage} from "@/storage/tokenStorage.ts";
import type {contactType} from "@/types/contact.ts";

export const contactStorage = reactive({
    contacts: [],
    async refresh() {
        try {
            const response = await fetch(`${import.meta.env.VITE_API_URL}/Contact`);


            if (!response.ok) {
                return;
            }
            this.contacts = await response.json();
        } catch (error) {
            console.error(error);
        }
    },
    async deleteContact(contact: contactType | null) {
        if (!contact) {
            return;
        }
        await fetch(`${import.meta.env.VITE_API_URL}/Contact/${contact.id}`,
            {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${tokenStorage.token}`,
                }
            });
        await this.refresh()
    },
    async updateContact(contact: contactType | null) {
        if (!contact) {
            return;
        }
        await fetch(`${import.meta.env.VITE_API_URL}/Contact`,
            {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${tokenStorage.token}`,
                },
                body: JSON.stringify(contact),
            })
        await this.refresh()
    },
    async createContact(contact: contactType | null) {
        if (!contact) {
            return;
        }
        await fetch(`${import.meta.env.VITE_API_URL}/Contact`,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${tokenStorage.token}`,
                },
                body: JSON.stringify(contact),
            })
        await this.refresh()
    },
    newContact(): contactType {
        return {
            businessCategory: 0,
            category: 0,
            dateOfBirth: "",
            email: "",
            id: 0,
            name: "",
            otherCategory: "",
            password: "",
            phoneNumber: "",
            surname: "",
        };
    },
    clone(input: contactType): contactType {
        return {
            businessCategory: input.businessCategory,
            category: input.category,
            dateOfBirth: input.dateOfBirth,
            email: input.email,
            id: input.id,
            name: input.name,
            otherCategory: input.otherCategory,
            password: input.password,
            phoneNumber: input.phoneNumber,
            surname: input.surname,
        };
    }

})
