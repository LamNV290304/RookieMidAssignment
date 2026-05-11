export type Customer = {
  id: string
  name: string
  email: string
  phone: string
  createdAt: string
  updatedAt?: string | null
}

export type PagedResult<T> = {
  items: T[]
  totalCount: number
  pageNumber: number
  pageSize: number
}

export type CustomerState = {
  items: Customer[]
  totalCount: number
  pageNumber: number
  pageSize: number
  keyword: string
  loading: boolean
  error: string | null
}
