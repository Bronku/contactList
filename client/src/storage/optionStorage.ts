import { ref } from 'vue'
import type { selectOption } from '@/types/selectOption.ts'
import { optionService } from '@/services/optionService.ts'

export const optionStorage = {
    categories: ref<selectOption[]>([]),
    businessCategories: ref<selectOption[]>([]),
    async refresh() {
        this.categories.value = await optionService.fetchCategories()
        this.businessCategories.value = await optionService.fetchBusinessCategories()
    },
    getCategoryById(id: number) {
        const option = this.categories.value.find((opt) => opt.id === id)
        return option ? option.name : 'not found'
    },
    getBusinessCategoryById(id: number) {
        const option = this.businessCategories.value.find((opt) => opt.id === id)
        return option ? option.name : 'not found'
    },
}
