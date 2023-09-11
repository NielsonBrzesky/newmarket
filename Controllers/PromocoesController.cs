using Microsoft.AspNetCore.Mvc;
using newmarket.Data;
using newmarket.DTO;
using newmarket.Models;

namespace newmarket.Controllers
{
    public class PromocoesController : Controller
    {
        private readonly ApplicationDbContext dataBase;

        public PromocoesController(ApplicationDbContext dataBase)
        {
            this.dataBase = dataBase;
        }


        [HttpPost]
        public IActionResult Salvar(PromocaoDTO promocaoTemporaria)
        {
            if (ModelState.IsValid)
            {
                Promocao promocao = new Promocao();
                promocao.Nome = promocaoTemporaria.Nome;
                promocao.Produto = dataBase.Produtos.First(prod => prod.Id.Equals(promocaoTemporaria.ProdutoID));
                promocao.Porcentagem = promocaoTemporaria.Porcentagem;
                promocao.Status = true;
                dataBase.Promocoes.Add(promocao);
                dataBase.SaveChanges();
                return RedirectToAction("Promocoes", "Gestao");
            }
            else
            {
                ViewBag.Produtos = dataBase.Produtos.ToList();
                return View("../Getao/NovaPromocao");
            }
        }

        [HttpPost]
        public IActionResult Atualizar(PromocaoDTO promocaoTemporaria)
        {
            if (ModelState.IsValid)
            {
                var promocao = dataBase.Promocoes.First(promo => promo.Id.Equals(promocaoTemporaria.Id));
                promocao.Nome = promocaoTemporaria.Nome;
                promocao.Porcentagem = promocaoTemporaria.Porcentagem;
                promocao.Produto = dataBase.Produtos.First(prod => prod.Id.Equals(promocaoTemporaria.ProdutoID));
                dataBase.SaveChanges();
                return RedirectToAction("Promocoes", "Gestao");
            }
            else
            {
                return RedirectToAction("Promocoes", "Gestao");
            }
        }

        [HttpPost]
        public IActionResult Deletar(int id)
        {
            if (id > 0)
            {
                var promocao = dataBase.Promocoes.First(promo => promo.Id.Equals(id));
                promocao.Status = false;
                dataBase.SaveChanges();
            }
            return RedirectToAction("Promocoes", "Gestao");
        }
    }
}