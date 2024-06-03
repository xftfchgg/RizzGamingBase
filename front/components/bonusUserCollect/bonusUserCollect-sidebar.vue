<template>
  <aside class="shop-sidebar">
    <!-- 關鍵字查詢 -->
    <div class="shop__widget">
      <h4 class="shop__widget-title">search</h4>
      <div class="shop__widget-inner">
        <div class="shop__search">
          <input type="text" placeholder="Search" v-model="keyword" />
          <button class="p-0 border-0" @click="handleSearch">
            <i class="flaticon-search"></i>
          </button>
        </div>
      </div>
    </div>

    <!-- 類型篩選 -->
    <!-- <div class="shop__widget">
      <h4 class="shop__widget-title">
        <button
          class="accordion-button collapsed"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#flush_collapse_Type"
          aria-expanded="false"
          aria-controls="flush_collapse_Type"
        >
          分類1
        </button>
      </h4>
      <div class="shop__widget-myinner">
        <div
          id="flush_collapse_Type"
          class="accordion-collapse collapse show"
          aria-labelledby="flush_collapse_Type"
          data-bs-parent="#accordionFlushExample"
        >
          <div class="accordion-body">
            <h4>內容</h4>
            <div>
              <label class="form-check-label"> 變更密碼 </label>
            </div>
            <div>
              <label class="form-check-label"> 變更名稱 </label>
            </div>
          </div>
        </div>
      </div>
      <h4 class="shop__widget-title">
        <button
          class="accordion-button collapsed"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#flush-collapse_price"
          aria-expanded="false"
          aria-controls="flush-collapse_price"
        >
          分類2
        </button>
      </h4>
      <div class="shop__widget-myinner">
        <div
          id="flush-collapse_price"
          class="accordion-collapse collapse"
          aria-labelledby="flush-collapse_price"
          data-bs-parent="#accordionFlushExample"
        >
          <div class="accordion-body">
            <h4>內容</h4>
          </div>
        </div>
      </div>
    </div> -->
  </aside>
</template>

<script setup>
import "@vueform/slider/themes/default.css";
import { defineEmits } from "vue";

let keyword = "";

const emits = defineEmits(["search"]);
const props = defineProps({
  
})

function handleSearch() {
  //toLower
  const lowercaseKeyword = keyword.toLowerCase();
  emits("search", lowercaseKeyword);
}
</script>
