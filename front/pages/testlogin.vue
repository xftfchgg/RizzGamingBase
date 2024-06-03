<template>
    <section class="slider__area slider__bg" style="background-image: url(/images/slider/slider_bg.jpg);height:750px"
      data-background="/images/slider/slider_bg.jpg">
      <div style="display: flex;justify-content: center;align-items: center;height: 50vh;">
      <!-- width:40% -->
      <!-- justify-content: center;align-items: center -->
      <!-- #242633 -->
        <div style="display: grid; justify-content: center; align-items: center;background-color:#171d24;width:30%; border: solid; border-color: #0EFC8C; border-radius: 5px 5px 5px 5px;">
          <!-- height: 30vh;width:50vh; -->
          <!-- background-color:#242633  -->
          <!-- style="margin-left:4vh;margin-top:3vh;" -->
          <!-- <div style="width: 100%; height: 3vh; background-color:#0EFC8C;"><p></p></div> -->
              <div  style="margin-top:3vh;">
                  <p style="display: inline-block; color:white">帳號: </p>
                  <input  v-model="postData.account" id="account" class="textinput" style="display: inline-block; margin-left: 10px;">
              </div>
              <div  style="margin-top:3vh;">
                  <p style="display: inline-block; color:white">密碼: </p>
                  <input  v-model="postData.password" id="password"  class="textinput" type="password"  style="display: inline-block; margin-left: 10px;">
              </div>
                 
 
              <div style="text-align: center;">
                     <p v-if="message" style="color: yellow;">{{ message }}</p>
              </div>
              <button v-on:click="login" style="display: flex; justify-content: center; align-items: center; margin-left: 8vh; margin-top: 2vh; width: 10vh; text-align: center;background-color: #0EFC8C;color:#324052">登入</button>
              <!-- Google Sign-In 按钮 -->
              <div class="flex flex-col items-center justify-center px-4 py-12 sm:px-6 lg:px-8" style="margin-top:2vh;">
                <div class="flex w-full max-w-md flex-col items-center justify-center" style="margin-top:2vh;">
                  <img src="../public/images/login/googleSign.png" alt="" @click="handleGoogleLogin">
            <!-- <button 
            class="flex rounded-md border border-gray-100 bg-white px-4 py-2 text-sm font-medium shadow-sm hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-emerald-500 focus:ring-offset-2" style="margin-left:12vh;margin-top:8vh;background-image:url(../public/images/login/googleSign.png)"
            @click="handleGoogleLogin"
            >
  
               <span class="text-slate-500 group-hover:text-slate-600">使用 Google 進行登入</span>
            </button> -->
        <LoginModal v-if="isActive" @closeModal="handleGoogleCloseLogin"/>
        </div>
      </div>
      <!-- 一鍵登入 -->
      <!-- <button>123</button> -->
    <div  class="flex w-full max-w-md flex-col items-center justify-center" style="margin-left:4vh;margin-top:2vh;">
        <p style="font-size: 14px;">沒有帳號嗎?點擊此處<router-link to="/register">註冊新帳號</router-link></p>
        <!-- <button  @click="register">註冊帳號</button> -->
    </div>
  <!--  -->
  
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
  //   import { useAuthStore } from '../store/logincookies'
  // const authStore = useAuthStore()
  
  // 在页面组件的生命周期钩子中获取 Cookie 值并存储到 Pinia store 中
  // onMounted(() => {
  
  //     })
  
  const isActive = ref<boolean>(false);
  
    onBeforeMount(() => {
    definePageMeta({
      middleware: 'auth',
    });
  });
  
  // import { ElMessageBox } from 'element-plus';
  
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
  // 開彈窗
  // const handleOpenLogin = (audioPath: string) => {
  //   const audio = new Audio(audioPath);
  //   audio.play();
  //   isActive.value = !isActive.value;
  // };
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
      if(response.data[0]=="登入成功"){
        // 設置 'account' Cookie，過期時間為一小時後
        
        $cookie.setCookie('accountId', response.data[1]);
        $cookie.setCookie('avatarUrl', response.data[2]);
        $cookie.setCookie('bonus', response.data[3]);
        $cookie.setCookie('name', response.data[4]);
        $cookie.setCookie('Id', response.data[5]);
  
        // const accountId=$cookie.getCookie('accountId');
        // authStore.setCookieData({ accountId})
  
  
  
  
          startConnection();
          //
          // console.log( header.isAccountIdExists);
          // header.isAccountIdExists = true;
          //
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
          $cookie.setCookie('google', google);
  
  
          startConnection();
          router.push('/');
        }
        else {
          //const audio = new Audio('/audio/click.wav');
          //audio.play();
          console.log(isActive.value);
          isActive.value = !isActive.value;
          console.log(isActive.value);
          // router.push('/register');
        };
  
      })
      .catch(error => {
        console.log(error);
      });
  
  }
  
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
  