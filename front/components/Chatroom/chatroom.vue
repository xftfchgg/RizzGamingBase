<template>
    <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1"
        aria-labelledby="staticBackdropLabel" aria-hidden="true" ref="chatModal" @shown="handleModalShown();">
        <div class="modal-dialog ">
            <div class="modal-content bg-success">
                <div class="modal-header">
                    <h1 class="modal-title fs-5 text-black" id="staticBackdropLabel">聊天室</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="chat  bg-black">
                        <div class="chat-header clearfix sticky-top bg-black">
                            <div class="row">
                                <div class="col-lg-6">
                                    <a href="javascript:void(0);" data-toggle="modal" data-target="#view_info">
                                        <img v-if="friendInfo.value.avatarUrl != null" :src="friendInfo.value.avatarUrl"
                                            alt="avatar"
                                            @load="getMessageHistory(IdFromCookie, friendInfo.value.userId);">
                                        <img v-else src="/public/images/avatar/152.png" alt="avatar"
                                            @load="getMessageHistory(IdFromCookie, friendInfo.value.userId);">
                                    </a>
                                    <div class="chat-about">
                                        <h6 class="m-b-0 text-white">{{ friendInfo.value.userName }}</h6>
                                        <div class="status"> <i class="fa fa-circle online"></i> online </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="chat-history">
                            <ul class="m-b-0" id="chatroom">
                                <li v-for="message in messages" class="clearfix">
                                    <div class="clearfix">
                                        <div
                                            :class="{ 'message-data': true, 'text-right': message.sender_id === IdFromCookie }">
                                            <span class="message-data-time text-white"
                                                :class="{ 'float-right': message.sender_id === IdFromCookie }">{{
            formattedTime(message.time)
        }}</span>
                                            <i class="fa fa-check"></i>
                                        </div>
                                        <div
                                            :class="{ 'message': true, 'other-message': message.sender_id === IdFromCookie, 'my-message': message.sender_id != IdFromCookie, 'float-right': message.sender_id === IdFromCookie }">
                                            {{ message.content }}
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                        <div class=" chat-footer chat-message clearfix  sticky-bottom">
                            <div class="input-area mb-0 ">
                                <input type="text" v-model="newMessage" placeholder="請輸入訊息....."
                                    @keydown.enter="sendMessage()">
                                <button @click="sendMessage()">send</button>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>
</template>


<script setup>
import startConnection from '@/data/signalR';
import { VueCookieNext as $cookie } from 'vue-cookie-next';
import axios from 'axios';

// 定义一个响应式变量来存储新消息的内容
const newMessage = ref('');
let friendInfo = ref('');
let selectedFriend = ref(null);
selectedFriend = inject('selectedFriend');
friendInfo.value = selectedFriend;

let IdFromCookie = ref('');
IdFromCookie.value = parseInt($cookie.getCookie('Id'));
let connection = startConnection();

// 模拟聊天消息数据
let messages = ref([]);



const formattedTime = (lastLoginTime) => {
    const date = new Date(lastLoginTime);
    return `${('0' + (date.getMonth() + 1)).slice(-2)}-${('0' + date.getDate()).slice(-2)} ${('0' + date.getHours()).slice(-2)}:${('0' + date.getMinutes()).slice(-2)}`;
};

async function getMessageHistory(x, y) {
    x = parseInt(x);
    y = parseInt(y);
    console.log(x, y);
    messages.value = [];
    try {
        let response = await axios.get(`https://localhost:7048/Chat/GetMessageHistory?memberId=${x}&friendId=${y}`);
        messages.value = response.data;
        console.log(response.data);

        setTimeout(() => {
            scrollToBottom();
        });
    } catch (error) {
        console.error('An error occurred while fetching message history:', error);
    }
    setTimeout(() => {
        scrollToBottom();
    });
}

// 定义 scrollToBottom 方法
function scrollToBottom() {
    const chatHistoryElement = document.querySelector('.chat-history');
    chatHistoryElement.scrollTop = chatHistoryElement.scrollHeight;
}


connection.on('sendMessageTo', (message) => {
    let receiveMessage = {
        id: message.Id,
        sender_id: message.SenderId,
        receiver_id: message.ReceiveId,
        content: message.Content,
        time: message.Time
    };
    console.log(receiveMessage);
    messages.value.push(receiveMessage);
    console.log(message);
    setTimeout(() => {
        scrollToBottom();
    });
});

connection.on('sendCaller', (message) => {

    let sendMessage = {
        id: message.Id,
        sender_id: message.SenderId,
        receiver_id: message.ReceiveId,
        content: message.Content,
        time: message.Time
    };
    messages.value.push(sendMessage);
    console.log(sendMessage);
    console.log(message);



    setTimeout(() => {
        scrollToBottom();
    });
});

const sendMessage = () => {
    const messageContent = newMessage.value.trim();
    if (messageContent === '') {
        return; // 如果消息内容为空，则不发送消息
    }
    console.log(IdFromCookie.value, selectedFriend.value.userId, selectedFriend.value.connectionId, messageContent);
    connection.invoke('SendMessageToFriend', IdFromCookie.value, selectedFriend.value.userId, selectedFriend.value.connectionId, messageContent);
    newMessage.value = '';
}

// 监听模态框的显示状态，并在显示时执行滚动操作
watchEffect(() => {
    const modalElement = document.getElementById('staticBackdrop');
    if (modalElement && modalElement.classList.contains('show')) {
        scrollToBottom();
    }
});

