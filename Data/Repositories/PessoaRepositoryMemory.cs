using Rinha.Entities;

namespace Rinha.Data.Repositories
{
    public class PessoaRepositoryMemory : IPessoaRepository
    {
        public Task<int> Count()
        {
            throw new NotImplementedException();
        }

        public Task<Pessoa?> Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Pessoa?> FindByNome(string nome)
        {
            throw new NotImplementedException();
        }

        public Task<Pessoa> Save(Pessoa pessoa)
        {
            throw new NotImplementedException();
        }

        public Task<List<Pessoa>> Search(string term)
        {
            throw new NotImplementedException();
        }
    }
}