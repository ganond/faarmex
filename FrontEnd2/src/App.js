import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './App.css'; // Importa los estilos CSS

function App() {
  const [formData, setFormData] = useState({
    username: '',
    password: '',
    birthDate: '',
    profileId: '',
  });

  const [profiles, setProfiles] = useState([]);
  const [errors, setErrors] = useState({});

  useEffect(() => {
    fetchProfiles();
  }, []);

  const fetchProfiles = async () => {
    try {
      const response = await axios.get('http://localhost:5211/api/Usuarios/Perfiles');
      setProfiles(response.data);
    } catch (error) {
      console.error('Error fetching profiles:', error);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const validationErrors = validateForm(formData);
    if (Object.keys(validationErrors).length === 0) {
      submitForm(formData);
    } else {
      setErrors(validationErrors);
    }
  };

  const validateForm = (data) => {
    const errors = {};

    if (!data.username) {
      errors.username = 'El nombre de usuario es obligatorio';
    }
    if (!data.password) {
      errors.password = 'La contraseña es obligatoria';
    } else if (data.password.length !== 10) {
      errors.password = 'La contraseña debe tener una longitud de 10 caracteres';
    } else if (data.password.includes(data.username)) {
      errors.password = 'La contraseña no puede contener el nombre de usuario';
    }
    if (!data.birthDate) {
      errors.birthDate = 'La fecha de nacimiento es obligatoria';
    } else if (!isValidDateFormat(data.birthDate)) {
      errors.birthDate = 'El formato de fecha de nacimiento debe ser DD-MM-YYYY';
    } else {
      const dateParts = data.birthDate.split('-');
      const dateObject = new Date(`${dateParts[2]}-${dateParts[1]}-${dateParts[0]}`);
      if (isNaN(dateObject.getTime()) || dateObject > new Date()) {
        errors.birthDate = 'La fecha de nacimiento no es válida';
      }
    }
    if (!data.profileId) {
      errors.profileId = 'Debe seleccionar un perfil';
    }

    return errors;
  };

  const isValidDateFormat = (date) => {
    const dateFormat = /^\d{2}-\d{2}-\d{4}$/;
    return dateFormat.test(date);
  };

  const submitForm = async (data) => {
    try {
      await axios.post('http://localhost:5211/api/Usuarios/Registrar', data);
      alert('Usuario registrado exitosamente');
    } catch (error) {
      console.error('Error submitting form:', error);
    }
  };

  return (
    <div className="app-container">
      <h1>Registro de Usuario</h1>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="username">Nombre de usuario:</label>
          <input
            type="text"
            id="username"
            name="username"
            value={formData.username}
            onChange={handleChange}
          />
          {errors.username && <span className="error">{errors.username}</span>}
        </div>
        <div className="form-group">
          <label htmlFor="password">Contraseña:</label>
          <input
            type="password"
            id="password"
            name="password"
            value={formData.password}
            onChange={handleChange}
          />
          {errors.password && <span className="error">{errors.password}</span>}
        </div>
        <div className="form-group">
          <label htmlFor="birthDate">Fecha de nacimiento (DD-MM-YYYY):</label>
          <input
            type="text"
            id="birthDate"
            name="birthDate"
            value={formData.birthDate}
            onChange={handleChange}
          />
          {errors.birthDate && <span className="error">{errors.birthDate}</span>}
        </div>
        <div className="form-group">
          <label htmlFor="profileId">Perfil:</label>
          <select
            id="profileId"
            name="profileId"
            value={formData.profileId}
            onChange={handleChange}
          >
            <option value="">Seleccione un perfil</option>
            {profiles.map((profile) => (
              <option key={profile.id_perfil} value={profile.id_perfil}>
                {profile.nombre_perfil}
              </option>
            ))}
          </select>
          {errors.profileId && <span className="error">{errors.profileId}</span>}
        </div>
        <button type="submit">Registrar</button>
      </form>
    </div>
  );
}

export default App;
