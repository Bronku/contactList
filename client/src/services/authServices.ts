// seems a bit inconsistent to be importing storage in one service, and doing it the other way around,
// but it made sense when I was writing it
import { tokenStorage } from '../storage/tokenStorage.ts'

// https://stackoverflow.com/questions/38552003/how-to-decode-jwt-token-in-javascript-without-using-a-library
export function isExpired(token: string | null) {
    if (token == null) {
        return true
    }
    const base64Url = token.split('.')[1]
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
    const jsonPayload = decodeURIComponent(
        window
            .atob(base64)
            .split('')
            .map(function (c) {
                return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
            })
            .join(''),
    )

    return JSON.parse(jsonPayload).exp < Date.now() / 1000
}

// takes in credentials, sends them to the server, and saves the result in token storage
// should probably throw or return errors instead of returning an error string, but it's simpler this way
export async function sendLogin(username: string, password: string) {
    try {
        const response = await fetch(`${import.meta.env.VITE_API_URL}/Auth`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ username, password }),
        })
        if (!response.ok) {
            if (response.status === 404) {
                return `Invalid login: ${username}`
            }
            return 'Wrong password'
        }
        const token = await response.json()
        tokenStorage.setToken(token.token)
        return ''
    } catch (error) {
        return `Unauthorized, ${error}`
    }
}

export async function sendRegister(username: string, password: string) {
    if (isExpired(tokenStorage.token)) {
        tokenStorage.clearToken()
        return 'unauthorized'
    }
    try {
        const response = await fetch(`${import.meta.env.VITE_API_URL}/Auth/register`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${tokenStorage.token}`,
            },
            body: JSON.stringify({ username, password }),
        })
        if (!response.ok) {
            return `Invalid register: ${username}`
        }
        return ''
    } catch (error) {
        return `${error}`
    }
}
