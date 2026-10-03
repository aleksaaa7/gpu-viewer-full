import { useState } from 'react'
import './Login.css'

function Login({ onLoginSuccess }) {
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')

  const handleSubmit = (e) => {
    e.preventDefault()
    setError('')

    fetch('https://localhost:7155/api/Auth/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ username, password })
    })
      .then(response => {
        if (!response.ok) {
          throw new Error('Pogrešno korisničko ime ili lozinka')
        }
        return response.json()
      })
      .then(data => {
        localStorage.setItem('accessToken', data.accessToken)
        localStorage.setItem('username', data.username)
        localStorage.setItem('roles', JSON.stringify(data.roles))
        onLoginSuccess(data)
      })
      .catch(err => {
        setError(err.message)
      })
  }

  return (
    <div className="login-container">
      <form className="login-form" onSubmit={handleSubmit}>
        <h2>Login</h2>

        {error && <p className="error-message">{error}</p>}

        <label>
          Username
          <input
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
          />
        </label>

        <label>
          Password
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </label>

        <button type="submit">Log in</button>
      </form>
    </div>
  )
}

export default Login