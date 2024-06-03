<template>
  <nuxt-layout name="layout-two">
    <!-- hero banner start -->
    <hero-banner-two></hero-banner-two>
    <!-- hero banner end -->

    <!-- match area start -->
    <match-area></match-area>
    <!-- match area end -->

    <!-- area-background-start -->
    <div class="area-background" style="background-image: url(/images/bg/area_bg02.jpg)">
      <!-- about area start -->
      <about-area-2></about-area-2>
      <!-- about area end -->

      <!-- streamers area start -->
      <streamers-area></streamers-area>
      <!-- streamers area end -->
    </div>
    <!-- area-background-end -->

    <!-- upcoming match area start -->
    <upcoming-match-area></upcoming-match-area>
    <!-- upcoming match area end -->

    <!-- project area start -->
    <project-area></project-area>
    <!-- project area end -->

    <!-- social area start -->
    <social-area></social-area>
    <!-- social area end -->

    <!-- brand area start -->
    <brand-area></brand-area>
    <!-- brand area end -->
  </nuxt-layout>
</template>

<script setup lang="ts">
definePageMeta({
  layout: false,
});
useSeoMeta({ title: "Home Two - MYKD" });
</script>
