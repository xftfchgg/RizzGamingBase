<template>
    <section class="blog-area">
        <div class="container">
            <div class="row justify-content-center">
                <div class="blog-post-wrapper">
                    <!-- blog item start -->
                    <blog-item
                        v-for="blog in blog_items"
                        :key="blog.id"
                        :blog="blog"
                    />
                    <!-- blog item end -->

                    <div class="pagination__wrap">
                        <!-- pagination start -->
                        <ui-pagination />
                        <!-- pagination end -->
                    </div>
                </div>
                <div class="blog-post-sidebar">
                    <!-- blog sidebar start -->
                    <blog-sidebar />
                    <!-- blog sidebar end -->
                </div>
            </div>
        </div>
    </section>
</template>

<script setup lang="ts">
import blog_data from "@/data/blog-data";
const blog_items = [...blog_data];
</script>
