using System.ComponentModel.DataAnnotations;

namespace Rinha.Dtos
{
    public class CreatePessoaDto
    {
        [StringLength(32)]
        public string Apelido { get; set; }

        [StringLength(100)]
        public string Nome { get; set; }

        [DataType(DataType.Date)]
        public DateOnly Nascimento { get; set; }

        // [StringLength(1)]
        public IList<string>? Stack { get; set; }
    }
}