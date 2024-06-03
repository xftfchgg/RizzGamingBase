<template>
    <section class="gallery__area fix section-pb-80">
        <div class="gallery__slider">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-xl-9 col-lg-10 col-md-11">
                        <swiper v-bind="slider_setting" :modules="[Navigation, Scrollbar]"
                            class="swiper-container gallery-active" :centeredSlides="true" :observer="true"
                            :observeParents="true">
                            <swiper-slide v-for="(item, index) in mediaItems" :key="item.id">
                                <div class="gallery__item">
                                    <div class="gallery__thumb">
                                        <a data-cursor="-theme" data-cursor-text="View <br> Image"
                                            class="popup-image cursor-pointer" @click.prevent="handleShowImage(index)"
                                            @mouseenter="handleMouseEnter" @mouseleave="handleMouseLeave">
                                            <s v-if="item.type === 'video'">
                                                <video controls class="swiper-item" :autoplay="true" :muted="true">
                                                    <source :src="item.src" type="video/webm">
                                                </video>
                                            </s>
                                            <s v-else>
                                                <img :src="item.src" alt="img" class="swiper-item">
                                            </s>
                                        </a>
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
    <popup-image-lightbox :images="mediaItems" :indexVal="index" :visible="visible"
        @handleHide="handleHide"></popup-image-lightbox>
    <!-- image lightbox end -->
</template>

<script setup>
import { Swiper, SwiperSlide } from "swiper/vue";
import { Navigation, Scrollbar, Autoplay } from "swiper/modules";
import { defineProps, ref, onMounted } from "vue";

const props = defineProps({
    gameData: Object,
});

const mediaItems = ref([]);
onMounted(() => {
    // 添加视频
    mediaItems.value.push({
        type: 'video',
        src: `/images/games/video/${props.gameData.developerId}/${props.gameData.id}/${props.gameData.video}.webm`,
    });

    // 添加图片
    props.gameData.displayImages.forEach(image => {
        mediaItems.value.push({
            type: 'image',
            src: `/images/games/image/${props.gameData.developerId}/${props.gameData.id}/${image}.jpg`,
        });
    });
});


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
.swiper-item {
    width: 100%;
    /* 设置宽度为100% */
    height: auto;
    /* 高度自适应 */
}
</style>