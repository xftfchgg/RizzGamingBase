<template>
  <header>
    <div id="sticky-header"
      :class="`tg-header__area transparent-header ${isSticky ? 'tg-sticky-menu' : ''} ${isStickyVisible ? 'sticky-menu__show' : ''}`">
      <div class="container custom-container">
        <div class="row">
          <div class="col-12">
            <div class="mobile-nav-toggler" @click="handleOpenMobileOffCanvas('/audio/click.wav')">
              <i class="fas fa-bars"></i>
            </div>
            <div class="tgmenu__wrap">
              <nav class="tgmenu__nav">
                <div class="logo">
                  <nuxt-link to="/">
                    <img src="/images/logo/logo.png" alt="Logo" />
                  </nuxt-link>
                </div>
                <div class="tgmenu__navbar-wrap tgmenu__main-menu d-none d-xl-flex">
                  <ul class="navigation">
                    <!-- 驗證抓不抓得到cookie -->
                    <!-- v-if="isAccountIdExists === false" -->
                    <template v-if="isAccountIdExists === false" v-for="menu in menu_data" :key="menu.id">
                      <li v-if="menu.sub_menu"
                        :class="{ 'menu-item-has-children active': menu.sub_menu && menu.sub_menu.some(sub => route.path === sub.link) }">
                        <nuxt-link to="#">
                          {{ menu.title }}
                        </nuxt-link>
                        <ul v-if="menu.sub_menu" class="sub-menu">
                          <li v-for="(sub, i) in menu.sub_menu" :key="i" :class="{ active: route.path === sub.link }">
                            <nuxt-link :to="sub.link">{{ sub.title }}</nuxt-link>
                          </li>
                        </ul>
                      </li>
                      <li v-else :class="{ active: route.path === menu.link }">
                        <nuxt-link :to="menu.link">
                          {{ menu.title }}
                        </nuxt-link>
                      </li>
                    </template>
                    <!-- 抓到時 -->
                    <template v-if="isAccountIdExists" v-for="menu in menu_data_cookie" :key="menu.id">
                      <li v-if="menu.sub_menu"
                        :class="{ 'menu-item-has-children active': menu.sub_menu && menu.sub_menu.some(sub => route.path === sub.link) }">
                        <nuxt-link to="#">
                          {{ menu.title }}
                        </nuxt-link>
                        <ul v-if="menu.sub_menu" class="sub-menu">
                          <li v-for="(sub, i) in menu.sub_menu" :key="i" :class="{ active: route.path === sub.link }">
                            <nuxt-link :to="sub.link">{{ sub.title }}</nuxt-link>
                          </li>
                        </ul>
                      </li>
                      <li v-else :class="{ active: route.path === menu.link }">
                        <nuxt-link :to="menu.link">
                          {{ menu.title }}
                        </nuxt-link>
                      </li>
                      <chatroom-toasts></chatroom-toasts>

                    </template>


                  </ul>
                </div>
                <div class="tgmenu__action d-none d-md-block">
                  <ul class="list-wrap">

                    <!-- 登入按鈕 -->
                    <li class="header-btn" v-if="isAccountIdExists === false">
                      <nuxt-link to="/Login" :class="`${style_2 ? 'tg-btn-3 tg-svg' : 'tg-border-btn'}`">
                        <svg-animate-icon v-if="style_2" icon='/images/icons/shape02.svg' id="svg-2" />
                        <i class="flaticon-edit"></i> ~sign in
                      </nuxt-link>
                    </li>

                    <!-- 購物車 -->


                    <li class="search" v-if="isAccountIdExists">
                      <nuxt-link to="/checkout/cart">
                        <img src="../../public/images/icons/shopping-cart.png">
                      </nuxt-link>
                    </li>



                    <!-- 會員頭像 -->
                    <li class="search" v-if="isAccountIdExists">
                      <nuxt-link to="">
                        <div style="height: 30px; width:30px" class="my-outer-container">
                          <img :src="ava" alt="Member Avatar" class="my-image">
                          <!-- <img src="/public/images/Bonus/4/ApexFrame.png" alt="Image 2" class="my-Frameimage" /> -->
                        </div>
                      </nuxt-link>
                    </li>

                    <!-- 會員名稱 -->
                    <li class="search" v-if="isAccountIdExists">
                      <nuxt-link :to="`/Id=${id}/profiles`">
                        {{ name }}
                      </nuxt-link>
                    </li>
                    <li class="header-btn" v-if="isAccountIdExists">
                      <!-- @click="handleSignOut" -->
                      <nuxt-link :class="`${style_2 ? 'tg-btn-3 tg-svg' : 'tg-border-btn'}`" @click="handleSignOut">
                        <svg-animate-icon v-if="style_2" icon='/images/icons/shape02.svg' id="svg-2" />
                        <i class="flaticon-edit"></i> ~sign out
                      </nuxt-link>
                    </li>

                  </ul>
                </div>
              </nav>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- header-search -->
    <popup-search :isActive="isActive" @closeSearch="handleCloseSearch"></popup-search>
    <!-- header-search-end -->

    <!--  off canvas start -->
    <off-canvas :isOffCanvasOpen="isOffCanvasOpen" @closeOffcanvas="handleCloseOffCanvas" />
    <!--  off canvas end -->

    <!-- mobile off canvas start -->
    <mobile-offcanvas :isMobileOffCanvasOpen="isMobileOffCanvasOpen"
      @closeMobileOffcanvas="handleCloseMobileOffCanvas" />
    <!-- mobile off canvas end -->
  </header>
