export type User = {
  [x: string]: any
  name: string
  todoList: Todo[]
}

export type Todo = {
  id: number
  title: string
  isDone: boolean
}
