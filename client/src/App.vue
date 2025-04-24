<script setup>
import { RouterView } from 'vue-router';
import SideBar from './components/SideBar.vue';
import ReportModal from './components/ReportModal.vue';

</script>

<template>
  <main>
    <div class="container-fluid">
      <div class="row">
        <div class="col-md-2">
          <SideBar />
        </div>
        <div class="col-md-10">
          <RouterView />
        </div>
      </div>
    </div>
  </main>
  <ReportModal />
</template>

<style lang="scss"></style>
