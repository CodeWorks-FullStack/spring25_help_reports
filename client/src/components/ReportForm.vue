<script setup>
import { AppState } from '@/AppState.js';
import { reportsService } from '@/services/ReportsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { Modal } from 'bootstrap/dist/js/bootstrap.bundle.js';
import { computed, ref } from 'vue';


const restaurantsThatIDoNotOwn = computed(() => AppState.restaurants.filter(restaurant => restaurant.creatorId != AppState.account?.id))

const restaurantImg = computed(() => {
  const restaurantId = editableReportData.value.restaurantId

  if (!restaurantId) {
    return 'https://images.unsplash.com/photo-1686836715835-65af22ea5cd4?w=700&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8cmVzdGF1cmFudHxlbnwwfHwwfHx8MA%3D%3D'
  }

  const restaurant = restaurantsThatIDoNotOwn.value.find(restaurant => restaurant.id == restaurantId)

  return restaurant.imgUrl
})

const editableReportData = ref({
  title: '',
  body: '',
  score: 3,
  imgUrl: null,
  restaurantId: '' // NOTE this is technically going to be a number
})

async function createReport() {
  try {
    if (editableReportData.value.imgUrl == '') editableReportData.value.imgUrl = null
    await reportsService.createReport(editableReportData.value)
    editableReportData.value = {
      title: '',
      body: '',
      score: 3,
      imgUrl: null,
      restaurantId: ''
    }
    Modal.getOrCreateInstance('#reportModal').hide()
  }
  catch (error) {
    Pop.error(error);
    logger.error(error)
  }
}

</script>


<template>
  <form @submit.prevent="createReport()" class="baloo-font">
    <div class="mb-3">
      <label for="report-restaurant-id" class="form-label fw-bold">Select a Restaurant</label>
      <div class="container-fluid">
        <div class="row">
          <div class="col-md-6">
            <img :src="restaurantImg" alt="Image of restaurant you are leaving a report for">
          </div>
          <div class="col-md-6">
            <select v-model="editableReportData.restaurantId" class="form-select" aria-label="Select a restaurant"
              required id="report-restaurant-id">
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


<style lang="scss" scoped>
img {
  height: 10dvh;
  object-fit: cover;
  width: 100%;
}
</style>