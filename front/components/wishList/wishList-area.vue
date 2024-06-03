<template>
    <section class="blog-area">
        <div class="container">
            <div class="row justify-content-center">
                <div class="blog-post-wrapper">
                    <wishList-list :gameData="games" @refreshWishList="fetchWishList"/>
                    
                </div>
            </div>
        </div>
    </section>
</template>

<script setup>
    import axios from "axios";
    import { VueCookieNext as $cookie } from 'vue-cookie-next'
    let games = ref(null)
    let id = ref('');
    (async () => {
        id = $cookie.getCookie("Id");
        fetchWishList();
    })();
    
    async function fetchWishList(){
        const response = await axios.get(`https://localhost:7048/api/Games/GetWishList?memberId=${id}`)
        games.value = response.data;
    }
</script>