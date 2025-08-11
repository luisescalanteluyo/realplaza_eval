import React, {useState,useEffect} from 'react'

import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query'
import { getProducts, createProduct, updateProduct, deleteProduct } from '../api/products'

export default function ProductList(){
 // const qc = useQueryClient()
  //const { data, isLoading } = useQuery(['products'], getProducts)


  const [productos, setProducts] = useState()
//  const dataProductos  = getProducts()
//  console.log("data",data)


    useEffect(  ()=> {
      getProducts().then(data =>{
         console.log("datazz");
          console.log(data);
          setProducts(data);
      })
    },[]);

  // const createMut = useMutation(createProduct, { onSuccess: () => qc.invalidateQueries(['products']) })
  // const updateMut = useMutation(({id,p}) => updateProduct(id,p), { onSuccess: () => qc.invalidateQueries(['products']) })
  // const delMut = useMutation(id => deleteProduct(id), { onSuccess: () => qc.invalidateQueries(['products']) })

  // const [form, setForm] = useState({ name:'', description:'', price:0, stock:0, id: null })

  // if (isLoading) return <div className="container">Cargando...</div>

  // const submit = async (e) => {
  //   e.preventDefault()
  //   if (form.id) {
  //     await updateMut.mutateAsync({ id: form.id, p: { name: form.name, description: form.description, price: Number(form.price), stock: Number(form.stock) }})
  //   } else {
  //     await createMut.mutateAsync({ name: form.name, description: form.description, price: Number(form.price), stock: Number(form.stock) })
  //   }
  //   setForm({ name:'', description:'', price:0, stock:0, id: null })
  // }

  return (
    <div className="container">
      <h2>Productos</h2>
      {/* <form onSubmit={submit}>
        <input placeholder="Nombre" value={form.name} onChange={e=>setForm({...form, name:e.target.value})} />
       <input placeholder="Precio" value={form.price} onChange={e=>setForm({...form, price:e.target.value})} />
     
        <button type="submit">{form.id ? 'Actualizar' : 'Crear'}</button>
      </form> */}

      <hr/>
      <table>
 
        <tr>          
           <th>name</th>
           <th>price</th>
        </tr>
        {productos?.map(p => (
          <tr key={p.id}>
           
            <td>{p.name}</td>
             <td>{p.price}</td>
            {/* <button onClick={()=>setForm({ id: p.id, name: p.name, price: p.price })}>Editar</button> */}
            <button onClick={""}>editar</button>
          </tr>
        ))}
      
      </table>
      
     
    </div>
  )
}
