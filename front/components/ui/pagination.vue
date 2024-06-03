<template>
  <ul class="list-wrap d-flex flex-wrap justify-content-center">
    <li>
      <nuxt-link to="#" class="page-numbers"> 01 </nuxt-link>
    </li>
    <li>
      <span class="page-numbers current">02</span>
    </li>
    <li>
      <nuxt-link to="#" class="page-numbers"> 03 </nuxt-link>
    </li>
    <li>
      <nuxt-link to="#" class="page-numbers"> 04 </nuxt-link>
    </li>
    <li>
      <nuxt-link to="#" class="page-numbers"> .... </nuxt-link>
    </li>
    <li>
      <nuxt-link to="#" class="next page-numbers">
        <i class="fas fa-angle-double-right"></i>
      </nuxt-link>
    </li>
  </ul>
</template>

<script setup lang="ts"></script>
