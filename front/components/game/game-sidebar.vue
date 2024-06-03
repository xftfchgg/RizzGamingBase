<template>
    <aside class="blog-sidebar">


        <div class="blog-widget">
            <div class="sidebar__author">
                <div class="sidebar__author-thumb">
                    <img :src="`/images/games/cover/${gameData.developerId}/${gameData.id}/${gameData.cover}.jpg`"
                        alt="img" />
                </div>
                <div class="sidebar__author-content">
                    <h4 class="name">{{ gameData.name }}</h4>
                    <p>{{ gameData.introduction }}</p>
                </div>
            </div>
        </div>


        <div class="blog-widget">
            <h4 class="fw-title">標籤</h4>
            <div class="tagcloud">
                <nuxt-link v-for="(tag, index) in gameData.tags" :key="index" :to="`/game?search=tag_${tag.id}`">{{
                        tag.name
                    }}</nuxt-link>
            </div>
        </div>


        <div v-if="releaseDate < currentDate" class="blog-widget">
            <div v-if="gameData.discountPrice > 0" class="price d-flex justify-content-around">
                <span class="discount-text">
                    NT $ {{ gameData.discountPrice }}
                </span>
                <span class="discount-text text">
                    特價
                </span>
                <span class="discount-text text-decoration-line-through">
                    NT $ {{ gameData.price }}
                </span>
                <span class="discount-text">
                    原價
                </span>
            </div>

            <div v-else class="price d-flex justify-content-around">
                <span class="discount-text">
                    NT $ {{ gameData.price }}
                </span>
                <span class="discount-text">
                    原價
                </span>
            </div>
        </div>

        <div v-if="checkGame == true" class="blog-widget">
            <div>
                <nuxt-link to="" class="tg-btn-2 -secondary mb-2 d-flex">
                    你已擁有此遊戲
                </nuxt-link>
            </div>
        </div>


        <div v-else class="blog-widget">
            <div>
                <nuxt-link to="" class="tg-btn-2 -secondary mb-2 d-flex clickable-element"
                    @click="AddToWishList(gameData.id)">
                    加入願望清單
                </nuxt-link>
            </div>
            <div>
                <nuxt-link v-if="releaseDate < currentDate" to="" class="tg-btn-2 d-flex clickable-element"
                    @click="AddToCart(gameData.id)">
                    加入購物車
                </nuxt-link>
                <!-- <button class="btn" @click="IsHaveGame(gameData.id)">test</button> -->
            </div>
        </div>

    </aside>
</template>

<script setup>
import { VueCookieNext as $cookie } from 'vue-cookie-next'
import axios from 'axios';

const currentDate = new Date()

let id = $cookie.getCookie("Id");
const props = defineProps({
    gameData: Object,
});

function test() {
    console.log(props.gameData.id)
}

(async () => {
    IsHaveGame(props.gameData.id)
})();
let checkGame = ref(false)
async function IsHaveGame(gameId) {
    let wishList = {};
    wishList.gameId = gameId;
    wishList.memberId = id;
    await axios.post(`https://localhost:7048/api/Games/IsHaveGame`, wishList)
        .then(response => {
            console.log(wishList.gameId);
            console.log(wishList.memberId);
            console.log(response.data);
            checkGame.value = response.data
        })
        .catch(error => {
            console.error('Error occurred while checking game existence:', error);
        });
}

async function AddToWishList(gameId) {
    if (id === null) {
        alert('你還尚未登入')
        return;
    }
    let wishList = {};
    wishList.gameId = gameId;
    wishList.memberId = id;
    console.log(wishList)

    try {
        await axios.post('https://localhost:7048/api/Games/AddToWishList', wishList)
        alert('已加入願望清單')
    } catch (error) {
        console.error(error);
    }
}

async function AddToCart(gameId) {
    if (id === null) {
        alert('你還尚未登入')
        return;
    }

    let cartItem = {};
    cartItem.gameId = gameId;
    cartItem.memberId = id;
    try {
        await axios.post('https://localhost:7048/api/CartItems', cartItem)
        alert('已加入購物車')
    } catch (error) {
        console.error(error);
    }
}

const releaseDate = new Date(props.gameData.releaseDate);

</script>

<style scoped>
.price {
    background-color: #8bfad9e0;
    box-shadow: 7px 7px 20px rgba(73, 255, 225, 0.7),
        -7px -7px 30px rgba(73, 255, 225, 0.7),
        inset 0px 0px 4px rgba(73, 255, 225, 0.7),
        inset 7px 7px 15px rgba(73, 255, 225, 0.7);
    border-radius: 10px;
    direction: rtl !important;
    margin-left: auto;
}

.discount-text {
    color: #383838;
    text-shadow: 0 0 10px rgba(73, 255, 225, 0.7), 0 0 10px rgba(73, 255, 225, 0.7), 0 0 10px rgba(73, 255, 225, 0.7), 0 0 10px rgba(73, 255, 225, 0.7);
    font-size: 20px;
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
    /* flex-direction: column; */
}

.normal-container {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 60px;
}

.clickable-element {
    cursor: pointer;
}
</style>