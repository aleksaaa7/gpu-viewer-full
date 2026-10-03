import { useState, useEffect } from 'react'
import CardSearchSelect from './CardSearchSelect'
import './CompareCards.css'

function CompareCards() {
  const [cards, setCards] = useState([])
  const [id1, setId1] = useState('')
  const [id2, setId2] = useState('')
  const [result, setResult] = useState(null)
  const [error, setError] = useState('')

  useEffect(() => {
    fetch('https://localhost:7155/api/GraphicsCards')
      .then(response => response.json())
      .then(data => setCards(data))
      .catch(err => console.error('Error fetching cards:', err))
  }, [])

  const handleCompare = () => {
    setError('')
    setResult(null)

    if (!id1 || !id2) {
      setError('Please select both cards.')
      return
    }

    if (id1 === id2) {
      setError('Please select two different cards.')
      return
    }

    fetch(`https://localhost:7155/api/GraphicsCards/compare?id1=${id1}&id2=${id2}`)
      .then(response => {
        if (!response.ok) {
          throw new Error('Error comparing cards.')
        }
        return response.json()
      })
      .then(data => setResult(data))
      .catch(err => setError(err.message))
  }

  return (
    <div className="compare-container">
      <h2>Compare Cards</h2>

      <div className="compare-selectors">
        <CardSearchSelect
          cards={cards}
          selectedId={id1}
          onSelect={setId1}
          placeholder="Select first card"
        />

        <span className="vs-label">VS</span>

        <CardSearchSelect
          cards={cards}
          selectedId={id2}
          onSelect={setId2}
          placeholder="Select second card"
        />

        <button onClick={handleCompare}>Compare</button>
      </div>

      {error && <p className="error-message">{error}</p>}

      {result && (
        <div className="compare-result">
          <div className="result-header">
            <div className="result-card-name">{result.card1.manufacturer} {result.card1.model}</div>
            <div className="result-vs">VS</div>
            <div className="result-card-name">{result.card2.manufacturer} {result.card2.model}</div>
          </div>

          {result.newerCard && (
            <p className="info-line">Newer card: <strong>{result.newerCard}</strong></p>
          )}

          <div className="metrics-list">
            {result.metrics.map((metric, index) => (
              <div className="metric-row" key={index}>
                <span className="metric-label">{metric.fieldName}</span>
                <span className="metric-winner">
                  {metric.betterCard === 'Jednako'
                    ? 'Equal'
                    : `${metric.betterCard} (+${metric.percentageDifference}%)`}
                </span>
              </div>
            ))}
          </div>

          <div className="overall-result">
            <span>Overall better card:</span>
            <strong>
              {result.overallBetterCard} ({result.overallPercentage}%)
            </strong>
          </div>
        </div>
      )}
    </div>
  )
}

export default CompareCards