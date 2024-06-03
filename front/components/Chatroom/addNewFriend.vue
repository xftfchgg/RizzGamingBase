<template>
    <div class="modal fade" id="addnewfriend" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5 text-black" id="exampleModalLabel">添加好友</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <input type="text" class="form-control" placeholder="請輸入好友名稱" v-model="friendName">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">取消</button>
                    <button type="button" class="btn btn-primary" @click="addFriend()">加入</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import axios from 'axios';
import { VueCookieNext as $cookie } from 'vue-cookie-next';

let friendName = ref('');


async function addFriend() {
    try {
        const id = $cookie.getCookie('Id');
        const receiveIdResponse = await axios.get(`https://localhost:7048/Chat/GetMemberId?userName=${friendName.value}`);
        const receiveId = receiveIdResponse.data;

        console.log(receiveId);

        const res = await axios.post(`https://localhost:7048/Chat/AddFriendRequest?memberId=${id}&friendId=${receiveId}`, {
            friendName: friendName.value
        });

        console.log(res.data);
        friendName.value = '';
        window.alert('已發送交友邀請');
    } catch (error) {
        console.error('添加好友失敗:', error);
    }
}


</script>
