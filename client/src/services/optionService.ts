async function fetchCategories() {
    try {
        const response = await fetch(`${import.meta.env.VITE_API_URL}/Contact/categories`);
        if (!response.ok) {
            console.error(response.status);
            return [];
        }
        return await response.json();
    } catch (error) {
        console.error(error);
        return [];
    }
}

async function fetchBusinessCategories() {
    try {
        const response = await fetch(`${import.meta.env.VITE_API_URL}/Contact/categories/business`);
        if (!response.ok) {
            console.error(response.status);
            return [];
        }
        return await response.json();
    } catch (error) {
        console.error(error);
        return [];
    }
}

export const optionService = {
    fetchCategories,
    fetchBusinessCategories,
}
