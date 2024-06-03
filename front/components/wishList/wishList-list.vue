<template>
    <div>
        <div v-for="game in gameData" :key="game.id">
            <!-- <nuxt-link :to="`/game-details/${game.id}`"> -->
            <div class="row mb-2 clickable-element" @click="LinkToGame(game.id)">
                <div class="col-3">
                    <img :src="'images/games/cover/' + game.developerId + '/' + game.id + '/' + game.cover + '.jpg'" />
                </div>
                <div class="col-5 d-flex align-items-center">
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
                <div class="col-2">
                    <nuxt-link to="" class="tg-btn-2 -secondary mb-2 d-flex btnfont"
                        @click.stop="DeleteWishList(game.id)">
                        移除願望清單
                    </nuxt-link>
                    <nuxt-link to="" class="tg-btn-2 d-flex btnfont" @click.stop="AddToCart(game.id)">
                        加入購物車
                    </nuxt-link>
                </div>
            </div>
            <!-- </nuxt-link> -->
        </div>
    </div>
</template>

<script setup>
import axios from "axios";
import { VueCookieNext as $cookie } from 'vue-cookie-next'
import { useRoute, useRouter } from 'vue-router';

const router = useRouter();
const route = useRoute();

const props = defineProps({
    gameData: Object,
});

const emitEvents = defineEmits(['refreshWishList']);
let id = ref('');
onBeforeMount(async () => {
    id = $cookie.getCookie("Id");
})

function LinkToGame(gameId) {
    router.push(`/game-details/${gameId}`)
}

function DeleteWishList(gameId) {
    axios.delete(`https://localhost:7048/api/Games/DeleteWishList?memberId=${id}&gameId=${gameId}`)
    alert('已移除自願望清單')
    emitEvents('refreshWishList');
}

function AddToCart(gameId) {
    let cartItem = {};
    cartItem.gameId = gameId;
    cartItem.memberId = id;
    try {
        axios.post('https://localhost:7048/api/CartItems', cartItem)
        alert('已加入購物車')
    } catch (error) {
        console.error(error);
    }

    // alert('已加入購物車')
    emitEvents('refreshWishList');
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

.clickable-element {
    cursor: pointer;
}

.btnfont {
    font-size: 16px;
}
</style>