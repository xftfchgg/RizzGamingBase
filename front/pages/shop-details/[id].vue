<template>
  <div>
    <!-- breadcrumb area start -->
    <breadcrumb-three
      title="PRODUCT SINGLE"
      subtitle="PRODUCT SINGLE"
    ></breadcrumb-three>
    <!-- breadcrumb area end -->

    <!-- shop details area start -->
    <shop-details-area :product="product"></shop-details-area>
    <!-- shop details area end -->
  </div>
</template>

<script setup lang="ts">
import product_data from "@/data/product-data";

useSeoMeta({ title: "Shop Details - MYKD" });
const route = useRoute();
const product = [...product_data].find(p => Number(p.id) === Number(route.params.id))!;
</script>
