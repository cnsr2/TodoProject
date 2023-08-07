export type User = {
  userId: number
  todoList: TodoList
}
export type TodoList = Todo[]

export type Todo = {
  todoId: number
  todoText: string
  isDone: boolean
  userId: number
}
