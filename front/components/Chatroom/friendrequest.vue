<template>
    <div class="modal fade" id="friendrequset" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5 text-black" id="exampleModalLabel">好友邀請</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div>
                    <ul>
                        <li v-for="request in firnedRequsets" class="d-flex justify-content-between align-items-center">
                            <div>
                                <img v-if="request.senderAvatarUrl != null" :src="request.senderAvatarUrl">
                                <img v-else src="/public/images/avatar/152.png">
                                <span class="text-black">{{ request.senderName }}</span>
                            </div>

                            <div>
                                <button type="button" class="btn btn-primary me-2"
                                    @click="acceptFriendRequest(request.senderId, request.receiveId)">接受</button>
                                <button type="button" class="btn btn-danger"
                                    @click="rejectFriendRequest(request.senderId, request.receiveId)">拒絕</button>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import axios from 'axios';
import { VueCookieNext as $cookie } from 'vue-cookie-next';

const modalRef = ref(null); // 模态框组件的引用

const props = defineProps({
    firnedRequsets: Object,
});

async function acceptFriendRequest(senderId, receiveId) {
    try {

        const response = await axios.put(`https://localhost:7048/Chat/Accept?senderId=${senderId}&receiveId=${receiveId}`);
        console.log(response.data); // 打印成功消息
        // 从好友请求列表中移除已接受的请求
        checkAndCloseModal();
        emit('friendRequestAccepted');
    } catch (error) {
        console.error('接受好友请求失败:', error);
    }
}

async function rejectFriendRequest(senderId, receiveId) {
    try {
        const response = await axios.put(`https://localhost:7048/Chat/Reject?senderId=${senderId}&receiveId=${receiveId}`);
        console.log(response.data); // 打印成功消息
        // 从好友请求列表中移除已拒绝的请求
        checkAndCloseModal();
    } catch (error) {
        console.error('拒绝好友请求失败:', error);
    }
}

function checkAndCloseModal() {
    modalRef.value.hide(); // 关闭模态框
}


onMounted(() => {
    modalRef.value = new bootstrap.Modal(document.getElementById('friendrequset'));
});

</script>

<style scoped>
img {
    width: 50px;
    height: 50px;
    border-radius: 50%;
}
</style>