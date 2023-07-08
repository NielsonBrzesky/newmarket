using Microsoft.AspNetCore.Mvc;
using newmarket.Data;
using newmarket.DTO;
using newmarket.Models;

namespace newmarket.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext dataBase;

        public ProdutosController(ApplicationDbContext dataBase)
        {
            this.dataBase = dataBase;
        }

        [HttpPost]
        public IActionResult Salvar(ProdutoDTO produtoTemp)
        {
            if (ModelState.IsValid)
            {
                Produto produto = new Produto();
                produto.Nome = produtoTemp.Nome;
                produto.Categoria = dataBase.Categorias.First(categoria => categoria.Id.Equals(produtoTemp.CategoriaID));
                produto.Fornecedor = dataBase.Fornecedores.First(fornecedor => fornecedor.Id.Equals(produtoTemp.FornecedorID));
                produto.PrecoDeCusto = produtoTemp.PrecoDeCusto;
                produto.PrecoDeVenda = produtoTemp.PrecoDeVenda;
                produto.Medicao = produtoTemp.Medicao;
                produto.Status = true;
                dataBase.Produtos.Add(produto);
                dataBase.SaveChanges();
                return RedirectToAction("Produtos", "Gestao");
            } else {
                ViewBag.Categorias = dataBase.Categorias.ToList();
                ViewBag.Fornecedores = dataBase.Fornecedores.ToList();
                return View("../Gestao/NovoProduto");
            }
        }
    }
}