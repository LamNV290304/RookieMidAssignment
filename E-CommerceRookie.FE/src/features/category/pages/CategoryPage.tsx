import { useEffect, useMemo, useState } from 'react'
import type { ChangeEvent } from 'react'
import {
  clearError,
  createCategory,
  deleteCategory,
  fetchCategories,
  setPage,
  setKeyword,
  setSelectedId,
  updateCategory,
} from '../categorySlice'
import { useAppDispatch, useAppSelector } from '../../../app/hooks'
import Modal from '../../../components/common/Modal.tsx'


const emptyForm = {
  name: '',
  description: '',
}

export default function CategoryPage() {
  const dispatch = useAppDispatch()
  const {
    items,
    totalCount,
    pageNumber,
    pageSize,
    keyword,
    loading,
    error,
    selectedId,
  } = useAppSelector((state) => state.category)

  const [form, setForm] = useState(emptyForm)
  const [isModalOpen, setIsModalOpen] = useState(false)
  const [modalMode, setModalMode] = useState<'create' | 'update'>('create')
  const [search, setSearch] = useState('')

  const selectedCategory = useMemo(
    () => items.find((item) => item.id === selectedId) || null,
    [items, selectedId],
  )

  const totalPages = Math.max(1, Math.ceil(totalCount / pageSize))

  useEffect(() => {
    dispatch(fetchCategories())
  }, [dispatch, pageNumber, pageSize, keyword])

  useEffect(() => {
    const handler = setTimeout(() => {
      if (search !== undefined) dispatch(setKeyword(search))
    }, 400)
    return () => clearTimeout(handler)
  }, [search, dispatch])

  useEffect(() => {
    if (selectedCategory) {
      setForm({
        name: selectedCategory.name,
        description: selectedCategory.description,
      })
      return
    }
    setForm(emptyForm)
  }, [selectedCategory])

  const handleChange = (event: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = event.target
    setForm((prev) => ({ ...prev, [name]: value }))
  }

  const handleCreate = async () => {
    if (!form.name.trim()) return
    await dispatch(createCategory(form))
    dispatch(setSelectedId(null))
    setForm(emptyForm)
    setIsModalOpen(false)
  }

  const handleUpdate = async () => {
    if (!selectedId || !form.name.trim()) return
    await dispatch(updateCategory({ id: selectedId, ...form }))
    setIsModalOpen(false)
  }

  const handleDelete = async (id: string) => {
    const shouldDelete = window.confirm('Delete this category?')
    if (!shouldDelete) return
    await dispatch(deleteCategory(id))
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
  }

  const openCreateModal = () => {
    handleNew()
    setModalMode('create')
    setIsModalOpen(true)
  }

  const handleEdit = (id: string) => {
    handleSelect(id)
    setModalMode('update')
    setIsModalOpen(true)
  }

  const isFormDirty = form.name.trim().length > 0

  return (
    <>
      <header className="content-header">
        <div>
          <h1>Category Management</h1>
          <p className="subtitle">
            Organize product categories with a tidy, reliable workflow.
          </p>
        </div>
        <button className="primary-button" onClick={openCreateModal}>
          Add Category
        </button>
      </header>

      <section className="table-card">
        {error ? <div className="error-banner">{error}</div> : null}

        <div style={{ display: 'flex', gap: 8, marginBottom: 12 }}>
          <input
            placeholder="Search categories..."
            value={search}
            onChange={(e) => setSearch(e.target.value)}
            className="search-input"
            style={{ flex: 1 }}
          />
          <button
            className="ghost-button"
            onClick={() => {
              setSearch('')
              dispatch(setKeyword(''))
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
                <th>Name</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {items.length === 0 && !loading ? (
                <tr>
                  <td colSpan={3} className="empty-cell">
                    No categories yet.
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
                  <td>
                    <div className="action-buttons">
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
        eyebrow="Category"
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
              placeholder="Category name"
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
        </div>

        {error ? <div className="error-banner">{error}</div> : null}

        <div className="form-actions">
          <button
            className="primary-button"
            disabled={!isFormDirty || loading}
            onClick={modalMode === 'create' ? handleCreate : handleUpdate}
          >
            {modalMode === 'create' ? 'Create' : 'Update'}
          </button>
          <button
            className="secondary-button"
            onClick={() => setIsModalOpen(false)}
          >
            Cancel
          </button>
        </div>
      </Modal>
    </>
  )
}