// 在模态框渲染后执行滚动操作
function handleModalShown() {
    nextTick(scrollToBottom);
}
onMounted(() => {
    setTimeout(() => {
        scrollToBottom();
    });
});

</script>

<style scoped>
body {
    background-color: #f4f7f6;
    margin-top: 20px;
}

.modal-dialog {
    max-width: 600px;
    /* 设置模态框的最大宽度 */
    max-height: 600px;
    /* 设置模态框的最大高度 */
    margin: 30px auto;
    /* 居中模态框 */
}


.card {
    background: black;
    transition: .5s;
    border: 0;
    margin-bottom: 30px;
    border-radius: .55rem;
    position: relative;
    width: 100%;
    box-shadow: 0 1px 2px 0 rgb(0 0 0 / 10%);
}

.chat-app .people-list {
    width: 280px;
    position: absolute;
    left: 0;
    top: 0;
    padding: 20px;
    z-index: 7
}

.chat-app .chat {
    margin-left: 280px;
    border-left: 1px solid #eaeaea
}

.people-list {
    -moz-transition: .5s;
    -o-transition: .5s;
    -webkit-transition: .5s;
    transition: .5s
}

.people-list .chat-list li {
    padding: 10px 15px;
    list-style: none;
    border-radius: 3px
}

.people-list .chat-list li:hover {
    background: #efefef;
    cursor: pointer
}

.people-list .chat-list li.active {
    background: #efefef
}

.people-list .chat-list li .name {
    font-size: 15px
}

.people-list .chat-list img {
    width: 45px;
    border-radius: 50%
}

.people-list img {
    float: left;
    border-radius: 50%
}

.people-list .about {
    float: left;
    padding-left: 8px
}

.people-list .status {
    color: #999;
    font-size: 13px
}

.chat .chat-header {
    padding: 15px 20px;
    border-bottom: 2px solid #f4f7f6
}

.chat .chat-header img {
    float: left;
    border-radius: 40px;
    width: 40px
}

.chat .chat-header .chat-about {
    float: left;
    padding-left: 10px
}

.chat .chat-history {
    padding: 20px;
    border-bottom: 2px solid #fff;
    overflow-y: auto;
    height: 600px;
    max-height: 600px;
}

.chat .chat-history ul {
    padding: 0
}

.chat .chat-history ul li {
    list-style: none;
    margin-bottom: 30px;
    word-wrap: break-word;

}

.chat .chat-history ul li:last-child {
    margin-bottom: 0px;
    max-width: 600px;
    word-wrap: break-word;
}

.chat .chat-history .message-data {
    margin-bottom: 15px
}

.chat .chat-history .message-data img {
    border-radius: 40px;
    width: 40px
}

.chat .chat-history .message-data-time {
    color: #434651;
    padding-left: 6px
}

.chat .chat-history .message {
    color: #444;
    padding: 18px 20px;
    line-height: 26px;
    font-size: 16px;
    border-radius: 7px;
    display: inline-block;
    position: relative
}

.chat .chat-history .message:after {
    bottom: 100%;
    left: 7%;
    border: solid transparent;
    content: " ";
    height: 0;
    width: 0;
    position: absolute;
    pointer-events: none;
    border-bottom-color: #fff;
    border-width: 10px;
    margin-left: -10px
}

.chat .chat-history .my-message {
    background: #efefef
}

.chat .chat-history .my-message:after {
    bottom: 100%;
    left: 30px;
    border: solid transparent;
    content: " ";
    height: 0;
    width: 0;
    position: absolute;
    pointer-events: none;
    border-bottom-color: #efefef;
    border-width: 10px;
    margin-left: -10px
}

.chat .chat-history .other-message {
    background: #e8f1f3;
    text-align: right
}

.chat .chat-history .other-message:after {
    border-bottom-color: #e8f1f3;
    left: 80%
}

.chat .chat-message {
    padding: 20px
}

.online,
.offline,
.me {
    margin-right: 2px;
    font-size: 8px;
    vertical-align: middle
}

.online {
    color: #86c541
}

.offline {
    color: #e47297
}

.me {
    color: #1d8ecd
}

.float-right {
    float: right
}

.clearfix:after {
    visibility: hidden;
    display: block;
    font-size: 0;
    content: " ";
    clear: both;
    height: 0
}

@media only screen and (max-width: 767px) {
    .chat-app .people-list {
        height: 465px;
        width: 100%;
        overflow-x: auto;
        background: #fff;
        left: -400px;
        display: none
    }

    .chat-app .people-list.open {
        left: 0
    }

    .chat-app .chat {
        margin: 0
    }

    .chat-app .chat .chat-header {
        border-radius: 0.55rem 0.55rem 0 0
    }

    .chat-app .chat-history {
        height: 300px;
        overflow-x: auto
    }
}

@media only screen and (min-width: 768px) and (max-width: 992px) {
    .chat-app .chat-list {
        height: 650px;
        overflow-x: auto
    }

    .chat-app .chat-history {
        height: 600px;
        overflow-x: auto
    }
}

@media only screen and (min-device-width: 768px) and (max-device-width: 1024px) and (orientation: landscape) and (-webkit-min-device-pixel-ratio: 1) {
    .chat-app .chat-list {
        height: 480px;
        overflow-x: auto
    }

    .chat-app .chat-history {
        height: calc(100vh - 350px);
        overflow-x: auto
    }
}
</style>../../data/signalR