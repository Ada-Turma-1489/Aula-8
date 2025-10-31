using DemoUpload.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoUpload.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(PessoaViewModel pessoa)
        {
            var extension = Path.GetExtension(pessoa.Foto.FileName);
            if (!".jpg".Equals(extension, StringComparison.OrdinalIgnoreCase))
                return BadRequest("Extensão Inválida");


            var maxAllowedSize = 1024 * 1024 * 10;
            if (pessoa.Foto.Length > maxAllowedSize)
                return BadRequest("Arquivo muito grande");

            var nome = Path.GetFileNameWithoutExtension(pessoa.Foto.FileName);
            var newName = nome + Guid.NewGuid() + extension;

            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Uploads");
            Directory.CreateDirectory(path);

            var filePath = Path.Combine(path, newName);

            using var stream = System.IO.File.Create(filePath);
            pessoa.Foto.CopyTo(stream);

            return View("Index");
        }
    }
}
