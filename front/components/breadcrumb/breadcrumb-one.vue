<template>
  <section class="breadcrumb-area" :style="`background-image:url(${bg})`">
    <div class="container">
      <div class="breadcrumb__wrapper">
        <div class="row">
          <div class="col-xl-6 col-lg-7">
            <div class="breadcrumb__content">
              <div style="display: flex; align-items: center;">
                <h2 class="title" v-if="notEdit && !isEdited">{{ title }}</h2>
                <h2 class="title" v-if="notEdit && isEdited">{{ editTitle }}</h2>
                <!-- title -->
                <img v-if="notEdit" src="/images/icons/Edit_icon.png" @click="edit()">

                <div v-if="isEdit" style="display: flex;">
                  <div v-if="isEdit">
                    <input type="text" placeholder="暱稱" v-model="postData.name" v-show="isEdit" class="textinput"
                      style="display: inline-block;" autocomplete="off" @blur="focusoutname(); validatebutton()" />
                    <p v-if="isNicknameOK" class="dangertext">暱稱已存在</p>
                    <p v-if="isNicknameEmpty" class="dangertext">暱稱不得為空</p>
                  </div>
                  <div>
                    <button class="button" style="margin: 5px; background-color: gray;" @click="cancel">取消</button>
                    <button :disabled="isButtonDisabled" class="button" style="margin: 5px;" @click="submit">完成</button>
                  </div>
                </div>
              </div>

              <nav aria-label="breadcrumb">
                <ol class="breadcrumb" style="list-style-type: none;">
                  <!-- <li class="breadcrumb-item">
                    <nuxt-link to="/">Home</nuxt-link>
                  </li> -->
                  <!--  -->
                  <li class="breadcrumb-item active" aria-current="page">
                    {{ subtitle }}
                  </li>
                </ol>
              </nav>
            </div>

          </div>
          <div class="col-xl-6 col-lg-5 position-relative d-none d-lg-block">
            <!-- breadcrumb__img -->
            <div class="breadcrumb__myimg;" style="background-color: red; width:100%">
              <div class="my-outer-container;">

                <img :src="brd_img" alt="img" class="my-image" style="width: 279px;height:279px"
                  @click="handleOpenModal()" />
                <!-- <img :src="Frame_img" alt="img" class="my-Frameimage" style="width: 300px;height:300px" @click="handleOpenModal()"/> -->

              </div>
            </div>

            <!-- <div class="my-outer-container">
                　<img
                  src="/public/images/Bonus/2/CatImage.gif"
                  alt="Image 1"
                  class="my-image"
                />
                　<img
                  src="/public/images/Bonus/4/ApexFrame.png"
                  alt="Image 2"
                  class="my-Frameimage"
                />
              </div> -->

          </div>
        </div>

      </div>
    </div>
  </section>
  <MemberMemberprofilemodal v-if="isActive" @closeModal="handleCloseModal" />
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router';
import { defineEmits } from 'vue';
import axios from 'axios';
import { VueCookieNext as $cookie } from 'vue-cookie-next';
import { useNameStore } from '../../store/nameStore'


const isActive = ref<boolean>(false);

const props = defineProps<{
  bg?: string;
  brd_img?: string;
  Frame_img?: string;
  title: string;
  subtitle: string;
}>()

const nameStore = useNameStore()

const setName = () => {
  nameStore.setName(postData.value.name)
}

const handleCloseModal = () => {
  isActive.value = !isActive.value;
};
const handleOpenModal = () => {
  isActive.value = !isActive.value;
};


// withDefaults(
//    defineProps<{
//     bg?: string;
//     brd_img?: string;
//     Frame_img?: string;
//     title: string;
//     subtitle: string;
//   }>(),
//   {
//     bg: "/images/bg/breadcrumb_bg01.jpg",
//     brd_img: " /images/Bonus/2/CatImage.gif",
//     Frame_img:"/images/Bonus/4/ApexFrame.png"
//   }
// );

const router = useRoute();
const id = router.params.memberId;
let editTitle = ref("");

let notEdit = ref(true);
let isEdit = ref(false);
let isEdited = ref(false);

let state;
const isNicknameOK = ref<boolean>(false);
const isNicknameEmpty = ref<boolean>(false);
const isButtonDisabled = ref<boolean>(true);

const postData = ref({
  id: "",
  name: '',
});

const emits = defineEmits(['changeName']);

onBeforeMount(() => {

});

//修改
const edit = () => {

  notEdit.value = !notEdit.value;
  isEdit.value = !isEdit.value;

};

//取消
const cancel = () => {
  notEdit.value = !notEdit.value;
  isEdit.value = !isEdit.value;
  postData.value.name = "";

  isButtonDisabled.value = true;
}


//送出
const submit = () => {
  notEdit.value = !notEdit.value;
  isEdit.value = !isEdit.value;
  postData.value.id = router.params.memberId;
  console.log(postData.value.id)
  isEdited.value = true;
  editTitle.value = postData.value.name
  axios.put(`https://localhost:7048/api/Members/${id}?nickName=${postData.value.name}`, postData.value.name)
    .then(response => {
      console.log(response.data);
    })
    .catch(error => {
      console.log(error);
    });
  $cookie.setCookie('name', postData.value.name);
  isButtonDisabled.value = true;
  setName();
  location.reload();
}


//暱稱驗證
const focusoutname = () => {

  isNicknameEmpty.value = false;
  isNicknameOK.value = false;

  if (postData.value.name == "") {
    isNicknameEmpty.value = !isNicknameEmpty.value;

  }
  else {
    axios.post(`https://localhost:7048/api/Members/TestMemberName?name=${postData.value.name}`, postData.value.name)
      .then(response => {
        state = response.data
        if (state == false) {
          isNicknameOK.value = !isNicknameOK.value;

        }
        if (isNicknameOK.value == false &&
          isNicknameEmpty.value == false) {
          isButtonDisabled.value = false
        }
        else {
          isButtonDisabled.value = true
        }
      })
      .catch(error => {
        console.log(error);
      });
  }
}

//驗證按鈕
const validatebutton = () => {
  if (isNicknameOK.value == false &&
    isNicknameEmpty.value == false) {
    isButtonDisabled.value = false
  }
  else {
    isButtonDisabled.value = true
  }
}



</script>


<style lang="scss">
@import "@/assets/css/my-style.css";

.my-outer-container {
  position: relative;
  width: 50px;
  height: 50px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.my-image {
  position: absolute;
  top: 50%;
  /* 依附於父物件的垂直中心點 */
  left: 50%;
  /* 依附於父物件的水平中心點 */
  transform: translate(-50%, -50%);
  /* 把圖從第1象限調整至第3象限調整中心點(應該?) */
  width: 93%;
  height: auto;
}

.my-Frameimage {
  position: absolute;
  top: 50%;
  /* 依附於父物件的垂直中心點 */
  left: 50%;
  /* 依附於父物件的水平中心點 */
  transform: translate(-50%, -50%);
  /* 把圖從第1象限調整至第3象限調整中心點(應該?) */
  width: 100%;
  height: auto;
}
</style>

<style>
.textinput {
  background: #54575a;
  border: 1px solid #26292c;
  border-radius: 5px 5px 5px 5px;
  color: white;
  overflow: hidden;
  /* 隱藏超出範圍的內容 */
}

.dangertext {
  color: red;
  text-align: center;
  /* font-size: 14px; */
  /* margin-right: 10px; */
  margin: 0px;
}

.button {
  background-color: #0EFC8C;
  color: #324052;
}

.button:disabled {
  background-color: #54575a;
  color: #4AC877;
}
</style>
