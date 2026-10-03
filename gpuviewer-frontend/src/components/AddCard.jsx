import { useState } from 'react'
import './AddCard.css'

function AddCard() {
  const [formData, setFormData] = useState({
    manufacturer: '',
    model: '',
    architecture: '',
    vramGb: '',
    memoryType: '',
    memoryBusWidth: '',
    coreClockMhz: '',
    boostClockMhz: '',
    tdpWatts: ''
  })
  const [message, setMessage] = useState('')
  const [isError, setIsError] = useState(false)

  const handleChange = (e) => {
    const { name, value } = e.target
    setFormData(prev => ({ ...prev, [name]: value }))
  }

  const handleSubmit = (e) => {
    e.preventDefault()
    setMessage('')

    const token = localStorage.getItem('accessToken')

    const payload = {
      ...formData,
      vramGb: parseFloat(formData.vramGb),
      memoryBusWidth: parseInt(formData.memoryBusWidth),
      coreClockMhz: parseFloat(formData.coreClockMhz),
      boostClockMhz: parseFloat(formData.boostClockMhz),
      tdpWatts: parseInt(formData.tdpWatts)
    }

    fetch('https://localhost:7155/api/GraphicsCards/add', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      body: JSON.stringify(payload)
    })
      .then(response => {
        if (!response.ok) {
          throw new Error('Error adding card')
        }
        return response.json()
      })
      .then(data => {
        setIsError(false)
        setMessage(`successfully added: ${data.manufacturer} ${data.model}`)
        setFormData({
          manufacturer: '', model: '', architecture: '', vramGb: '',
          memoryType: '', memoryBusWidth: '', coreClockMhz: '',
          boostClockMhz: '', tdpWatts: ''
        })
      })
      .catch(err => {
        setIsError(true)
        setMessage(err.message)
      })
  }

  return (
    <div className="add-card-container">
      <form className="add-card-form" onSubmit={handleSubmit}>
        <h2>Add new graphics card</h2>

        {message && (
          <p className={isError ? 'error-message' : 'success-message'}>{message}</p>
        )}

        <div className="form-grid">
          <label>
            Manufacturer
            <input name="manufacturer" value={formData.manufacturer} onChange={handleChange} required />
          </label>
          <label>
            Model
            <input name="model" value={formData.model} onChange={handleChange} required />
          </label>
          <label>
            Architecture
            <input name="architecture" value={formData.architecture} onChange={handleChange} />
          </label>
          <label>
            VRAM (GB)
            <input name="vramGb" type="number" step="0.1" value={formData.vramGb} onChange={handleChange} required />
          </label>
          <label>
            Memory Type
            <input name="memoryType" value={formData.memoryType} onChange={handleChange} required />
          </label>
          <label>
            Memory Bus Width
            <input name="memoryBusWidth" type="number" value={formData.memoryBusWidth} onChange={handleChange} required />
          </label>
          <label>
            Core Clock (MHz)
            <input name="coreClockMhz" type="number" value={formData.coreClockMhz} onChange={handleChange} required />
          </label>
          <label>
            Boost Clock (MHz)
            <input name="boostClockMhz" type="number" value={formData.boostClockMhz} onChange={handleChange} required />
          </label>
          <label>
            TDP (W)
            <input name="tdpWatts" type="number" value={formData.tdpWatts} onChange={handleChange} required />
          </label>
        </div>

        <button type="submit">Dodaj karticu</button>
      </form>
    </div>
  )
}

export default AddCard