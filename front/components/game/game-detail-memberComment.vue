<template>
    <div class="latest-comments">
        <ul class="list-wrap">
            <li v-for="comment in comments" :key="comment.id">
                <div class="comments-box">
                    <div class="comments-avatar">
                        <img :src="comment.avatarUrl" alt="img">

                        <div class="container-wrapper">
                            <div class="container d-flex align-items-center justify-content-center">
                                <div class="row justify-content-center">
                                    <div class="rating-wrapper">

                                        <input type="radio" id="5-star-rating" name="star-rating" value="5"
                                            @click="getRating">
                                        <label for="5-star-rating" class="star-rating"
                                            :style="highlight(comment.rating, 5)">
                                            <i class="fas fa-star d-inline-block"></i>
                                        </label>

                                        <input type="radio" id="4-star-rating" name="star-rating" value="4"
                                            @click="getRating">
                                        <label for="4-star-rating" class="star-rating star"
                                            :style="highlight(comment.rating, 4)">
                                            <i class="fas fa-star d-inline-block"></i>
                                        </label>

                                        <input type="radio" id="3-star-rating" name="star-rating" value="3"
                                            @click="getRating">
                                        <label for="3-star-rating" class="star-rating star"
                                            :style="highlight(comment.rating, 3)">
                                            <i class="fas fa-star d-inline-block"></i>
                                        </label>

                                        <input type="radio" id="2-star-rating" name="star-rating" value="2"
                                            @click="getRating">
                                        <label for="2-star-rating" class="star-rating star"
                                            :style="highlight(comment.rating, 2)">
                                            <i class="fas fa-star d-inline-block"></i>
                                        </label>

                                        <input type="radio" id="1-star-rating" name="star-rating" value="1"
                                            @click="getRating">
                                        <label for="1-star-rating" class="star-rating star"
                                            :style="highlight(comment.rating, 1)">
                                            <i class="fas fa-star d-inline-block"></i>
                                        </label>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="comments-text">
                        <div class="avatar-name">
                            <h6 class="name">{{ comment.name }} <a href="" class="comment-reply-link"
                                    @click.prevent="toggleVisibility(comment.id)"><i class="fas fa-reply"></i> 查看回復</a>
                            </h6>
                            <span class="date">{{ formatDate(comment.date) }}</span>
                        </div>
                        <p>
                            {{ comment.comment1 }}
                        </p>
                    </div>
                </div>
                <ul v-show="showComments[comment.id]" v-if="comment.attachedComment.length > 0" class="children">
                    <li v-for="attachedComment in comment.attachedComment">
                        <div class="comments-box">
                            <div class="comments-avatar">
                                <img :src="attachedComment.avatarUrl" alt="img">
                            </div>
                            <div class="comments-text">
                                <div class="avatar-name">
                                    <h6 class="name">{{ attachedComment.name }} </h6>
                                    <span class="date">{{ formatDate(attachedComment.dateTime) }}</span>
                                </div>
                                <p>{{ attachedComment.comment }}</p>
                            </div>
                        </div>
                    </li>
                </ul>
                <ul class="children" v-show="showComments[comment.id]">
                    <li>
                        <div class="comments-box">
                            <div class="comments-text">
                                <div class="avatar-name">
                                    <h6 class="name">reply the comment </h6>
                                </div>
                                <form class="comment-form" @submit.prevent="submitAttachedComment(comment.id)"
                                    :validation-schema="schema">
                                    <div class="form-grp">
                                        <err-message :msg="errors.comment" />
                                        <Field name="comment" v-slot="{ field }">
                                            <textarea v-bind="field" name="comment"
                                                placeholder="Reply The Comment *"></textarea>
                                        </Field>
                                    </div>
                                    <button type="submit">Post Reply</button>
                                </form>
                            </div>
                        </div>
                    </li>
                </ul>
            </li>
        </ul>
    </div>
</template>

<script setup>
import { VueCookieNext as $cookie } from 'vue-cookie-next'
import { useForm, Field } from 'vee-validate';
import * as yup from 'yup';
import axios from 'axios';

const props = defineProps({
    gameId: Number,
});

let id = $cookie.getCookie("accountId");
let memberId;
axios.post(`https://localhost:7048/api/Members/MemberId?protectId=${id}`, id)
    .then(response => {
        memberId = response.data
    })
    .catch(error => {
        console.log(error);
    });

let comments = ref([]);



async function fetchComments(gameId) {
    try {
        const response = await axios.get(`https://localhost:7048/api/Comments/${gameId}`);
        comments.value = response.data.filter(comment => comment.memberId === memberId);
        for (const comment of comments.value) {
            comment.avatarUrl = await getMemberAvatar(comment.memberId);
            comment.name = await getMemberName(comment.memberId)

            for (const attachedComment of comment.attachedComment) {
                attachedComment.avatarUrl = await getMemberAvatar(attachedComment.memberId);
                attachedComment.name = await getMemberName(attachedComment.memberId)
            }
        }

    } catch (error) {
        console.error(error);
        throw new Error('Failed to fetch comments'); // 如果出现错误，抛出一个错误
    }
}

