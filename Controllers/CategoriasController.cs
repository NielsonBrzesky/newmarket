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

        public IActionResult Atualizar(CategoriaDTO categoriaTemp) {
            if(ModelState.IsValid) {
                var categoria = dataBase.Categorias.First(cat => cat.Id.Equals(categoriaTemp.Id));
                categoria.Nome = categoriaTemp.Nome;
                dataBase.SaveChanges();
                return RedirectToAction("Categorias", "Gestao");
            } else {
                return View("../Gestao/EditarCategoria");
            }
        }

        [HttpPost]
        public IActionResult Deletar(int id) {
            if(id > 0) {
                var categoria = dataBase.Categorias.First(cat => cat.Id.Equals(id));
                categoria.Status = false;
                dataBase.SaveChanges();
            }
            return RedirectToAction("Categorias", "Gestao");
        }
    }
}