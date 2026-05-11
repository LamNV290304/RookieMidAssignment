import { Outlet } from 'react-router-dom'
import Sidebar from '../components/common/Sidebar'

export default function DashboardLayout() {
  return (
    <div className="dashboard">
      <Sidebar />
      <main className="content">
        <Outlet />
      </main>
    </div>
  )
}
