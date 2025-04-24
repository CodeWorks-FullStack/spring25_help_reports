<script setup>
import { Report } from '@/models/Report.js';

defineProps({
  report: { type: Report, required: true }
})

</script>


<template>
  <div class="bg-light shadow-lg p-3 mb-3">
    <p class="fs-2 fw-bold baloo-font text-center text-green mb-0">"{{ report.title }}"</p>
    <p class="text-center mb-1">
      <span :title="`${report.reporter.name} gives this restaurant ${report.score} out of 5`">
        <span v-for="score in report.score" :key="report.id + score" class="mdi mdi-star text-warning fs-2"></span>
      </span>
    </p>
    <p v-if="report.body">{{ report.body }}</p>
  </div>
</template>


<style lang="scss" scoped></style>