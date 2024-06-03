<template>
<client-only>
    <section class="blog-area">
        
        <div style="width:100%;height:100%;display:flex;justify-content: center;">
            <div  style="display: flex; justify-content: center;place-items: center; background-color:#171d24;width: 100%;" >
                <!-- 左側自訂分類 -->
                <!-- <div  class="cbuttonlist" style="background-color: black;height:100%;margin-right: 30px;">
                    <button @click="">全部遊戲</button>
                </div> -->

                <!--  -->
                <div style="display: grid; justify-content: center;place-items: center;">
                
                 <div class="container" v-for="game in games" :key="game.id" style="width: 800px;margin-top: 5px;">
                    <div class="gamedisplay" >
                        <!-- 左側 -->
                        <div style="">
                            <img :src="'../../'+'images/games/cover/' + game.developerId + '/' + game.id + '/' + game.cover + '.jpg'" style="width: 259px;height:121px" class="gameimg"/>
                        </div>
                        <!-- 右側 -->
                        <div style="width:60%">
                            <!-- 右左 -->
                            <!-- 右左上 -->
                            <!-- 標題 -->
                            <div style="margin-top: 5px;height:25%">
                                <p style="color: white;margin-left: 15px;margin-top: 10px; font-size: 18px;">{{ game.name, tagName }}</p>
                            </div>
                            <!-- 右左下 -->
                            <!-- dlc -->
                            <div style="display: grid; place-content: end stretch;  height: 60%;">
                                <div style=" height:10%;width:60%;margin-left: 5px;margin-bottom: 10px;" v-if="game.dlcs && game.dlcs.length > 0"> 
                                    
                                    <select class="selectlist" style="width: 120px;" @click="isOpen = !isOpen" @blur="isOpen = false">
                                          <!-- 固定显示的选项 -->
                                          <option value="" disabled selected>你擁有的DLC</option>
                                          <!-- 循环显示游戏的 DLC 列表 -->
                                          <!-- style="background-color: #606471;color:white" -->
                                          <option v-for="dlc in game.dlcs" :key="dlc.id" :value="dlc.name" disabled style="background-color: #606471;color:white">{{ dlc.name }}</option>
                                    </select>
                                    
                                </div>
                            </div>

                        </div>
                        <!-- 右右 -->
                        <div style="width:40%;">
                            <!-- 右右上 -->
                            <div style="height: 50%;">
                            </div>
                            <!-- 右右下 -->
                            <div style="position: relative; height: 50%; ">
                                <a :href="'../../'+'images/games/cover/' + game.developerId + '/' + game.id + '/' + game.cover + '.jpg'" download="header.jpg">
                                    <button class="download" style="position: absolute; bottom: 0; right: 0;margin-right: 5px;margin-bottom: 5px;">下載</button>
                                </a>

                                    
                            </div>
                        </div>
                    </div>

                 </div>
                </div>
<!-- 圖片 -->
            <!-- <img :src="'images/games/cover/' + game.developerId + '/' + game.id + '/' + game.cover + '.jpg'" /> -->


            </div>
        </div>

    </section>
</client-only>
</template>

<script setup>

import { VueCookieNext as $cookie } from "vue-cookie-next";
import axios from "axios";
import { useRoute } from 'vue-router';

const router = useRoute();
let games = ref(null);
let isOpen = ref(false);
const memberId = $cookie.getCookie("accountId");
onBeforeMount(async () => {
  definePageMeta({
    middleware: "nologin",
  });
  const response = await axios.get(`https://localhost:7048/api/Collection/collectiongames?memberId=${memberId}`,memberId)
  console.log( response.data)
  games.value = response.data;
  console.log( games.value[0].dlcs)

  // 移动到 onBeforeMount 钩子中
  const downloadButtons = document.querySelectorAll('.download');
  downloadButtons.forEach(button => {
    button.addEventListener('click', () => {
      // 在这里添加处理点击事件的代码
    });
  });
});
</script>




<style>
.gameimg{
    margin: 5px;
}
.gamedisplay{
    background-color: #616880;
    display: flex;;
}
.download{
    border: 1px solid #26292c;
    border-radius: 5px 5px 5px 5px;
    background-color: #1A9FFF;
    color: white;
}
.selectlist{
    border: 1px solid #26292c;
    border-radius: 5px 5px 5px 5px;
    margin-left: 5px;
    /* background-color: #515A65;
    color: white; */
}
</style>