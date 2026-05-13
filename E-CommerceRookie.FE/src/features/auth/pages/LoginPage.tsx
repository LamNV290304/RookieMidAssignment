import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import { login, clearError } from '../authSlice'
import { useAppDispatch, useAppSelector } from '../../../app/hooks'

export default function LoginPage() {
  const dispatch = useAppDispatch()
  const { loading, error } = useAppSelector((state) => state.auth)
  const navigate = useNavigate()
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [formError, setFormError] = useState<string | null>(null)

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    setFormError(null)
    if (error) dispatch(clearError())

    if (!email.trim() || !password) {
      setFormError('Please fill in all fields.')
      return
    }

    const result = await dispatch(login({ username: email.trim(), password }))
    if (login.fulfilled.match(result)) {
      navigate('/', { replace: true })
    }
  }

  return (
    <div className="auth-layout">
      <div className="auth-card">
        <div>
          <p className="auth-eyebrow">Rookie Admin</p>
          <h1>Welcome back</h1>
          <p className="subtitle">Sign in to manage the dashboard.</p>
        </div>

        {formError || error ? (
          <div className="error-banner">{formError || error}</div>
        ) : null}

        <form className="auth-form" onSubmit={handleSubmit}>
          <label className="field">
            Email
            <input
              type="email"
              value={email}
              onChange={(event) => setEmail(event.target.value)}
              placeholder="admin@example.com"
              autoComplete="email"
            />
          </label>

          <label className="field">
            Password
            <input
              type="password"
              value={password}
              onChange={(event) => setPassword(event.target.value)}
              placeholder="Enter your password"
              autoComplete="current-password"
            />
          </label>

          <button className="primary-button" type="submit" disabled={loading}>
            {loading ? 'Signing in...' : 'Login'}
          </button>
        </form>
      </div>
    </div>
  )
}
