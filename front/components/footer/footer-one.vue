<template>
  <footer class="footer-style-one">
    <div class="footer__top-wrap">
      <div class="container">
        <div class="row">
          <div class="col-xl-4 col-lg-5 col-md-7">
            <div class="footer-widget">
              <div class="footer-logo logo">
                <nuxt-link to="/contact">
                  <img src="/images/logo/logo.png" alt="Logo" />
                </nuxt-link>
              </div>
              <div class="footer-text">
                <p class="desc"></p>
                <p class="social-title"><span>關於我們 <i class="fas fa-angle-double-right"></i></span></p>
                <div class="footer-social">
                  <nuxt-link to="#"><img src="/images/icons/social_icon01.png" alt="iocn" /></nuxt-link>
                  <nuxt-link to="#"><img src="/images/icons/social_icon02.png" alt="iocn" /></nuxt-link>
                  <nuxt-link to="#"><img src="/images/icons/social_icon03.png" alt="iocn" /></nuxt-link>
                  <nuxt-link to="#"><img src="/images/icons/social_icon04.png" alt="iocn" /></nuxt-link>
                </div>
              </div>
            </div>
          </div>
          <div class="col-xl-2 col-lg-3 col-md-5 col-sm-6">
            <div class="footer-widget widget_nav_menu">
              <h4 class="fw-title">前端使用技術</h4>
              <ul class="list-wrap menu">
                <li><a href="https://vuejs.org/">Vue</a></li>
                <li><nuxt-link to="#">JavaScript</nuxt-link></li>
                <li><nuxt-link to="#">CSS</nuxt-link></li>
                <li><nuxt-link to="#">Ajax</nuxt-link></li>
                <li><nuxt-link to="#">Nuxt</nuxt-link></li>
                <li><nuxt-link to="#">Swipper</nuxt-link></li>
              </ul>
            </div>
          </div>
          <div class="col-xl-2 col-lg-3 col-md-5 col-sm-6">
            <div class="footer-widget widget_nav_menu">
              <h4 class="fw-title">後端使用技術</h4>
              <ul class="list-wrap menu">
                <li><nuxt-link to="#">C#</nuxt-link></li>
                <li><nuxt-link to="#">ASP.NET CORE</nuxt-link></li>
                <li><nuxt-link to="#">SIGNALR</nuxt-link></li>
                <li><nuxt-link to="#">JWT</nuxt-link></li>
                <li><nuxt-link to="#">XX</nuxt-link></li>
                <li><nuxt-link to="#">XX</nuxt-link></li>
              </ul>
            </div>
          </div>
          <div class="col-xl-4 col-lg-5 col-md-7">
            <div class="footer-widget">
              <h4 class="fw-title">聯絡我們</h4>
              <div class="footer-newsletter">
                <p>若有任何問題請留下您的電子郵件，將會有專人跟你聯絡</p>
                <form action="#" class="footer-newsletter-form">
                  <input type="email" placeholder="Your email address" />
                  <button type="submit"><i class="flaticon-paper-plane"></i></button>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="copyright__wrap">
      <div class="container">
        <div class="row align-items-center">
          <div class="col-md-7">
            <div class="copyright__text">
              <p>全端班第二組 © {{ new Date().getFullYear() }} - RizzGamingBase <span>Rizz</span></p>
            </div>
          </div>
          <div class="col-md-5">
            <div class="copyright__card text-center text-md-end">
              <img src="/public/images/Home/Pay/linepay.png" alt="img" />
              <img src="/public/images/Home/Pay/ecpay.png" alt="img" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </footer>
</template>

<script setup lang="ts">

</script>
