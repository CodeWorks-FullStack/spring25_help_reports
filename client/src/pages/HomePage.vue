<script setup>
import { AppState } from '@/AppState.js';
import RestaurantCard from '@/components/RestaurantCard.vue';
import { restaurantsService } from '@/services/RestaurantsService.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';

const restaurants = computed(() => AppState.restaurants)

onMounted(() => {
  getAllRestaurants()
})

async function getAllRestaurants() {
  try {
    await restaurantsService.getAllRestaurants()
  }
  catch (error) {
    Pop.error(error);
  }
}
</script>

<template>
  <div class="container-fluid">
    <div class="row">
      <div v-for="restaurant in restaurants" :key="restaurant.id" class="col-md-4">
        <RestaurantCard :restaurant="restaurant" />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss"></style>
