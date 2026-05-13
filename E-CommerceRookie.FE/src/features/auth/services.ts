import type { AxiosError } from 'axios'
import { api } from '../../services/api'

type AuthResponse = {
  token?: string
  Token?: string
}

export const authService = {
  async login(username: string, password: string) {
    try {
      const response = await api.post<AuthResponse>('/authen/login', {
        username,
        password,
      })

      const token = response.data.token ?? response.data.Token
      if (!token) {
        throw new Error('Token is missing in response.')
      }

      return token
    } catch (error) {
      if (isAxiosError(error)) {
        const status = error.response?.status
        if (status === 400) {
          throw new Error(extractValidationMessage(error))
        }
        if (status === 401) {
          throw new Error('Invalid username or password.')
        }
        const message = extractProblemDetailsMessage(error)
        if (message) {
          throw new Error(message)
        }
      }

      throw error
    }
  },
}

function isAxiosError(error: unknown): error is AxiosError {
  return typeof error === 'object' && error !== null && 'isAxiosError' in error
}

function extractValidationMessage(error: AxiosError): string {
  const data = error.response?.data
  if (Array.isArray(data) && data.length > 0) {
    const first = data[0] as { ErrorMessage?: string; errorMessage?: string }
    return first.ErrorMessage || first.errorMessage || 'Validation failed.'
  }

  if (data && typeof data === 'object' && 'errors' in data) {
    const errors = data.errors as Record<string, string[]>
    const firstKey = Object.keys(errors)[0]
    if (firstKey && errors[firstKey]?.[0]) {
      return errors[firstKey][0]
    }
  }

  return 'Validation failed.'
}

function extractProblemDetailsMessage(error: AxiosError): string | null {
  const data = error.response?.data
  if (!data || typeof data !== 'object') return null
  const detail = (data as { detail?: string }).detail
  if (detail) return detail
  const title = (data as { title?: string }).title
  return title || null
}
