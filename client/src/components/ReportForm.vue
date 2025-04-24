<script setup>
import { AppState } from '@/AppState.js';
import { computed, ref } from 'vue';


const restaurantsThatIDoNotOwn = computed(() => AppState.restaurants.filter(restaurant => restaurant.creatorId != AppState.account?.id))

const editableReportData = ref({
  title: '',
  body: '',
  score: 3,
  imgUrl: '',
  restaurantId: '' // NOTE this is technically going to be a number
})

</script>


<template>
  <form class="baloo-font">
    <div class="mb-3">
      <div class="container-fluid">
        <div class="row">
          <div class="col-md-6">
            <!-- <img src="" alt=""> -->
          </div>
          <div class="col-md-6">
            <select v-model="editableReportData.restaurantId" class="form-select" aria-label="Select a restaurant"
              required>
              <option selected disabled value="">Choose a restaurant</option>
              <option v-for="restaurant in restaurantsThatIDoNotOwn" :key="'report form ' + restaurant.id"
                :value="restaurant.id">
                {{ restaurant.name }}
              </option>
            </select>
          </div>
        </div>
      </div>
    </div>
    <div class="mb-3">
      <label for="report-title" class="form-label fw-bold">Title for Report</label>
      <input v-model="editableReportData.title" type="text" class="form-control" id="report-title"
        placeholder="Report Title..." aria-describedby="title-help" required maxlength="255">
      <div id="title-help" class="form-text">make it eye catching...</div>
    </div>
    <div class="mb-3">
      <label for="report-details" class="form-label">Report details</label>
      <textarea v-model="editableReportData.body" class="form-control" id="report-details" rows="3"
        aria-describedby="details-help" maxlength="500"></textarea>
      <div id="details-help" class="form-text">make it juicy...</div>
    </div>
    <div class="mb-3">
      <label for="report-image" class="form-label fw-bold">Image for Report</label>
      <input v-model="editableReportData.imgUrl" type="url" class="form-control" id="report-image"
        placeholder="Report Image..." aria-describedby="image-help" maxlength="1000">
      <div id="image-help" class="form-text">make it a picture...</div>
    </div>
    <div class="mb-3">
      <label for="report-score" class="form-label">Report Score {{ editableReportData.score }}</label>
      <input v-model="editableReportData.score" type="range" class="form-range" min="1" max="5" id="report-score"
        required>
    </div>
    <div class="text-end">
      <button class="btn btn-success" type="submit">Submit</button>
    </div>
  </form>
</template>


<style lang="scss" scoped></style>