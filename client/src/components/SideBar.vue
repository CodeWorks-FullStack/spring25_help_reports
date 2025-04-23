<script setup>
import { AppState } from '@/AppState.js';
import { computed } from 'vue';
import Login from './Login.vue';

const account = computed(() => AppState.account)
</script>


<template>
  <aside class="baloo-font">
    <div class="bg-success text-light text-center fs-1">
      <p class="fw-bold">HELP</p>
      <span class="mdi mdi-rodent display-1"></span>
    </div>
    <p class="fs-3 text-center text-success fw-bold">food reports</p>
    <button class="btn btn-success w-100 fw-bold fs-3 rounded-0">Report!</button>
    <div class="rounded-top bg-success-subtle">
      <!-- TODO make this not look like total butt -->
      <Login />
    </div>
  </aside>
</template>


<style lang="scss" scoped></style>