import { ref } from 'vue'
import { defineStore } from 'pinia'
import { type Todo, type User } from '../types'

export const useUserStore = defineStore('user', () => {
  const userTodos = ref<User>({
    userId: 0,
    todoList: []
  })
  function login(comingTodoList: User) {
    console.log('comingTodoList', comingTodoList)
    userTodos.value = comingTodoList
  }
  function register(comingUserId: number) {
    console.log('comingUserId', comingUserId)
    userTodos.value.userId = comingUserId
    userTodos.value.todoList = []
  }
  function addTodo(comingTodo: Todo) {
    userTodos.value.todoList.push(comingTodo)
  }

  return { userTodos, addTodo, login, register }
})
