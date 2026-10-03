import './Navbar.css'

function Navbar({ activeTab, onTabChange, user, onLogout, isAdmin }) {
  const tabs = [
    { id: 'all', label: 'All Cards' },
    { id: 'compare', label: 'Compare Cards' },
    { id: 'cpu', label: 'Compare CPU' },
    ...(isAdmin ? [{ id: 'add', label: 'Add Card' }] : [])
  ]

  return (
    <nav className="navbar">
      <div className="navbar-title">GPU Viewer</div>
      <div className="navbar-tabs">
        {tabs.map(tab => (
          <button
            key={tab.id}
            className={`nav-tab ${activeTab === tab.id ? 'active' : ''}`}
            onClick={() => onTabChange(tab.id)}
          >
            {tab.label}
          </button>
        ))}
      </div>

      <div className="navbar-auth">
        {user ? (
          <>
            <span className="username-display">{user.username}</span>
            <button className="nav-tab" onClick={onLogout}>Logout</button>
          </>
        ) : (
          <button
            className={`nav-tab ${activeTab === 'login' ? 'active' : ''}`}
            onClick={() => onTabChange('login')}
          >
            Login
          </button>
        )}
      </div>
    </nav>
  )
}

export default Navbar