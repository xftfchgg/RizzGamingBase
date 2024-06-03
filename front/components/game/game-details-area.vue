<template>
    <section class="blog-area blog-details-area">
        <div class="container">
            <div class="row justify-content-center">
                <div class="blog-post-wrapper">
                    <div class="blog-post-item">
                        <div class="blog-post-thumb">
                            <game-gallery :gameData="gameData"></game-gallery>
                        </div>
                        <div class="blog-post-content blog-details-content">
                            <div class="blog-post-meta">
                                <ul class="list-wrap">
                                    <li>發行商 : <nuxt-link to="#">{{ developerName }}</nuxt-link></li>
                                    <li v-if="releaseDate < currentDate"><i class="far fa-calendar-alt"></i>發行日期 : {{
                                formatDate(gameData.releaseDate)
                            }}</li>
                                    <li v-else><i class="far fa-calendar-alt"></i>發行日期 : 即將發行</li>
                                    <li v-if="releaseDate < currentDate && gameData.rating != 0">評分 : {{
                                gameData.rating }}</li>
                                    <li v-else>評分 : 暫無評分</li>
                                </ul>
                            </div>
                            <h1 class="title text-capitalize">遊戲介紹</h1>
                            <p>{{ gameData.description }}</p>
                        </div>
                        <div v-if="gameData.dlCs.length > 0">
                            <h2>DLC</h2>
                            <div v-for="dlc in gameData.dlCs">
                                <nuxt-link :to="`/game-details/${dlc.id}`">
                                    <div class="row dlc-container justify-content-center align-items-center">
                                        <div class="col-8">
                                            <h4>{{ dlc.name }}</h4>
                                        </div>
                                        <div class="col-4 image-container">
                                            <img
                                                :src="`/images/games/cover/${dlc.developerId}/${dlc.id}/${dlc.cover}.jpg`">
                                        </div>
                                    </div>
                                </nuxt-link>
                            </div>
                        </div>
                        <div v-if="games">
                            <h2>主要遊戲</h2><span>(你必須擁有主要遊戲才能安裝DLC內容)</span>
                            <div>
                                <nuxt-link :to="`/game-details/${games.id}`">
                                    <div class="row dlc-container justify-content-center align-items-center">
                                        <div class="col-8">
                                            <h4>{{ games.name }}</h4>
                                        </div>
                                        <div class="col-4 image-container">
                                            <img
                                                :src="`/images/games/cover/${games.developerId}/${games.id}/${games.cover}.jpg`">
                                        </div>
                                    </div>
                                </nuxt-link>
                            </div>
                        </div>
                    </div>
                    <div v-if="memberId && releaseDate < currentDate">

                        <div v-if="memberComment.length > 0" class="comment-respond mb-3">
                            <h1 class="fw-title">你的評分與評價</h1>
                            <game-detail-memberComment :gameId="gameId" />
                        </div>

                        <div v-else class="comment-respond mb-3">
                            <h1 class="fw-title">留下評分與評價</h1>
                            <game-detail-comment-form :gameId="gameId" @refreshComment="loadComment" />
                        </div>



                    </div>
                    <div v-if="releaseDate < currentDate" class="comments-wrap">
                        <h4 class="comments-wrap-title">評論</h4>
                        <game-detail-comments :gameId="gameId" />
                    </div>
                </div>

                <div class="blog-post-sidebar">
                    <game-sidebar :gameData="gameData" />
                </div>

            </div>
        </div>
    </section>
</template>

<script setup>
import { ref, defineProps, onMounted } from "vue";
import { VueCookieNext as $cookie } from 'vue-cookie-next'
import axios from 'axios';

const props = defineProps({
    gameData: Object,
});
const currentDate = new Date()
const releaseDate = new Date(props.gameData.releaseDate);

let id = $cookie.getCookie("accountId");
let memberId;
if (id != undefined) {

    axios.post(`https://localhost:7048/api/Members/MemberId?protectId=${id}`, id)
        .then(response => {
            memberId = response.data
        })
        .catch(error => {
            console.log(error);
        });
}

let games = ref(null);
let developerName = ref(null);
const gameId = props.gameData.id;

let memberComment = ref(null);
let memberCommentLength = ref(0);
onMounted(async () => {
    try {
        const response1 = await axios.get(`https://localhost:7048/api/Games/dlc/${props.gameData.id}`);
        games.value = response1.data;
        const response2 = await axios.get(`https://localhost:7048/api/Games/developerName/${props.gameData.developerId}`);
        developerName.value = response2.data;
        const response = await axios.get(`https://localhost:7048/api/Comments/${gameId}`);
        memberComment.value = response.data.filter(comment => comment.memberId === memberId)
        memberCommentLength.value = memberComment.value.length
        console.log(memberCommentLength.value)

    } catch (error) {
        console.log(error);
    }
});

// 格式化日期的方法
const formatDate = (dateString) => {
    // 创建一个新的 Date 对象并传入日期字符串
    const date = new Date(dateString);

    // 使用 Date 对象的方法获取年月日部分
    const year = date.getFullYear();
    const month = date.getMonth() + 1; // 月份从 0 开始，所以需要加 1
    const day = date.getDate();

    // 返回格式化后的日期字符串
    return `${year}-${month}-${day}`;
};

async function loadComment() {
    const response = await axios.get(`https://localhost:7048/api/Comments/${gameId}`);
    memberComment.value = response.data.filter(comment => comment.memberId === memberId)
}

// function show(gameData) { console.log(gameData.displayImages) };
</script>
<style scoped>
.image-container {
    position: relative;
    overflow: hidden;
    border-radius: 10px;
    padding-right: 0;
}

.image-container::after {
    content: "";
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-image: linear-gradient(to left, transparent, rgba(38, 48, 48, 1));
}

.dlc-container {
    background-color: rgb(38, 48, 48);
    border-radius: 10px;
}
</style>
