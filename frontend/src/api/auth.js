import API from './apiClient';
//http://localhost:25723/api/Auth
export const testauth = () => API.get('/auth').then(r => r.data);
export const login = (data) => API.post('/auth/login', data).then(r => r.data);
export const register = (data) => API.post('/auth/register', data).then(r => r.data);
