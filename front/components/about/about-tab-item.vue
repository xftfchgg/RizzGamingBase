<template>
  <div :class="`tab-pane ${isActive ? 'show active' : ''}`" :id="`about${id}`" role="tabpanel"
    :aria-labelledby="`about${id}-tab`">
    <div class="row justify-content-center">
      <div class="col-xl-5 col-lg-10">
        <div class="about__img">
          <img :src="img" alt="img" />
        </div>
      </div>
      <div class="col-xl-7 col-lg-10">
        <div class="about__flex-wrap">
          <div class="about__content-wrap">
            <div class="about__content">
              <h2 class="title">{{ title }}</h2>
              <div v-if="rate != 0">
                <span class="rate">rate {{ rate }}</span>
              </div>
              <h4 class="text_light">
                熱門排行第{{ id }}名
              </h4>
            </div>
            <div class="about__content-list">
              <div v-if="rate != 0" class="trendingNft__item-price">
                <span class="bid">原價</span>
                <div class="price">
                  <h6 class="eth">
                    <span>$</span>
                    <i>{{ price }}</i>
                  </h6>
                </div>
                <div>
                  <span class="bid">特價</span>
                  <h6 class="eth"><span>$</span>
                    <i></i>{{ discountPrice }}
                  </h6>
                </div>
              </div>
              <div v-else class="trendingNft__item-price">
                <span class="bid">價格</span>
                <h1 class="eth">{{ price }}</h1>
              </div>
            </div>
          </div>
          <div class="about__btn-wrap">
            <ul class="list-wrap">
              <li>
                <NuxtLink :to="`/game-details/${gameId}`">查看資訊</NuxtLink>
              </li>
              <li>
                <NuxtLink to="/shop">加入購物車</NuxtLink>
              </li>
              <li>
                <NuxtLink to="/contact">直接購買</NuxtLink>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
const props = defineProps(['id', 'img', 'title', 'rate', 'isActive', 'price', 'discountPrice', 'gameId']);
</script>

<style scoped>
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

.text_light {
  color: darkturquoise;
}
</style>