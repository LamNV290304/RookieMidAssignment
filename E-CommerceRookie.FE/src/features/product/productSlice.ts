    import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import { productService } from './services'
import type { ProductState, PagedResult, Product } from './types'

const initialState: ProductState = {
  items: [],
  totalCount: 0,
  pageNumber: 1,
  pageSize: 5,
  keyword: '',
  categoryId: null,
  loading: false,
  error: null,
  selectedId: null,
}

type ThunkState = {
  product: ProductState
}

export const fetchProducts = createAsyncThunk(
  'product/fetchProducts',
  async (_, { getState, rejectWithValue }) => {
    try {
      const state = getState() as ThunkState
      const { pageNumber, pageSize, keyword, categoryId } = state.product
      return await productService.getPaged(
        pageNumber,
        pageSize,
        keyword,
        categoryId,
      )
    } catch (error) {
      return rejectWithValue('Failed to load products.')
    }
  },
)

export const createProduct = createAsyncThunk(
  'product/createProduct',
  async (
    payload: {
      name: string
      description: string
      price: number
      categoryId: string
      images?: File[]
    },
    { dispatch, rejectWithValue },
  ) => {
    try {
      await productService.create(payload)
      await dispatch(fetchProducts())
    } catch (error) {
      return rejectWithValue('Failed to create product.')
    }
  },
)

export const updateProduct = createAsyncThunk(
  'product/updateProduct',
  async (
    payload: {
      id: string
      name: string
      description: string
      price: number
      categoryId: string
      images?: File[]
    },
    { dispatch, rejectWithValue },
  ) => {
    try {
      await productService.update(payload.id, {
        name: payload.name,
        description: payload.description,
        price: payload.price,
        categoryId: payload.categoryId,
        images: payload.images,
      })
      await dispatch(fetchProducts())
    } catch (error) {
      return rejectWithValue('Failed to update product.')
    }
  },
)

export const deleteProduct = createAsyncThunk(
  'product/deleteProduct',
  async (id: string, { dispatch, rejectWithValue }) => {
    try {
      await productService.remove(id)
      await dispatch(fetchProducts())
    } catch (error) {
      return rejectWithValue('Failed to delete product.')
    }
  },
)

export const productSlice = createSlice({
  name: 'product',
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
    setKeyword(state, action: PayloadAction<string>) {
      state.keyword = action.payload
      state.pageNumber = 1
    },
    setCategoryId(state, action: PayloadAction<string | null>) {
      state.categoryId = action.payload
      state.pageNumber = 1
    },
    clearError(state) {
      state.error = null
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchProducts.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(
        fetchProducts.fulfilled,
        (state, action: PayloadAction<PagedResult<Product>>) => {
          state.loading = false
          state.items = action.payload.items
          state.totalCount = action.payload.totalCount
          state.pageNumber = action.payload.pageNumber
          state.pageSize = action.payload.pageSize
        },
      )
      .addCase(fetchProducts.rejected, (state, action) => {
        state.loading = false
        state.error = (action.payload as string) || 'Failed to load products.'
      })
      .addCase(createProduct.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(createProduct.fulfilled, (state) => {
        state.loading = false
      })
      .addCase(createProduct.rejected, (state, action) => {
        state.loading = false
        state.error = (action.payload as string) || 'Failed to create product.'
      })
      .addCase(updateProduct.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(updateProduct.fulfilled, (state) => {
        state.loading = false
      })
      .addCase(updateProduct.rejected, (state, action) => {
        state.loading = false
        state.error = (action.payload as string) || 'Failed to update product.'
      })
      .addCase(deleteProduct.pending, (state) => {
        state.loading = true
        state.error = null
      })
      .addCase(deleteProduct.fulfilled, (state) => {
        state.loading = false
      })
      .addCase(deleteProduct.rejected, (state, action) => {
        state.loading = false
        state.error = (action.payload as string) || 'Failed to delete product.'
      })
  },
})

export const {
  setSelectedId,
  setPage,
  setPageSize,
  setKeyword,
  setCategoryId,
  clearError,
} = productSlice.actions

export default productSlice.reducer
