<template>
	<!-- <game-detail-comments v-if="memberComment"/> -->

	<div class="latest-comments">
		<ul class="list-wrap">
			<li>
				<ul>
					<li>
						<div class="comments-box">
							<div class="comments-avatar">
								<img :src="avatar" alt="img">

								<div class="container-wrapper">
									<div class="container d-flex align-items-center justify-content-center">
										<div class="row justify-content-center">

											<!-- star rating -->
											<div class="rating-wrapper">

												<!-- star 5 -->
												<input type="radio" id="5-star-rating" name="star-rating" value="5"
													@click="getRating">
												<label for="5-star-rating" class="star-rating">
													<i class="fas fa-star d-inline-block"></i>
												</label>

												<!-- star 4 -->
												<input type="radio" id="4-star-rating" name="star-rating" value="4"
													@click="getRating">
												<label for="4-star-rating" class="star-rating star">
													<i class="fas fa-star d-inline-block"></i>
												</label>

												<!-- star 3 -->
												<input type="radio" id="3-star-rating" name="star-rating" value="3"
													@click="getRating">
												<label for="3-star-rating" class="star-rating star">
													<i class="fas fa-star d-inline-block"></i>
												</label>

												<!-- star 2 -->
												<input type="radio" id="2-star-rating" name="star-rating" value="2"
													@click="getRating">
												<label for="2-star-rating" class="star-rating star">
													<i class="fas fa-star d-inline-block"></i>
												</label>

												<!-- star 1 -->
												<input type="radio" id="1-star-rating" name="star-rating" value="1"
													@click="getRating">
												<label for="1-star-rating" class="star-rating star">
													<i class="fas fa-star d-inline-block"></i>
												</label>

											</div>

										</div>
									</div>
								</div>

							</div>
							<div class="comments-text">
								<form class="comment-form" @submit.prevent="submitComment()"
									:validation-schema="schema">
									<div class="form-grp">
										<err-message :msg="errors.comment1" />
										<Field name="comment1" v-slot="{ field }">
											<textarea v-bind="field" name="comment1"
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

const schema = yup.object({
	comment1: yup.string().required().label("Comment1")
});

const { errors, handleSubmit, resetForm } = useForm({
	validationSchema: schema
});

let rating = null;
function getRating() {
	const radioButtons = document.getElementsByName('star-rating');

	radioButtons.forEach(button => {
		if (button.checked) {
			rating = button.value;
		}
	});
	console.log(rating);
}

let id = $cookie.getCookie("accountId");

let avatar = ref('');
avatar.value = $cookie.getCookie("avatarUrl");


let memberId;

axios.post(`https://localhost:7048/api/Members/MemberId?protectId=${id}`, id)
	.then(response => {
		memberId = response.data
	})
	.catch(error => {
		console.log(error);
	});

const emitEvents = defineEmits(['refreshComment']);

const submitComment = () => {
	handleSubmit((values) => {

		values.memberId = memberId;
		values.date = new Date();
		values.gameId = props.gameId;
		values.rating = rating;

		axios.post('https://localhost:7048/api/Comments', values)
			.then(response => {
				emitEvents('refreshComment', response.data);
				alert('提交成功');
			})
			.catch(error => {
				alert('提交失败');
			});

		resetForm()
	})();
};
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
