import { api } from '../../services/api'
import type { Customer, PagedResult } from './types'

export const customerService = {
  async getPaged(pageNumber: number, pageSize: number, keyword?: string) {
    const params: Record<string, unknown> = { pageNumber, pageSize }
    if (keyword && keyword.trim().length > 0) params.keyword = keyword.trim()

    const response = await api.get<PagedResult<Customer>>('/customers', {
      params,
    })
    return response.data
  },
}
