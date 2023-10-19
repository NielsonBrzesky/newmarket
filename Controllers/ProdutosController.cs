using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
                produto.PrecoDeCusto = float.Parse(produtoTemp.PrecoDeCustoString.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
                produto.PrecoDeVenda = float.Parse(produtoTemp.PrecoDeVendaString.Replace(",", "."), CultureInfo.InvariantCulture.NumberFormat);
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

		[HttpPost]
		public IActionResult Atualizar(ProdutoDTO produtoTemporario)
		{
			if (ModelState.IsValid)
			{
				var produto = dataBase.Produtos.First(prod => prod.Id.Equals(produtoTemporario.Id));
				produto.Nome = produtoTemporario.Nome;
				produto.Categoria = dataBase.Categorias.First(categoria => categoria.Id.Equals(produtoTemporario.CategoriaID));
				produto.Fornecedor = dataBase.Fornecedores.First(fornecedor => fornecedor.Id.Equals(produtoTemporario.FornecedorID));
				produto.PrecoDeCusto = produtoTemporario.PrecoDeCusto;
				produto.PrecoDeVenda = produtoTemporario.PrecoDeVenda;
				produto.Medicao = produtoTemporario.Medicao;
				dataBase.SaveChanges();
				return RedirectToAction("Produtos", "Gestao");
			}
			else
			{
				return RedirectToAction("Produtos", "Gestao");
			}
		}

		[HttpPost]
		public IActionResult Deletar(int id)
		{
			if (id > 0)
			{
				var produto = dataBase.Produtos.First(prod => prod.Id.Equals(id));
				produto.Status = false;
				dataBase.SaveChanges();
			}
			return RedirectToAction("Produtos", "Gestao");
		}

		[HttpPost]
		public IActionResult Produto(int id)
		{
			if (id > 0)
			{
				var produto = dataBase.Produtos.Where(p => p.Status.Equals(true)).Include(p => p.Categoria).Include(p => p.Fornecedor).First(p => p.Id.Equals(id));

				if (produto != null)
				{
					var estoque = dataBase.Estoques.First(e => e.Produto.Id.Equals(produto.Id));
					if (estoque == null)
						produto = null;
				}

				if (produto != null) {

					Promocao promocao;
					try {
					 	promocao = dataBase.Promocoes.First(prod => prod.Produto.Id.Equals(produto.Id) && prod.Status.Equals(true));

					} catch(Exception e) {
						promocao = null;
					} 
					if (promocao != null) {
						produto.PrecoDeVenda -= (produto.PrecoDeVenda * (promocao.Porcentagem / 100)); // Preço da venda menos a subtração da promoção: R$ 20 -= 20 * (% / 100) == 20 -= 20 * 0.2 desconto de 4 então "20 -= 4 = 16"
					}
					Response.StatusCode = 200; // OK
					return Json(produto);
				}
				else
				{
					Response.StatusCode = 404; // Falha
					return Json(null);
				}

			}
			else
			{
				Response.StatusCode = 403; // Falha
				return Json(null);
			}
		}

		[HttpPost]
		public IActionResult GerarVenda([FromBody] VendaDTO dados) {//Anotação que o JQuery encherga a requisição.
			Venda venda = new Venda();
			venda.Total = dados.total;
			venda.Troco = dados.troco;
			venda.ValorPago = dados.troco <= 0.01f ? dados.total : dados.total - dados.troco;
			venda.Data = DateTime.Now;
			dataBase.Vendas.Add(venda);
			dataBase.SaveChanges();
			//Registrar saídas
			List<Saida> saidas = new List<Saida>();
			foreach(var saida in dados.produtos) {
				Saida s = new Saida();
				s.Quantidade = saida.quantidade;
				s.ValorDaVenda = saida.subtotal;
				s.venda = venda;
				s.Produto = dataBase.Produtos.First(p => p.Id.Equals(saida.id));
				s.Data = DateTime.Now;
				saidas.Add(s);
			}
			//Salvae no banco
			dataBase.AddRange(saidas); //Adiciona todas as saidas de uma vez.
			dataBase.SaveChanges();
			return Ok(new{msg="Venda processada com sucesso!"});
			// return Ok(dados);
		}

		public class SaidaDTO {
			
			public int id;

			public int quantidade; 
			
			public float subtotal;
		}

		public class VendaDTO {
			public float total;
			public float troco;
			public SaidaDTO[] produtos;
		}
    }
}