<template>
    <section class="trendingNft-area section-pt-120 section-pb-90" @load="getGameInfo()">
        <div class="container">
            <div class="trendingNft__title-wrap">
                <div class="row">
                    <div class="col-md-7">
                        <div class="trendingNft__title">
                            <h2 class="title">為您推薦 <i class="fas fa-thumbs-up"></i>
                            </h2>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="trendingNft__nav">
                            <button class="slider-button-prev"><i class="fas fa-angle-left"></i></button>
                            <button class="slider-button-next"><i class="fas fa-angle-right"></i></button>
                        </div>
                    </div>
                </div>
            </div>

            <Swiper v-bind="slider_setting" :modules="[Navigation]" class="swiper-container trendingNft-active">
                <SwiperSlide v-for="item in fireGameData" :key="item.id">
                    <div class="trendingNft__item">
                        <div class="trendingNft__item-top">
                            <h3 class="ellipsis">{{ item.name }}</h3>
                            <div class="trendingNft__item-wish">
                                <nuxt-link to="/"><i class="far fa-heart"></i></nuxt-link>
                            </div>
                        </div>
                        <div class="trendingNft__item-image">
                            <nuxt-link :to="`/game-details/${item.id}`">
                                <img :src="`/images/games/cover/${item.developerId}/${item.id}/${item.cover}.jpg`"
                                    alt="img" @mouseover="showInfo(item.id)" @mouseleave="hideInfo()" />
                                <div v-if="activeIndex === item.id" class="info text-white">
                                    點擊查看詳細資訊
                                </div>
                            </nuxt-link>
                        </div>
                        <div class="trendingNft__item-bottom">
                            <div v-if="item.discountPrice != 0" class="trendingNft__item-price">
                                <span class="bid">原價</span>
                                <div class="price">
                                    <h6 class="eth">
                                        <span>$</span>
                                        <i>{{ item.price }}</i>
                                    </h6>
                                </div>
                                <div>
                                    <span class="bid">特價</span>
                                    <h6 class="eth"><span>$</span>
                                        <i></i>{{ item.discountPrice }}
                                    </h6>
                                </div>
                            </div>
                            <div v-else class="trendingNft__item-price">
                                <span class="bid">價格</span>
                                <h6 class="eth">{{ item.price }}</h6>
                            </div>
                            <button class="bid-btn" @click="AddToCart(item.id)">加入購物車 <i
                                    class="fas fa-long-arrow-alt-right"></i></button>
                        </div>
                    </div>
                </SwiperSlide>
            </Swiper>
        </div>
    </section>
</template>

<script setup>
import { Swiper, SwiperSlide } from "swiper/vue";
import { Navigation } from "swiper/modules";
import axios from "axios";
import { VueCookieNext as $cookie } from 'vue-cookie-next';


let fireGameData = ref([]);

let getGameInfo = () => {
    let Id;
    Id = $cookie.getCookie('Id');
    axios.post(`https://localhost:7048/api/Games/Commend?memberId=${Id}`).then((res) => {
        fireGameData.value = res.data;
        console.log(fireGameData.value);
    });
};

onMounted(() => {
    getGameInfo();
});

async function AddToCart(gameId) {
    let id;
    id = $cookie.getCookie('Id');
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

const activeIndex = ref(null);

const showInfo = (index) => {
    activeIndex.value = index;
    zoomIn(event);
};

const hideInfo = () => {
    activeIndex.value = null;
    zoomOut(event);
};

const zoomIn = (event) => {
    event.target.style.transform = 'scale(1.1)'; // 放大 1.1 倍
};

const zoomOut = (event) => {
    event.target.style.transform = 'scale(1)'; // 恢復原始大小
};
// slider setting 
const slider_setting = {
    observer: true,
    observeParents: true,
    loop: false,
    slidesPerView: 3,
    spaceBetween: 30,
    breakpoints: {
        '1500': {
            slidesPerView: 3,
        },
        '1200': {
            slidesPerView: 3,
        },
        '992': {
            slidesPerView: 2,
        },
        '768': {
            slidesPerView: 2,
        },
        '576': {
            slidesPerView: 1,
        },
        '0': {
            slidesPerView: 1,
        },
    },
    navigation: {
        nextEl: ".slider-button-next",
        prevEl: ".slider-button-prev",
    }
}



const nfts_data = [
    {
        id: 4,
        img: '/images/nft/nft_img04.jpg',
        title: 'Crypto Max',
        eth: 1.002,
        trending: true,
    },
    {
        id: 5,
        img: '/images/nft/nft_img05.jpg',
        title: 'Golden Crypto',
        eth: 1.004,
        trending: true,
    },
    {
        id: 6,
        img: '/images/nft/nft_img06.jpg',
        title: 'Black Crypto',
        eth: 1.005,
        trending: true,
    },
    {
        id: 7,
        img: '/images/nft/nft_img07.jpg',
        title: 'Luck Crypto',
        eth: 1.006,
        trending: true,
    },
]
</script>

<style scoped>
/* 可以添加一些過渡效果，使放大和縮小更加平滑 */
img {
    transition: transform 0.3s ease;
}

.trendingNft__item-image {
    position: relative;
    /* 將父元素設置為相對定位 */
}

.ellipsis {
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
}

.price {
    /* 确保 h6 元素定位 */
    position: relative;
}

.eth i {
    text-decoration: line-through;
    /* 划掉文本 */
    margin-right: 4px;
    /* 调整斜线前面的间距 */
}

.eth span {
    margin-right: 4px;
    /* 调整斜线前面的间距 */
}

.info {
    position: absolute;
    bottom: 100%;
    /* 設置在圖片上方 */
    left: 50%;
    /* 水平置中 */
    transform: translateX(-50%);
    background-color: springgreen;
    padding: 10px;
    border: 1px solid #ccc;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
    border-radius: 5px;
    transition: opacity 0.3s;
    opacity: 0;
    z-index: 9999;
    /* 設置 z-index 為最大值以確保在最上層 */
    white-space: nowrap;
    /* 避免文本换行 */
    max-width: 200px;
    /* 最大宽度，根据需要调整 */
    overflow: hidden;
    /* 超出部分隐藏 */
}

.trendingNft__item-image:hover .info {
    opacity: 1;
    /* 滑鼠移入時顯示 */
}
</style>