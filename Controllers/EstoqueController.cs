using Microsoft.AspNetCore.Mvc;
using newmarket.Data;
using newmarket.Models;

namespace newmarket.Controllers
{
    public class EstoqueController : Controller
    {
         private readonly ApplicationDbContext database;
        public EstoqueController(ApplicationDbContext database)
        {
            this.database = database;
        }

        [HttpPost]
        public IActionResult Salvar(Estoque estoqueTemporario) {
            database.Estoques.Add(estoqueTemporario);
            database.SaveChanges();
            return RedirectToAction("Estoque", "Gestao");
        }
    }
}