export type Category = {
  id: string
  name: string
  description: string
}

export type PagedResult<T> = {
  items: T[]
  totalCount: number
  pageNumber: number
  pageSize: number
}

export type CategoryState = {
  items: Category[]
  totalCount: number
  pageNumber: number
  pageSize: number
  loading: boolean
  error: string | null
  selectedId: string | null
}
