<template>
  <div>
    <div v-if="cartGames && cartGames.length > 0">
      <div v-for="game in cartGames" :key="game.id">
        <p>購買遊戲：{{ game.name }},{{ game.images }}</p>
        <p>價格：{{ game.price }}</p>
      </div>
      <p>總價格：{{ total }}</p> <!-- 顯示總價格 -->
    </div>
    <div v-else>
      <p>購物車為空</p>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue';
import axios from 'axios';
import { VueCookieNext as $cookie } from 'vue-cookie-next';

// 定義要在模板中使用的變數
const cartGames = ref([]);
const total = computed(() => {
  return cartGames.value.reduce((acc, game) => acc + game.price, 0);
});

// 獲取購物車數據的函數
async function fetchCartData() {
  try {
    const id = $cookie.getCookie('Id');
    const response = await axios.get(`https://localhost:7048/api/CartItems/${id}`);
    const cartItems = response.data;

    for (let item of cartItems) {
      const gameResponse = await axios.get(`https://localhost:7048/api/Games/${item.gameId}`);
      const game = gameResponse.data;
      // 將遊戲數據添加到 cartGames 中
      cartGames.value.push(game);
    }
  } catch (error) {
    console.error('獲取購物車數據時出錯：', error);
  }
}

// 在元件掛載時獲取購物車數據
onMounted(fetchCartData);
</script>

<style scoped>
.card {
  width: 100%;
  border: 1px solid #ccc;
  border-radius: 5px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.card-body {
  padding: 1.25rem;
}

.font-weight-bold {
  font-weight: bold;
  margin-top: 1rem;
}

.text-muted {
  color: #6c757d;
}
</style>