import {reactive} from "vue";

const TOKEN_KEY = 'token-token';

// util object for storing the token in localstorage
export const tokenStorage = reactive({
  token: localStorage.getItem(TOKEN_KEY) || null,
  setToken(token: string) {
    localStorage.setItem(TOKEN_KEY, token);
    this.token = token;
  },
  clearToken(){
    localStorage.removeItem(TOKEN_KEY);
    this.token = null;
  },
  isAuthenticated(){
    return !!this.token;
  }
})