async function getMemberAvatar(memberId) {
    try {
        const response = await axios.get(`https://localhost:7048/api/Comments/memberAvatar/${memberId}`);
        const avatarUrl = response.data;
        return avatarUrl;
    } catch (error) {
        console.error(error);
    }
}

async function getMemberName(memberId) {
    try {
        const response = await axios.get(`https://localhost:7048/api/Comments/memberName/${memberId}`);
        const name = response.data;
        return name;
    } catch (error) {
        console.error(error);
    }
}

(async () => {
    try {
        fetchComments(props.gameId);

    } catch (error) {
        console.log(error);
    }
})();


const formatDate = (dateString) => {
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = date.getMonth() + 1;
    const day = date.getDate();

    return `${year}-${month}-${day}`;
};

const showComments = reactive(
    Object.fromEntries(comments.value.map(comment => [comment.id, false]))
);


function toggleVisibility(commentId) {
    showComments[commentId] = !showComments[commentId];
}

const schema = yup.object({
    comment: yup.string().required().label("Comment")
});

const { errors, handleSubmit, resetForm } = useForm({
    validationSchema: schema
});



const submitAttachedComment = (commentId) => {
    handleSubmit((values) => {

        values.memberId = memberId;
        values.mainCommentId = commentId;
        values.dateTime = new Date();

        axios.post('https://localhost:7048/api/Comments/attachedComment', values)
            .then(response => {
                fetchComments(props.gameId);
                alert('提交成功');

            })
            .catch(error => {
                alert('提交失败');
                console.error(error);
            });


        resetForm()
    })();
};

const highlight = (rating, num) => {
    if (rating >= num) {
        return { color: '#34AC9E' }
    } else {
        return { color: '#E1E6F6' }
    }
}


</script>

<style scoped>
/* .container-wrapper {
  background-color: #EDF0F9;
} */

.container {
    height: 10vh;
}

.rating-wrapper {
    background-color: #edf0f9e0;
    align-self: center;
    box-shadow: 7px 7px 20px rgba(198, 206, 237, .7),
        -7px -7px 30px rgba(255, 255, 255, .7),
        inset 0px 0px 4px rgba(255, 255, 255, .9),
        inset 7px 7px 15px rgba(198, 206, 237, .8);
    border-radius: 5rem;
    display: inline-flex;
    direction: rtl !important;
    padding: 1px 10px;
    margin-left: auto;


    label {
        color: #E1E6F6;
        cursor: pointer;
        display: inline-flex;
        font-size: 20px;
        padding: 1rem .2rem;
        transition: color 0.5s;
    }

    svg {
        -webkit-text-fill-color: transparent;
        -webkit-filter: drop-shadow (4px 1px 6px rgba(198, 206, 237, 1));
        filter: drop-shadow(5px 1px 3px rgba(198, 206, 237, 1));
    }

    input {
        height: 100%;
        width: 100%;
    }

    input {
        display: none;
    }

    label:hover,
    label:hover~label,
    input:checked~label {
        color: #34AC9E;
    }

    label:hover,
    label:hover~label,
    input:checked~label {
        color: #34AC9E;
    }
}
</style>

<!-- <div class="container-wrapper">
    <div class="container d-flex align-items-center justify-content-center">
        <div class="row justify-content-center">


            <div class="rating-wrapper">

                <input type="radio" id="5-star-rating" name="star-rating" value="5"
                    @click="getRating">
                <label for="5-star-rating" class="star-rating">
                    <i class="fas fa-star d-inline-block"></i>
                </label>
   
                <input type="radio" id="4-star-rating" name="star-rating" value="4"
                    @click="getRating">
                <label for="4-star-rating" class="star-rating star">
                    <i class="fas fa-star d-inline-block"></i>
                </label>

                <input type="radio" id="3-star-rating" name="star-rating" value="3"
                    @click="getRating">
                <label for="3-star-rating" class="star-rating star">
                    <i class="fas fa-star d-inline-block"></i>
                </label>

                <input type="radio" id="2-star-rating" name="star-rating" value="2"
                    @click="getRating">
                <label for="2-star-rating" class="star-rating star">
                    <i class="fas fa-star d-inline-block"></i>
                </label>

                <input type="radio" id="1-star-rating" name="star-rating" value="1"
                    @click="getRating">
                <label for="1-star-rating" class="star-rating star">
                    <i class="fas fa-star d-inline-block"></i>
                </label>

            </div>

        </div>
    </div>
</div> -->