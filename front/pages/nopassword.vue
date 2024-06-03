<template>
    <section class="slider__area slider__bg" style="background-image: url(/images/slider/slider_bg.jpg);height:750px" data-background="/images/slider/slider_bg.jpg" >
        <div style="display: flex;justify-content: center;align-items: center;height: 50vh; ">
            <div style="display: grid; justify-content: center;place-items: center; background-color:#171d24; width:30%;height:50%;border: solid; border-color: #0EFC8C; border-radius: 5px 5px 5px 5px;">
                <h3 v-if="notsend">免密碼登入</h3>
                <div style="margin-top:2vh;margin-left:32px;" v-if="notsend">
                    <p style="display: inline-block;color:white " v-if="notsend">帳號:</p>
                    <input  class="textinput" id="account" v-model="postData.account" style="display: inline-block; " autocomplete="off" v-if="notsend">
                </div>
                <button  style="display: flex; justify-content: center; align-items: center;  margin-top: 2vh;margin-bottom: 2vh; width: 10vh; text-align: center;background-color:#0EFC8C;color:#324052" @click="send" v-if="notsend">發送</button>
                <h3 v-if="onsend">已向您的郵箱發送信件,請查看郵箱完成登入</h3>
            </div>
        </div>
    </section>
</template>


<script setup>
import axios from 'axios';


let notsend = ref(true);
let onsend = ref(false);

const postData = ref({
  account: '',
});


const send = () => {
    axios.post(`https://localhost:7048/api/Members/noPassword?account=${postData.value.account}`,postData.value.account)
    .then(response => {
        console.log(postData.value.account)
        console.log(response.data)
        if(response.data == "已向您的郵箱發送信件,請查看郵箱完成登入")
        {
            notsend.value = !notsend.value;
            onsend.value = !onsend.value;
        }
    })
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