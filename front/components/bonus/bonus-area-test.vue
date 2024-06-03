<template>
  <div v-for="bonusProduct in bonusProducts" :key="bonusProduct.id">{{ bonusProduct.name }}
    <a :href="`/public/images/bonus/${bonusProduct.productTypeId}/${bonusProduct.url}`">123</a>
  </div>
</template>

<!-- <script setup lang="ts">
    import product_data from "@/data/product-data";
    
    const product_items = [...product_data]
    </script> -->

<script setup>
import { defineProps } from "vue";

const props = defineProps({
  bonusProducts: Object,
});

</script>