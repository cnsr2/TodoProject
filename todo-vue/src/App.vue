<script setup lang="ts">
import { RouterView } from 'vue-router'
</script>

<template>
  <main>
    <RouterView />
  </main>
</template>

<style scoped>
main {
  height: 100vh;
  width: 100vw;
  display: grid;
  place-items: center;
  background: rgb(255, 246, 0);
  background: -moz-linear-gradient(
    37deg,
    rgba(255, 246, 0, 1) 0%,
    rgba(255, 160, 0, 1) 50%,
    rgba(255, 0, 0, 1) 100%
  );
  background: -webkit-linear-gradient(
    37deg,
    rgba(255, 246, 0, 1) 0%,
    rgba(255, 160, 0, 1) 50%,
    rgba(255, 0, 0, 1) 100%
  );
  background: linear-gradient(
    37deg,
    rgba(255, 246, 0, 1) 0%,
    rgba(255, 160, 0, 1) 50%,
    rgba(255, 0, 0, 1) 100%
  );
  filter: progid:DXImageTransform.Microsoft.gradient(startColorstr="#fff600",endColorstr="#ff0000",GradientType=1);
}
</style>
