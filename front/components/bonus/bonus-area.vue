<style lang="scss">
@import "@/assets/css/my-style.css";
</style>

<template>
  <section class="bonus-area">
    <div class="container">
      <div class="row justify-content-center">
        <div class="col-xl-3 col-lg-4 col-md-11 order-2 order-lg-0">
          <!-- 搜尋功能列 -->
          <bonus-sidebar @search="handleSearch" />
        </div>
        <div class="col-xl-9 col-lg-8 col-md-11">
          <div v-if="memberId">
            <h5>
              您的點數餘額：<img
                src="/public//images/gold-coin-icon.png"
                style="width: 30px; height: 30px; align-items: center"
              />{{ memberBonusInArea }}
            </h5>
          </div>
          <div>
            <!-- <h4>投影片部分</h4>
            <div
              id="carouselExampleControls"
              class="carousel slide"
              data-bs-ride="false"
            >
              <div class="my-carousel-inner">
                <div class="my-carousel-item active">
                  <h4>
                  第一頁
                  </h4>
                </div>
                <div class="carousel-item">
                  <h4>第二頁</h4>
                </div>
                <div class="carousel-item">
                  <h4>第三頁</h4>
                </div>
              </div>
              <div id="button-container">
              
              </div>
              <button
                class="carousel-control-prev"
                type="button"
                data-bs-target="#carouselExampleControls"
                data-bs-slide="prev"
              >
                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Previous</span>
              </button>
              <button
                class="carousel-control-next"
                type="button"
                data-bs-target="#carouselExampleControls"
                data-bs-slide="next"
              >
                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                <span class="visually-hidden">Next</span>
              </button>
            </div> -->
          </div>
          <!--物品清單-->
          <div
            class="row justify-content-start row-cols-xl-3 row-cols-lg-2 row-cols-md-2 row-cols-sm-2 row-cols-1"
          >
            <!-- 把接到的請求資料丟到bonusProducts -->
            <div v-for="bonusProductItem in bonusProductsInArea"
              :key="bonusProductItem.id"
              class="col"
            >
              <bonus-item
                :bonusProductsInItem="bonusProductItem"
                :bonusProductTypesInItem="bonusProductTypesInArea"
                :bonusItemInItem="bonusItemInArea"
                :modalId="'exampleModal_' + bonusProductItem.id" 
                @childClick="handleChildClick"
                @buyProduct= "buyEvenFormItem"
              />
            </div>
          </div>
          <div style="position: absolute; left: 50%">
            <h4>{{ errormessageInArea }}</h4>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup>
// import product_data from "@/data/product-data";
import { defineProps, defineEmits, onMounted, watchEffect} from "vue";
//cookie
import { VueCookieNext as $cookie } from "vue-cookie-next";
let memberId = $cookie.getCookie("Id");
// console.log(memberAvatarURL)

const props = defineProps({
  bonusProductsInArea: Object,
  bonusProductTypesInArea: Object,
  bonusItemInArea: Object,
  errormessageInArea: String,
  memberBonusInArea: Number,
});


const handleChildClick = (id) =>
{
  console.log('測試子物件傳數值',id)
}

onMounted(() => 
{
  // console.log(props.bonusItemInArea)
});

watchEffect(() => {
  // console.log(props.bonusItemInArea);
});

const emits = defineEmits(["data-from-bonus"],["byProduct"]);

function handleSearch(keyword) {
  emits("data-from-bonus", keyword);
}

function buyEvenFormItem(id,name,price)
{
  emits("byProduct",id,name,price)
}

</script>
