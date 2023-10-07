namespace Rinha.Entities
{
    public class Pessoa
    {
        public Pessoa()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string? Apelido { get; set; }
        public string? Nome { get; set; }
        public DateOnly Nascimento { get; set; }
        public IList<string>? Stack { get; set; }
    }
}