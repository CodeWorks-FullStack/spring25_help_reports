<script setup>
import Example from '@/components/Example.vue';
import { restaurantsService } from '@/services/RestaurantsService.js';
import { Pop } from '@/utils/Pop.js';
import { onMounted } from 'vue';

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
  <Example />
</template>

<style scoped lang="scss"></style>
