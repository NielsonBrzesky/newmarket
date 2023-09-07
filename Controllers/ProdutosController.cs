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

        /*%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%*/

        // 		[HttpPost]
		// public IActionResult Salvar(ProdutoDTO produtoTemporario)
		// {
		// 	// return Content(produtoTemporario.PrecoDeCustoString + " " + produtoTemporario.PrecoDeVendaString);
		// 	if (ModelState.IsValid)
		// 	{
		// 		Produto produto = new Produto();
		// 		produto.Nome = produtoTemporario.Nome;
		// 		produto.Categoria = database.Categorias
		// 			.First(categoria => categoria.Id == produtoTemporario.CategoriaID);
		// 		produto.Fornecedor = database.Fornecedores
		// 			.First(fornecedor => fornecedor.Id == produtoTemporario.FornecedorID);
		// 		produto.PrecoDeCusto = produtoTemporario.PrecoDeCusto;
		// 		produto.PrecoDeVenda = produtoTemporario.PrecoDeVenda;
		// 		// produto.PrecoDeCusto = float.Parse(produtoTemporario.PrecoDeCustoString, CultureInfo.InvariantCulture.NumberFormat);
		// 		// produto.PrecoDeVenda = float.Parse(produtoTemporario.PrecoDeVendaString, CultureInfo.InvariantCulture.NumberFormat);
		// 		produto.Medicao = produtoTemporario.Medicao;
		// 		produto.Status = true;
		// 		database.Produtos.Add(produto);
		// 		database.SaveChanges();
		// 		return RedirectToAction("Produtos", "Gestao");
		// 	}
		// 	else
		// 	{
		// 		ViewBag.Categorias = database.Categorias.ToList(); //Trás a lista de Categorias dentro do banco.
		// 		ViewBag.Fornecedores = database.Fornecedores.ToList();
		// 		return View("../Gestao/NovoProduto");
		// 	}
		// }

		// [HttpPost]
		// public IActionResult Atualizar(ProdutoDTO produtoTemporario)
		// {
		// 	if (ModelState.IsValid)
		// 	{
		// 		var produto = database.Produtos.First(prod => prod.Id == produtoTemporario.Id);
		// 		produto.Nome = produtoTemporario.Nome;
		// 		produto.Categoria = database.Categorias
		// 			.First(categoria => categoria.Id == produtoTemporario.CategoriaID);
		// 		produto.Fornecedor = database.Fornecedores
		// 			.First(fornecedor => fornecedor.Id == produtoTemporario.FornecedorID);
		// 		produto.PrecoDeCusto = produtoTemporario.PrecoDeCusto;
		// 		produto.PrecoDeVenda = produtoTemporario.PrecoDeVenda;
		// 		produto.Medicao = produtoTemporario.Medicao;
		// 		database.SaveChanges();
		// 		return RedirectToAction("Produtos", "Gestao");
		// 	}
		// 	else
		// 	{
		// 		return RedirectToAction("Produtos", "Gestao");
		// 	}
		// }

		// public IActionResult Deletar(int id)
		// {
		// 	if (id > 0)
		// 	{
		// 		var produto = database.Produtos.First(prod => prod.Id.Equals(id));
		// 		produto.Status = false;
		// 		database.SaveChanges();
		// 	}
		// 	return RedirectToAction("Produtos", "Gestao");
		// }

		// [HttpPost]
		// public IActionResult Produto(int id)
		// {
		// 	return Json("O JSON está funcional, SUCESSOOOO!!!");
		// 	if (id > 0)
		// 	{
		// 		var produto = database.Produtos
		// 			.Where(p => p.Status.Equals(true))
		// 			.Include(p => p.Categoria)
		// 			.Include(p => p.Fornecedor)
		// 			.First(p => p.Id.Equals(id));

		// 		if (produto == null)
		// 		{
		// 			var estoque = database.Estoques.First(e => e.Produto.Id.Equals(produto.Id));
		// 			if (estoque != null)
		// 				produto = null;
		// 		}

		// 		if (produto != null)
		// 		{
		// 			Promocao promocao;

		// 			try
		// 			{
		// 				promocao = database.Promocoes.First(p => p.Produto.Id.Equals(produto.Id) && p.Status.Equals(true));
		// 			}
		// 			catch (Exception e)
		// 			{
		// 				promocao = null;
		// 			}

		// 			if (promocao != null)
		// 			{
		// 				produto.PrecoDeVenda -= (produto.PrecoDeVenda * (promocao.Porcentagem / 100));
		// 			}

		// 			Response.StatusCode = 200;
		// 			return Json(produto);
		// 		}
		// 		else
		// 		{
		// 			Response.StatusCode = 404;
		// 			return Json(null);
		// 		}

		// 	}
		// 	else
		// 	{
		// 		Response.StatusCode = 403;
		// 		return Json(null);
		// 	}
		// }

		// [HttpPost]
		// public IActionResult GerarVenda([FromBody] SaidaDTO[] dados) {//Anotação que o JQuery encherga a requisição.
		// 	return Ok(dados);
		// }

		// public class SaidaDTO {
			
		// 	public int id;

		// 	public int quantidade; 
			
		// 	public float subtotal;
		// }
    }
}