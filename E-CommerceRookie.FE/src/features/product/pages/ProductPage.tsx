import { useEffect, useMemo, useState } from 'react'
import type { ChangeEvent } from 'react'
import { useNavigate } from 'react-router-dom'
import {
  clearError,
  createProduct,
  deleteProduct,
  fetchProducts,
  setCategoryId,
  setKeyword,
  setPage,
  setSelectedId,
  updateProduct,
} from '../productSlice'
import { useAppDispatch, useAppSelector } from '../../../app/hooks'
import Modal from '../../../components/common/Modal'
import { categoryService } from '../../category/services'
import type { Category } from '../../category/types'
import { productService } from '../services'
import { baseURL } from '../../../services/api'

const emptyForm = {
  name: '',
  description: '',
  price: '',
  categoryId: '',
  images: [] as File[],
}

export default function ProductPage() {
  const dispatch = useAppDispatch()
  const {
    items,
    totalCount,
    pageNumber,
    pageSize,
    keyword,
    categoryId,
    loading,
    error,
    selectedId,
  } = useAppSelector((state) => state.product)

  const [form, setForm] = useState(emptyForm)
  const [isModalOpen, setIsModalOpen] = useState(false)
  const [modalMode, setModalMode] = useState<'create' | 'update'>('create')
  const [search, setSearch] = useState('')
  const [categories, setCategories] = useState<Category[]>([])
  const [categoryLoading, setCategoryLoading] = useState(false)
  const [currentImages, setCurrentImages] = useState<string[]>([])
  const [formErrors, setFormErrors] = useState<string[]>([])

  const navigate = useNavigate()

  const selectedProduct = useMemo(
    () => items.find((item) => item.id === selectedId) || null,
    [items, selectedId],
  )

  const totalPages = Math.max(1, Math.ceil(totalCount / pageSize))

  useEffect(() => {
    dispatch(fetchProducts())
  }, [dispatch, pageNumber, pageSize, keyword, categoryId])

  useEffect(() => {
    const handler = setTimeout(() => {
      if (search !== undefined) dispatch(setKeyword(search))
    }, 400)
    return () => clearTimeout(handler)
  }, [search, dispatch])

  useEffect(() => {
    let isMounted = true
    const loadCategories = async () => {
      setCategoryLoading(true)
      try {
        const response = await categoryService.getPaged(1, 200)
        if (isMounted) setCategories(response.items)
      } finally {
        if (isMounted) setCategoryLoading(false)
      }
    }
    loadCategories()
    return () => {
      isMounted = false
    }
  }, [])

  useEffect(() => {
    if (selectedProduct) {
      setForm({
        name: selectedProduct.name,
        description: selectedProduct.description,
        price: selectedProduct.price.toString(),
        categoryId: selectedProduct.categoryId,
        images: [],
      })
      return
    }
    setForm(emptyForm)
  }, [selectedProduct])

  const handleChange = (
    event: ChangeEvent<HTMLInputElement | HTMLSelectElement>,
  ) => {
    const { name, value } = event.target
    setForm((prev) => ({ ...prev, [name]: value }))
    if (formErrors.length > 0) setFormErrors([])
  }

  const handleFiles = (event: ChangeEvent<HTMLInputElement>) => {
    const files = event.target.files ? Array.from(event.target.files) : []
    setForm((prev) => ({ ...prev, images: files }))
    if (formErrors.length > 0) setFormErrors([])
  }

  const validateProductForm = (values: typeof emptyForm) => {
    const errors: string[] = []
    const name = values.name.trim()
    const description = values.description.trim()
    const priceNumber = Number(values.price)

    if (!name) {
      errors.push('Product name is required.')
    } else if (name.length > 200) {
      errors.push('Product name must not exceed 200 characters.')
    }

    if (!description) {
      errors.push('Description is required.')
    } else if (description.length > 2000) {
      errors.push('Description must not exceed 2000 characters.')
    }

    if (!Number.isFinite(priceNumber) || priceNumber <= 0) {
      errors.push('Price must be greater than 0.')
    }

    if (!values.categoryId) {
      errors.push('Category ID is required.')
    }

    if (values.images.length > 0) {
      const maxSize = 2 * 1024 * 1024
      const allowedTypes = new Set(['image/jpeg', 'image/png', 'image/gif'])
      let hasSizeError = false
      let hasTypeError = false

      values.images.forEach((file) => {
        if (file.size > maxSize) hasSizeError = true
        if (!allowedTypes.has(file.type)) hasTypeError = true
      })

      if (hasSizeError) errors.push('Image size must not exceed 2MB.')
      if (hasTypeError) errors.push('Only JPG, PNG, or GIF formats are allowed.')
    }

    return errors
  }

  const handleCreate = async () => {
    const errors = validateProductForm(form)
    setFormErrors(errors)
    if (errors.length > 0) return
    const priceNumber = Number(form.price)
    await dispatch(
      createProduct({
        name: form.name.trim(),
        description: form.description.trim(),
        price: priceNumber,
        categoryId: form.categoryId,
        images: form.images,
      }),
    )
    dispatch(setSelectedId(null))
    setForm(emptyForm)
    setFormErrors([])
    setIsModalOpen(false)
  }

  const handleUpdate = async () => {
    const errors = validateProductForm(form)
    setFormErrors(errors)
    if (!selectedId || errors.length > 0) return
    const priceNumber = Number(form.price)
    await dispatch(
      updateProduct({
        id: selectedId,
        name: form.name.trim(),
        description: form.description.trim(),
        price: priceNumber,
        categoryId: form.categoryId,
        images: form.images,
      }),
    )
    setFormErrors([])
    setIsModalOpen(false)
  }

  const handleDelete = async (id: string) => {
    const shouldDelete = window.confirm('Delete this product?')
    if (!shouldDelete) return
    await dispatch(deleteProduct(id))
    if (selectedId === id) {
      dispatch(setSelectedId(null))
    }
  }

  const handleSelect = (id: string) => {
    if (error) dispatch(clearError())
    dispatch(setSelectedId(id))
  }

  const handleNew = () => {
    if (error) dispatch(clearError())
    dispatch(setSelectedId(null))
    setForm(emptyForm)
    setCurrentImages([])
    setFormErrors([])
  }

  const openCreateModal = () => {
    handleNew()
    setModalMode('create')
    setIsModalOpen(true)
  }

  const handleEdit = async (id: string) => {
    handleSelect(id)
    setModalMode('update')
    setIsModalOpen(true)
    setFormErrors([])
    try {
      const detail = await productService.getById(id)
      setForm({
        name: detail.name,
        description: detail.description,
        price: detail.price.toString(),
        categoryId: detail.categoryId,
        images: [],
      })
      setCurrentImages(detail.imageUrls || [])
    } catch (fetchError) {
      setCurrentImages([])
    }
  }

  const handleRemoveImage = async (url: string) => {
    if (!selectedId) return
    const shouldDelete = window.confirm('Delete this image?')
    if (!shouldDelete) return
    try {
      await productService.removeImage(selectedId, url)
      setCurrentImages((prev) => prev.filter((item) => item !== url))
    } catch (removeError) {
      window.alert('Failed to delete image.')
    }
  }

  const priceFormatter = new Intl.NumberFormat('vi-VN')
  const apiRoot = baseURL.replace(/\/api\/?$/, '')
  const resolveImageUrl = (url: string) =>
    url.startsWith('http') ? url : `${apiRoot}${url}`
  const modalErrors = error ? [...formErrors, error] : formErrors

  return (
    <>
      <header className="content-header">
        <div>
          <h1>Product Management</h1>
          <p className="subtitle">
            Curate products with pricing, categories, and quick edits.
          </p>
        </div>
        <button className="primary-button" onClick={openCreateModal}>
          Add Product
        </button>
      </header>

      <section className="table-card">
        {error ? <div className="error-banner">{error}</div> : null}

        <div style={{ display: 'flex', gap: 8, marginBottom: 12 }}>
          <input
            placeholder="Search products..."
            value={search}
            onChange={(event) => setSearch(event.target.value)}
            className="search-input"
            style={{ flex: 1 }}
          />
          <select
            className="search-input"
            value={categoryId ?? ''}
            onChange={(event) =>
              dispatch(setCategoryId(event.target.value || null))
            }
            disabled={categoryLoading}
            style={{ minWidth: 180 }}
          >
            <option value="">All categories</option>
            {categories.map((category) => (
              <option key={category.id} value={category.id}>
                {category.name}
              </option>
            ))}
          </select>
          <button
            className="ghost-button"
            onClick={() => {
              setSearch('')
              dispatch(setKeyword(''))
              dispatch(setCategoryId(null))
            }}
          >
            Clear
          </button>
        </div>

        <div className="table-wrapper">
          <table>
            <thead>
              <tr>
                <th>ID</th>
                <th>Product</th>
                <th>Category</th>
                <th>Price</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {items.length === 0 && !loading ? (
                <tr>
                  <td colSpan={5} className="empty-cell">
                    No products yet.
                  </td>
                </tr>
              ) : null}
              {items.map((item, index) => (
                <tr
                  key={item.id}
                  className={item.id === selectedId ? 'active' : ''}
                  onClick={() => handleSelect(item.id)}
                >
                  <td className="id-cell">{index + 1}</td>
                  <td>
                    <div className="name-cell">
                      <span className="name-text">{item.name}</span>
                      <span className="desc-text">{item.description}</span>
                    </div>
                  </td>
                  <td>{item.categoryName}</td>
                  <td>{priceFormatter.format(item.price)}</td>
                  <td>
                    <div className="action-buttons">
                      <button
                        className="icon-button"
                        onClick={(event) => {
                          event.stopPropagation()
                          navigate(`/products/${item.id}`)
                        }}
                      >
                        View
                      </button>
                      <button
                        className="icon-button edit"
                        onClick={(event) => {
                          event.stopPropagation()
                          handleEdit(item.id)
                        }}
                      >
                        Edit
                      </button>
                      <button
                        className="icon-button delete"
                        onClick={(event) => {
                          event.stopPropagation()
                          handleDelete(item.id)
                        }}
                      >
                        Delete
                      </button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>

        <div className="table-footer">
          <span>
            {totalCount} items · Page {pageNumber} of {totalPages}
          </span>
          <div className="pagination">
            <button
              className="ghost-button"
              disabled={pageNumber <= 1 || loading}
              onClick={() => dispatch(setPage(pageNumber - 1))}
            >
              Prev
            </button>
            <button
              className="ghost-button"
              disabled={pageNumber >= totalPages || loading}
              onClick={() => dispatch(setPage(pageNumber + 1))}
            >
              Next
            </button>
          </div>
        </div>
      </section>

      <Modal
        isOpen={isModalOpen}
        eyebrow="Product"
        title={modalMode === 'create' ? 'Create new' : 'Update'}
        onClose={() => setIsModalOpen(false)}
      >
        <div className="form-grid">
          <label className="field">
            <span>Name</span>
            <input
              name="name"
              value={form.name}
              onChange={handleChange}
              placeholder="Product name"
            />
          </label>
          <label className="field">
            <span>Price</span>
            <input
              type="number"
              name="price"
              value={form.price}
              onChange={handleChange}
              placeholder="0"
              min="0"
              step="0.01"
            />
          </label>
          <label className="field">
            <span>Description</span>
            <input
              name="description"
              value={form.description}
              onChange={handleChange}
              placeholder="Short description"
            />
          </label>
          <label className="field">
            <span>Category</span>
            <select
              name="categoryId"
              value={form.categoryId}
              onChange={handleChange}
              disabled={categoryLoading}
            >
              <option value="">Select category</option>
              {categories.map((category) => (
                <option key={category.id} value={category.id}>
                  {category.name}
                </option>
              ))}
            </select>
          </label>
          <label className="field" style={{ gridColumn: '1 / -1' }}>
            <span>Images</span>
            <input
              type="file"
              multiple
              accept="image/*"
              onChange={handleFiles}
            />
          </label>
          {currentImages.length > 0 ? (
            <div className="field" style={{ gridColumn: '1 / -1' }}>
              <span>Current images</span>
              <div className="image-grid">
                {currentImages.map((url, index) => (
                  <div className="image-tile" key={`${url}-${index}`}>
                    <img src={resolveImageUrl(url)} alt={`Product ${index + 1}`} />
                    <button
                      type="button"
                      className="image-delete"
                      onClick={() => handleRemoveImage(url)}
                    >
                      Remove
                    </button>
                  </div>
                ))}
              </div>
            </div>
          ) : null}
        </div>
        {modalErrors.length > 0 ? (
          <div className="error-banner">
            {modalErrors.map((message, index) => (
              <div key={`${message}-${index}`}>{message}</div>
            ))}
          </div>
        ) : null}
        <div className="form-actions">
          <button
            className="primary-button"
            onClick={modalMode === 'create' ? handleCreate : handleUpdate}
            disabled={loading}
          >
            {modalMode === 'create' ? 'Create' : 'Save changes'}
          </button>
          <button
            className="ghost-button"
            onClick={() => {
              setIsModalOpen(false)
              setFormErrors([])
            }}
          >
            Cancel
          </button>
        </div>
      </Modal>
    </>
  )
}
