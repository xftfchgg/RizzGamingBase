<template>
    <section class="slider__area slider__bg" style="background-image: url(/images/slider/slider_bg.jpg);height:750px" data-background="/images/slider/slider_bg.jpg" >
        <div style="display: flex; justify-content: center; align-items: center; height: 100%;">
          <h2 v-if="loading">驗證中...</h2>
          <h2 v-if="message">{{ message }}</h2>
          <router-link to="/" v-if="trueactive">點此返回首頁</router-link>
        </div>
    </section>
  </template>
  
  
  
  <script setup>
  import axios from 'axios'
  import { useRoute } from 'vue-router';
  import { VueCookieNext as $cookie } from 'vue-cookie-next';
  const loading = ref(true);
  const message = ref(null);
  const trueactive = ref(false);
  const router = useRoute();
  
  
  
  
  (async () => {
    // 获取URL参数
    const memberId = router.params.memberId;
    const confirmCode = router.params.confirmCode;
  
    // 模拟异步验证
    try {
      // 这里你可以根据memberId和confirmCode进行验证操作，例如向后端API发送验证请求等
      console.log(memberId);
      console.log(typeof memberId);
      console.log(confirmCode);
      console.log(typeof confirmCode);
      await new Promise(resolve => setTimeout(resolve, 2000));
  
      login(memberId, confirmCode);
  
  
      // 假设这里直接验证通过
  
  
      // loading.value = false;
      // message.value = '已開通成功,謝謝!';
    } catch (error) {
      // loading.value = false;
      // message.value = '此用戶已通過驗證.';
    }
  })();
  
  const login = (memberId, confirmCode) => {
      const postData = {
        Id: memberId,
        confirmCode: confirmCode
      };
      console.log(postData);
      //https://localhost:7048/api/Members/ActiveRegister?id=${memberId}&confirmCode=${confirmCode}
      axios.post(`https://localhost:7048/api/Members/noPasswordLogin`, postData)
      .then(response => {
        console.log(response.data);
      if(response.data[0] =="登入成功"){
        loading.value = false;
        trueactive.value= true;
        message.value = '登入成功!';
        $cookie.setCookie('accountId', response.data[1]);
        $cookie.setCookie('avatarUrl', response.data[2]);
        $cookie.setCookie('bonus', response.data[3]);
        $cookie.setCookie('name', response.data[4]);
        $cookie.setCookie('Id', response.data[5]);    
      }
      else{
        loading.value = false;
        message.value = '此連結已失效.';
      }
    })
    .catch(error => {
      console.log(error);
    });
  };
  
  </script>
  
  