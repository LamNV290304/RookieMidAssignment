import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import { customerService } from './services'
import type { CustomerState, PagedResult, Customer } from './types'

const initialState: CustomerState = {
  items: [],
  totalCount: 0,
  pageNumber: 1,
  pageSize: 5,
  keyword: '',
  loading: false,
  error: null,
}

type ThunkState = {
  customer: CustomerState
}

export const fetchCustomers = createAsyncThunk(
  'customer/fetchCustomers',
  async (_, { getState, rejectWithValue }) => {
    try {
      const state = getState() as ThunkState
      const { pageNumber, pageSize, keyword } = state.customer
      return await customerService.getPaged(pageNumber, pageSize, keyword)
    } catch (error) {
      return rejectWithValue('Failed to load customers.')
    }
  },
)

export const customerSlice = createSlice({
  name: 'customer',
  initialState,
  reducers: {
    setPage(state, action: PayloadAction<number>) {
      state.pageNumber = action.payload
    },
    setPageSize(state, action: PayloadAction<number>) {
      state.pageSize = action.payload
    },
    setKeyword(state, action: PayloadAction<string>) {
      state.keyword = action.payload
      state.pageNumber = 1
    },
    clearError(state) {
      state.error = null
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchCustomers.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(
        fetchCustomers.fulfilled,
        (state, action: PayloadAction<PagedResult<Customer>>) => {
          state.loading = false
          state.items = action.payload.items
          state.totalCount = action.payload.totalCount
          state.pageNumber = action.payload.pageNumber
          state.pageSize = action.payload.pageSize
        },
      )
      .addCase(fetchCustomers.rejected, (state, action) => {
        state.loading = false
        state.error = (action.payload as string) || 'Failed to load customers.'
      })
  },
})

export const { setPage, setPageSize, setKeyword, clearError } =
  customerSlice.actions

export default customerSlice.reducer
