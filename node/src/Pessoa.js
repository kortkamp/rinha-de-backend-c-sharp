import {v4 as uuid} from "uuid"

import { isValid, parseISO } from 'date-fns'

export class Pessoa {
  constructor(
    pessoaDto
  ){
    this.id = uuid();
    this.nome = pessoaDto?.nome;
    this.apelido = pessoaDto?.apelido;
    this.nascimento = pessoaDto?.nascimento;
    this.stack = pessoaDto?.stack;
  }

  id
  nome
  apelido
  nascimento
  stack = []

  isValid(){
    if(this.nome.length > 100 || !isNaN(this.nome)){
      return false;
    }
    if(this.apelido.length > 32 || !isNaN(this.apelido)){
      return false;
    }

    if(
      this.nascimento.split('-').length !== 3
      || !this.nascimento
      || !isValid(parseISO(this.nascimento))
    ){
      return false;
    }
    
    if(this.stack){ // allow null
      if(!Array.isArray(this.stack) || this.stack.some(s => s.length > 32 || !isNaN(s))){
        return false;
      }
    }
    return true;
  }
}