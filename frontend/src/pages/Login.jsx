import React, {useState} from 'react'
import { useNavigate } from 'react-router-dom'
import { login,testauth } from '../api/auth'

export default function Login(){
  const [username, setUsername] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState(null)
  const nav = useNavigate()

  const onSubmit = async (e) => {
    e.preventDefault()
    try {

      console.log("login");
      const resp = await login({ Username:username, password });
      console.log("resp",resp);
      localStorage.setItem('token', resp.token);
      nav('/products');
    } catch (err) {
      setError('Login failed')
    }
  }

  return (
    <div className="container">
      <h2>Login</h2>
      <form onSubmit={onSubmit}>
        <div><input placeholder="Username" value={username} onChange={e=>setUsername(e.target.value)} /></div>
        <div><input placeholder="Password" type="password" value={password} onChange={e=>setPassword(e.target.value)} /></div>
        <div><button type="submit">Login</button></div>
        {error && <div style={{color:'red'}}>{error}</div>}
      </form>
      <p>Para probar, registra un usuario vía POST /auth/register </p>
    </div>
  )
}
