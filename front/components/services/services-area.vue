<template>
  <section class="services-area services__bg-color section-pt-120 section-pb-120">
    <div class="container">
      <div class="row align-items-end align-items-xl-start">
        <div class="col-lg-6">

          <div class="services__wrapper">
            <div v-for="(item, i) in service_items" :key="i" class="services__item" @mouseenter="handleMouseOver(i)"
              @mouseout="handleMouseOver(i)">
              <div class="services__icon">
                <i :class="item.icon"></i>
              </div>
              <div class="services__content">
                <h4 class="title">
                  <nuxt-link to="/">{{ item.title }}</nuxt-link>
                </h4>
                <span class="text-clgreen">{{ item.desc1 }}</span>
                <br>
                <span class="text-clgreen">{{ item.desc2 }}</span>
                <br>
                <span class="text-clgreen">{{ item.desc3 }}</span>
                <br>
                <span class="text-clgreen">{{ item.desc4 }}</span>
              </div>
            </div>
          </div>
        </div>
        <div class="col-lg-6">
          <div class="services__images">
            <div v-for="(s, i) in service_images" :key="i"
              :class="`services__images-item ${activeIndex === i ? 'active' : ''}`">
              <img :src="s" alt="img" />
              <nuxt-link to="/service-details" class="services__link">
                <i class="flaticon-next"></i>
              </nuxt-link>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
let activeIndex = ref<number | null>(0);
const handleMouseOver = (index: number) => {
  activeIndex.value = index;
};

const handleMouseOut = (index: number) => {
  activeIndex.value = index;
};
// service images
const service_images = [
  '/images/個人照/陳冠廷.jpg',
  '/images/個人照/陳冠廷.jpg',
  '/images/個人照/陳冠廷.jpg',
  '/images/個人照/陳冠廷.jpg',
  '/images/個人照/陳冠廷.jpg',
  '/images/個人照/徐宏勝.jpg',
];
const service_items: {
  id: number;
  icon: string;
  title: string;
  desc1: string;
  desc2: string;
  desc3: string;
  desc4: string;
}[] = [
    {
      id: 1,
      icon: 'flaticon-diamond',
      title: '組員介紹',
      desc1: '',
      desc2: '',
      desc3: '',
      desc4: '',
    },
    {
      id: 2,
      icon: 'flaticon-diamond',
      title: '姚欽堂',
      desc1: '紅利商店-紅利商品購買',
      desc2: '紅利收藏庫-紅利商品的呈現',
      desc3: '動態更換投像、頭像框',
      desc4: '紅利清單動態生成&篩選',
    },
    {
      id: 3,
      icon: 'flaticon-user-profile',
      title: '張淳毓',
      desc1: '登入系統:驗證密碼回傳jwt',
      desc2: 'Email驗證:註冊、免密碼登入',
      desc3: '第三方登入:Google帳號登入',
      desc4: '',
    },
    {
      id: 4,
      icon: 'flaticon-ethereum',
      title: '徐宏勝',
      desc1: '遊戲頁面 - 遊戲相關資訊呈現',
      desc2: '遊戲評論 - 遊戲依附評論的CRUD ',
      desc3: '遊戲篩選 - 清單的各種篩選條件',
      desc4: '遊戲相關資訊整合',
    },
    {
      id: 5,
      icon: 'flaticon-settings-1',
      title: '陳冠廷',
      desc1: '好友聊天室 - SignalR端對端連線實現',
      desc2: '推播通知 - 不漏重要消息',
      desc3: '首頁建置 - 前端實作，介面排版',
      desc4: '智能客服 - 串接API，跟上AI主流',
    },
    {
      id: 6,
      icon: 'flaticon-settings-1',
      title: '彭湫雯',
      desc1: '金流資料庫:SQL資料庫設計並在後端加密',
      desc2: '購物車 - 購物車API撰寫',
      desc3: '結帳系統 - 串接LinePay API',
      desc4: '訂單系統-訂單查詢',
    }

  ];
</script>

<style scoped>
.text-clgreen {
  color: greenyellow;
}
</style>