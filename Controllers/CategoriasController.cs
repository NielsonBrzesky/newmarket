using Microsoft.AspNetCore.Mvc;
using newmarket.DTO;

namespace newmarket.Controllers
{
    public class CategoriasController : Controller
    {
        [HttpPost]
        public IActionResult Salvar(CategoriaDTO categoriaDTO) {
            return Content("Olá!!!");
        }
    }
}