<template>
    <section class="blog-area blog-details-area">
        <div class="container">
            <div class="row justify-content-center">
                <div class="blog-post-wrapper">
                    <div class="blog-post-item">
                        <div class="blog-post-thumb">
                            <img :src="blog.img" alt="img" />
                        </div>
                        <div class="blog-post-content blog-details-content">
                            <div class="blog-post-meta">
                                <ul class="list-wrap">
                                    <li>By<nuxt-link to="#">{{ blog.author }}</nuxt-link></li>
                                    <li><i class="far fa-calendar-alt"></i> {{ blog.date }}</li>
                                    <li><i class="far fa-comments"></i>
                                        <nuxt-link to="#">{{ blog.comments === 0 ? 'No comments' : `${blog.comments}
                                            comments`}}</nuxt-link>
                                    </li>
                                </ul>
                            </div>
                            <h2 class="title text-capitalize">{{blog.title}}</h2>
                            <p>Excepteur sint occaecat atat non proident, sunt in culpa qui officia deserunt mollit anim
                                id est labor umLor em ipsum dolor amet, consteur adiscing Duis elentum solliciin is
                                yaugue euismods Nulla ullaorper. Ipsum is simply dummy text of printing and typeetting
                                industry. Lorem Ipsum has been the industries standsaard sipiscing Duis elementum
                                solliciin. Duis aute irure dolor in repderit in voluptate velit esse cillum dolorliquip
                                ex ea commodo repderit in voluptate consequat nulla ullaorper.</p>
                            <blockquote>
                                <p>Duis elentum solliciin is yaugue euismods Nulla ullaorper. Ipsum is simply dummy text
                                    of printing and typeetting industry.</p>
                            </blockquote>
                            <p>Axcepteur sint occaecat atat non proident, sunt in culpa qui officia deserunt mollit anim
                                id est labor umLor em ipsum dolor amet, consteur adiscing Duis elentum solliciin is
                                yaugue euismods Nulla ullaorper. Ipsum is simply dummy text of printing and typeetting
                                industry. Lorem Ipsum has been the industries standsaard sipiscing Duis elementum
                                solliciin. Duis aute irure dolor in repderit.</p>
                            <p>Simply dummy text of printing and typeetting industry. Lorem Ipsum has been the
                                industries standsaard sipiscing Duis elementum solliciin.Duis aute irure dolor in
                                repderit.</p>
                            <div class="blog-details-inner">
                                <h4 class="inner-title">nft games android no investment</h4>
                                <p>Axcepteur sint occaecat atat non proident, sunt in culpa qui officia deserunt mollit
                                    anim id est labor umLor em ipsum dolor amet, consteur adiscing Duis elentum
                                    solliciin is yaugue euismods Nulla ullaorper. Ipsum is simply dummy text of printing
                                    and typeetting industry. Lorem Ipsum has been the industries standsaard sipiscing
                                    Duis elementum solliciin.Duis aute irure dolor in repderit.</p>
                            </div>
                            <div class="blog-details-inner-img">
                                <img src="/images/blog/blog_post02.jpg" alt="img" />
                            </div>
                            <p>Axcepteur sint occaecat atat non proident, sunt in culpa qui officia deserunt mollit anim
                                id est labor umLor em ipsum dolor amet, consteur adiscing Duis elentum solliciin is
                                yaugue euismods Nulla ullaorper. Ipsum is simply dummy text of printing and typeetting
                                industry.</p>
                            <div class="blog-details-bottom">
                                <div class="row">
                                    <div class="col-xl-6 col-md-7">
                                        <div class="tg-post-tags">
                                            <h5 class="tags-title">tags :</h5>
                                            <ul class="list-wrap d-flex flex-wrap align-items-center m-0">
                                                <li><nuxt-link to="#">Esports</nuxt-link>,</li>
                                                <li><nuxt-link to="#">Fantasy</nuxt-link>,</li>
                                                <li><nuxt-link to="#">game</nuxt-link></li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="col-xl-6 col-md-5">
                                        <div class="blog-post-share justify-content-start justify-content-md-end">
                                            <h5 class="share">share :</h5>
                                            <ul class="list-wrap">
                                                <li>
                                                    <nuxt-link to="#"><i class="fab fa-facebook-f"></i></nuxt-link>
                                                </li>
                                                <li>
                                                    <nuxt-link to="#"><i class="fab fa-twitter"></i></nuxt-link>
                                                </li>
                                                <li>
                                                    <nuxt-link to="#"><i class="fab fa-linkedin-in"></i></nuxt-link>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="blog__avatar-wrap mb-65">
                        <div class="blog__avatar-img">
                            <nuxt-link to="#">
                                <img src="/images/blog/avatar.jpg" alt="img" />
                            </nuxt-link>
                        </div>
                        <div class="blog__avatar-info">
                            <span class="designation">Written by</span>
                            <h4 class="name"><nuxt-link to="#">Kaceytron G.</nuxt-link></h4>
                            <p>Axcepteur sint occaecat atat non proident, sunt culpa officia deserunt mollit anim id est
                                labor umLor emdolor.</p>
                        </div>
                    </div>
                    <div class="comments-wrap">
                        <h4 class="comments-wrap-title">3 Comments</h4>
                        <!-- blog comments start -->
                        <blog-details-comments />
                        <!-- blog comments end -->
                    </div>
                    <div class="comment-respond">
                        <h3 class="comment-reply-title">Leave a Reply</h3>
                        <!-- form start -->
                        <form-blog-details-reply />
                        <!-- form end -->
                    </div>
                </div>
                <div class="blog-post-sidebar">
                    <!-- blog sidebar start -->
                    <blog-sidebar />
                    <!-- blog sidebar end -->
                </div>
            </div>
        </div>
    </section>
</template>

<script setup lang="ts">
import type { IBlog } from "@/types/blog-type";

defineProps<{ blog: IBlog }>()
</script>
