<template>
  <aside class="blog-sidebar">
      <div class="blog-widget">
          <div class="sidebar__author">
              <div class="sidebar__author-thumb">
                  <img src="/images/blog/avatar.jpg" alt="img" />
              </div>
              <div class="sidebar__author-content">
                  <h4 class="name">Kaceytron G.</h4>
                  <p>Lorem ipsum sitamet conteur adipiscing Duis elementum solliciin</p>
                  <div class="sidebar__author-social">
                      <nuxt-link to="#"><i class="fab fa-twitter"></i></nuxt-link>
                      <nuxt-link to="#"><i class="fab fa-facebook-f"></i></nuxt-link>
                      <nuxt-link to="#"><i class="fab fa-linkedin-in"></i></nuxt-link>
                  </div>
              </div>
          </div>
      </div>
      <div class="blog-widget">
          <form class="sidebar-search-form position-relative">
              <input type="text" placeholder="Search here.."/>
              <button><i class="flaticon-loupe"></i></button>
          </form>
      </div>
      <div class="blog-widget widget_categories">
          <h4 class="fw-title">CATEGORIES</h4>
          <ul class="list-wrap">
              <li><nuxt-link to="#">gaming</nuxt-link><span class="float-right">(11)</span></li>
              <li><nuxt-link to="#">ELECTRONIC</nuxt-link><span class="float-right">(4)</span></li>
              <li><nuxt-link to="#">online</nuxt-link><span class="float-right">(21)</span></li>
              <li><nuxt-link to="#">TOURNAMENT</nuxt-link><span class="float-right">(15)</span></li>
              <li><nuxt-link to="#">controller</nuxt-link><span class="float-right">(2)</span></li>
              <li><nuxt-link to="#">live</nuxt-link><span class="float-right">(7)</span></li>
          </ul>
      </div>
      <div class="blog-widget">
          <h4 class="fw-title">Recent Posts</h4>
          <div class="rc__post-wrapper">
              <div v-for="b in blog_data" :key="b.id" class="rc__post-item">
                  <div class="rc__post-thumb">
                      <nuxt-link :to="`/blog-details/${b.id}`">
                        <img :src="b.img" alt="img" width="112" height="88" />
                      </nuxt-link>
                  </div>
                  <div class="rc__post-content">
                      <h6 class="title">
                        <nuxt-link :to="`/blog-details/${b.id}`">
                          {{b.title}}
                        </nuxt-link>
                      </h6>
                      <span class="date">{{b.date}}</span>
                  </div>
              </div>
          </div>
      </div>
      <div class="blog-widget">
          <h4 class="fw-title">Newsletter</h4>
          <div class="sidebar__newsletter">
              <p>Lorem ipsum sitamet conteur adipiscin</p>
              <form action="#" class="sidebar__newsletter-form">
                  <input type="email" name="email" placeholder="Enter Your Email"/>
                  <button type="submit"><i class="fas fa-arrow-right"></i></button>
              </form>
          </div>
      </div>
      <div class="blog-widget">
          <h4 class="fw-title">INSTAGRAM</h4>
          <div class="sidebar__insta">
              <div v-for="(item,i) in instagram_images" :key="i" class="sidebar__insta-item">
                <nuxt-link :to="item.link" target="_blank">
                  <img :src="item.img" alt="img" />
                </nuxt-link>
              </div>
          </div>
      </div>
      <div class="blog-widget">
          <h4 class="fw-title">Tag Cloud</h4>
          <div class="tagcloud">
              <nuxt-link to="#">E-sports</nuxt-link>
              <nuxt-link to="#">Fantasy</nuxt-link>
              <nuxt-link to="#">game</nuxt-link>
              <nuxt-link to="#">Tournaments</nuxt-link>
              <nuxt-link to="#">Matches</nuxt-link>
              <nuxt-link to="#">Streamers</nuxt-link>
          </div>
      </div>
  </aside>
</template>

<script setup lang="ts">
import blog_data from "@/data/blog-data";
// instagram images
const instagram_images:{
  link: string;
  img: string;
}[] = [
  {link:'https://www.instagram.com/',img:'/images/instagram/insta01.jpg'},
  {link:'https://www.instagram.com/',img:'/images/instagram/insta02.jpg'},
  {link:'https://www.instagram.com/',img:'/images/instagram/insta03.jpg'},
  {link:'https://www.instagram.com/',img:'/images/instagram/insta04.jpg'},
  {link:'https://www.instagram.com/',img:'/images/instagram/insta05.jpg'},
  {link:'https://www.instagram.com/',img:'/images/instagram/insta06.jpg'},
];

</script>
