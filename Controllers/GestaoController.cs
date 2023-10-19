using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newmarket.Data;
using newmarket.DTO;

namespace newmarket.Controllers
{
    [Authorize]
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
            var produtos = dataBase.Produtos.Include(produtos => produtos.Categoria).Include(produtos => produtos.Fornecedor).Where(p => p.Status.Equals(true)).ToList();
            return View(produtos);
        } 

        public IActionResult NovoProduto() {
            ViewBag.Categorias = dataBase.Categorias.ToList();
            ViewBag.Fornecedores = dataBase.Fornecedores.ToList();
            return View();
        }  

        public IActionResult EditarProduto(int id)
        {
            var produto = dataBase.Produtos.Include(p => p.Categoria).Include(p => p.Fornecedor).First(p => p.Id.Equals(id));
            ProdutoDTO produtoView = new ProdutoDTO();
            produtoView.Id = produto.Id;
            produtoView.Nome = produto.Nome;
            produtoView.PrecoDeCusto = produto.PrecoDeCusto;
            produtoView.PrecoDeVenda = produto.PrecoDeVenda;
            produtoView.CategoriaID = produto.Categoria.Id;
            produtoView.FornecedorID = produto.Fornecedor.Id;
            produtoView.Medicao = produto.Medicao;
            ViewBag.Categorias = dataBase.Categorias.ToList(); //Trás a lista de Categorias dentro do banco.
            ViewBag.Fornecedores = dataBase.Fornecedores.ToList();
            return View(produtoView);
        }

        public IActionResult Promocoes()
        {
            var promocoes = dataBase.Promocoes.Include(p => p.Produto).Where(itemExiste => itemExiste.Status.Equals(true)).ToList();
            return View(promocoes);
        }

        public IActionResult NovaPromocao()
        {
            ViewBag.Produtos = dataBase.Produtos.ToList();
            return View();
        }

        public IActionResult EditarPromocao(int id)
        {
            var promocao = dataBase.Promocoes.Include(p => p.Produto).First(pro => pro.Id.Equals(id));
            PromocaoDTO promoView = new PromocaoDTO();
            promoView.Id = promocao.Id;
            promoView.Nome = promocao.Nome;
            promoView.Porcentagem = promocao.Porcentagem;
            promoView.ProdutoID = promocao.Produto.Id;
            ViewBag.Produtos = dataBase.Produtos.ToList();
            return View(promoView);
        }

        public IActionResult Estoque() {
            var listaEstoque = dataBase.Estoques.Include(e => e.Produto).ToList();
            return View(listaEstoque);
        }

         public IActionResult NovoEstoque() {
            ViewBag.Produtos = dataBase.Produtos.ToList();
            return View();
        }

         public IActionResult EditarEstoque() {
            return Content("");
        }

        public IActionResult Vendas() {
            var listaDeVendas = dataBase.Vendas.ToList();
            return View(listaDeVendas);
        }

        //Action que carrega os gráficos do relatório de vendas.
        [HttpPost]
        public IActionResult RelatorioDeVendas() {
            return Ok(dataBase.Vendas.ToList());
        }
    }
}