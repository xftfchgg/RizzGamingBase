<template>
    <div>
        <iframe title="RGB智能客服" width="100%" style="height: 100%; min-height: 700px" frameborder="0"></iframe>
    </div>
</template>

<script>
export default {
    mounted() {
        // 設置 embeddedChatbotConfig 對象
        window.embeddedChatbotConfig = {
            chatbotId: "19Yqv8__w7PUZrzWCRszl",
            domain: "www.chatbase.co"
        };

        // 動態加載 embed.min.js 腳本
        const script = document.createElement('script');
        script.src = 'https://www.chatbase.co/embed.min.js';
        script.setAttribute('chatbotId', '19Yqv8__w7PUZrzWCRszl');
        script.setAttribute('domain', 'www.chatbase.co');
        script.defer = true;
        document.body.appendChild(script);

    }
};
</script>

<style>
/* 可能需要一些樣式來美化您的智能 AI 客服 */
</style>