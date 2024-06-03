<template>
  <section class="slider__area slider__bg" style="background-image: url(/images/slider/slider_bg.jpg);height:750px" data-background="/images/slider/slider_bg.jpg" >
      <div style="display: flex; justify-content: center; align-items: center; height: 100%;">
        <h2 v-if="loading">驗證中...</h2>
        <h2 v-if="message">{{ message }}</h2>
        <router-link to="/Login" v-if="trueactive">點此進行登入</router-link>
      </div>
  </section>
</template>



<script setup>
import axios from 'axios'
import { useRoute } from 'vue-router';

const loading = ref(true);
const message = ref(null);
const trueactive = ref(false);
const router = useRoute();




(async () => {
  // 获取URL参数
  const memberId = router.params.memberId;
  const confirmCode = router.params.confirmCode;


  try {

    console.log(memberId);
    console.log(typeof memberId);
    console.log(confirmCode);
    console.log(typeof confirmCode);
    await new Promise(resolve => setTimeout(resolve, 2000));

    active(memberId, confirmCode);

  } catch (error) {

  }
})();

const active = (memberId, confirmCode) => {
    const postData = {
      Id: memberId,
      confirmCode: confirmCode
    };
    console.log(postData);

    axios.post(`https://localhost:7048/api/Members/ActiveRegister`, postData)
    .then(response => {
      console.log(response.data);
    if(response.data ==1){
      loading.value = false;
      trueactive.value= true;
      message.value = '已開通成功,謝謝!';    
    }
    else{
      loading.value = false;
      message.value = '此用戶早已通過驗證.';
    }
  })
  .catch(error => {
    console.log(error);
  });
};

</script>

