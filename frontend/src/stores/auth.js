import { reactive } from "vue";

const AUTH_TOKEN = "auth_token";
const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

export const authStore = reactive({
  token: localStorage.getItem("auth_token") || null,
  isAuthenticated() {
    if (this.token == null) {
      return false;
    }
    return true;
  },
  setToken(token) {
    this.token = token;
    localStorage.setItem(AUTH_TOKEN, token);
  },
  logout() {
    this.token = null;
    localStorage.removeItem(AUTH_TOKEN);
  },
});

export async function sendLogin(username, password) {
  try {
    const response = await fetch(`${API_BASE_URL}/Auth`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ username, password }),
    });
    if (!response.ok) {
      throw new Error("Login failed");
    }
    const data = await response.json();
    authStore.setToken(data.token);
    console.log("Login success: ", data);
    return true;
  } catch (error) {
    console.error("Login error: ", error);
    return false;
  }
}
