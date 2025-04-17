import {reactive} from "vue";
import {tokenStorage} from "@/storage/tokenStorage.ts";

export const contactStorage = reactive({
    contacts: [],
    async refresh() {
        try {
            const response = await fetch(`${import.meta.env.VITE_API_URL}/Contact`);


            if (!response.ok) {
                return;
            }
            //console.log(response);
            const body = await response.json();
            this.contacts = body
            //console.log(body);
        } catch (error) {
            console.error(error);
        }
    },
    async deleteContact(contact) {
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
    async updateContact(contact) {
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
    async createContact(contact) {
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
    newContact() {
        const output = {};
        output.businessCategory = 0;
        output.category = 0;
        output.dateOfBirth = "";
        output.email = "";
        output.id = 0;
        output.name = "";
        output.phoneNumber = "";
        output.surname = "";
        return output;
    },
    clone(input) {
        const output = {};
        output.businessCategory = input.businessCategory;
        output.category = input.category;
        output.dateOfBirth = input.dateOfBirth;
        output.email = input.email;
        output.id = input.id;
        output.name = input.name;
        output.password = input.password;
        output.phoneNumber = input.phoneNumber;
        output.surname = input.surname;
        return output;
    }

})
