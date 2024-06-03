<template>
    <section class="gallery__area fix  section-pt-120 section-pb-120">
        <div class="gallery__slider">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-xl-9 col-lg-10 col-md-11">
                        <swiper v-bind="slider_setting" :modules="[Navigation, Scrollbar]"
                            class="swiper-container gallery-active" :centeredSlides="true" :observer="true"
                            :observeParents="true">
                            <swiper-slide v-for="(item, index) in discounts" :key="item.id">
                                <div class="gallery__item">
                                    <div class="gallery__thumb">
                                        <a data-cursor="-theme" data-cursor-text="View"
                                            class="popup-image cursor-pointer" :title="item.discountName"
                                            @mouseenter="handleMouseEnter" @mouseleave="handleMouseLeave">
                                            <nuxt-link :to="`/discount/${item.id}`" :discountItem="item">
                                                <img :src="item.image" alt="img" />
                                            </nuxt-link>
                                        </a>
                                    </div>
                                    <div class="gallery__content">
                                        <h2 class="text_blue">{{ item.discountName }}</h2>
                                        <h3 class="text_green">最高-{{ discountPercent(item.percent) }}%off</h3>
                                    </div>
                                </div>
                            </swiper-slide>
                            <div class="swiper-scrollbar"></div>
                        </swiper>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- image lightbox start -->
    <popup-image-lightbox :images="discounts.map((p) => p.image)" :indexVal="index" :visible="visible"
        @handleHide="handleHide"></popup-image-lightbox>
    <!-- image lightbox end -->
</template>

<script setup>
import { Swiper, SwiperSlide } from "swiper/vue";
import { Navigation, Scrollbar } from "swiper/modules";
import axios from "axios";

let discounts = ref([]);

let discount = axios.get("https://localhost:7048/api/Discount/GetDiscount").then((res) => {
    console.log(res.data);
    discounts.value = res.data;
});

function discountPercent(percent) {
    return 100 - percent;
}



const mouse = useBigMouse();

const handleMouseEnter = () => {
    mouse.value = true;
};
const handleMouseLeave = () => {
    mouse.value = false;
};
// image popup
const visible = ref(false);
const index = ref(0);
function handleHide() {
    visible.value = false;
}
const handleShowImage = (indexNum) => {
    index.value = indexNum;
    visible.value = true;
};
// slider setting
const slider_setting = {
    centeredSlides: true,
    centeredSlidesBounds: true,
    spaceBetween: 30,
    freeMode: false,
    observer: true,
    observeParents: true,
    breakpoints: {
        1920: {
            slidesPerView: 1,
        },
        992: {
            slidesPerView: 1,
        },
        320: {
            slidesPerView: 1,
        },
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
    scrollbar: {
        el: ".swiper-scrollbar",
        draggable: true,
    },
};
</script>
<style scoped>
.text_green {
    color: springgreen;
}

.text_blue {
    color: w;
}
</style>