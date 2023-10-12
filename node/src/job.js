import { createClient } from 'redis';

import { PessoaRepository } from './PessoaRepository.js';

import database from './database.js';

const redis = createClient();

redis.on('error', err => console.error('Queue: Redis Client Error', err));

redis.on('connect', () => console.log('Queue: Successfuly connected to Redis'));

let processedItems = 0;

let pessoaRepository;

export async function setupQueue(){
  redis.connect();
  pessoaRepository = new PessoaRepository(database.pool);
  runQueue();
}

export async function runQueue(){
  const response =  await redis.blPop('queue', 10);
  if(response){
    await processQueueItem(response.element);
  }else {
    console.log('processed items:', processedItems)
  }
  runQueue();
}

async function processQueueItem (pessoa) {
  processedItems++;
  await pessoaRepository.save(JSON.parse(pessoa))
  // console.log(pessoa)
}