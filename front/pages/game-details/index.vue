<template>
    <div>
        <!-- breadcrumb area start -->
        <breadcrumb-three
            title="BLOG DETAILS"
            subtitle="BLOG DETAILS"
        ></breadcrumb-three>
        <!-- breadcrumb area end -->

        <!-- blog details area start -->
        <blog-details-area :blog="blog"></blog-details-area>
        <!-- blog details area end -->
    </div>
</template>

<script setup lang="ts">
useSeoMeta({ title: "Blog Details - MYKD" });
import blog_data from "@/data/blog-data";
const blog = [...blog_data][0];
</script>
