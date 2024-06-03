<template>
    <h1></h1>
    <client-only>
        <DisocuntDiscountEvent :discountId="route.params.id"></DisocuntDiscountEvent>
        <DisocuntDiscountGames :discountId="route.params.id"></DisocuntDiscountGames>
    </client-only>
</template>

<script setup>
import axios from 'axios'
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute();



onMounted(async () => {
    // 检查是否存在必要的参数
    if (!route.params.id) {
        console.error('缺少必要的参数');
        return;
    }
})
</script>
