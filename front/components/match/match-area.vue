<template>
  <section class="match__result-area">
    <div class="match__result-bg" style="background-image:url(/images/bg/result_bg.png)"></div>
      <div class="container">
          <div class="row justify-content-center">
              <div class="col-xl-6 col-lg-7 col-md-10">
                  <div class="section__title text-center mb-55">
                      <text-animation title="LATEST RESULTS FOR" />
                      <h3 class="title">EXPERIENCE JUST FOR</h3>
                  </div>
              </div>
          </div>
          <div class="row">
              <div class="col-12">
                  <h4 class="match__winner-title">Premier league</h4>
              </div>
          </div>
          <div class="row match__result-wrapper justify-content-center">
              <div class="col-xl-5 col-sm-6">
                  <match-winner-item name='black hunt' amount='500 000' img="/images/others/win01.png" id='3' place='win'/>
              </div>
              <div class="col-xl-5 col-sm-6">
                  <match-winner-item name='sky Hunter' amount='300 000' img="/images/others/win02.png" id='4' place='2nd'/>
              </div>
          </div>
          <div class="row">
              <div class="col-12">
                  <div class="grand__final">
                      <h6 class="grand__final-date">December 19, 2021 | 9:50 am</h6>
                      <span class="grand__final-place">grand dubai</span>
                      <div class="grand__final-button">
                          <nuxt-link to="/contact" class="tg-btn-3 tg-svg mx-auto">
                              <svg-animate-icon icon="/images/icons/shape.svg" id="svg-5"/>
                              <span>read more</span>
                          </nuxt-link>
                      </div>
                  </div>
              </div>
          </div>
      </div>
   </section>
</template>

<script setup lang="ts">

</script>
