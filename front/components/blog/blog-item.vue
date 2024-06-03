<template>
 <div class="blog-post-item">
    <div class="blog-post-thumb">
      <nuxt-link :to="`/blog-details/${blog.id}`">
        <img :src="blog.img" alt="img" />
      </nuxt-link>
    </div>
    <div class="blog-post-content">
      <div class="blog-post-meta">
        <ul class="list-wrap">
          <li>
            By<nuxt-link :to="`/blog-details/${blog.id}`">{{blog.author}}</nuxt-link>
          </li>
          <li>
            <i class="far fa-calendar-alt"></i> {{blog.author}}
          </li>
          <li>
            <i class="far fa-comments"></i>
            <nuxt-link :to="`/blog-details/${blog.id}`">
              {{blog.comments === 0 ? 'No comments' : `${blog.comments} comments`}}
            </nuxt-link>
          </li>
        </ul>
      </div>
      <h2 class="title">
        <nuxt-link :to="`/blog-details/${blog.id}`">{{blog.title}}</nuxt-link>
      </h2>
      <p>
        {{blog.desc}}
      </p>
      <div class="blog-post-bottom">
        <div class="blog-post-read">
          <nuxt-link :to="`/blog-details/${blog.id}`">
            READ MORE <i class="fas fa-arrow-right"></i>
          </nuxt-link>
        </div>
        <div class="blog-post-share">
          <h5 class="share">share :</h5>
          <ul class="list-wrap">
            <li>
              <nuxt-link to="https://www.facebook.com/" target="_blank">
                <i class="fab fa-facebook-f"></i>
              </nuxt-link>
            </li>
            <li>
              <nuxt-link to="https://twitter.com/" target="_blank">
                <i class="fab fa-twitter"></i>
              </nuxt-link>
            </li>
            <li>
              <nuxt-link to="https://www.linkedin.com/" target="_blank">
                <i class="fab fa-linkedin-in"></i>
              </nuxt-link>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import type { IBlog } from "@/types/blog-type";

defineProps<{blog:IBlog}>()
</script>
