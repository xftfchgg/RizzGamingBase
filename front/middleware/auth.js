
import { VueCookieNext as $cookie } from 'vue-cookie-next'
export default defineNuxtRouteMiddleware((to, from) => {
    if (process.client) {
    const isAcvice=$cookie.isCookieAvailable('accountId');
    console.log(isAcvice)
    if (isAcvice) {
        return navigateTo('/');
    }
   }
});

