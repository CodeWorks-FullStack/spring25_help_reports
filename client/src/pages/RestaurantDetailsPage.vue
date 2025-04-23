<script setup>
import { restaurantsService } from '@/services/RestaurantsService.js';
import { Pop } from '@/utils/Pop.js';
import { onMounted } from 'vue';
import { useRoute } from 'vue-router';

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
  <h1>It's the page</h1>
</template>


<style lang="scss" scoped></style>