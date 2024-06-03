<template>
  <section class="streamers__area section-pt-95 section-pb-120">
    <div class="container">
      <div class="row justify-content-center">
        <div class="col-xl-6 col-lg-7 col-md-10">
          <div class="section__title text-center mb-60">
            <text-animation title="RizzGamingBase" />
            <h3 class="title">依照你的喜好選擇</h3>
          </div>
        </div>
      </div>
      <Swiper v-bind="slider_setting" :modules="[Navigation, Pagination]" class="swiper-container streamers-active">
        <SwiperSlide v-for="item in streamers_data" :key="item.id">
          <div class="streamers__item">
            <div class="streamers__thumb">
              <nuxt-link :to="`/game?search=tag_${item.id}`">
                <img :src="item.img" alt="img" />
              </nuxt-link>
            </div>
            <div class="streamers__content">
              <h4 class="name">{{ item.title }}</h4>
            </div>
          </div>
        </SwiperSlide>
      </Swiper>
      <div class="streamers__pagination">
        <div class="slider-button-prev streamers__pagination-arrow">
          <i class="fas fa-angle-left"></i>
        </div>
        <div class="swiper-pagination streamers__pagination-dots"></div>
        <div class="slider-button-next streamers__pagination-arrow">
          <i class="fas fa-angle-right"></i>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
import { Swiper, SwiperSlide } from "swiper/vue";
import { Navigation, Pagination } from "swiper/modules";

// slider data
const streamers_data: {
  id: number;
  img: string;
  title: string;
}[] = [
    {
      id: 17,
      img: '/images/Home/Tags/角色扮演.jpg',
      title: '角色扮演'
    },
    {
      id: 16,
      img: '/images/Home/Tags/射擊.jpg',
      title: '射擊'
    },
    {
      id: 26,
      img: '/images/Home/Tags/休閒.jpg',
      title: '休閒'
    },
    {
      id: 5,
      img: '/images/Home/Tags/開放世界.jpg',
      title: '開放世界'
    },
    {
      id: 24,
      img: '/images/Home/Tags/冒險.jpg',
      title: '冒險'
    },
    {
      id: 27,
      img: '/images/Home/Tags/格鬥.jpg',
      title: '格鬥'
    },
    {
      id: 28,
      img: '/images/Home/Tags/生存.jpg',
      title: '生存'
    },
    {
      id: 29,
      img: '/images/Home/Tags/模擬.jpg',
      title: '模擬'
    },
    {
      id: 30,
      img: '/images/Home/Tags/策略.jpg',
      title: '策略'
    },
    {
      id: 2,
      img: '/images/Home/Tags/合作.jpg',
      title: '合作'
    },
    {
      id: 4,
      img: '/images/Home/Tags/動作.jpg',
      title: '動作'
    },
    {
      id: 31,
      img: '/images/Home/Tags/運動.jpg',
      title: '運動'
    },
    {
      id: 32,
      img: '/images/Home/Tags/解謎.jpg',
      title: '解謎'
    },
  ]

// slider setting
const slider_setting = {
  observer: true,
  observeParents: true,
  loop: false,
  slidesPerView: 5,
  spaceBetween: 20,
  breakpoints: {
    '1500': {
      slidesPerView: 5,
    },
    '1200': {
      slidesPerView: 4,
    },
    '992': {
      slidesPerView: 4,
    },
    '768': {
      slidesPerView: 3,
    },
    '576': {
      slidesPerView: 2,
    },
    '0': {
      slidesPerView: 1.5,
      centeredSlides: true,
      centeredSlidesBounds: true,
    },
  },
  pagination: {
    el: '.swiper-pagination',
    clickable: true,
  },
  navigation: {
    nextEl: ".slider-button-next",
    prevEl: ".slider-button-prev",
  }
};
</script>
