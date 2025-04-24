<script setup>
import { AppState } from '@/AppState.js';
import { reportsService } from '@/services/ReportsService.js';
import { restaurantsService } from '@/services/RestaurantsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const restaurant = computed(() => AppState.activeRestaurant)
const account = computed(() => AppState.account)

const route = useRoute()
const router = useRouter()

onMounted(() => {
  getRestaurantById()
  getReportsByRestaurantId()
})

async function getRestaurantById() {
  try {
    await restaurantsService.getRestaurantById(route.params.restaurantId)
  } catch (error) {
    Pop.error(error, "Ain't no restaurant here, pal")
    logger.error(error)
    router.push({ name: 'Home' })
  }
}

async function getReportsByRestaurantId() {
  try {
    await reportsService.getReportsByRestaurantId(route.params.restaurantId)
  }
  catch (error) {
    Pop.error(error);
    logger.error(error)
  }
}

async function deleteRestaurant() {
  try {
    const confirmed = await Pop.confirm(`Are you sure you want to permanently delete ${restaurant.value.name}, dawg?`, 'You had better be sure, dawg', 'I am sure, dawg', 'Get me outta here, dawg')
    if (!confirmed) return
    await restaurantsService.deleteRestaurant(route.params.restaurantId)
    Pop.success("It's gone, dawg")
    router.push({ name: 'Home' })
  } catch (error) {
    Pop.error(error)
    logger.error(error)
  }
}

async function updateRestaurant() {
  try {
    const confirmed = await Pop.confirm(`Are you sure you want to ${restaurant.value.isShutdown ? 're-open' : 'shut down'} the ${restaurant.value.name}, dawg?`, '', 'yeah dawg', 'nah dawg')

    if (!confirmed) return

    const updateData = {
      isShutdown: !restaurant.value.isShutdown
    }

    await restaurantsService.updateRestaurant(route.params.restaurantId, updateData)
  } catch (error) {
    Pop.error(error)
    logger.error(error)
  }
}


</script>


<template>
  <div v-if="restaurant" class="container-fluid">
    <div class="row">
      <div class="col-12">
        <div class="d-flex justify-content-between">
          <h1 class="text-green baloo-font fw-bold">{{ restaurant.name }}</h1>
          <div v-if="restaurant.isShutdown" class="bg-danger fs-1 text-light px-3">
            <span class="mdi mdi-close-circle-outline"></span>
            <span>CURRENTLY SHUTDOWN</span>
          </div>
        </div>
        <div class="bg-light shadow-lg">
          <img :src="restaurant.imgUrl" :alt="'A picture of ' + restaurant.name" class="restaurant-img">
          <div class="p-3">
            <p>{{ restaurant.description }}</p>
            <div class="d-flex justify-content-between">
              <div class="d-flex gap-5">
                <div>
                  <span class="text-green mdi mdi-account-multiple fs-2"></span>
                  <span><b>{{ restaurant.visits }}</b> recent visits</span>
                </div>
                <div>
                  <span class="text-green mdi mdi-file-document fs-2"></span>
                  <span><b>0</b> reports</span>
                </div>
              </div>
              <div v-if="restaurant.creatorId == account?.id" class="d-flex gap-2">
                <button @click="updateRestaurant()" class="btn btn-success fs-5" type="button">
                  <span class="mdi" :class="restaurant.isShutdown ? 'mdi-door-open' : 'mdi-door-closed-cancel'"></span>
                  <span>{{ restaurant.isShutdown ? 'Re-Open' : 'Shutdown' }}</span>
                </button>
                <button @click="deleteRestaurant()" class="btn btn-danger fs-5" type="button">
                  <span class="mdi mdi-delete-forever"></span>
                  <span>Delete</span>
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div v-else class="container-fluid">
    <div class="row">
      <div class="col-12">
        <div class="text-green">
          <h1>Loading...</h1>
          <marquee direction="right" class="fs-1" scrollamount="30">
            <span class="mdi mdi-rodent"></span>
            <span class="mdi mdi-pizza ms-2 mdi-spin"></span>
          </marquee>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.restaurant-img {
  width: 100%;
  height: 50dvh;
  object-fit: cover;
}
</style>