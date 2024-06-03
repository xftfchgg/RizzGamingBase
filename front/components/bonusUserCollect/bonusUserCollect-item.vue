<style lang="scss">
@import "@/assets/css/my-style.css";
</style>

<template>
  <div class="shop__bonus__item">
    <div class="shop__bonus__item-thumb">
      <img :src="`/images/bonus/${bonusProductsInItem.productTypeId}/${bonusProductsInItem.url}`" typeof="btn"
        @click="imgclickEvent(bonusProductsInItem.id)" style="cursor: pointer;" :data-bs-toggle="'modal'"
        :data-bs-target="'#exampleModal_' + bonusProductsInItem.id" />
    </div>
    <div class="shop__item-line"></div>
    <div class="shop__item-content">
      <div class="shop__item-content-top">
        <h4 class="title">
          <nuxt-link to="">
            {{ bonusProductsInItem.name }}
          </nuxt-link>
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
            </h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>
          <div class="modal-body">
            <h2>{{ bonusProductsInItem.name }}</h2>
            <h5>{{ getProductTypeName(bonusProductsInItem.productTypeId - 1) }}</h5>
            <div class="my-center-container">
              <img :src="`/images/bonus/${bonusProductsInItem.productTypeId}/${bonusProductsInItem.url}`" typeof="btn"
                @click="imgclickEvent(bonusProductsInItem.id)" />
            </div>
          </div>
          <div class="modal-footer">
            <button v-if="bonusProductsInItem.using" type="button" class="btn btn-primary"
              @click="itemUsingEvent(bonusProductsInItem.bonusId, bonusProductsInItem.productTypeId, false)">取消套用</button>
            <button v-else type="button" class="btn btn-primary"
              @click="itemUsingEvent(bonusProductsInItem.bonusId, bonusProductsInItem.productTypeId, true)">套用</button>
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">關閉</button>
            <!-- <button type="button" class="btn" @click="testEvent(bonusProductsInItem)"></button> -->
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, defineProps, onMounted } from "vue";
import { VueCookieNext as $cookie } from 'vue-cookie-next'

//cookie
let memberId = ref('');
memberId = $cookie.getCookie("Id");
let memberBonusPoint = ref('');
memberBonusPoint = $cookie.getCookie("bouns");
let isModalOpen = ref(false);

const props = defineProps({
  bonusProductsInItem: Object,
  bonusProductTypesInItem: Object,
  modalId: String
});
const openModal = () => {
  // 點擊圖片時打開模態框
  isModalOpen.value = true;
};
const itemUsing = defineEmits(['itemUsing'])

onMounted(() => {
  // console.log(props.bonusProductsInItem)
})

//控制模態框
function imgclickEvent(id) {
  openModal();
  // console.log(id);
}
//傳出狀態
function itemUsingEvent(id, TypeId, using) {
  itemUsing("itemUsing", id, TypeId, using)
  console.log("item層:" + id, TypeId, using)
}

// function testEvent(bonusProductsInItem)
// {
//   console.log(bonusProductsInItem)
// }

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