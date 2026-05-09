import { api } from '../../services/api'
import type { Category, PagedResult } from './types'

export type CategoryPayload = {
  name: string
  description: string
}

export const categoryService = {
  async getPaged(pageNumber: number, pageSize: number) {
    const response = await api.get<PagedResult<Category>>('/categories', {
      params: { pageNumber, pageSize },
    })
    return response.data
  },

  async create(payload: CategoryPayload) {
    const response = await api.post<string>('/categories', payload)
    return response.data
  },

  async update(id: string, payload: CategoryPayload) {
    await api.put(`/categories/${id}`, payload)
  },

  async remove(id: string) {
    await api.delete(`/categories/${id}`)
  },
}
