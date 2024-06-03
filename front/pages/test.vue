<template>
    <div v-if="showModal" class="modal-container">
      <div class="modal-content">
        <p>{{ message }}</p>
        <div class="button-container" style="width: 50%; height:50%">
          <button @click="handleConfirm">Yes</button>
          <button @click="handleCancel">No</button>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        showModal: true,
        message: '您的google帳號'
      };
    },
    methods: {
      // 打开消息框并设置消息内容
      openModal(message) {
        this.message = message;
        this.showModal = true;
      },
      // 处理用户点击确认
      handleConfirm() {
        // 执行相应的操作
        console.log('User clicked Yes');
        // 关闭消息框
        this.showModal = false;
      },
      // 处理用户点击取消
      handleCancel() {
        // 执行相应的操作
        console.log('User clicked No');
        // 关闭消息框
        this.showModal = false;
      }
    }
  };
  </script>
  
  <style scoped>
  .modal-container {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
  }
  
  .modal-content {
    background-color: white;
    padding: 20px;
    border-radius: 5px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
  }
  
  .button-container {
    margin-top: 20px;
    text-align: center;
  }
  
  button {
    margin-right: 10px;
  }
  </style>