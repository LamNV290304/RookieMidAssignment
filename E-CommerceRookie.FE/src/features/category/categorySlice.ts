import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import { categoryService } from './services'
import type { CategoryState, PagedResult, Category } from './types'

const initialState: CategoryState = {
  items: [],
  totalCount: 0,
  pageNumber: 1,
  pageSize: 5,
  loading: false,
  error: null,
  selectedId: null,
}

type ThunkState = {
  category: CategoryState
}

export const fetchCategories = createAsyncThunk(
  'category/fetchCategories',
  async (_, { getState, rejectWithValue }) => {
    try {
      const state = getState() as ThunkState
      const { pageNumber, pageSize } = state.category
      return await categoryService.getPaged(pageNumber, pageSize)
    } catch (error) {
      return rejectWithValue('Failed to load categories.')
    }
  },
)

export const createCategory = createAsyncThunk(
  'category/createCategory',
  async (
    payload: { name: string; description: string },
    { dispatch, rejectWithValue },
  ) => {
    try {
      await categoryService.create(payload)
      await dispatch(fetchCategories())
    } catch (error) {
      return rejectWithValue('Failed to create category.')
    }
  },
)

export const updateCategory = createAsyncThunk(
  'category/updateCategory',
  async (
    payload: { id: string; name: string; description: string },
    { dispatch, rejectWithValue },
  ) => {
    try {
      await categoryService.update(payload.id, {
        name: payload.name,
        description: payload.description,
      })
      await dispatch(fetchCategories())
    } catch (error) {
      return rejectWithValue('Failed to update category.')
    }
  },
)

export const deleteCategory = createAsyncThunk(
  'category/deleteCategory',
  async (id: string, { dispatch, rejectWithValue }) => {
    try {
      await categoryService.remove(id)
      await dispatch(fetchCategories())
    } catch (error) {
      return rejectWithValue('Failed to delete category.')
    }
  },
)

export const categorySlice = createSlice({
  name: 'category',
  initialState,
  reducers: {
    setSelectedId(state, action: PayloadAction<string | null>) {
      state.selectedId = action.payload
    },
    setPage(state, action: PayloadAction<number>) {
      state.pageNumber = action.payload
    },
    setPageSize(state, action: PayloadAction<number>) {
      state.pageSize = action.payload
    },
    clearError(state) {
      state.error = null
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchCategories.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(
        fetchCategories.fulfilled,
        (state, action: PayloadAction<PagedResult<Category>>) => {
          state.loading = false
          state.items = action.payload.items
          state.totalCount = action.payload.totalCount
          state.pageNumber = action.payload.pageNumber
          state.pageSize = action.payload.pageSize
        },
      )
      .addCase(fetchCategories.rejected, (state, action) => {
        state.loading = false
        state.error = (action.payload as string) || 'Failed to load categories.'
      })
      .addCase(createCategory.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(createCategory.fulfilled, (state) => {
        state.loading = false
      })
      .addCase(createCategory.rejected, (state, action) => {
        state.loading = false
        state.error = (action.payload as string) || 'Failed to create category.'
      })
      .addCase(updateCategory.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(updateCategory.fulfilled, (state) => {
        state.loading = false
      })
      .addCase(updateCategory.rejected, (state, action) => {
        state.loading = false
        state.error = (action.payload as string) || 'Failed to update category.'
      })
      .addCase(deleteCategory.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(deleteCategory.fulfilled, (state) => {
        state.loading = false
      })
      .addCase(deleteCategory.rejected, (state, action) => {
        state.loading = false
        state.error = (action.payload as string) || 'Failed to delete category.'
      })
  },
})

export const { setSelectedId, setPage, setPageSize, clearError } =
  categorySlice.actions

export default categorySlice.reducer
