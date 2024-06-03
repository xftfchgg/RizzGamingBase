<template>
  <div class="nft-item__box">
    <div class="nft-item__thumb">
      <nuxt-link to="/shop-details">
        <img :src="item.img" alt="img" />
      </nuxt-link>
    </div>
    <div class="nft-item__content">
      <h4 class="title">
        <nuxt-link to="/shop-details">{{ item.title }}</nuxt-link>
      </h4>
      <div class="nft-item__avatar">
        <div class="avatar-img">
          <nuxt-link to="/shop-details">
            <img :src="item.creator" alt="img" />
          </nuxt-link>
        </div>
        <div class="avatar-name">
          <h5 class="name">
            <nuxt-link to="/shop-details">{{ item.creator_name }}</nuxt-link>
          </h5>
          <span class="designation">Creator</span>
        </div>
      </div>
      <div class="nft-item__bid">
        <div class="nft-item__price">
          <p>
            {{ item.eth }}
            <span class="currency">Eth</span>
          </p>
          <nuxt-link to="/shop-details" class="bid-btn">
            Bid <i class="fas fa-long-arrow-alt-right"></i>
          </nuxt-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, defineProps } from 'vue';

const props = defineProps(['item']);
</script>
