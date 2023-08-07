import { ref } from 'vue'
import { defineStore } from 'pinia'
import { type User, type Todo } from '../types'

export const useUserStore = defineStore('user', () => {
  const user = ref<User>({
    name: '',
    todoList: [
      {
        id: 0,
        title: '',
        isDone: false
      }
    ]
  })
  function login(user: User) {
    user.value = user
  }
  function addTodo(todo: Todo) {
    user.value.todoList.push(todo)
  }

  return { user, addTodo, login }
})
