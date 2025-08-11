import React from 'react'
import { createRoot } from 'react-dom/client'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom'
import Login from './pages/Login'
import ProductList from './pages/ProductList'
import './styles.css'

const queryClient = new QueryClient()

function App(){
  const token = localStorage.getItem('token')
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<Login/>} />
          <Route path="/products" element={ token ? <ProductList/> : <Navigate to="/login" />} />
          <Route path="*" element={<Navigate to={token ? "/products" : "/login"} />} />
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  )
}

createRoot(document.getElementById('root')).render(<App/>)
