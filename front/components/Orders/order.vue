<template>
  <div class="container">
    <h1 class="text-center mb-4">會員訂單</h1>
    <div v-if="orders.length > 0">
      <div v-for="order in orders" :key="order.id" class="card mt-3">
        <div class="card-body">
          <p><strong>訂單編號:</strong> {{ order.id }}</p>
          <p><strong>遊戲名稱:</strong> {{ order.game.name }}</p>
          <p><strong>遊戲價格:</strong> {{ order.game.price }}</p>
        </div>
      </div>
    </div>
    <div v-else>
      <p class="text-center">該會員尚無訂單</p>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';
import { VueCookieNext as $cookie } from 'vue-cookie-next';

const memberId = ref(''); // 初始化會員ID
const orders = ref([]); // 定義存儲訂單的數組

// 在組件掛載時自動執行的函數
onMounted(async () => {
  try {
    // 從 Cookie 中獲取會員ID
   let id;
  id = $cookie.getCookie('Id');
    // 向後端發送請求獲取該會員的所有訂單資料
    const response = await axios.get(`https://localhost:7048/api/Order/member/${id}`);
    orders.value = response.data;
  } catch (error) {
    console.error('獲取訂單時出錯：', error);
  }
});
</script>
<style scoped>
/* 添加您的自定義樣式 */
</style>
