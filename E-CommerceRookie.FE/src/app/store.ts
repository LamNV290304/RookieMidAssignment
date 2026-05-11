import { configureStore } from '@reduxjs/toolkit'
import authReducer from '../features/auth/authSlice'
import accountReducer from '../features/account/accountSlice'
import categoryReducer from '../features/category/categorySlice'

export const store = configureStore({
	reducer: {
		auth: authReducer,
		account: accountReducer,
		category: categoryReducer,
	},
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
