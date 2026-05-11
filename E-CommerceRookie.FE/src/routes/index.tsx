import { createBrowserRouter } from 'react-router-dom'
import DashboardLayout from '../layouts/DashboardLayout'
import CategoryPage from '../features/category/pages/CategoryPage'
import ProductPage from '../features/product/pages/ProductPage'
import ProductDetailPage from '../features/product/pages/ProductDetailPage'
import CustomerPage from '../features/customer/pages/CustomerPage'

export const router = createBrowserRouter([
  {
    path: '/',
    element: <DashboardLayout />,
    children: [
      { index: true, element: <CategoryPage /> },
      { path: 'categories', element: <CategoryPage /> },
      { path: 'products', element: <ProductPage /> },
      { path: 'products/:id', element: <ProductDetailPage /> },
      { path: 'customers', element: <CustomerPage /> },
    ],
  },
])
