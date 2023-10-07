using Rinha.Entities;

namespace Rinha.Data.Repositories
{
    public interface IPessoaRepository
    {
        public Task<Pessoa> Save(Pessoa pessoa);

        public Task<Pessoa?> Find(Guid id);

        public Task<Pessoa?> FindByNome(string nome);

        public Task<List<Pessoa>> Search(string term);

        public Task<int> Count();
    }
}