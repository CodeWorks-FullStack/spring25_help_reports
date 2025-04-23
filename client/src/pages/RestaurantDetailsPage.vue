<script setup>
import { AppState } from '@/AppState.js';
import { restaurantsService } from '@/services/RestaurantsService.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const restaurant = computed(() => AppState.activeRestaurant)
const account = computed(() => AppState.account)

const route = useRoute()

onMounted(() => {
  getRestaurantById()
})

async function getRestaurantById() {
  try {
    await restaurantsService.getRestaurantById(route.params.restaurantId)
  } catch (error) {
    Pop.error(error)
  }
}
</script>


<template>
  <div v-if="restaurant" class="container">
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
                <button class="btn btn-success fs-5">
                  <span class="mdi" :class="restaurant.isShutdown ? 'mdi-door-open' : 'mdi-door-closed-cancel'"></span>
                  <span>{{ restaurant.isShutdown ? 'Re-Open' : 'Shutdown' }}</span>
                </button>
                <button class="btn btn-danger fs-5">
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
  <div v-else class="container">
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