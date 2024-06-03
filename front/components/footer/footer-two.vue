<template>
 <footer class="footer-style-two has-footer-animation">
  <div class="footer__country">
      <div class="container custom-container">
          <div class="row">
              <div class="col-6">
                  <div class="footer__country-name">
                      <h2 class="text">Dubai</h2>
                  </div>
              </div>
              <div class="col-6">
                  <div class="footer__country-name text-center text-sm-end">
                      <h2 class="text">France</h2>
                  </div>
              </div>
          </div>
      </div>
  </div>
  <div class="footer__two-widgets">
      <div class="container custom-container">
          <div class="row">
              <div class="col-md-4 col-sm-7 order-1 order-md-0">
                  <div class="footer-el-widget">
                      <h4 class="title">information</h4>
                      <ul class="list-wrap">
                          <li><nuxt-link to="tel:123">+971 333 222 557</nuxt-link></li>
                          <li><nuxt-link to="mailto:info@exemple.com">info@exemple.com</nuxt-link></li>
                          <li>Firestone building, Sharjah Ain <br/> Box 265, Dubai</li>
                      </ul>
                  </div>
              </div>
              <div class="col-md-4 col-sm-5 order-0 order-md-2">
                  <div class="footer-el-widget text-start text-md-center widget_nav_menu">
                      <div class="footer-el-logo mb-35">
                          <nuxt-link to="/">
                            <img src="/images/logo/logo.png" alt="Mykd"/>
                          </nuxt-link>
                      </div>
                      <div class="footer-el-menu">
                          <ul class="list-wrap">
                              <li><nuxt-link to="/">Home</nuxt-link></li>
                              <li><nuxt-link to="/about">About Us</nuxt-link></li>
                              <li><nuxt-link to="/tournament">Tournament</nuxt-link></li>
                          </ul>
                      </div>
                  </div>
              </div>
              <div class="col-md-4 col-sm-7 order-3">
                  <div class="footer-el-widget text-start text-md-end">
                      <h4 class="title">head office</h4>
                      <ul class="list-wrap">
                          <li><nuxt-link to="tel:123">+33 423 222 555</nuxt-link></li>
                          <li><nuxt-link to="mailto:info@exemple.com">info@exemple.com</nuxt-link></li>
                          <li>New Central Park W7 Street Lan <br/> France</li>
                      </ul>
                  </div>
              </div>
          </div>
      </div>
  </div>
  <div class="copyright__wrap -style-two">
      <div class="container custom-container">
          <div class="row align-items-center">
              <div class="col-lg-6">
                  <div class="copyright__text text-center text-lg-start">
                      <p>Copyright © {{new Date().getFullYear()}} - All Rights Reserved By <span>Mykd</span></p>
                  </div>
              </div>
              <div class="col-lg-6">
                  <div class="copyright__menu">
                      <ul class="list-wrap d-flex flex-wrap justify-content-center justify-content-lg-end">
                          <li><nuxt-link to="/contact">Contact Us</nuxt-link></li>
                          <li><nuxt-link to="/contact">Join our team</nuxt-link></li>
                          <li><nuxt-link to="/contact">Privacy Policy</nuxt-link></li>
                      </ul>
                  </div>
              </div>
          </div>
      </div>
  </div>
</footer>
</template>

<script setup lang="ts">
onMounted(() => {
  if (window.IntersectionObserver) {
      const observer = new IntersectionObserver((entries, observer) => {
      entries.forEach(entry => {
          if (entry.isIntersecting) {
          entry.target.classList.add("active-footer");
          observer.unobserve(entry.target);
          }
      });
      }, {
      rootMargin: "0px 0px -100px 0px"
      });

    document.querySelectorAll('.has-footer-animation').forEach(block => {
        observer.observe(block);
     });
    } else {
      document.querySelectorAll('.has-footer-animation').forEach(block => {
      block.classList.remove('has-footer-animation');
      });
    }
})
</script>
