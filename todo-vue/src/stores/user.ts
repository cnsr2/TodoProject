import { ref } from 'vue'
import { defineStore } from 'pinia'
import { type TodoList, type Todo } from '../types'

export const useUserStore = defineStore('user', () => {
  const todoList = ref<TodoList>([
    {
      todoId: 0,
      todoText: '',
      isDone: false,
      userId: 0
    }
  ])
  function login(comingTodoList: TodoList) {
    console.log('comingTodoList', comingTodoList)
    // comingTodoList.value = comingTodoList
  }
  function addTodo(comingTodo: Todo) {
    todoList.value.push(comingTodo)
  }

  return { todoList, addTodo, login }
})
