import express from 'express'
import "express-async-errors"
import { createClient } from 'redis';
import database from './database.js'
import { Pessoa } from './Pessoa.js'
import { PessoaRepository } from './PessoaRepository.js';
import {setupQueue} from './job.js'


await database.setup();

const pessoaRepository = new PessoaRepository(database.pool);

// console.log(await pessoaRepository.count());

const app = express()
const port = process.env.PORT || 9999

setupQueue();

app.use(express.json());

const redis = createClient();

redis.on('error', err => console.error('Redis Client Error', err));

redis.on('connect', () => console.log('App: Successfuly connected to Redis'));

await redis.connect();

if(!port){
  console.error("port not set")
}

//busca de pessoas
app.get('/pessoas', async (req, res) => {
  const termo = req.query.t
  if(!termo){
    return res.status(400).send()
  }
  const pessoas = await pessoaRepository.search(termo);
  return res.json(pessoas);
})

// consulta de pessoa por id
app.get('/pessoas/:id', async (req, res) => {
  const pessoaJson = await redis.get(req.params.id);
  if(!pessoaJson){
    return res.status(404).send();
  }
  return res.contentType("application/json").send(pessoaJson);
})

//conta o total de registro no banco
app.get('/contagem-pessoas', async (req, res) => {
  const count = await pessoaRepository.count();
  return res.send(count);
})

//cadastro
app.post('/pessoas', async (req, res) => {
  const pessoa = new Pessoa(req.body)

  if(!pessoa.apelido || !pessoa.nome || !pessoa.nascimento){
    return res.status(422).send()
  }

  if(!pessoa.isValid()){
    return res.status(400).send()
  }

  const pessoaExists = await redis.get(pessoa.nome);
  if(pessoaExists){
    return res.status(422).send();
  }

  const pessoaJson = JSON.stringify(pessoa)

  await redis.set(pessoa.id, pessoaJson);
  await redis.set(pessoa.nome, pessoa.id);
  await redis.lPush('queue', [pessoaJson] , (e,r)=> console.log(r));

  // await pessoaRepository.save(pessoa)

  return res.status(201).location(`/pessoas/${pessoa.id}`).send();
})


//error handler
app.use(async (
  err,
  request,
  response,
  next,
) => {
  const data = {
    requestMethod: request.method,
    requestBody: request.body,
    requestQuery: request.query,
  };
  console.error("Error:", err);

  console.error(data);

  return response.status(500).json({
    message: 'Internal server error.',
  });
})

app.listen(port, () => {
  console.log(`Example app listening on port ${port}`)
})