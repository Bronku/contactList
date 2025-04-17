// src/services/contactService.ts
import {tokenStorage} from "@/storage/tokenStorage.ts";
import type {contactType} from "@/types/contact.ts";

const API_BASE_URL = import.meta.env.VITE_API_URL;

async function fetchContacts(): Promise<contactType[]> {
    try {
        const response = await fetch(`${API_BASE_URL}/Contact`);
        if (!response.ok) {
            console.error(`Failed to fetch contacts: ${response.status}`);
            return [];
        }
        return await response.json();
    } catch (error) {
        console.error("Error fetching contacts:", error);
        return [];
    }
}

async function deleteContact(contactId: number): Promise<void> {
    await fetch(`${API_BASE_URL}/Contact/${contactId}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${tokenStorage.token}`,
        },
    });
}

async function updateContact(contact: contactType): Promise<void> {
    await fetch(`${API_BASE_URL}/Contact`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${tokenStorage.token}`,
        },
        body: JSON.stringify(contact),
    });
}

async function createContact(contact: contactType): Promise<void> {
    await fetch(`${API_BASE_URL}/Contact`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${tokenStorage.token}`,
        },
        body: JSON.stringify(contact),
    });
}

export const contactService = {
    fetchContacts,
    deleteContact,
    updateContact,
    createContact,
};
