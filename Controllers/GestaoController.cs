using Microsoft.AspNetCore.Mvc;
using newmarket.Data;
using newmarket.DTO;

namespace newmarket.Controllers
{
    public class GestaoController : Controller
    {
        private readonly ApplicationDbContext dataBase;

        public GestaoController(ApplicationDbContext dataBase) {
            this.dataBase = dataBase;
        }

        public IActionResult Index() {
            return View();
        }  
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
        public IActionResult Categorias() {
            var categorias =dataBase.Categorias.ToList();
            return View(categorias);
        } 

        public IActionResult NovaCategoria() {
            return View();
        }

        public IActionResult EditarCategoria(int id) {
            var categoria = dataBase.Categorias.First(cat => cat.Id.Equals(id));
            CategoriaDTO categoriaView = new CategoriaDTO();
            categoriaView.Id = categoria.Id;
            categoriaView.Nome = categoria.Nome;
            return View(categoriaView);
        }

        public IActionResult Fornecedores() {
            return View();
        } 

        public IActionResult NovoFornecedor() {
            return View();
        }

        public IActionResult Produtos() {
            return View();
        } 

        public IActionResult NovoProduto() {
            ViewBag.Categorias = dataBase.Categorias.ToList();
            ViewBag.Fornecedores = dataBase.Fornecedores.ToList();
            return View();
        }  

    }
}