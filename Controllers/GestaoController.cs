using Microsoft.AspNetCore.Mvc;
using newmarket.Data;

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
            return View();
        } 

        public IActionResult NovaCategoria() {
            return View();
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