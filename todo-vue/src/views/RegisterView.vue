<script setup lang="ts">
import { useUserStore } from '@/stores/user'
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios'

const userStore = useUserStore()
const userNameRef = ref<string>('')
const passwordRef = ref<string>('')
const passwordRef2 = ref<string>('')
const router = useRouter()

function refReset() {
  userNameRef.value = ''
  passwordRef.value = ''
  passwordRef2.value = ''
}

async function RegisterFn() {
  // const response = await fetch("http://localhost:5292/api/User/Register", {
  //     headers:
  //     {
  //         'Accept': 'application/json',
  //         'Content-Type': 'application/json'
  //     },
  //     method: "POST",
  //     body: JSON.stringify({
  //         "name": userNameRef.value,
  //         "pw": passwordRef.value,
  //         "valPw": passwordRef2.value
  //     })
  // })
  try {
    const response = await axios.post(
    'http://localhost:5292/api/User/Register',
    {
      name: userNameRef.value,
      pw: passwordRef.value,
      valPw: passwordRef2.value
    },
    {
      headers: { 'Content-Type': 'application/json' }
    }
  )
  if (response.status == 200) {
    alert('Kayıt başarılı.')
    userStore.register(response.data)
    router.push('/todos')
  }  
  } catch (error) {
    if (error.response.status == 400) {
    alert('Kullanıcı adı daha önce alınmış.')
  } else if (error.response.status == 500) {
    alert('Kullanıcı adı en az 3 karakter ve Şifre en az 8 karakter olmalı.')
  }
  }
  
  refReset()
}
</script>

<template>
  <div class="main2">
    <div>
      <div class="inputs">
        <h1>Register</h1>
        <label for="username">Kullanıcı Adı</label>
        <input type="text" id="username" v-model="userNameRef" />
        <label for="password1">Şifre</label>
        <input type="password" id="password1" v-model="passwordRef" />
        <label for="password2">Şifre Tekrar</label>
        <input type="password" id="password2" v-model="passwordRef2" />
      </div>
      <div class="buttons">
        <router-link to="/">Login</router-link>
        <button @click.prevent="RegisterFn">Register</button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.main2 {
  max-width: 480px;
  display: grid;
  grid-template-columns: 1fr;
  border-radius: 20px;
  place-items: center;
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
      cursor: pointer;
    }
  }

  & .inputs {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    gap: 1em;
    & input {
      width: 100%;
      padding: 0.5em;
      border-radius: 10px;
      border: 1px solid black;
    }
  }
}
</style>
