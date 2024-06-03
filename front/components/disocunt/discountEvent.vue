<template>
    <section class="services__details-area section-pt-120 section-pb-120"
        style="background-image:url(/images/bg/team_details_bg.jpg)">
        <div class="container">
            <div class="row">
                <div class="col-8">
                    <div class="">
                        <img :src="discount.image" alt="img" />
                        <Dots />
                    </div>
                </div>
                <div class="team__details-content col-3">
                    <span class="sub-title"></span>
                    <h2 class="title text_green">{{ discount.discountName }}</h2>
                    <h3>活動日期</h3>
                    <br>
                    <h3>{{ discount.startTime }} 起</h3>
                    <br>
                    <h3>~</h3>
                    <br>
                    <h3>{{ discount.endTime }} 結束</h3>
                    <br>
                    <h2 class="text_blue">活動最高折扣</h2>
                    <h2 class=" text-right">-{{ trueDiscountPercent }}<i class="fa fa-percent"></i></h2>
                </div>
                <div>

                    <div class="col-12">
                        <blockquote class="team__details-quote">
                            <p>{{ discount.discountDescription }}</p>
                            <cite>{{ discount.discountType }}</cite>
                        </blockquote>
                    </div>
                </div>
            </div>
        </div>

    </section>
</template>

<script setup>
import axios from 'axios';

let discount = ref([]);
let trueDiscountPercent = ref('');

const props = defineProps({
    discountId: String
})

let dis = axios.get(`https://localhost:7048/api/Discount/GetDiscountItem?id=${props.discountId}`)
    .then((response) => {
        discount.value = response.data;
        console.log(discount.value);
        trueDiscountPercent.value = 100 - discount.value.percent
    })
    .catch((error) => {
        console.log(error);
    });

</script>

<style scoped>
.text_blue {
    color: darkturquoise;
}

.text_green {
    color: springgreen;
}
</style>
