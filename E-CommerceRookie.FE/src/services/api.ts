import axios, { AxiosError } from 'axios'

export const baseURL =
	import.meta.env.VITE_API_URL?.toString() || 'https://localhost:7274/api'

export const api = axios.create({
	baseURL,
	timeout: 15000,
	headers: {
		'Content-Type': 'application/json',
	},
})

api.interceptors.request.use((config) => {
	const token = localStorage.getItem('accessToken')
	if (token) {
		config.headers = config.headers || {}
		config.headers.Authorization = `Bearer ${token}`
	}
	return config
})

api.interceptors.response.use(
	(response) => response,
	(error: AxiosError) => {
		if (error.response?.status === 401) {
			localStorage.removeItem('accessToken')
		}
		return Promise.reject(error)
	}
)
