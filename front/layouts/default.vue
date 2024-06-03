<template>
  <div>
    <header-one></header-one>
    <main class="main--area">
      <slot />
    </main>
    <footer-one></footer-one>
    <ChatroomChatBot></ChatroomChatBot>

    <back-to-top></back-to-top>
  </div>
</template>

<script setup lang="ts">
import { VueCookieNext as $cookie } from 'vue-cookie-next'
let isAccountIdExists: boolean;



onBeforeMount(() => {
  isAccountIdExists = $cookie.isCookieAvailable('accountId');
}

);
</script>
