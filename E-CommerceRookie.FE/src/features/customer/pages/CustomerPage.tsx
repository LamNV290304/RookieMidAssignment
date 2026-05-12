import { useEffect, useState } from 'react'
import type { ChangeEvent } from 'react'
import {
  clearError,
  fetchCustomers,
  setKeyword,
  setPage,
} from '../customerSlice'
import { useAppDispatch, useAppSelector } from '../../../app/hooks'

export default function CustomerPage() {
  const dispatch = useAppDispatch()
  const { items, totalCount, pageNumber, pageSize, keyword, loading, error } =
    useAppSelector((state) => state.customer)

  const [search, setSearch] = useState('')

  const totalPages = Math.max(1, Math.ceil(totalCount / pageSize))

  useEffect(() => {
    dispatch(fetchCustomers())
  }, [dispatch, pageNumber, pageSize, keyword])

  useEffect(() => {
    const handler = setTimeout(() => {
      if (search !== undefined) dispatch(setKeyword(search))
    }, 400)
    return () => clearTimeout(handler)
  }, [search, dispatch])

  const handleSearchChange = (event: ChangeEvent<HTMLInputElement>) => {
    setSearch(event.target.value)
  }

  return (
    <>
      <header className="content-header">
        <div>
          <h1>Customer List</h1>
          <p className="subtitle">
            View registered customers and search quickly.
          </p>
        </div>
      </header>

      <section className="table-card">
        {error ? <div className="error-banner">{error}</div> : null}

        <div style={{ display: 'flex', gap: 8, marginBottom: 12 }}>
          <input
            placeholder="Search customers..."
            value={search}
            onChange={handleSearchChange}
            className="search-input"
            style={{ flex: 1 }}
          />
          <button
            className="ghost-button"
            onClick={() => {
              setSearch('')
              if (error) dispatch(clearError())
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
                <th>Email</th>
                <th>Phone</th>
                <th>Created</th>
              </tr>
            </thead>
            <tbody>
              {items.length === 0 && !loading ? (
                <tr>
                  <td colSpan={5} className="empty-cell">
                    No customers yet.
                  </td>
                </tr>
              ) : null}
              {items.map((item, index) => (
                <tr key={item.id}>
                  <td className="id-cell">{index + 1}</td>
                  <td>{item.name}</td>
                  <td>{item.email}</td>
                  <td>{item.phone}</td>
                  <td>{new Date(item.createdAt).toLocaleDateString()}</td>
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
    </>
  )
}
