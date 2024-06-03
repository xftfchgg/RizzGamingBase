<template>
  <section class="about__area section-pt-130 section-pb-130">
    <div class="container">
      <div class="row justify-content-center">
        <div class="col-xl-10">
          <div class="about__tab-wrap">
            <ul class="nav nav-tabs" id="myTab" role="tablist">
              <about-nav-btn id="01" img="/images/Home/Number/1.png" :isActive="true" />
              <about-nav-btn id="02" img="/images/Home/Number/2.png" />
              <about-nav-btn id="03" img="/images/Home/Number/3.png" />
              <about-nav-btn id="04" img="/images/Home/Number/4.png" />
              <about-nav-btn id="05" img="/images/Home/Number/5.png" />
              <about-nav-btn id="06" img="/images/Home/Number/6.png" />
            </ul>
          </div>
        </div>
      </div>
      <div class="tab-content" id="myTabContent">
        <about-tab-item id="01" img="/images/others/about_img01.jpg" :isActive="true" title="human game" rate="50%" />
        <about-tab-item id="02" img="/images/others/about_img02.jpg" title="Axie Infinity" rate="60%" />
        <about-tab-item id="03" img="/images/others/about_img03.jpg" title="The Walking Dead" rate="75%" />
        <about-tab-item id="04" img="/images/others/about_img04.jpg" title="The Dogami" rate="65%" />
        <about-tab-item id="05" img="/images/others/about_img05.jpg" title="The Sandbox" rate="85%" />
        <about-tab-item id="06" img="/images/others/about_img06.jpg" title="Pegaxy Horses" rate="90%" />
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">

</script>
