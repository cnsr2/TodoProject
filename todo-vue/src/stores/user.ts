import { ref } from 'vue'
import { defineStore } from 'pinia'
import { type Todo, type User } from '../types'
import axios from 'axios'

export const useUserStore = defineStore('user', () => {
  const userTodos = ref<User>({
    userId: 0,
    todoList: [
      {
        todoId: 0,
        todoText: 'test',
        isDone: false,
        userId: 0
      },
      {
        todoId: 1,
        todoText: 'test2',
        isDone: true,
        userId: 0
      }
    ]
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
  async function addTodo(comingTodoText: string) {
    const response = await axios.post('http://localhost:5292/api/Todo/id', {
      text: comingTodoText,
      id: userTodos.value.userId
    })
    if (response.status == 200) {
      const comingTodo: Todo = { ...response.data }
      userTodos.value.todoList.push(comingTodo)
    } else if (response.status == 404) {
      return 'User not found'
    }
  }
  async function toggleTodo(comingTodoId: number) {
    const todo = userTodos.value.todoList.find((todo) => todo.todoId == comingTodoId)
    if (todo) {
      const response = await axios.put('http://localhost:5292/api/Todo', {
        _boolean: !todo.isDone,
        todoId: todo.todoId
      })
      if (response.status == 200) {
        todo.isDone = !todo.isDone
      }
    }
  }

  async function removeTodo(comingTodoId: number) {
    const response = await axios.delete(`http://localhost:5292/api/Todo/${comingTodoId}`)
    if (response.status == 200) {
      userTodos.value.todoList = userTodos.value.todoList.filter(
        (todo) => todo.todoId != comingTodoId
      )
    }
  }

  return { userTodos, addTodo, login, register, toggleTodo, removeTodo }
})
