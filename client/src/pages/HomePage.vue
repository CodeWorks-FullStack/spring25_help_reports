<script setup>
import { AppState } from '@/AppState.js';
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
  <div class="container"></div>
</template>

<style scoped lang="scss"></style>
