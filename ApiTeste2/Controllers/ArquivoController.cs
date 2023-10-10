using Microsoft.AspNetCore.Mvc;

namespace ApiTeste2.Controllers
{

  [ApiController]
  [Route("[controller]")]
  public class ArquivoController : ControllerBase
  {
    [HttpPost("upload")]
    public async Task<ActionResult> Upload([FromForm] ICollection<IFormFile> arquivo)
    {
      if (arquivo == null || arquivo.Count == 0) return BadRequest();

      List<byte[]> data = new();

      foreach (var formArquivo in arquivo)
      {
        if (formArquivo.Length > 0)
        {
          using (var stream = new MemoryStream())
          {
            await formArquivo.CopyToAsync(stream);

            data.Add(stream.ToArray());
          }
        }
      }
      return File(data[0], arquivo.FirstOrDefault().ContentType, "ResultadoTeste.txt");
    }
  }
}
