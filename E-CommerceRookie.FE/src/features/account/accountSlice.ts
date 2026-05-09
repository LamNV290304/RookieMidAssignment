import { createSlice } from '@reduxjs/toolkit'
import type { PayloadAction } from '@reduxjs/toolkit'
import type { AccountProfile, AccountState } from './types'

const initialState: AccountState = {
	profile: null,
	loading: false,
	error: null,
}

export const accountSlice = createSlice({
	name: 'account',
	initialState,
	reducers: {
		accountStart(state) {
			state.loading = true
			state.error = null
		},
		accountSuccess(state, action: PayloadAction<AccountProfile>) {
			state.loading = false
			state.profile = action.payload
		},
		accountFailure(state, action: PayloadAction<string>) {
			state.loading = false
			state.error = action.payload
		},
		clearAccount(state) {
			state.profile = null
			state.loading = false
			state.error = null
		},
	},
})

export const { accountStart, accountSuccess, accountFailure, clearAccount } =
	accountSlice.actions

export default accountSlice.reducer
