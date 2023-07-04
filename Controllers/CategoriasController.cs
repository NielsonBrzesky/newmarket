using Microsoft.AspNetCore.Mvc;
using newmarket.DTO;
using newmarket.Models;
using newmarket.Data;

namespace newmarket.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext dataBase;

        public CategoriasController(ApplicationDbContext dataBase) {
            this.dataBase = dataBase;
        }

        [HttpPost]
        public IActionResult Salvar(CategoriaDTO categoriaTemp) {
            if (ModelState.IsValid) {
                Categoria categoria = new Categoria();
                categoria.Nome = categoriaTemp.Nome;
                categoria.Status = true;
                dataBase.Categorias.Add(categoria);
                dataBase.SaveChanges();
                return RedirectToAction("Categorias", "Gestao");
            } else {
                return View("../Gestao/NovaCategoria");
            }
        }
    }
}