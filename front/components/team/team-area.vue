<template>
  <section class="team__area team-bg section-pt-130 section-pb-100"
    style="background-image: url(/images/bg/team_bg.jpg)">
    <div class="container">
      <div class="row justify-content-center">
        <div v-for="(t, i) in title_data" :key="t.id" class="col-xl-3 col-lg-4 col-sm-6 wow fadeInUp"
          :data-wow-delay="`.${i + 1}s`">
          <div class="team__item">
            <div class="team__thumb">
              <nuxt-link to="">
                <img :src="t.img" alt="img" />
              </nuxt-link>
            </div>
            <div class="team__content">
              <h4 class="name">
                <nuxt-link :to="t.link">{{ t.title }}</nuxt-link>
              </h4>
              <span class="designation">{{ t.subtitle }}</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
const title_data = [
  {
    id: 1,
    img: '/images/Home/Tags/開放世界.jpg',
    title: '熱門遊戲',
    subtitle: '',
    link: '/game?search=popular'
  },
  {
    id: 2,
    img: '/images/Home/Tags/休閒.jpg',
    title: '優惠遊戲',
    subtitle: '',
    link: '/game?search=discount'
  },
  {
    id: 3,
    img: '/images/Home/Tags/解謎.jpg',
    title: '推薦新品',
    subtitle: '',
    link: '/team-details'
  },
  {
    id: 4,
    img: '/images/Home/Tags/射擊.jpg',
    title: '免費遊戲',
    subtitle: '',
    link: '/game?search=tag_13'
  }
]
</script>
