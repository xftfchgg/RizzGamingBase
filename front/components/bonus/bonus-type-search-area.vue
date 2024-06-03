<template>
  <div class="shop__item" v-for="product in bonusProduct" :key="product">
    <div v-if="bonusProduct != null">
      <p>123456</p>
      <p>{{ bonusProduct.id }}</p>
    </div>
    <div class="shop__item-thumb">
      <img src="/public/images/bonus/4/ApexFrame.png" />
      <nuxt-link to="#" class="wishlist-button">
        <i class="far fa-heart"></i>
      </nuxt-link>
    </div>
  </div>
</template>

<script setup>
import { ref, defineProps } from "vue";
import axios from "axios";

const bonusProduct = ref(null);
onMounted(async () => {
  try {
    const response = await axios.get("https://localhost:7048/api/BonusProducts");
    bonusProduct.value = response.data;
  } catch (error) {
    console.error("未正確找到紅利商品", error);
  }
  //console.log(bonusProduct.value) // GET Object value test 每渲染一筆商品找一次
});
</script>
