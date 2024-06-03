<template>
  <section class="slider__area slider__bg" style="background-image: url(/images/slider/slider_bg.jpg);height:750px"
    data-background="/images/slider/slider_bg.jpg">
    <!-- <div style="display: flex;justify-content: center;align-items: center;height: 50vh;background-color: red;"> -->
    <div style="display: flex; justify-content: flex-start; align-items: center; height: 50vh;">
      <div style="height:100%; display: flex;flex-direction: column; margin-right: 10px;" >
        <button @click="acc1" style="background-color: #0EFC8C;color:#324052;">測試帳號1</button>
        <button @click="acc2" style="background-color: #0EFC8C;color:#324052;margin-top: 5px;">測試帳號2</button>
        <button @click="acc3" style="background-color: #0EFC8C;color:#324052;margin-top: 5px;">測試帳號3</button>
      </div>
      <div style="display: grid; justify-content: center; align-items: center;background-color:#171d24;width:30%; border: solid; border-color: #0EFC8C; border-radius: 5px 5px 5px 5px;margin-left: 30%;">
            <div>
              <h1 style="margin-left: 83px;">登入</h1>
            </div>
            <div  style="margin-top:3vh;">
                <p style="display: inline-block; color:white">帳號: </p>
                <input  v-model="postData.account" id="account" class="textinput" style="display: inline-block; margin-left: 10px;" autocomplete="off">
            </div>
            <div  style="margin-top:3vh;">
                <p style="display: inline-block; color:white">密碼: </p>
                <input  v-model="postData.password" id="password"  class="textinput" type="password"  style="display: inline-block; margin-left: 10px;" autocomplete="off">
            </div>
               

            <div style="text-align: center;">
                   <p v-if="message" style="color: red;">{{ message }}</p>
            </div>
            <button v-on:click="login" style="display: flex; justify-content: center; align-items: center; margin-left: 8vh; margin-top: 2vh; width: 10vh; text-align: center;background-color: #0EFC8C;color:#324052">登入</button>
            <div>
              <h4 style="margin-left: 12vh; margin-top: 2vh;">或</h4>
            </div>
            <div>
              <button style="margin-left: 8vh; margin-top: 2vh;background-color: #0EFC8C;color:#324052" @click="nopassword">免密碼登入</button>
            </div>
            <!-- Google Sign-In 按钮 -->
            <div class="flex flex-col items-center justify-center px-4 py-12 sm:px-6 lg:px-8" style="margin-top:1vh;">
              <div class="flex w-full max-w-md flex-col items-center justify-center" style="margin-top:1vh;">
                <img src="../public/images/login/googleSign.png" alt="" @click="handleGoogleLogin">

      <LoginModal v-if="isActive" @closeModal="handleGoogleCloseLogin" />
      </div>
    </div>
  <div  class="flex w-full max-w-md flex-col items-center justify-center" style="margin-left:4vh;margin-top:2vh;">
      <p style="font-size: 14px;">沒有帳號嗎?點擊此處<router-link to="/register">註冊新帳號</router-link></p>
  </div>


        </div>
    </div>
  </section>
</template>


<script setup lang="ts">

  
  import { useRouter } from 'vue-router';
  import { googleTokenLogin } from 'vue3-google-login'
  import axios from 'axios';
  import { VueCookieNext as $cookie } from 'vue-cookie-next'
  import { ref } from 'vue'; // 引入 ref 函数用于创建响应式数据
  import startConnection from '@/data/signalR';

  const router = useRouter();



const isActive = ref<boolean>(false);

  onBeforeMount(() => {
  definePageMeta({
    middleware: 'auth',
  });
});


  const message = ref(null);
  var id ="";
  let globalId;
  useSeoMeta({ title: "Login" });

const runtimeConfig = useRuntimeConfig()
const { googleClientId: GOOGLE_CLIENT_ID } = runtimeConfig.public

const userInfo = ref(null)


  const callback = (response:any) => {
  console.log(response)
}
// 關彈窗
const handleGoogleCloseLogin = (audioPath: string) => {
  const audio = new Audio(audioPath);
  audio.play();
  isActive.value = !isActive.value;
};

// 创建响应式数据对象用于存储帐号和密码
const postData = ref({
  account: '',
  password: ''
});
const login = () => {
  console.log(postData)
  axios.post('https://localhost:7048/api/Members/Login', postData.value)
  .then(response => {
    console.log(response.data)
    if(response.data[0]=="登入成功")
    {
      // 設置 'account' Cookie，過期時間為一小時後
      
      $cookie.setCookie('accountId', response.data[1]);
      $cookie.setCookie('avatarUrl', response.data[2]);
      $cookie.setCookie('bonus', response.data[3]);
      $cookie.setCookie('name', response.data[4]);
      $cookie.setCookie('Id', response.data[5]);
      $cookie.setCookie('jwt', response.data[6]);


      startConnection();
      router.push('/');

        //從cookies中拿Id
        axios.post(`https://localhost:7048/api/Members/MemberId?protectId=${id}`, id)
          .then(response => {
            console.log(response.data);
            globalId = response.data;
            console.log(globalId);
            console.log($cookie.getCookie("accountId"));
          })
          .catch(error => {
            console.log(error);
          });
      
    }
    else{
      console.log(response.data[0]);
      message.value = response.data[0];
    }
  })
  .catch(error => {
    console.log(error);
  });
};
const test = async () => {
  let name = $cookie.getCookie("name")
  console.log(name);
};

const register = async () => {
  router.push('/register');
};

const nopassword = async () => {
  router.push('/nopassword');
};

const handleGoogleLogin = async () => {
  const accessToken = await googleTokenLogin({
    clientId: GOOGLE_CLIENT_ID
  }).then((response) => response?.access_token)

  if (!accessToken) {
    // 登入失敗
    return
  }
  const { data } = await useFetch('/api/auth/google-auth-token', {
    method: 'POST',
    body: {
      accessToken
    }
  })
  console.log(data.value.id);

  let google = data.value.id;
  console.log(google);
  axios.post(`https://localhost:7048/api/Members/GoogleId?googleId=${data.value.id}`, data.value.id)
    .then(response => {

      if (response.data[0] == "登入成功") {
        console.log(response.data[1]);
        $cookie.setCookie('accountId', response.data[1]);
        $cookie.setCookie('avatarUrl', response.data[2]);
        $cookie.setCookie('bonus', response.data[3]);
        $cookie.setCookie('name', response.data[4]);
        $cookie.setCookie('Id', response.data[5]);
        $cookie.setCookie('jwt', response.data[6]);
        $cookie.setCookie('google', google);


        startConnection();
        router.push('/');
      }
      else {
        console.log(isActive.value);
        isActive.value = !isActive.value;
        console.log(isActive.value);
      };

    })
    .catch(error => {
      console.log(error);
    });

}

const acc1 = () => {
  postData.value.account = "123";
  postData.value.password = "123";
};

const acc2 = () => {
  postData.value.account = "456";
  postData.value.password = "456";
};

const acc3 = () => {
  postData.value.account = "789";
  postData.value.password = "789";
};



</script>


<style>
.textinput{
  background: #54575a;
  border: 1px solid #26292c;
  border-radius: 5px 5px 5px 5px;
  color:white;
  overflow: hidden; /* 隱藏超出範圍的內容 */
}
</style>