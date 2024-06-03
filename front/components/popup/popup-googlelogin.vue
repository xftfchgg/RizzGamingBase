<template>
    <div :class="`Login__popup-wrap ${isActive?'Login__active':''}`">
       <div class="Login__layer"></div>
       <div class="Login__close"  @click="closeLogin">
         <span>
           <i class="flaticon-swords-in-cross-arrangement"></i>
         </span>
       </div>
       <div class="Login__wrap text-center">
         <div class="container">
           <div class="row">
             <div class="col-12">
               <h2 class="title">
                 ... <span>Search Here</span> ...
               </h2>
               <div class="Login__form">
                 <form action="#">
                   <input
                     type="text"
                     name="Login"
                     placeholder="Type keywords here"
                     required
                   />
                   <button class="Login-btn">
                     <i class="flaticon-loupe"></i>
                   </button>
                 </form>
               </div>
             </div>
           </div>
         </div>
       </div>
     </div>
 </template>
 
 <script setup lang="ts">
 defineProps<{isActive:boolean}>();
 const emit = defineEmits();
 
 const closeLogin = () => {
   emit('closeLogin', '/audio/remove.wav');
 };
 
 </script>
 