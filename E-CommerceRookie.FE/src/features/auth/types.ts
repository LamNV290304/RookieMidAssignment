export type AuthUser = {
  id: string
  email: string
  name: string
}

export type AuthState = {
  user: AuthUser | null
  accessToken: string | null
  isAuthenticated: boolean
  loading: boolean
  error: string | null
}
