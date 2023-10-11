using Microsoft.AspNetCore.Mvc;
using Rinha.Data.Repositories;
using Rinha.Entities;

namespace Rinha.Controllers;

[ApiController]
[Route("")]
public class PessoaController : ControllerBase
{
    private readonly IPessoaRepository _repository;

    public PessoaController(IPessoaRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("pessoas")]
    public async Task<ActionResult> Create(Pessoa pessoa)
    {
        var isDtoProcessable = pessoa.Nome != null && pessoa.Apelido != null && pessoa.Nascimento != null;

        var invalid = pessoa.Nome.Length > 100 || pessoa.Apelido.Length > 32;

        if (invalid)
        {
            return BadRequest();
        }

        if (pessoa.Stack != null && pessoa.Stack.Any(s => s.Length > 32))
        {
            return UnprocessableEntity();
        }

        var id = Guid.NewGuid();

        pessoa.Id = id;


        // var pessoaExists = await _repository.FindByNome(pessoa.Nome);

        // if (pessoaExists != null)
        // {
        //     return UnprocessableEntity();
        // }


        await _repository.Save(pessoa);

        return Created($"/pessoas/{pessoa.Id}", pessoa);
    }

    [HttpGet("pessoas")]
    public async Task<ActionResult> Get(string t)
    {
        // return Ok();

        return Ok(await _repository.Search(t));
    }

    [HttpGet("pessoas/{id}")]
    public async Task<ActionResult> GetOne(Guid id)
    {
        // return Ok();

        // return Ok(_cacheService.GetData(id));

        var pessoa = await _repository.Find(id);



        if (pessoa == null)
        {
            return NotFound();
        }
        return Ok(pessoa);
    }

    [HttpGet("contagem-pessoas")]
    public async Task<ActionResult> Count()
    {
        return Ok(await _repository.Count());
    }
}
