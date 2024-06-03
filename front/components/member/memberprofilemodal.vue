<template>
  <div class="modal-overlay">
    <div class="modal">
      <div style="display: flex; justify-content: flex-start; align-items: center; height: 10%; width: 100%;">
        <img src="../../public/images/icons/close32px.png"
          style="margin-left: auto; margin-top: 10px; margin-right: 10px;" @click="close">
      </div>
      <h2 style="color:black">上傳頭像</h2>
      <div>
        <input type="file" accept="image/*" style="margin-left: 150px;" @change="uploadImage">
      </div>
      <div v-if="imageUrl" style="width:100%;">
        <img :src="imageUrl" alt="Uploaded Image" style="width: 279px;height:279px;">
      </div>
      <div>
        <button v-if="imageUrl" @click="ok">確定</button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import axios from 'axios';
import { VueCookieNext as $cookie } from 'vue-cookie-next'
const runtimeConfig = useRuntimeConfig()
const { githubtoken: token } = runtimeConfig.public

const router = useRoute();
const memberId = router.params.memberId;

const emits = defineEmits(['closeModal']); // 定义 close modal 事件

let imageUrl = ref(''); // 存储上传图片的URL

const close = async () => {
  emits('closeModal'); // 触发 close modal 事件
};

const ok = () => {
  $cookie.setCookie("avatarUrl", imageUrl.value);
  console.log(imageUrl.value)

  axios.put(`https://localhost:7048/api/Members/ava${memberId}?ava=${imageUrl.value}`, imageUrl.value)
    .then(response => {
      console.log(`https://localhost:7048/api/Members/ava${memberId}?ava=${imageUrl.value}`);
      console.log(response.data);
    })
    .catch(error => {
      console.log(error);
    });


  emits('closeModal');
  location.reload();
};

const uploadImage = async (event) => {
  const file = event.target.files[0];

  try {
    const accessToken = token;
    const username = 'xftfchgg';
    const repository = 'picture';
    const timestamp = Date.now(); // 获取当前时间戳
    const randomString = Math.random().toString(36).substring(2, 15); // 生成随机字符串
    const filePath = `images/uploaded_image_${timestamp}_${randomString}.png`; // 构建文件路径

    const apiUrl = `https://api.github.com/repos/${username}/${repository}/contents/${filePath}`;

    const headers = {
      'Authorization': `token ${accessToken}`,
      'Accept': 'application/vnd.github.v3+json',
      'Content-Type': 'application/json' // 设置请求的 Content-Type 为 JSON
    };

    const fileData = await readFileAsBase64(file); // 将文件数据编码为 Base64 字符串

    const requestBody = {
      message: 'Upload image',
      content: fileData, // 将文件数据放入 content 字段中
      committer: {
        name: 'Your Name',
        email: 'your.email@example.com'
      }
    };

    const response = await axios.put(apiUrl, requestBody, { headers }); // 发送请求

    imageUrl.value = response.data.content.download_url;
  } catch (error) {
    console.error('Error uploading image:', error);
  }
};

// 读取文件数据为 ArrayBuffer
const readFileAsBase64 = (file) => {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.onload = () => resolve(reader.result.split(',')[1]); // 只返回 Base64 部分
    reader.onerror = (error) => reject(error);
    reader.readAsDataURL(file); // 读取文件为 Data URL，其中包含 Base64 编码的文件内容
  });
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
  text-align: center;
  border-radius: 20px;
  background-color: white;
  display: flex;
  height: 500px;
  width: 500px;

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