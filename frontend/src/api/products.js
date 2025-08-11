import API from './apiClient';
export const getProducts = () => API.get('/product').then(r => r.data);
export const createProduct = (p) => API.post('/product', p).then(r => r.data);
export const updateProduct = (id, p) => API.put(`/product/${id}`, {...p, id}).then(r => r.data);
export const deleteProduct = (id) => API.delete(`/product/${id}`).then(r => r.data);
