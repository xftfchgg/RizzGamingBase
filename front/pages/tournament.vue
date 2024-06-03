<template>
  <div>
    <!-- breadcrumb area start -->
    <breadcrumb-one
      title="TOURNAMENT"
      subtitle="TOURNAMENT"
      bg="/images/bg/breadcrumb_bg03.jpg"
      brd_img="/images/others/breadcrumb_img03.png"
    ></breadcrumb-one>
    <!-- breadcrumb area end -->

    <!-- tournament area start -->
    <tournaments-area></tournaments-area>
    <!-- tournament area end -->

    <!-- tournament list area start  -->
    <tournaments-list-area />
    <!-- tournament list area end  -->
  </div>
</template>

<script setup lang="ts">
useSeoMeta({ title: "Tournament - MYKD" });
</script>
