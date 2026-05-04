export type AccountProfile = {
	id: string
	email: string
	name: string
	phone?: string
	address?: string
}

export type AccountState = {
	profile: AccountProfile | null
	loading: boolean
	error: string | null
}
