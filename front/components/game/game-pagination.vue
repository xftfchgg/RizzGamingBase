<template>
  <div class="d-flex justify-content-center">
    <ul class="list-wrap d-flex flex-wrap justify-content-center">

      <li>
        <!-- <span v-if="currentPage != 1" class="page-numbers"><i class="fas fa-angle-double-left"></i></span> -->
        <nuxt-link v-if="currentPage != 1" :to="frontPage()" class="next page-numbers"
          @mouseenter="addCurrentClass($event.target)" @mouseout="removeCurrentClass($event.target)">
          <i class="fas fa-angle-double-left" @mouseenter="addCurrentClass2($event.target)"></i>
        </nuxt-link>

      </li>

      <li v-for="page in gamePage" :key="page">
        <span v-if="currentPage == page" class="page-numbers current">{{page}}</span>
        <nuxt-link v-else :to="getRoute(page)" class="page-numbers" :key="1"
          @mouseenter="addCurrentClass($event.target)" @mouseout="removeCurrentClass($event.target)"> {{page}}
        </nuxt-link>
      </li>

      <!-- <li>
      <span v-if="currentPage == 1" class="page-numbers current">01</span>
      <nuxt-link v-else :to="getRoute(1)" class="page-numbers" :key="1" @mouseenter="addCurrentClass($event.target)"
        @mouseout="removeCurrentClass($event.target)"> 01 </nuxt-link>
    </li> -->

      <li>
        <!-- <span v-if="currentPage == gamePage" class="page-numbers"><i class="fas fa-angle-double-right"></i></span> -->
        <nuxt-link v-if="currentPage != gamePage" :to="nextPage()" class="next page-numbers"
          @mouseenter="addCurrentClass($event.target)" @mouseout="removeCurrentClass($event.target)">
          <i class="fas fa-angle-double-right" @mouseenter="addCurrentClass2($event.target)"></i>
        </nuxt-link>


      </li>

      <!-- <li>
      <span class="page-numbers current" @click="show">test</span>
    </li> -->

    </ul>
  </div>
</template>

<script setup>
  import {
    useRouter,
    useRoute
  } from 'vue-router';

  const props = defineProps({
    gamePage: Number,
  });

  const router = useRouter();
  const route = useRoute();

  let currentPage = ref(1);
  let currentRoute = ref(null);

  router.beforeEach((to, from) => {
    if(to.query.search !== from.query.search){
      currentPage.value = 1;
    }else{
      currentPage.value = to.query.page
    }
  })

  function getRoute(key) {
    if (Object.keys(route.query).length == 0) {
      return route.fullPath + `?page=${key}`;
    } else {
      if (route.query.page == undefined) {
        return route.fullPath + `&page=${key}`;
      } else {
        const baseUrl = route.fullPath.split('?')[0];
        const queryParams = new URLSearchParams(route.fullPath.split('?')[1]);
        queryParams.set('page', key);
        return `${baseUrl}?${queryParams.toString()}`;
      }
    }
  }

  function show(){
   
    console.log(currentPage.value)

  }

  (async () => {
    checkPage()
    currentPage.value = 1;
  })();

  function nextPage() {
    const baseUrl = route.fullPath.split('?')[0];
    const queryParams = new URLSearchParams(route.fullPath.split('?')[1]);
    queryParams.set('page', parseInt(currentPage.value) + 1);
    return `${baseUrl}?${queryParams.toString()}`;
  }

  function frontPage() {
    const baseUrl = route.fullPath.split('?')[0];
    const queryParams = new URLSearchParams(route.fullPath.split('?')[1]);
    queryParams.set('page', currentPage.value - 1);
    return `${baseUrl}?${queryParams.toString()}`;
  }

  function checkPage() {
    currentPage.value = route.query.page;
  }

  function addCurrentClass(element) {
    element.classList.add('current');
  }

  function addCurrentClass2(element) {
    element.parentNode.classList.add('current');
  }

  function removeCurrentClass(element) {
    element.classList.remove('current');
  }
</script>