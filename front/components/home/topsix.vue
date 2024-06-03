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
                <about-tab-item id="01" img="/images/games/cover/1/7/header.jpg" :isActive="true"
                    title="MONSTER HUNTER RISE" rate="0" price="1120" discountPrice="1120" gameId="7" />
                <about-tab-item id="02" img="/images/games/cover/1/8/header.jpg" title="MONSTER HUNTER RISE: SUNBREAK"
                    rate="0" price="1120" discountPrice="1120" gameId="8" />
                <about-tab-item id="03" img="/images/games/cover/1/9/header.jpg" title="Mega Man 11" rate="0"
                    price="980" discountPrice="980" gameId="9" />
                <about-tab-item id="04" img="/images/games/cover/3/21/header.jpg"
                    title="Marvel’s Spider-Man: Miles Morales" rate="0" price="1190" discountPrice="1190" gameId="21" />
                <about-tab-item id="05" img="/images/games/cover/1/1/header.jpg" title="Monster Hunter: World"
                    rate="80%" price="920" discountPrice="736" gameId="1" />
                <about-tab-item id="06" img="/images/games/cover/1/2/header.jpg" title="Monster Hunter World: Iceborne"
                    rate="80%" price="1130" discountPrice="984" gameId="2" />
            </div>
        </div>
    </section>
</template>

<script setup>
import axios from 'axios'

let Gamedata = ref([]);
</script>