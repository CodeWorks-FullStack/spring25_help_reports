<script setup>
import { Restaurant } from '@/models/Restaurant.js';

defineProps({
  restaurant: { type: Restaurant, required: true }
})
</script>


<template>
  <RouterLink :to="{ name: 'Restaurant Details', params: { restaurantId: restaurant.id } }"
    :title="`Go to the ${restaurant.name} page`">
    <div class="shadow-lg">
      <img :src="restaurant.imgUrl" :alt="`A picture of the ${restaurant.name} restaurant`"
        class="w-100 restaurant-img">
      <div class="bg-light p-2">
        <span class="text-green fw-bold">{{ restaurant.name }}</span>
        <p>{{ restaurant.description }}</p>
        <div class="d-flex justify-content-between">
          <div>
            <span class="text-green mdi mdi-account-multiple fs-2"></span>
            <span><b>{{ restaurant.visits }}</b> recent visits</span>
          </div>
          <div>
            <span class="text-green mdi mdi-file-document fs-2"></span>
            <span><b>0</b> reports</span>
          </div>
        </div>
      </div>
    </div>
  </RouterLink>
</template>


<style lang="scss" scoped>
.restaurant-img {
  height: 40dvh;
  object-fit: cover;
}
</style>