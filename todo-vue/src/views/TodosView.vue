<script setup lang="ts">
import { useUserStore } from '@/stores/user'
import { ref } from 'vue'

const userStore = useUserStore()
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
    <div>
      <button @click="() => changeFilter('all')">All</button>
      <button @click="() => changeFilter('active')">Active</button>
      <button @click="() => changeFilter('completed')">Completed</button>
    </div>
  </div>
</template>

<style>
.mainDiv {
  display: flex;
  flex-direction: column;
  gap: 1em;
  width: 500px;
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
  text-decoration: unset;
}
</style>
