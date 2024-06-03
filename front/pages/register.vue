<template>
  <section class="slider__area slider__bg" style="background-image: url(/images/slider/slider_bg.jpg);height:750px" data-background="/images/slider/slider_bg.jpg" >
    <div style="display: flex;justify-content: center;align-items: center;height: 50vh; ">
        <div style="display: grid; justify-content: center;place-items: center; background-color:#171d24; width:25%;border: solid; border-color: #0EFC8C; border-radius: 5px 5px 5px 5px;">
          <h1>註冊</h1>
            <div style="margin-top:2vh;margin-left:32px;">
                <p style="display: inline-block;color:white ">帳號:</p>
                <input  v-model="postData.account" class="textinput" id="account"  style="display: inline-block; "@blur="focusoutaccount();" autocomplete="off">
                <p v-if="isAccountOK" class="dangertext">帳號已存在</p>
                <p v-if="isAccountEmpty" class="dangertext">帳號不得為空</p>
            </div>
            <div style="margin-top:10px;margin-left:32px; ">
                <p style="display: inline-block;color:white ">暱稱:</p>
                <input v-model="postData.name" class="textinput"  id="name"  style="display: inline-block; "@blur="focusoutname();validatebutton()" autocomplete="off">
                <p v-if="isNicknameOK" class="dangertext">暱稱已存在</p>
                <p v-if="isNicknameEmpty" class="dangertext">暱稱不得為空</p>
            </div>
            <div style="margin-top:10px;margin-left:32px; width:280px">
                <p style="display: inline-block;color:white;margin-left:18px">密碼:</p>
                <input v-model="postData.password" class="textinput"  id="password" type="password"  style="display: inline-block; "@blur="focusoutPassword();validatebutton()" autocomplete="off">
                <p v-if="isPasswordOK" class="dangertext">請確認密碼格式正確:</p>
                <p v-if="isPasswordOK" class="dangertext">需有大小寫英文和數字 長度介於6-12間</p>
                <!-- dangerrepasswordtext -->
            </div>
            <div style="margin-top:10px;">
                <p style="display: inline-block;color:white ">重複密碼:</p>
                <!-- @focusout="" -->
                <input v-model="rePassword" class="textinput"  id="rePassword" type="password"  style="display: inline-block; "@blur="focusoutRePassword();validatebutton()" autocomplete="off">
                <p v-if="isRePasswordOK" class="dangertext">密碼需一致</p>
            </div>
            <div style="margin-top:10px;margin-left:32px;">
                <p style="display: inline-block;color:white ">郵箱:</p>
                <input v-model="postData.email" class="textinput"  id="email" type="email"  style="display: inline-block; "@blur="validateEmail();validatebutton()" autocomplete="off">
                <p v-if="isEmailOK" class="dangertext">郵箱格式不正確</p>
            </div>
            <div style="margin-top:10px;margin-bottom: 10px;">
                <button  @click="register" :disabled="isButtonDisabled" style="justify-content: center;" class="button" >註冊帳號</button>
                <!-- class="button" -->
            </div>
        </div>
    </div>
  </section>
</template>

<script setup lang="ts">

definePageMeta({
  middleware: 'auth',
});


  import { useRouter } from 'vue-router';
  import axios from 'axios';
  import { VueCookieNext as $cookie } from 'vue-cookie-next'
  import { ref, onBeforeMount  } from 'vue'; // 引入 ref 函数用于创建响应式数据

  const router = useRouter();

  const isAccountOK = ref<boolean>(false);
  const isAccountEmpty = ref<boolean>(false);
  const isFirstAccount = ref<boolean>(true);
  const isNicknameOK = ref<boolean>(false);
  const isNicknameEmpty = ref<boolean>(false);
  const isFirstNickname = ref<boolean>(true);
  const isPasswordOK = ref<boolean>(false);
  const isFirstPassword = ref<boolean>(true);
  const isRePasswordOK = ref<boolean>(false);
  const isFirstRePassword = ref<boolean>(true);
  const isEmailOK = ref<boolean>(false);
  const isFirstEmail = ref<boolean>(true);
  const isButtonDisabled = ref<boolean>(true);

    // 创建响应式数据对象用于存储帐号和密码
  const postData = ref({
    account: '',
    password: '',
    name:'',
    email:'',
    google:null
  });

  const rePassword =ref("");
  onBeforeMount(() => {
  if($cookie.getCookie("google")){
    postData.value.google = $cookie.getCookie("google");
  
 }
});
const register = () => {
    console.log(postData)
    axios.post('https://localhost:7048/api/Members/Create', postData.value)
    .then(response => {
    if(response.data =="註冊成功"){

      console.log(response.data);    
      router.push("/registersuccess");
    }
    else{
      console.log(response.data);
    }
  })
  .catch(error => {
    console.log(error);
  });
};
let state;

