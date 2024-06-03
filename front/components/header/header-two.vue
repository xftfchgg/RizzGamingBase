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

                      <template v-if="isAccountIdExists === false" v-for="menu in menu_data_test" :key="menu.id">
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
                      <template v-if="isAccountIdExists" v-for="menu in menu_data_test" :key="menu.id">
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
  
  
                    </ul>
                  </div>
                  <div class="tgmenu__action d-none d-md-block">
                    <ul class="list-wrap">
                      <!-- 搜尋 -->
                      <li class="search">
                        <a class="cursor-pointer" @click="handleOpenSearch('/audio/click.wav')">
                          <i class="flaticon-search-1"></i>
                        </a>
                      </li>
  
                      <!-- 登入按鈕 -->
                      <li class="header-btn" v-if="isAccountIdExists === false">
                        <nuxt-link to="/Login" :class="`${style_2 ? 'tg-btn-3 tg-svg' : 'tg-border-btn'}`">
                          <svg-animate-icon v-if="style_2" icon='/images/icons/shape02.svg' id="svg-2" />
                          <i class="flaticon-edit"></i> ~sign in
                        </nuxt-link>
                      </li>
  
                      <!-- 購物車 -->
  
  
                      <li class="search" v-if="isAccountIdExists">
                        <nuxt-link to="/">
                          <!-- <img src="/images/icons/shopping-cart.png"> -->
                          <img src="/images/icons/shopping-cart.png">
                        </nuxt-link>
                      </li>
  
                      <!-- 會員名稱 -->
                      <li class="search" v-if="isAccountIdExists">
                        <nuxt-link to="/">
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
                      <li class="side-toggle-icon" @click="handleOpenOffCanvas('/audio/click.wav')">
                        <span></span>
                        <span></span>
                        <span></span>
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
  
  <script setup lang="ts">
  import menu_data from '@/data/menu-data';
  import menu_data_cookie from '@/data/menu-data-cookie';
  import menu_data_test from '@/data/menu-data-test';
  import connection from '@/data/signalR';
  
  let name ="";
  
  defineProps<{ style_2?: boolean }>();
  
  
  import { VueCookieNext as $cookie } from 'vue-cookie-next'
  let isAccountIdExists: boolean;
  
  
  
  
    //  // 监听 accountId cookie 的变化
    //  watch(() => $cookie.getCookie('accountId'), (newVal, oldVal) => {
    //   // 如果 cookie 发生变化，更新 isAccountIdExists 的值
    //   isAccountIdExists = $cookie.isCookieAvailable('accountId');
    //   // 处理其他逻辑
    // });
  
  // onMounted
  onBeforeMount(() => {
    isAccountIdExists = $cookie.isCookieAvailable('accountId');
    if($cookie.getCookie("name")){
    name =$cookie.getCookie("name");
   }
  
  });
  
  
  const { isSticky, isStickyVisible } = useSticky();
  const route = useRoute();
  const isActive = ref<boolean>(false);
  const isOffCanvasOpen = ref<boolean>(false);
  const isMobileOffCanvasOpen = ref<boolean>(false);
  
  const handleOpenSearch = (audioPath: string) => {
    const audio = new Audio(audioPath);
    audio.play();
    isActive.value = !isActive.value;
  };
  
  const handleCloseSearch = (audioPath: string) => {
    const audio = new Audio(audioPath);
    audio.play();
    isActive.value = !isActive.value;
  };
  // off canvas
  const handleOpenOffCanvas = (audioPath: string) => {
    const audio = new Audio(audioPath);
    audio.play();
    isOffCanvasOpen.value = !isOffCanvasOpen.value;
  };
  
  const handleCloseOffCanvas = (audioPath: string) => {
    const audio = new Audio(audioPath);
    audio.play();
    isOffCanvasOpen.value = !isOffCanvasOpen.value;
  };
  // mobile off canvas
  const handleOpenMobileOffCanvas = (audioPath: string) => {
    const audio = new Audio(audioPath);
    audio.play();
    isMobileOffCanvasOpen.value = !isMobileOffCanvasOpen.value;
  };
  
  const handleCloseMobileOffCanvas = (audioPath: string) => {
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
  
  
    connection.stop().then(() => {
        console.log('SignalR 斷開連線');
      }).catch(err => {
        console.error('SignalR connection failed:', err.toString());
      });
  
  
    location.reload();
  
  };
  
  
  
  </script>
  