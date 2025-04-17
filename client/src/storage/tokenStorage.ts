import { reactive } from 'vue'

const TOKEN_KEY = 'token-token'

// util object for storing the token in localstorage
// could probably make only the token reactive, or find other way to deal with it
export const tokenStorage = reactive({
    token: localStorage.getItem(TOKEN_KEY) || null,
    setToken(token: string) {
        localStorage.setItem(TOKEN_KEY, token)
        this.token = token
    },
    clearToken() {
        localStorage.removeItem(TOKEN_KEY)
        this.token = null
    },
    isAuthenticated() {
        return !!this.token
    },
})
