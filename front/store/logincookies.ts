// 在 `logincookies.ts` 文件中定义 useAuthStore
import { defineStore } from 'pinia'

export const useAuthStore = defineStore({
  id: 'auth',
  state: () => ({
    accountId: null,

  }),
  getters: {
    isAuthenticated: state => !!state.accountId
  },
  actions: {
    setCookieData(cookieData) {
      this.accountId = cookieData.accountId
    }
  }
})

// import { defineStore } from 'pinia'

// export const useAuthStore = defineStore({
//   id: 'auth',
//   state: () => ({
//     accountId: null,
//     avatarUrl: null,
//     bonus: null,
//     name: null,
//     Id: null
//   }),
//   getters: {
//     isAuthenticated: state => !!state.accountId
//   },
//   actions: {
//     setCookieData(cookieData) {
//       this.accountId = cookieData.accountId
//       this.avatarUrl = cookieData.avatarUrl
//       this.bonus = cookieData.bonus
//       this.name = cookieData.name
//       this.Id = cookieData.Id
//     }
//   }
// })