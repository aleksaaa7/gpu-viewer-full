import { useState, useEffect } from 'react'
import './GraphicsCardList.css'

function GraphicsCardList() {
  const [cards, setCards] = useState([])
  const [loading, setLoading] = useState(true)
  const [searchTerm, setSearchTerm] = useState('')

  useEffect(() => {
    fetch('https://localhost:7155/api/GraphicsCards')
      .then(response => response.json())
      .then(data => {
        setCards(data)
        setLoading(false)
      })
      .catch(error => {
        console.error('Error fetching cards:', error)
        setLoading(false)
      })
  }, [])

  if (loading) {
    return <p className="status-message">Loading cards...</p>
  }

  const filteredCards = cards.filter(card => {
    const search = searchTerm.toLowerCase()
    return (
      card.model.toLowerCase().includes(search) ||
      card.manufacturer.toLowerCase().includes(search)
    )
  })

  return (
    <div className="card-list-container">
      <div className="card-list-header">
        <h2>Grafičke kartice <span className="count-badge">{filteredCards.length}</span></h2>
        <input
          type="text"
          className="search-input"
          placeholder="Search by model or Manufacturer"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
      </div>

      <div className="card-grid">
        {filteredCards.map(card => (
          <div className="gpu-card" key={card.id}>
            <div className="gpu-card-header">
              <span className="manufacturer">{card.manufacturer}</span>
              <span className="model">{card.model}</span>
            </div>
            <div className="gpu-card-specs">
              <div className="spec-row">
                <span className="spec-label">VRAM</span>
                <span className="spec-value">{card.vramGb} GB</span>
              </div>
              <div className="spec-row">
                <span className="spec-label">Memory</span>
                <span className="spec-value">{card.memoryType}</span>
              </div>
              <div className="spec-row">
                <span className="spec-label">Boost Clock</span>
                <span className="spec-value">{card.boostClockMhz} MHz</span>
              </div>
              <div className="spec-row">
                <span className="spec-label">TDP</span>
                <span className="spec-value">{card.tdpWatts} W</span>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  )
}

export default GraphicsCardList