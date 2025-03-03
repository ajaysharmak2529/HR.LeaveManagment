import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import { Provider } from 'react-redux'
import { store } from './Redux/Store/Store.ts'
import AppRoutes from './Routes/AppRoutes.tsx'

createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <Provider store={store}>
            <AppRoutes />
        </Provider>
    </StrictMode>,
)
