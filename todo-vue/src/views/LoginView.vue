<script setup lang="ts">
import { useUserStore } from '@/stores/user'
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const userStore = useUserStore()
const userNameRef = ref<string>('')
const passwordRef = ref<string>('')
const buttonDisabled = ref<boolean>(false)
const buttonValue = ref<string>('Login')
const router = useRouter()

function refReset() {
  userNameRef.value = ''
  passwordRef.value = ''
}

function LoginFn() {
  buttonDisabled.value = true
  buttonValue.value = 'Checking user'
  let check: boolean = false
  if (userNameRef.value == 'admin' && passwordRef.value == 'admin') {
    buttonValue.value = 'Redirecting...'
    check = true
  }
  refReset()
  const userCheck = new Promise((resolve, reject) => {
    setTimeout(() => {
      if (check) {
        resolve(true)
      } else {
        reject('Kullanıcı adı veya şifre hatalı.')
      }
    }, 1000)
  })
  userCheck
    .then((res) => {
      if (res) {
        router.push('/todos')
      }
    })
    .catch((err) => {
      alert(err)
      buttonDisabled.value = false
      buttonValue.value = 'Login'
    })
}
</script>

<template>
  <main>
    <div>
      <div class="inputs">
        <h1>Login</h1>
        <label for="username">Kullanıcı Adı</label>
        <input type="text" id="username" v-model="userNameRef" />
        <label for="password">Şifre</label>
        <input type="password" id="password" v-model="passwordRef" />
      </div>
      <div class="buttons">
        <router-link to="/register">Register</router-link>
        <button :disabled="buttonDisabled" @click.prevent="LoginFn">{{ buttonValue }}</button>
      </div>
    </div>
  </main>
</template>

<style scoped>
@media screen and (max-width: 500px) {
  main {
    width: 100%;
    display: grid;
    place-items: center;
    justify-items: center;
    border-radius: 20px;
    & .buttons {
      margin-top: 2em;
      display: flex;
      flex-direction: row;
      justify-content: center;
      align-items: center;
      gap: 1em;
      & button {
        width: 80%;
        padding: 0.5em;
        border-radius: 10px;
        border: 1px solid black;
      }
    }
    & .inputs {
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      gap: 1em;
      & input {
        width: 80%;
        padding: 0.5em;
        border-radius: 10px;
        border: 1px solid black;
      }
    }
  }
}
</style>
