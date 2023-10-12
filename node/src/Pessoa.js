import {v4 as uuid} from "uuid"

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

  id = ""
  nome = ""
  apelido = ""
  nascimento = ""
  stack = []

  isValid(){
    return true;
  }
}