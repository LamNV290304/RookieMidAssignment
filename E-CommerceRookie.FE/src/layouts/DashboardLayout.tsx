import { Outlet } from 'react-router-dom'
import Header from '../components/common/Header'
import Sidebar from '../components/common/Sidebar'

export default function DashboardLayout() {
  return (
    <div className="dashboard">
      <Sidebar />
      <main className="content">
        <Header />
        <Outlet />
      </main>
    </div>
  )
}
