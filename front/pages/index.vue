<template>
  <div>
    <HomeAutoplay @load="checkLogin"></HomeAutoplay>
    <team-area></team-area>
    <div className="area-background" style="background-image: url(/images/bg/area_bg01.jpg)">
      <HomeTopsix></HomeTopsix>
    </div>
    <streamers-area></streamers-area>

    <!-- <button class="btn btn-success" @click="checkLogin">checkLogin</button> -->
    <div v-if="isLogin == true">
      <HomeGameformember></HomeGameformember>
    </div>
    <HomeGameArea></HomeGameArea>
    <HomeGamenew></HomeGamenew>

  </div>
</template>

<script setup>
import { VueCookieNext as $cookie } from 'vue-cookie-next';

//import { useRouter } from 'vue-router';
useSeoMeta({ title: "Home - MYKD" });

const isLogin = ref(false);

onMounted(
  function checkLogin() {
    let id;
    id = $cookie.getCookie('Id');
    if (id === null) {
      isLogin.value = false;
    } else {
      isLogin.value = true;
    }
  })


</script>
