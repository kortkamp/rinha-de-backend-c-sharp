import pkg from 'pg';
const { Pool } = pkg;

class Database {

  pool = undefined;

  async setup(){
    this.pool = new Pool({
      user: 'postgres',
      host: process.env.DB_HOST || 'localhost',
      database: 'postgres',
      password: 'postgres',
      port: 5432,
    })
  }
}

export default new Database()