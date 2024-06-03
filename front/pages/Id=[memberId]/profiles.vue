<template>
  <div>
    <!-- http://localhost:3000/images/Bonus/2/CatImage.gif -->
    <!-- breadcrumb area start -->
    <breadcrumb-one :title="nickName" :subtitle="registrationDate" bg="/images/bg/breadcrumb_bg01.jpg" :brd_img="ava"
      Frame_img="http://localhost:3000/images/Bonus/4/ApexFrame.png" @changeName="changeName"></breadcrumb-one>
    <!-- breadcrumb area end -->

    <!-- team info area start -->
    <div class="container">
      <div class="row">
        <div class="col-12">
          <div class="team__info-wrap">
            <div style="width: 100%; height: 80px">

            </div>
          </div>
        </div>
      </div>
    </div>

  </div>
</template>

<script setup lang="ts">
onBeforeMount(() => {
  definePageMeta({
    middleware: "nologin",
  });
});

import axios from "axios";
import { VueCookieNext as $cookie } from "vue-cookie-next";
import { useRoute } from "vue-router";


const router = useRoute();

const memberId = router.params.memberId;
const postData = ref({
  NickName: "",
});


let registrationDate = ref("");
let Frame_img = ref("");
let ava = "";

let nickName = ref("");
let oldnickname = "";

onBeforeMount(() => {

});

const changeName = (data) => {
  nickName = data;
  console.log('收到子组件发送的数据:', data);
};

async function fetchData() {
  try {
    const response = await axios.get(`https://localhost:7048/api/Members/${memberId}`);
    console.log(response.data);
    oldnickname = response.data.nickName;
    nickName.value = response.data.nickName;
    registrationDate.value = "於" + response.data.registrationDate + "創建";
    postData.value.NickName = oldnickname;
    if ($cookie.getCookie("avatarUrl")) {
      ava = $cookie.getCookie("avatarUrl");
    }
    console.log(oldnickname);

  } catch (error) {
    console.error("Error fetching member data:", error);
  }
}

fetchData(); 



</script>
