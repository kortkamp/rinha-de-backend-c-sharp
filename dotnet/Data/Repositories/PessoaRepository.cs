using Microsoft.EntityFrameworkCore;
using Rinha.Data.Context;
using Rinha.Entities;

namespace Rinha.Data.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly DataContext _context;
        public PessoaRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Pessoa?> Find(Guid id)
        {
            return await _context.Pessoas.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Pessoa>> Search(string search)
        {
            return await _context.Pessoas
            .FromSqlRaw($"SELECT * FROM pessoas where pessoas.apelido ilike '%{search}%' or pessoas.nome ilike '%{search}%' or pessoas.stack ilike '%{search}%' LIMIT 50;", search)
            .ToListAsync();
        }

        public async Task<Pessoa?> FindByNome(string nome)
        {
            return await _context.Pessoas.FirstOrDefaultAsync(p => p.Nome.Equals(nome));
        }

        public async Task<Pessoa> Save(Pessoa pessoa)
        {
            await _context.AddAsync(pessoa);
            await _context.SaveChangesAsync();

            return pessoa;
        }

        public async Task<int> Count()
        {
            return await _context.Pessoas.CountAsync();
        }
    }
}