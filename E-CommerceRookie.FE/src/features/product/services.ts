import { api } from '../../services/api'
import type { PagedResult, Product } from './types'

export type ProductPayload = {
  name: string
  description: string
  price: number
  categoryId: string
  images?: File[]
}

const buildFormData = (payload: ProductPayload) => {
  const formData = new FormData()
  formData.append('name', payload.name)
  formData.append('description', payload.description)
  formData.append('price', payload.price.toString())
  formData.append('categoryId', payload.categoryId)
  if (payload.images && payload.images.length > 0) {
    payload.images.forEach((file) => {
      formData.append('imageUrls', file)
    })
  }
  return formData
}

export const productService = {
  async getPaged(
    pageNumber: number,
    pageSize: number,
    keyword?: string,
    categoryId?: string | null,
  ) {
    const params: Record<string, unknown> = { pageNumber, pageSize }
    if (keyword && keyword.trim().length > 0) params.keyword = keyword.trim()
    if (categoryId) params.categoryId = categoryId

    const response = await api.get<PagedResult<Product>>('/products', {
      params,
    })
    return response.data
  },

  async getById(id: string) {
    const response = await api.get<Product>(`/products/${id}`)
    return response.data
  },

  async create(payload: ProductPayload) {
    const formData = buildFormData(payload)
    const response = await api.post<string>('/products', formData, {
      headers: { 'Content-Type': 'multipart/form-data' },
    })
    return response.data
  },

  async update(id: string, payload: ProductPayload) {
    const formData = buildFormData(payload)
    await api.put(`/products/${id}`, formData, {
      headers: { 'Content-Type': 'multipart/form-data' },
    })
  },

  async remove(id: string) {
    await api.delete(`/products/${id}`)
  },

  async removeImage(productId: string, imageUrl: string) {
    await api.delete(`/products/${productId}/images`, {
      params: { url: imageUrl },
    })
  },
}
