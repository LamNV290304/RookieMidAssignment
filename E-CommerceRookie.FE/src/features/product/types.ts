export type Product = {
  id: string
  name: string
  description: string
  price: number
  categoryId: string
  categoryName: string
  imageUrls: string[]
  createdAt: string
  updatedAt?: string | null
}

export type PagedResult<T> = {
  items: T[]
  totalCount: number
  pageNumber: number
  pageSize: number
}

export type ProductState = {
  items: Product[]
  totalCount: number
  pageNumber: number
  pageSize: number
  keyword: string
  categoryId: string | null
  loading: boolean
  error: string | null
  selectedId: string | null
}
