<style lang="scss">
@import "@/assets/css/my-style.css";
</style>

<template>
  <div class="shop__bonus__item">
    <div class="wishlist-button">
      <h6 v-if="isProductOwned()" style="color:#45f882 ;">已持有</h6>
      <h6 v-else style="color: #FFF;">未持有</h6>
    </div>
    <div class="shop__bonus__item-thumb">
      <img :src="`/images/bonus/${bonusProductsInItem.productTypeId}/${bonusProductsInItem.url}`" typeof="btn"
        @click="imgclickEvent" style="cursor: pointer;" :data-bs-toggle="'modal'"
        :data-bs-target="'#exampleModal_' + bonusProductsInItem.id" />
      <!-- 已持有 -->
      <!-- <div class="wishlist-button">
        <h6 style="color:#45f882 ;">已持有</h6>
        <h6 style="color: #FFF;">未持有</h6>
      </div> -->
    </div>
    <div class="shop__item-line"></div>
    <div class="shop__item-content">
      <div class="shop__item-content-top">
        <h4 class="title">
          <nuxt-link to="">
            {{ bonusProductsInItem.name }}
          </nuxt-link>
          <!-- <div v-if="isModalOpen">測試元件開關</div> -->
        </h4>
      </div>
      <div class="shop__item-content-top">
        <nuxt-link to="">
          {{ getProductTypeName(bonusProductsInItem.productTypeId - 1) }}
        </nuxt-link>
        <!-- 價格 -->
        <div class="shop__item-price">
          ${{ bonusProductsInItem.price }}
        </div>
      </div>
    </div>
    <!-- 定義模態框 -->
    <div class="modal fade my-modal" :id="'exampleModal_' + bonusProductsInItem.id" tabindex="-1"
      aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content" style="background-color: #182029;">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">
              <!-- <p>{{ bonusProductsInItem.id }}</p> -->
            </h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <h2>{{ bonusProductsInItem.name }}</h2>
            <h5>{{ getProductTypeName(bonusProductsInItem.productTypeId - 1) }}</h5>
            <div class="my-center-container">
              <img :src="`/images/bonus/${bonusProductsInItem.productTypeId}/${bonusProductsInItem.url}`"
                typeof="btn" />
            </div>
            <!-- 價格 -->
            <div class="shop__item-price">
              ${{ bonusProductsInItem.price }}
              <img src="/public//images/gold-coin-icon.png" style="width: 30px; height: 30px; align-items: center" />
            </div>
          </div>
          <div v-if="isProductOwned()" class="modal-footer">
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">關閉</button>
          </div>
          <div v-else class="modal-footer">
            <button type="button" class="btn btn-primary"
              @click="buyProductEvent(bonusProductsInItem.id, bonusProductsInItem.name, bonusProductsInItem.price)">購買</button>
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">關閉</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, watchEffect, defineProps, defineEmits } from "vue";
import { VueCookieNext as $cookie } from 'vue-cookie-next'

//cookie
let memberId = $cookie.getCookie("Id");
let memberBonusPoint = $cookie.getCookie("bouns");

let isModalOpen = ref(false);
let isApplyMode = ref(true);

const props = defineProps({
  bonusProductsInItem: Object,
  bonusProductTypesInItem: Object,
  bonusItemInItem: Object,
  modalId: String
});


const openModal = () => {
  isModalOpen.value = true;
};

const closeModal = () => {
  isModalOpen.value = false;
};

onMounted(() => {
  // console.log(props.bonusItemInItem)
})

watchEffect(() => {
  // console.log(props.bonusItemInItem);
  // console.log(isProductOwned());
});

// 所有物品與使用者擁有的物品比對
const isProductOwned = () => {
  // console.log(props.bonusItemInItem)
  // 檢查初始化 bonusItemInItem 是否 null 或者 undefined
  if (!props.bonusItemInItem) {
    return false;
  }
  // 使用 Array.some() 方法遍歷 bonusItemInItem 
  // 如果有任何一個物品的 name 欄位與 bonusProductsInItem 的 name 欄位匹配，則返回 true
  // 否則返回 false
  return props.bonusItemInItem.some(item => {
    return item.name === props.bonusProductsInItem.name;
  });
};
const buyProduct = defineEmits(['byProduct'])

const imgclickEvent = () => {
  openModal();
}

function buyProductEvent(id, name, price) {
  if (memberId == null || memberId == undefined) {
    console.log("請先登入")

  } else {
    buyProduct("buyProduct", id, name, price)
    // console.log("購買按鈕Item層:"+id,name,price)
  }
}

function getProductTypeName(productTypeId) {
  //都有傳進來
  if (props.bonusProductTypesInItem && props.bonusProductTypesInItem[productTypeId]) {
    //傳出對應ID的名稱
    return props.bonusProductTypesInItem[productTypeId].name;
  }
  else {
    return '';
  }
}
</script>