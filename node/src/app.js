import express from 'express'

import { Pessoa } from './Pessoa.js'

const app = express()
const port = process.env.PORT

if(!port){
  console.error("port not set")
}

//busca de pessoas
app.get('/pessoas', (req, res) => {
  const termo = req.query.t
  if(!termo){
    return res.status(400).send()
  }
  return res.json([])
})

// consulta de pessoa por id
app.get('/pessoas/:id', (req, res) => {
  res.send()
})

app.get('/pessoas/contagem-pessoas', (req, res) => {
  res.send(0)
})

//cadastro
app.post('/pessoas', (req, res) => {
  const pessoa = new Pessoa(req.body)
  // console.log(pessoa)
  res.status(201).location(`/pessoas/${pessoa.id}`).send()
})

app.listen(port, () => {
  console.log(`Example app listening on port ${port}`)
})