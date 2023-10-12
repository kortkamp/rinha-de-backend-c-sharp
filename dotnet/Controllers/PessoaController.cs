using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Rinha.Data.Redis;
using Rinha.Data.Repositories;
using Rinha.Entities;
using System.Threading;

namespace Rinha.Controllers;

[ApiController]
[Route("")]
public class PessoaController : ControllerBase
{
    private readonly IPessoaRepository _repository;
    private readonly IRedisService _redisService;

    public PessoaController(IPessoaRepository repository, IRedisService redisService)
    {
        _repository = repository;
        _redisService = redisService;
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

        var pessoaExists = _redisService.GetData(pessoa.Nome);
        if (pessoaExists != null)
        {
            return UnprocessableEntity();
        }

        var id = Guid.NewGuid();

        pessoa.Id = id;

        _redisService.SetData(id.ToString(), JsonSerializer.Serialize(pessoa));
        _redisService.SetData(pessoa.Nome, id.ToString());

        await _repository.Save(pessoa);

        return Created($"/pessoas/{pessoa.Id}", pessoa);
    }

    [HttpGet("pessoas")]
    public async Task<ActionResult> Get(string t)
    {
        return Ok(await _repository.Search(t));
    }

    [HttpGet("pessoas/{id}")]
    public async Task<ActionResult> GetOne(Guid id)
    {
        var pessoa = _redisService.GetData(id.ToString());

        if (pessoa == null)
        {
            return NotFound();
        }
        return Content(pessoa, "application/json");
    }

    [HttpGet("contagem-pessoas")]
    public async Task<ActionResult> Count()
    {
        return Ok(await _repository.Count());
    }

    [HttpGet("redis-test")]
    public async Task<ActionResult> RedisTest(int count = 100)
    {
        _redisService.SetData("test", ".");


        var sb = new System.Text.StringBuilder();
        for (int i = 0; i < count; i++)
        {
            var data = _redisService.GetData("test");
            sb.Append(data);
        }
        return Ok(sb.ToString());
    }
}
