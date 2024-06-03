<template>
  <div>
    <!-- <button class="checkout" @click="">test1</button>
    <button class="checkout" @click="">test2</button> -->
    <!-- <button class="checkout" @click="getLinePayData">getLinePayData</button> -->
    <button class="checkout" @click="postLinePay">LinePay結帳</button>
    <button class="checkout" @click="postEcPay">EcPay結帳</button>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import axios from 'axios';
import { VueCookieNext as $cookie } from 'vue-cookie-next';
let orderId = ref('');
let amount = 0;
let cartGames = ref([]);



async function getLinePayData() {
  let id = $cookie.getCookie('Id')

  let cartItems = ref(null);

  let cartData = {};
  const response = await axios.get(`https://localhost:7048/api/CartItems/${id}`)
  // const response2 = await axios.get('https://localhost:7048/api/Order/GetLastOrderId')
  cartItems.value = response.data;


  cartData.amount = 0;
  cartData.currency = 'TWD';
  // orderId = parseInt(response2.data) + 1;
  orderId = cartItems.value[0].id.toString();
  cartData.orderId = orderId;
  cartData.packages = [];

  let packageData = {};

  packageData.id = '1';
  packageData.amount = 0;
  packageData.name = "test";
  packageData.products = [];

  for (let item of cartItems.value) {
    // 发送另一个axios请求
    let gameResponse = await axios.get(`https://localhost:7048/api/Games/${item.gameId}`);
    cartGames.value.push(gameResponse.data);
    let game = gameResponse.data;

    let cartGame = {};
    cartGame.name = game.name;
    cartGame.quantity = 1;

    if (game.discountPrice != 0) {
      amount += game.discountPrice;
      cartData.amount += game.discountPrice;
      packageData.amount += game.discountPrice;
      cartGame.price = game.discountPrice;
    } else {
      amount += game.price;
      cartData.amount += game.price;
      packageData.amount += game.price;
      cartGame.price = game.price;
    }

    packageData.products.push(cartGame)
  }

  cartData.redirectUrls = {
    "confirmUrl": `http://localhost:3000/checkout/cart`,//付款完成之後就會跳轉至confirmUrl
    "cancelUrl": "https://localhost:7048/api/LinePay/Cancel"
  }
  // axios.post('https://localhost:7048/api/Order',)

  cartData.packages.push(packageData)

  console.log(cartData)
  //  console.log(JSON.stringify(cartData))

  return cartData
}

const postLinePay = async () => {
  try {
    // 獲取支付數據
    const data = await getLinePayData();

    // 請求 LinePay 創建支付
    const res = await axios.post('https://localhost:7048/api/LinePay/Create', data);

    // 檢查是否支付成功並獲取相關信息
    if (res.data.info && res.data.info.paymentUrl && res.data.info.paymentUrl.web) {
      // 獲取支付成功後的訂單信息，例如 orderId
      const orderId = res.data.orderId;

      // 將購物車項目添加到訂單中
      await addCartItemsToOrder(orderId);

      // 清除購物車項目
      await clearCartItems();



      // 將瀏覽器重定向到支付頁面
      const paymentUrl = res.data.info.paymentUrl.web;
      window.location.href = paymentUrl;
    } else {
      console.error("無法找到 paymentUrl 屬性或 paymentUrl.web 屬性。");
    }
  } catch (error) {
    console.error('調用 LINE PAY API 時出錯：', error);
  }
};
// 顯示支付成功提示
// alert(`結帳成功！訂單編號：${orderId}`);
// 添加購物車項目到訂單中
const addCartItemsToOrder = async (orderId) => {
  try {
    // 獲取購物車項目
    const id = $cookie.getCookie('Id');
    const response = await axios.get(`https://localhost:7048/api/CartItems/${id}`);
    const cartItems = response.data;

    // 將每個購物車項目添加到訂單中
    for (let item of cartItems) {
      const postData = {
        gameId: item.gameId,
        memberId: item.memberId,
        orderId: orderId // 使用支付成功後獲取的 orderId
      };
      await axios.post('https://localhost:7048/api/Order', postData);
    }
  } catch (error) {
    console.error('將購物車項目添加到訂單時出錯：', error);
  }
};

// 清除購物車項目
const clearCartItems = async () => {
  try {
    const id = $cookie.getCookie('Id');
    await axios.delete(`https://localhost:7048/api/CartItems/delete/${id}`);
  } catch (error) {
    console.error('清除購物車項目時出錯：', error);
  }
};


const postEcPay = async () => {
  try {
    const ecPayData = {
      "MerchantID": "3002607",
      "MerchantTradeNo": "a24e870b0f9f4c3c8c5b",
      "MerchantTradeDate": "2024/04/01 12:42:11",
      "PaymentType": "aio",
      "TotalAmount": "1",
      "TradeDesc": "無",
      "ItemName": "1",
      "ExpireDate": "3",
      "ReturnURL": "http://localhost:8889/api/ECPayPayments/PayInfo",
      "OrderResultURL": "http://localhost:8889/api/ECPayPayments/GetPayInfo",
      "ChoosePayment": "ALL",
      "PaymentInfoURL": "http://localhost:8889/api/ECPayPayments/AddAccountInfo",
      "EncryptType": "1",
      "ClientRedirectURL": "http://localhost:8888/Pay/milePurchaseTest",
      "CheckMacValue": "54FD269B4C4F7C335054DD79B409A7C43B18830AF95D417003BB33928AB2000F"
    };

    const res = await axios.get('https://localhost:7048/api/ECPayPayments/Index?TotalAmount=1&ItemName=1');

    const cartItem = res.data;
    console.log(res.data)
  } catch (error) {
    console.error('調用 ECPAY API 時出錯：', error);
  }
};
</script>

<style scoped>
.checkout {
  padding: 10px 20px;
  background-color: green;
  color: #fff;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}
</style>