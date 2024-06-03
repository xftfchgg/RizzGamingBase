<template>
    <div>
        <!-- breadcrumb area start -->
        <breadcrumb-three title="GAME DETAILS" subtitle="GAME DETAILS"></breadcrumb-three>
        <!-- breadcrumb area end -->

        <!-- blog details area start -->
        <div v-if="game">
            <!-- 游戏详情内容 -->
            <game-details-area :gameData="game"></game-details-area>
        </div>
        <div v-else>
            <!-- 加载中的提示或其他内容 -->
            <p>Loading...</p>
        </div>
        <!-- blog details area end -->
    </div>
</template>

<script setup>
import axios from 'axios'
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute();
let game = ref(null);

onMounted(async () => {
    try {
        // 检查是否存在必要的参数
        if (!route.params.id) {
            console.error('缺少必要的参数');
            return;
        }
        const response = await axios.get(`https://localhost:7048/api/Games/${route.params.id}`);
        game.value = response.data;
    } catch (error) {
        console.log(error);
    }
});
</script>
