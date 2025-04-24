<script setup>
import { Report } from '@/models/Report.js';

defineProps({
  report: { type: Report, required: true }
})

</script>


<template>
  <div class="bg-light shadow-lg p-3 mb-3">
    <div class="container-fluid">
      <div class="row justify-content-end">
        <div class="col-4">
          <p class="fs-2 fw-bold baloo-font text-center text-green mb-0">"{{ report.title }}"</p>
          <p class="text-center mb-1">
            <span :title="`${report.reporter.name} gives this restaurant ${report.score} out of 5`">
              <span v-for="score in report.score" :key="report.id + score"
                class="mdi mdi-star text-warning fs-2"></span>
            </span>
          </p>
        </div>
        <div class="col-4">
          <div class="d-flex align-items-center justify-content-end gap-2">
            <div class="text-end  baloo-font fw-bold">
              <p class="mb-1">{{ report.reporter.name }}</p>
              <span>{{ report.createdAt.toLocaleDateString() }}</span>
            </div>
            <img :src="report.reporter.picture" :alt="report.reporter.name" class="reporter-picture">
          </div>
        </div>
      </div>
    </div>
    <p v-if="report.body">{{ report.body }}</p>
  </div>
</template>


<style lang="scss" scoped>
.reporter-picture {
  height: 5rem;
  aspect-ratio: 1/1;
  border-radius: 50%;
  object-fit: cover;
}
</style>