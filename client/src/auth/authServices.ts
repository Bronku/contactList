import {tokenStorage} from "../storage/tokenStorage.ts";

// takes in credentials, sends them to the server, and saves the result in token storage
// should probably throw or return errors instead of returning an error string, but it's simpler this way
export async function sendLogin(username: string, password: string) {
  try {
    const response = await fetch(`${import.meta.env.VITE_API_URL}/Auth`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({username, password}),
    });
    if (!response.ok) {
      if (response.status === 404) {
        return `Invalid login: ${username}`;
      }
      return "Wrong password"
    }
    const token = await response.json();
    tokenStorage.setToken(token.token);
    return "";
  } catch (error) {
    return `Unauthorized, ${error}`;
  }
}
