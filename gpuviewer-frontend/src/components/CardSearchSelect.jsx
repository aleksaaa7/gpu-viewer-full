import { useState, useRef, useEffect } from 'react'
import './CardSearchSelect.css'

function CardSearchSelect({ cards, selectedId, onSelect, placeholder }) {
  const [searchTerm, setSearchTerm] = useState('')
  const [isOpen, setIsOpen] = useState(false)
  const wrapperRef = useRef(null)

  const selectedCard = cards.find(c => c.id === Number(selectedId))

  useEffect(() => {
    function handleClickOutside(event) {
      if (wrapperRef.current && !wrapperRef.current.contains(event.target)) {
        setIsOpen(false)
      }
    }
    document.addEventListener('mousedown', handleClickOutside)
    return () => document.removeEventListener('mousedown', handleClickOutside)
  }, [])

  const filteredCards = cards.filter(card => {
    const search = searchTerm.toLowerCase()
    return (
      card.model.toLowerCase().includes(search) ||
      card.manufacturer.toLowerCase().includes(search)
    )
  }).slice(0, 50)

  const handlePick = (card) => {
    onSelect(card.id)
    setSearchTerm('')
    setIsOpen(false)
  }

  return (
    <div className="search-select-wrapper" ref={wrapperRef}>
      <input
        type="text"
        placeholder={selectedCard ? `${selectedCard.manufacturer} ${selectedCard.model}` : placeholder}
        value={searchTerm}
        onFocus={() => setIsOpen(true)}
        onChange={(e) => {
          setSearchTerm(e.target.value)
          setIsOpen(true)
        }}
      />

      {isOpen && (
        <div className="search-select-dropdown">
          {filteredCards.length === 0 && (
            <div className="no-results">Nema rezultata</div>
          )}
          {filteredCards.map(card => (
            <div
              className="search-select-option"
              key={card.id}
              onClick={() => handlePick(card)}
            >
              {card.manufacturer} {card.model}
            </div>
          ))}
        </div>
      )}
    </div>
  )
}

export default CardSearchSelect