<template>
  <ClientOnly>
  <div>
    <breadcrumb-three title="紅利商品收藏庫" subtitle="COLLEECT LIST"> </breadcrumb-three>

    <bonusUserCollect-area v-if="dbData_bonusProductsByMemberId && dbData_bonusProductTypes"
    :bonusProductsByMemberIdInArea="dbData_bonusProductsByMemberId"
    :bonusProductTypesInArea="dbData_bonusProductTypes" 
    :errormessageInArea="errormessage"
    @data-from-bonus="handleDataFromBonus"
    @itemUsing = "itemUsingEventFormArea"
    ></bonusUserCollect-area>
    <!-- @itmeUsing="itemUsingEvent" -->
  </div>
  </ClientOnly>
</template>

<script setup>
useSeoMeta({ title: "BONUS - MYKD" });
import { ref ,onMounted } from "vue";
//cookie
import { VueCookieNext as $cookie } from 'vue-cookie-next'
// 透過axios GET & POST請求
import axios from "axios";

const dbData_bonusProductsByMemberId = ref(null);
const dbData_bonusProductTypes = ref(null);
const dbData_mamberProductItemStatus = ref(null);

let errormessage = ``;

async function itemUsingEventFormArea(id,typeId,using)
{
  let memberId = $cookie.getCookie("Id");

  if(memberId != undefined && id != undefined && typeId !=undefined &&  using != undefined)
  { 
    // Update MemberBonusItem Using - 切換使用狀態 const responseMemberProductItem 
    await axios.post(`https://localhost:7048/api/BonusProducts/update?memberId=${memberId}&bonusId=${id}&typeid=${typeId}&usingStatus=${using}`)
    .then(response => {
      dbData_mamberProductItemStatus.value = response.data;
      fetchMemberProduct(memberId)
    })
    .catch(error=>{
      // ConsoleLogger("ItemStatue")
    })
    
    // Get All BonusProduct By MenberId
    // const responseByMenberId = await axios.get(`https://localhost:7048/api/BonusProducts/MemberId/${memberId}`);
    // dbData_bonusProductsByMemberId.value = responseByMenberId.data;
    // console.log(dbData_bonusProductsByMemberId.value)
  }
}

async function fetchMemberProduct(memberId){
  await axios.get(`https://localhost:7048/api/BonusProducts/MemberId/${memberId}`)
        .then(response => {
          dbData_bonusProductsByMemberId.value = response.data;
          console.log(dbData_bonusProductsByMemberId.value)
      })
    .catch(error=>{
        console.log("BymemberId error")
    })
}

onMounted(async () => 
{

  let memberId = $cookie.getCookie("Id");
  let cookiedetails = $cookie
  try 
  {
    // Get All BonusProduct By MenberId
    const responseByMenberId = await axios.get(`https://localhost:7048/api/BonusProducts/MemberId/${memberId}`);
    dbData_bonusProductsByMemberId.value = responseByMenberId.data;

    // console.log(dbData_bonusProductsByMemberId.value[0].id)

    //Get All BonusType
    const responseAllTypes = await axios.get("https://localhost:7048/api/BonusProducts/Type");
    dbData_bonusProductTypes.value = responseAllTypes.data;
    
    // // Get BonusProduct By TypeId
    // const responseTypes = await axios.get(`https://localhost:7048/api/BonusProducts/Type/${producttypeid}`);
    // bonusProducts.value = responseTypes.data;
  }
  catch (error) 
  {
    console.error("未正確找到紅利商品", error);
  }
});
async function handleDataFromBonus(keyword)
{
  let memberId = $cookie.getCookie("Id");

  if(keyword === '')
  {
    //Get All BonusProduct By MenberId
    const responseByMenberId = await axios.get(`https://localhost:7048/api/BonusProducts/MemberId/${memberId}`);
    dbData_bonusProductsByMemberId.value = responseByMenberId.data;
    console.error("未正確找到紅利商品");
  }
  else
  {
    const responseSearchName = await axios.get(`https://localhost:7048/api/BonusProducts/Name/${keyword}/MemberId/${memberId}`);
    dbData_bonusProductsByMemberId.value = responseSearchName.data;
    
    if(dbData_bonusProductsByMemberId.value == "")
    {
      errormessage = `噢不！好像沒有找到\"${keyword}\"相關的商品`;
    }
  }
}
</script>
