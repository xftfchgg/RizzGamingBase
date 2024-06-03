<template>

    <div class="row">
        <div class="col-4">
            <p v-if="tagName != null" class="h3">搜尋標籤: {{ tagName }}</p>
            <p v-if="popular != null" class="h3">搜尋: 熱門遊戲</p>
            <p v-if="discount != null" class="h3">搜尋: 特價遊戲</p>
        </div>
        <div class="col-4"></div>
        <div class="col-4">
            <input type="text" class="form-control mb-3" v-model="keyword" @input="handleInput">
            <div class="list-group" v-if="showList">
                <a v-for="title in filteredTitles" :key="title.id" :href="'game-details/' + title.id"
                    class="list-group-item list-group-item-action ">{{ title.name }}</a>
            </div>
        </div>
    </div>



    <div>
        <div v-for="game in gameData" :key="game.id">
            <nuxt-link :to="`/game-details/${game.id}`">
                <div class="row mb-2">
                    <div class="col-3">
                        <img
                            :src="'images/games/cover/' + game.developerId + '/' + game.id + '/' + game.cover + '.jpg'" />
                    </div>
                    <div class="col-7 d-flex align-items-center">
                        <h3 class="">{{ game.name, tagName }}</h3>
                    </div>
                    <div v-if="game.discountPrice" class="col-2 d-flex align-items-center container">
                        <p class="text-decoration-line-through text-start">
                            NT $ {{ game.price }}
                        </p>
                        <!-- <span>&nbsp;&nbsp;&nbsp;</span> -->
                        <p class="fw-bold discount-text">特價 NT $ {{ game.discountPrice }}</p>
                    </div>
                    <div v-else class="col-2 d-flex align-items-center normal-container">
                        <p v-if="game.price > 0" class="text-start price-text">
                            NT $ {{ game.price }}
                        </p>
                        <p v-else-if="game.price === 0" class="text-start free-text">
                            免費
                        </p>
                        <p v-else class="text-start future-text">
                            即將發行
                        </p>
                    </div>
                </div>
            </nuxt-link>
        </div>
    </div>
</template>

<script setup>
import { defineProps } from 'vue';
import axios from 'axios'

const props = defineProps({
    gameData: Object,
    allGame: Object,
    tagName: String,
    popular: String,
    discount: String,
});

const keyword = ref('');
const showList = ref(false);

const filteredTitles = computed(() => {
    return keyword.value ? filterTitles() : [];
});

function filterTitles() {
    return props.allGame.filter(item => item.name.toLowerCase().includes(keyword.value.toLowerCase()));
}

function handleInput() {
    showList.value = keyword.value.length > 0;
}


</script>

<style scoped>
.discount-text {
    color: #ffffff;
    text-shadow: 0 0 10px rgba(73, 255, 225, 0.7), 0 0 20px rgba(73, 255, 225, 0.7), 0 0 30px rgba(73, 255, 225, 0.7), 0 0 40px rgba(73, 255, 225, 0.7);
    font-size: 16px;
}

.price-text {
    color: #ffffff;
    text-shadow: 0 0 10px rgba(182, 255, 140, 0.7), 0 0 20px rgba(182, 255, 140, 0.7), 0 0 30px rgba(182, 255, 140, 0.7), 0 0 40px rgba(182, 255, 140, 0.7);
    font-size: 20px;
}

.free-text {
    color: #ffffff;
    text-shadow: 0 0 10px rgba(250, 102, 102, 0.7), 0 0 20px rgba(250, 102, 102, 0.7), 0 0 30px rgba(250, 102, 102, 0.7), 0 0 40px rgba(250, 102, 102, 0.7);
    font-size: 20px;
}

.future-text {
    color: #ffffff;
    text-shadow: 0 0 10px rgba(236, 88, 255, 0.7), 0 0 20px rgba(236, 88, 255, 0.7), 0 0 30px rgba(236, 88, 255, 0.7), 0 0 40px rgba(236, 88, 255, 0.7);
    font-size: 20px;
}

.container {
    display: flex;
    flex-direction: column;
}

.normal-container {
    display: flex;
    justify-content: center;
    align-items: center;
}

.list-group {
    z-index: 1000;
    /* 任何比其他元素更高的值都可以 */
    position: absolute;
    /* 为了使 z-index 生效，通常需要配合 position 属性 */
}
</style>
