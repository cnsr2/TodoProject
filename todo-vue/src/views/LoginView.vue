<script setup lang="ts">
import { useUserStore } from '@/stores/user'
import axios from 'axios'
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

async function LoginFn() {
  if (userNameRef.value == '' || passwordRef.value == '') {
    alert('Kullanıcı adı veya şifre boş olamaz.')
    return
  }
  let check: boolean = false
  buttonDisabled.value = true
  buttonValue.value = 'Checking user'
  try {
    const response = await axios.post(
    'http://localhost:5292/api/User/Login',
    {
      name: userNameRef.value,
      pw: passwordRef.value
    },
    {
      headers: {
        'Content-Type': 'application/json'
      }
    }
  )
  if (response.status == 200) {
    check = true
    buttonValue.value = 'Redirecting...'
    setTimeout(() => {
      userStore.login(response.data)
      router.push('/todos')
    }, 500)
  }
  } catch (error) {
      console.log(error.response)
    if (error.response.status == 400) {
    console.log(error.response.status)
    alert('Şifre yanlış.')
  } 
  if (error.response.status == 404) {
    console.log(error.response.status)
    alert('Kullanıcı kayıtlı değil.')
  }
  if (check == false) {
    buttonDisabled.value = false
    buttonValue.value = 'Login'
  }
 }
  refReset()
}
</script>

<template>
  <div class="main">
    <div>
      <div class="inputs">
        <h1>Login</h1>
        <label for="username">Kullanıcı Adı</label>
        <input type="text" id="username" v-model="userNameRef" />
        <label for="password">Şifre</label>
        <input type="password" id="password" v-model="passwordRef" />
      </div>
      <div class="buttons1">
        <router-link to="/register">Register</router-link>
        <button :disabled="buttonDisabled" @click.prevent="LoginFn">{{ buttonValue }}</button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.main {
  max-width: 480px;
  display: grid;
  place-items: center;
  justify-items: center;
  border-radius: 20px;
}
.buttons1 {
  margin-top: 4em;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  gap: 1em;
}
button {
  width: 80%;
  padding: 0.5em;
  border-radius: 10px;
  border: 1px solid black;
  cursor: pointer;
}
.inputs {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 1em;
}
input {
  width: 100%;
  padding: 0.5em;
  border-radius: 10px;
  border: 1px solid black;
}
</style>
