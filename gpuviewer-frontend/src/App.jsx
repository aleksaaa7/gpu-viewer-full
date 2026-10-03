import { useState, useEffect } from 'react'
import './App.css'
import Navbar from './components/Navbar'
import GraphicsCardList from './components/GraphicsCardList'
import CompareCards from './components/CompareCards'
import CompareCpu from './components/CompareCpu'
import Login from './components/Login'
import AddCard from './components/AddCard'

function App() {
  const [activeTab, setActiveTab] = useState('all')
  const [user, setUser] = useState(null)

  useEffect(() => {
    const savedUsername = localStorage.getItem('username')
    const savedToken = localStorage.getItem('accessToken')
    const savedRoles = localStorage.getItem('roles')
    if (savedUsername && savedToken) {
      setUser({
        username: savedUsername,
        roles: savedRoles ? JSON.parse(savedRoles) : []
      })
    }
  }, [])

  const handleLoginSuccess = (data) => {
    setUser({ username: data.username, roles: data.roles })
    setActiveTab('all')
  }

  const handleLogout = () => {
    localStorage.removeItem('accessToken')
    localStorage.removeItem('username')
    localStorage.removeItem('roles')
    setUser(null)
    setActiveTab('all')
  }

  const isAdmin = user?.roles?.includes('Admin')

  return (
    <div>
      <Navbar
        activeTab={activeTab}
        onTabChange={setActiveTab}
        user={user}
        onLogout={handleLogout}
        isAdmin={isAdmin}
      />

      {activeTab === 'all' && <GraphicsCardList />}
      {activeTab === 'compare' && <CompareCards />}
      {activeTab === 'cpu' && <CompareCpu />}
      {activeTab === 'login' && <Login onLoginSuccess={handleLoginSuccess} />}
      {activeTab === 'add' && isAdmin && <AddCard />}
    </div>
  )
}

export default App