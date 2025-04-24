<script setup>
import { Restaurant } from '@/models/Restaurant.js';

defineProps({
  restaurant: { type: Restaurant, required: true }
})
</script>


<template>
  <RouterLink :to="{ name: 'Restaurant Details', params: { restaurantId: restaurant.id } }"
    :title="`Go to the ${restaurant.name} page`">
    <div class="shadow-lg mb-3 position-relative">
      <img :src="restaurant.imgUrl" :alt="`A picture of the ${restaurant.name} restaurant`" class="w-100 restaurant-img"
        :class="{ 'gray-out': restaurant.isShutdown }">
      <div class="bg-light p-2">
        <span class="fw-bold" :class="restaurant.isShutdown ? 'text-danger' : 'text-green'">
          {{ restaurant.name }}
        </span>
        <p>{{ restaurant.description }}</p>
        <div class="d-flex justify-content-between">
          <div>
            <span class="text-green mdi mdi-account-multiple fs-2"></span>
            <span><b>{{ restaurant.visits }}</b> recent visits</span>
          </div>
          <div>
            <span class="text-green mdi mdi-file-document fs-2"></span>
            <span><b>{{ restaurant.reportCount }}</b> reports</span>
          </div>
        </div>
      </div>
      <div>
        <img :src="restaurant.owner.picture" :alt="restaurant.owner.name" class="owner-img m-1"
          :title="'The owner is ' + restaurant.owner.name">
      </div>
    </div>
  </RouterLink>
</template>


<style lang="scss" scoped>
.restaurant-img {
  height: 40dvh;
  object-fit: cover;
}

.gray-out {
  filter: grayscale(1);
}

.owner-img {
  height: 4em;
  aspect-ratio: 1/1;
  border-radius: 50%;
  object-fit: cover;
  position: absolute;
  top: 0;
  right: 0;
}

p {
  text-wrap: nowrap;
  text-overflow: ellipsis;
  overflow: hidden;
  white-space: nowrap;
}
</style>