<template>
  <section class="nft-item__area">
    <div class="container custom-container">
      <div class="row justify-content-center">
        <div v-for="item in nft_data.slice(0, 3)" :key="item.id" class="col-xxl-4 col-xl-5 col-lg-6 col-md-9">
          <nft-item-box :item="item" />
        </div>
      </div>
    </div>
  </section>
</template>

<script setup>
import nft_data from '@/data/nft-data';
</script>
