export class PessoaRepository {
  pool = undefined;

  constructor(pool){
    this.pool = pool
  }


  async save(pessoa){
    const sql = 'INSERT INTO pessoas VALUES ($1, $2, $3, $4, $5);'
    const values = [pessoa.id, pessoa.apelido, pessoa.nome, pessoa.nascimento, pessoa.stack?.join(",")]
    await this.pool.query(sql, values);
  }

  async count(){
    const result = await this.pool.query(`SELECT COUNT(*) FROM pessoas;`);

    return result.rows[0].count;
  }

  async search(term){
    const sql = "SELECT * FROM pessoas where pessoas.apelido ilike $1 or pessoas.nome ilike $1 or pessoas.stack ilike $1 LIMIT 50;"
    const values = ['%'+term+'%']

    const result = await this.pool.query(sql, values);
    return(result.rows.map(pessoa => ({
      ...pessoa, 
      nascimento: pessoa.nascimento.toISOString().split('T')[0], 
      stack: pessoa.stack?.split(',') || null
    })));
  }
}