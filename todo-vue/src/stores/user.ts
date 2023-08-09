import { ref } from 'vue'
import { defineStore } from 'pinia'
import { type Todo, type User } from '../types'
import axios from 'axios'

export const useUserStore = defineStore('user', () => {
  const userTodos = ref<User>({
    userId: null,
    todoList: []
  })
  function login(comingTodoList: User) {
    console.log('comingTodoList', comingTodoList)
    userTodos.value = comingTodoList
  }
  function logout() {
    userTodos.value = {
      userId: null,
      todoList: []
    }
  }
  function register(comingUserId: number) {
    console.log('comingUserId', comingUserId)
    userTodos.value.userId = comingUserId
    userTodos.value.todoList = []
  }
  async function addTodo(comingTodoText: string) {
    const response = await axios.post('http://localhost:5292/api/Todo/Post', {
      text: comingTodoText,
      id: userTodos.value.userId
    })
    if (response.status == 200) {
      const comingTodo: Todo = { ...response.data }
      userTodos.value.todoList.push(comingTodo)
    } 
  }
  async function toggleTodo(comingTodoId: number) {
    const todo = userTodos.value.todoList.find((todo) => todo.todoId == comingTodoId)
    if (todo) {
      const response = await axios.post('http://localhost:5292/api/Todo/Update', {
        boo: !todo.isDone,
        todoId: todo.todoId
      })
      if (response.status == 200) {
        todo.isDone = !todo.isDone
      }
    }
  }

  async function removeTodo(comingTodoId: number) {
    const response = await axios.post(`http://localhost:5292/api/Todo/Delete`, {
      todoId: comingTodoId
    })
    if (response.status == 200) {
      userTodos.value.todoList = userTodos.value.todoList.filter(
        (todo) => todo.todoId != comingTodoId
      )
    }
  }

  return { userTodos, addTodo, login, logout, register, toggleTodo, removeTodo }
})
