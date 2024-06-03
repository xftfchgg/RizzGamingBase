<template>
    <section class="slider__area slider__bg" style="display: flex; justify-content: center; align-items: center;background-image: url(/images/slider/slider_bg.jpg);height:750px" data-background="/images/slider/slider_bg.jpg" >
        <div style=" height: 100%; width:50%; display: flex;">
            <!-- 左側 -->
            <div style=" height:100%;width:20%">
                <button style="width: 90%;" @click="clickdata">一般</button>
                <button style="width: 90%;" @click="clickava">頭像</button>
            </div>
            <!-- 右側 -->
            <div style=" height:100%;width:80%">
                <MemberProfileData v-if="isActiveData"></MemberProfileData>
                <MemberProfileAvater v-if="isActiveAva"></MemberProfileAvater>
            </div>

        </div>
    </section>
  </template>

  <script setup  lang="ts">
  import {ref} from "vue"
    const isActiveData = ref<boolean>(true);
    const isActiveAva = ref<boolean>(false);

    const clickdata = () => {
        isActiveData.value =  true;
        isActiveAva.value = false;
  };
    const clickava = () => {
        isActiveData.value =  false;
        isActiveAva.value = true;
    };
  </script>


  <style>
  .button{
    width: 100px;
    border: 0px;
  }
  </style>