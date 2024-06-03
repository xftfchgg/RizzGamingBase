<template>
  <!-- class="modal-overlay"
  class="modal" -->
  <div class="modal-overlay">
    <div class="modal">
      <!-- <img class="check" src="~/assets/check-icon.png" alt="" /> -->
      <div>
        <h2 style="color:black" >您的google帳號並沒有綁定帳號</h2>
        <h2 style="color:black">是否要註冊帳號?</h2>
      </div>
      <div>
        <button @click="register" style="margin-right: 2px;">是</button>
        <button @click="close" style="margin-left: 2px;">否</button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { defineProps, defineEmits } from 'vue';
import { useRouter } from 'vue-router';

const props = defineProps({
  // 在这里可以定义props
});

const emits = defineEmits(['closeModal']); // 定义 close modal 事件

const router = useRouter();

const close = async () => {
  emits('closeModal'); // 触发 close modal 事件
};

const register = async () => {
  router.push('/register');
};
</script>

<style scoped>

.modal-overlay {
  position: fixed;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  /* display: flex; */
  /* justify-content: center; */

  background-color: #000000da;
  /* #000000da */
}

.modal {
  /* text-align: center;
  background-color: white;
  height: 500px;
  width: 500px;
  display: flex;
  justify-content: center; 
  align-items: center; 
  padding: 60px 0; */
  text-align: center;
  border-radius: 20px; 
  background-color: white;
  display: flex;
  height: 500px;
  width: 500px;
  justify-content: center; 
  align-items: center; 
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);

  flex-direction: column;
}
.close {
  margin: 10% 0 0 16px;
  cursor: pointer;
}

.close-img {
  width: 25px;
}

.check {
  width: 150px;
}

h6 {
  font-weight: 500;
  font-size: 28px;
  margin: 20px 0;
}

p {
  font-size: 16px;
  margin: 20px 0;
}

button {
  background-color: #ac003e;
  width: 150px;
  height: 40px;
  color: white;
  font-size: 14px;
  border-radius: 16px;
  margin-top: 50px;
}
</style>