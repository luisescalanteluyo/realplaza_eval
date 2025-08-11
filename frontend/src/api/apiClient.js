import axios from 'axios';
const API = axios.create({
  baseURL: import.meta.env.VITE_API_URL || 'http://localhost:25723/api',
  headers: {
        //'Access-Control-Allow-Origin': '*',
        'Content-Type': 'application/json'
    }
});

API.interceptors.request.use(cfg => {
  const t = localStorage.getItem('token');
  if (t) cfg.headers.Authorization = `Bearer ${t}`;
  return cfg;
});

export default API;
