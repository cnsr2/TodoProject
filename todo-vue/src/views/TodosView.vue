<script setup lang="ts">
import router from '@/router'
import { useUserStore } from '@/stores/user'
import { ref } from 'vue'

const userStore = useUserStore()
if (userStore.userTodos.userId == null) {
  router.push('/')
}
const filter = ref<string>('all')
const todoText = ref<string>('')
const filterEnum: {
  [key: string]: string | boolean
} = {
  all: '',
  active: true,
  completed: false
}
const changeFilter = (comingFilter: string) => {
  filter.value = comingFilter
}
const addTodoFn = () => {
  const err = userStore.addTodo(todoText.value)
  if (err) {
    alert(err)
  }
  todoText.value = ''
}
const logoutFn = () => {
  userStore.logout()
  router.push('/')
}
</script>

<template>
  <div class="mainDiv">
    <div>
      <input type="text" v-model="todoText" v-on:keyup.enter="addTodoFn" />
    </div>
    <ul
      :key="todo.todoId"
      v-for="todo in userStore.userTodos.todoList.filter(
        (todo) => todo.isDone !== filterEnum[filter]
      )"
    >
      <li>
        <p :class="`${todo.isDone == true && 'isDone'}`">
          {{ todo.todoText }}
        </p>
        <div class="buttonGroup">
          <button @click="() => userStore.toggleTodo(todo.todoId)">
            {{ todo.isDone == true ? 'UnDone' : 'Done' }}
          </button>
          <button @click="() => userStore.removeTodo(todo.todoId)">Remove</button>
        </div>
      </li>
    </ul>
    <div class="buttonGroup2">
      <button @click="() => changeFilter('all')">All</button>
      <button @click="() => changeFilter('active')">Active</button>
      <button @click="() => changeFilter('completed')">Completed</button>
    </div>
    <div class="buttonGroup2">
      <button @click="logoutFn">Çıkış</button>
    </div>
  </div>
</template>

<style>
.mainDiv {
  display: flex;
  flex-direction: column;
  gap: 1em;
  max-width: 480px;
}
ul {
  display: flex;
  flex-direction: column;
  gap: 1em;
  width: 100%;
}
li {
  display: flex;
  justify-content: space-between;
  padding: 10px;
  border-radius: 10px;
  background-color: rgb(199, 16, 16);
}
.isDone {
  text-decoration: line-through;
}
.buttonGroup {
  display: flex;
  gap: 5px;
}
.buttonGroup2 {
  display: flex;
  gap: 5px;
  & button {
    width: 100%;
    padding: 0.5em;
    border-radius: 10px;
    border: 1px solid black;
    cursor: pointer;
  }
}
input {
  width: 100%;
  padding: 0.5em;
  border-radius: 10px;
  border: 1px solid black;
}
</style>