</template>


<script setup>
import menu_data from '@/data/menu-data';
import menu_data_cookie from '@/data/menu-data-cookie';
import { ref, onBeforeMount } from 'vue'; // 引入 ref 和 onBeforeMount
import { useRouter } from 'vue-router';
import { VueCookieNext as $cookie } from 'vue-cookie-next';



const router = useRouter();

router.beforeEach((to, from) => {
  checkLogin()
  if ($cookie.getCookie("name")) {
    name = $cookie.getCookie("name");
  }
  if ($cookie.getCookie("avatarUrl")) {
    ava = $cookie.getCookie("avatarUrl");
  }
})
let ava = "";
let name = ref("");
let id = ref("");
defineProps({ style_2: Boolean });





let isAccountIdExists = ref(false);

// onMounted
onBeforeMount(() => {
  isAccountIdExists.value = $cookie.isCookieAvailable('accountId');
  if ($cookie.isCookieAvailable("name")) {
    name.value = $cookie.getCookie("name");
  }
  if ($cookie.isCookieAvailable("accountId")) {
    id.value = $cookie.getCookie('accountId');
    // id.value = parseInt($cookie.getCookie('Id'), 10);
    console.log(id.value)
  }
  if ($cookie.getCookie("avatarUrl")) {
    ava = $cookie.getCookie("avatarUrl");
  }
});

onMounted(() => {

});




function checkLogin() {
  isAccountIdExists.value = $cookie.isCookieAvailable('accountId');
  if ($cookie.isCookieAvailable("accountId")) {
    id.value = $cookie.getCookie('accountId');
    // id.value = parseInt($cookie.getCookie('Id'), 10);
    console.log(id.value)
  }
}

const { isSticky, isStickyVisible } = useSticky();
const route = useRoute();
const isActive = ref(false);
const isOffCanvasOpen = ref(false);
const isMobileOffCanvasOpen = ref(false);

const handleOpenSearch = (audioPath) => {
  const audio = new Audio(audioPath);
  audio.play();
  isActive.value = !isActive.value;
};

const handleCloseSearch = (audioPath) => {
  const audio = new Audio(audioPath);
  audio.play();
  isActive.value = !isActive.value;
};
// off canvas
const handleOpenOffCanvas = (audioPath) => {
  const audio = new Audio(audioPath);
  audio.play();
  isOffCanvasOpen.value = !isOffCanvasOpen.value;
};

const handleCloseOffCanvas = (audioPath) => {
  const audio = new Audio(audioPath);
  audio.play();
  isOffCanvasOpen.value = !isOffCanvasOpen.value;
};
// mobile off canvas
const handleOpenMobileOffCanvas = (audioPath) => {
  const audio = new Audio(audioPath);
  audio.play();
  isMobileOffCanvasOpen.value = !isMobileOffCanvasOpen.value;
};

const handleCloseMobileOffCanvas = (audioPath) => {
  const audio = new Audio(audioPath);
  audio.play();
  isMobileOffCanvasOpen.value = !isMobileOffCanvasOpen.value;
};

const handleSignOut = () => {
  $cookie.removeCookie('accountId');
  $cookie.removeCookie('avatarUrl');
  $cookie.removeCookie('bonus');
  $cookie.removeCookie('name');
  $cookie.removeCookie('google');
  $cookie.removeCookie('Id');
  $cookie.removeCookie('jwt');
  location.reload();
};


// try{

// }catch{
//   console.log("cookiename")
// }

if (process.client) {
  watch(() => $cookie.getCookie("name"), (newVal) => {
    name.value = newVal;
  });
}
//   watch($cookie.getCookie("name"), (newVal) => {
//   name.value = newVal;
</script>


<style lang="scss">
@import "@/assets/css/my-style.css";

.my-outer-container {
  position: relative;
  width: 250px;
  height: 250px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.my-image {
  position: absolute;
  top: 50%;
  /* 依附於父物件的垂直中心點 */
  left: 50%;
  /* 依附於父物件的水平中心點 */
  transform: translate(-50%, -50%);
  /* 把圖從第1象限調整至第3象限調整中心點(應該?) */
  width: 93%;
  height: auto;
}

.my-Frameimage {
  position: absolute;
  top: 50%;
  /* 依附於父物件的垂直中心點 */
  left: 50%;
  /* 依附於父物件的水平中心點 */
  transform: translate(-50%, -50%);
  /* 把圖從第1象限調整至第3象限調整中心點(應該?) */
  width: 100%;
  height: auto;
}
</style>