import { useNavigate } from 'react-router-dom'
import { useAppDispatch, useAppSelector } from '../../app/hooks'
import { clearToken } from '../../features/auth/authSlice'

export default function Header() {
  const dispatch = useAppDispatch()
  const token = useAppSelector((state) => state.auth.token)
  const navigate = useNavigate()
  const hasToken = !!token

  const handleLogout = () => {
    localStorage.removeItem('accessToken')
    dispatch(clearToken())
    navigate('/login', { replace: true })
  }

  return (
    <header className="dashboard-header">
      <div>
        <p className="header-label">Admin Workspace</p>
        <h2>Rookie Commerce</h2>
      </div>
      <button
        className="ghost-button"
        onClick={handleLogout}
        disabled={!hasToken}
      >
        Logout
      </button>
    </header>
  )
}
