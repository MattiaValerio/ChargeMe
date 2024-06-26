import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'https://api.example.com', // Sostituisci con la tua base URL
  timeout: 10000, // Tempo di timeout per le richieste
  headers: {
    'Content-Type': 'application/json',
  },
});

// Interceptor per la richiesta
axiosInstance.interceptors.request.use(
  (config) => {
    // Aggiungi token di autenticazione qui, se necessario
    const token = localStorage.getItem('token');
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// Interceptor per la risposta
axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => {
    // Gestisci errori di risposta qui
    if (error.response && error.response.status === 401) {
      // Esempio: logout utente o refresh token
    }
    return Promise.reject(error);
  }
);

export default axiosInstance;
