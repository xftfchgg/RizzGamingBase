<template>
    <div>
        <!-- breadcrumb area start -->
        <breadcrumb-three title="願望清單" subtitle="願望清單"></breadcrumb-three>
        <!-- breadcrumb area end -->

        <!-- blog area start -->
        <wishList-area></wishList-area>
        <!-- blog area end -->
    </div>
</template>

<script setup>
import { useRoute, useRouter } from 'vue-router';
useSeoMeta({ title: "願望清單 - MYKD" });

const route = useRoute();
const router = useRouter();
// if(route.query.page == undefined){
//      router.push({ query: { ...route.query, page: 1 }});
// }
  


</script>
