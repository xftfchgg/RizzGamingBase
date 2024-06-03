<template>
    <section class="blog-area">
        <div class="container">
            <div class="row justify-content-center">



                <div class="blog-post-wrapper">
                    <game-list v-if="injectGames!= null" :gameData="injectGames" :tagName="tagName" :allGame="games" :popular="popular" :discount="discount"/>
                    <div class="pagination__wrap">
                        <!-- pagination start -->
                        <game-pagination :gamePage="gamePage"></game-pagination>
                        <!-- pagination end -->
                    </div>
                </div>
            </div>
        </div>
    </section>
</template>

<script setup>
    import { useRoute, useRouter } from 'vue-router';
    import axios from "axios";

    const router = useRouter();
    const route = useRoute();

    let games = ref(null);
    let chuckGames = ref([]);
    let injectGames = ref(null);
    let tagName = ref(null);
    let gamePage = ref(null);
    let page = ref(0);
    let currentQuery = ref(null);
    let popular = ref(null);
    let discount = ref(null);

    currentQuery.value = route.query.search;

    router.beforeEach((to, from) => {
        currentQuery.value = to.query.search;
        if(to.query.search !== from.query.search){
            fetchGame()          
        }
        page.value = (to.query.page) - 1
        injectGames.value = chuckGames.value[page.value]
    })

    onBeforeMount(async () => {
        await fetchGame()
    });

    async function fetchGame(){
        tagName.value = null;
        games.value = null;
        popular.value = null;
        discount.value = null;
        chuckGames.value = [];
        injectGames.value = null;

        if (currentQuery.value != null) {
            const queryString = currentQuery.value.split('_');

            if (queryString[0] == "tag") {
                const tags = [Number(queryString[1])];
                const response = await axios.post("https://localhost:7048/api/Games/FilterByTags", tags);
                games.value = response.data;

                countPages(games.value.length, 4)
                const response1 = await axios.get(`https://localhost:7048/api/Games/tag/${tags}`);
                tagName.value = response1.data
            }

            if (queryString[0] == "popular") {          
                const response = await axios.post("https://localhost:7048/api/Games/popular?begin=1&end=100");
                games.value = response.data;
                countPages(games.value.length, 4)
                popular.value = 1;
            }

            if (queryString[0] == "discount") {      
                console.log(123)      
                const response = await axios.get("https://localhost:7048/api/Games/alldiscount");
                games.value = response.data;
                countPages(games.value.length, 4)
                discount.value = 1;
            }

        } else {
            const response = await axios.get("https://localhost:7048/api/Games");
            games.value = response.data;
            countPages(games.value.length, 4)
        }
    }

    async function chuckGame() {
        page.value = 0;
        const chunkSize = 4;
        for (let i = 0; i < games.value.length; i += chunkSize) {
            chuckGames.value.push(games.value.slice(i, i + chunkSize));
        }
        // chuckGames.value.reverse();
        injectGames.value = chuckGames.value[page.value]
       
    }

    async function countPages(allCounts, pageCounts) {
        if (allCounts % pageCounts == 0) {
            gamePage.value = allCounts / pageCounts;
        } else {
            gamePage.value = parseInt(allCounts / pageCounts) + 1;
        }
        chuckGame()
    }
</script>