using Microsoft.AspNetCore.Mvc;

namespace Sample.Seq.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LogsController : ControllerBase
{
    private readonly ILogger<LogsController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public LogsController(ILogger<LogsController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("cidades")]
    public async Task<string> GetCitiesAsync()
    {
        var result = string.Empty;
        var client = _httpClientFactory.CreateClient("google");
        var response = await client.GetAsync("https://servicodados.ibge.gov.br/api/v1/localidades/estados/SP/municipios");

        if (response.IsSuccessStatusCode)
        {
            result = await response.Content.ReadAsStringAsync();
        }

        return result;
    }

    [HttpGet("thread")]
    public IActionResult GetThreadAsync()
    {
        Task.Delay(3000);

        return Ok();
    }

    [HttpGet("exception")]
    public IActionResult GetException()
    {
        throw new ArgumentException("Erro");
    }
}