// 帳號驗證
const focusoutaccount = () => {

  isAccountEmpty.value = false;
  isAccountOK.value = false;
  isFirstAccount.value = false;

  if(postData.value.account=="")
  {
    isAccountEmpty.value = true;
    validatebutton();
    console.log("112")
  }
  else{
    axios.post(`https://localhost:7048/api/Members/TestMemberAccount?account=${postData.value.account}`, postData.value.account)
         .then(response => {
          state =response.data
          if(state==false)
          {
             isAccountOK.value = true;
             console.log(isAccountOK.value);
          }
          validatebutton();
            })
         .catch(error => {
            console.log(error);
          });
     }
  }




//暱稱驗證
const focusoutname = () => {

isNicknameEmpty.value = false;
isNicknameOK.value = false;
isFirstNickname.value = false;

if(postData.value.name=="")
{
  isNicknameEmpty.value = !isNicknameEmpty.value;

  console.log("112")
}
else{
  axios.post(`https://localhost:7048/api/Members/TestMemberName?name=${postData.value.name}`, postData.value.name)
       .then(response => {
        state =response.data
        if(state==false)
        {
           isNicknameOK.value = !isNicknameOK.value;

        }
          })
       .catch(error => {
          console.log(error);
        });
 }
}
// 正则表达式用于验证密码格式
const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,12}$/;
//驗證密碼
const focusoutPassword = () => {
    const password = postData.value.password;

    isPasswordOK.value = false;
    isFirstPassword.value = false;


    // 检查密码是否符合格式
    if (!passwordRegex.test(password)) {
      isPasswordOK.value = !isPasswordOK.value;

    }
    if(!isFirstRePassword.value){
      focusoutRePassword();
    }
}

//驗證重複密碼
const focusoutRePassword = () => {
  isFirstRePassword.value = false;
    if(rePassword.value!=postData.value.password){
      isRePasswordOK.value =true
    }
    else{
      isRePasswordOK.value =false
    }
}
const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
//驗證郵箱
const validateEmail = () => {
  isEmailOK.value = false;
  isFirstEmail.value = false;
  // return emailRegex.test(email);
  const email = postData.value.email;
  if (!emailRegex.test(email)) {
  isEmailOK.value=!isEmailOK.value
}
};




//驗證按鈕
const validatebutton =() => {
  if(isAccountOK.value == false&&isAccountEmpty.value==false&&isNicknameOK.value==false&&
  isNicknameEmpty.value==false&&isPasswordOK.value==false&&isRePasswordOK.value==false&&isEmailOK.value==false&&!isFirstAccount.value&&!isFirstNickname.value&&
  !isFirstPassword.value&&!isFirstRePassword.value&&!isFirstEmail.value){
    isButtonDisabled.value = false
  }
  else{
    isButtonDisabled.value = true
  }
}

</script>

<style scoped>

.dangertext {
  color: red;
  text-align: center;
  /* font-size: 14px; */
  /* margin-right: 10px; */
  margin:0px;
}
.dangerrepasswordtext {
  color: red;
  margin-top: 0;
  /* font-size: 14px; */
}
.textinput{
  background: #54575a;
  border: 1px solid #26292c;
  border-radius: 5px 5px 5px 5px;
  color:white;
  overflow: hidden; /* 隱藏超出範圍的內容 */
}
.button{
  background-color: #0EFC8C;
  color:#324052;
}
.button:disabled{
  background-color: #54575a;
  color:#4AC877;
}
</style>