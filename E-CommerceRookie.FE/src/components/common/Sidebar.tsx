import { useNavigate, useLocation } from 'react-router-dom'

export default function Sidebar() {
  const navigate = useNavigate()
  const location = useLocation()

  return (
    <aside className="sidebar">
      <div className="brand">
        <div className="brand-mark"></div>
        <div>
          <p className="brand-label">Rookie</p>
          <h2>Admin</h2>
        </div>
      </div>

      <nav className="sidebar-nav">
        <button
          className={
            location.pathname === '/categories' || location.pathname === '/'
              ? 'nav-item active'
              : 'nav-item'
          }
          onClick={() => navigate('/categories')}
        >
          Category
        </button>
        <button
          className={
            location.pathname === '/products' ? 'nav-item active' : 'nav-item'
          }
          onClick={() => navigate('/products')}
        >
          Product
        </button>
      </nav>
    </aside>
  )
}
