import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import { authService } from './services'
import type { AuthState } from './types'

const initialState: AuthState = {
  token: localStorage.getItem('accessToken'),
  loading: false,
  error: null,
}

export const login = createAsyncThunk(
  'auth/login',
  async (
    payload: { username: string; password: string },
    { rejectWithValue },
  ) => {
    try {
      const token = await authService.login(payload.username, payload.password)
      localStorage.setItem('accessToken', token)
      return token
    } catch (error) {
      const message = error instanceof Error ? error.message : 'Login failed.'
      return rejectWithValue(message)
    }
  },
)

export const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    clearError(state) {
      state.error = null
    },
    clearToken(state) {
      state.token = null
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(login.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(login.fulfilled, (state, action) => {
        state.loading = false
        state.token = action.payload
      })
      .addCase(login.rejected, (state, action) => {
        state.loading = false
        state.error = (action.payload as string) || 'Login failed.'
      })
  },
})

export const { clearError, clearToken } = authSlice.actions

export default authSlice.reducer
