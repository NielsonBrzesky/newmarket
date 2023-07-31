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
            var categorias = dataBase.Categorias.Where(cat => cat.Status.Equals(true)).ToList();
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
            var fornecedores = dataBase.Fornecedores.Where(fornecedor => fornecedor.Status.Equals(true)).ToList();
            return View(fornecedores);
        } 

        public IActionResult NovoFornecedor() {
            return View();
        }

        public IActionResult EditarFornecedor(int id) {
            var fornecedor = dataBase.Fornecedores.First(fornecedor => fornecedor.Id.Equals(id));
            FornecedorDTO fornecedorView = new FornecedorDTO();
            fornecedorView.Id = fornecedor.Id;
            fornecedorView.Nome = fornecedor.Nome;
            fornecedorView.Email = fornecedor.Email;
            fornecedorView.Telefone = fornecedor.Telefone;
            return View(fornecedorView);
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