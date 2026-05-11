import { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { productService } from '../services'
import type { Product } from '../types'
import { baseURL } from '../../../services/api'

export default function ProductDetailPage() {
  const { id } = useParams<{ id: string }>()
  const navigate = useNavigate()
  const [product, setProduct] = useState<Product | null>(null)
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    if (!id) return
    let isMounted = true
    const loadProduct = async () => {
      setLoading(true)
      setError(null)
      try {
        const detail = await productService.getById(id)
        if (isMounted) setProduct(detail)
      } catch (fetchError) {
        if (isMounted) setError('Failed to load product detail.')
      } finally {
        if (isMounted) setLoading(false)
      }
    }
    loadProduct()
    return () => {
      isMounted = false
    }
  }, [id])

  const priceFormatter = new Intl.NumberFormat('vi-VN')
  const apiRoot = baseURL.replace(/\/api\/?$/, '')
  const resolveImageUrl = (url: string) =>
    url.startsWith('http') ? url : `${apiRoot}${url}`

  const handleRemoveImage = async (url: string) => {
    if (!id) return
    const shouldDelete = window.confirm('Delete this image?')
    if (!shouldDelete) return
    try {
      await productService.removeImage(id, url)
      setProduct((prev) =>
        prev ? { ...prev, imageUrls: prev.imageUrls.filter((u) => u !== url) } : prev,
      )
    } catch (removeError) {
      setError('Failed to delete product image.')
    }
  }

  return (
    <section className="table-card">
      <header className="content-header">
        <div>
          <h1>Product Detail</h1>
          <p className="subtitle">Full information and image gallery.</p>
        </div>
        <button className="ghost-button" onClick={() => navigate('/products')}>
          Back to Products
        </button>
      </header>

      {error ? <div className="error-banner">{error}</div> : null}
      {loading ? <p>Loading...</p> : null}

      {product && !loading ? (
        <div className="detail-grid">
          <div className="detail-card">
            <h2>{product.name}</h2>
            <p className="subtitle">{product.description}</p>
            <div className="detail-meta">
              <div>
                <span className="detail-label">Category</span>
                <span>{product.categoryName}</span>
              </div>
              <div>
                <span className="detail-label">Price</span>
                <span>{priceFormatter.format(product.price)}</span>
              </div>
              <div>
                <span className="detail-label">Created</span>
                <span>{new Date(product.createdAt).toLocaleString()}</span>
              </div>
              {product.updatedAt ? (
                <div>
                  <span className="detail-label">Updated</span>
                  <span>{new Date(product.updatedAt).toLocaleString()}</span>
                </div>
              ) : null}
            </div>
          </div>

          <div className="detail-card">
            <h3>Images</h3>
            {product.imageUrls.length === 0 ? (
              <div className="image-placeholder">No images available.</div>
            ) : (
              <div className="image-grid">
                {product.imageUrls.map((url, index) => (
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
            )}
          </div>
        </div>
      ) : null}
    </section>
  )
}
